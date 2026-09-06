using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

var app = builder.Build();

app.MapPost("/api/es/local-ai-chat", async (
    LocalLLMChatRequest request,
    IHttpClientFactory factory,
    HttpResponse response) => {
        var client = factory.CreateClient();

        var ollamaRequest = new {
            model = request.model,
            messages = new[] {
                new {
                    role = "system",
                    content = "You're a daily life assistant." +
                        "You're designed to answer users' question like daily chats." +
                        "Answer as fast as you can."
                },
                new {
                    role = "user",
                    content = request.message
                }
            },

            // turn thinking / reasoning content off
            think = true,

            // use stream
            stream = true
        };

        using var result = await client.PostAsJsonAsync(
            request.llmUri,
            ollamaRequest
        );

        try {
            result.EnsureSuccessStatusCode();
        }
        catch {
            await response.WriteAsync("Cannot connect to LLM.");
            return Results.Empty;
        }
        

        /*
         if (!request.stream) {
            // pure text
            var json = await result.Content.ReadAsStringAsync();

            using var document = JsonDocument.Parse(json);

            var message = document
                .RootElement
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return Results.Ok(new {
                message
            });
        }
         */

        // SSE
        response.ContentType = "text/event-stream";

        await using var stream = await result.Content.ReadAsStreamAsync();
        using var reader = new StreamReader(stream);

        while (!reader.EndOfStream) {
            var line = await reader.ReadLineAsync();

            if (string.IsNullOrEmpty(line))
                continue;

            using var document = JsonDocument.Parse(line);

            var content = document
                .RootElement
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            if (!string.IsNullOrEmpty(content)) {
                await response.WriteAsync($"data: {content}\n\n");
                await response.Body.FlushAsync();
            }
        }

        return Results.Empty;
    });

app.Run();

public record LocalLLMChatRequest (
    string model,
    string message,
    string llmUri
);