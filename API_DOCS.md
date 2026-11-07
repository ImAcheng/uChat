# API Document
This file introduce every endpoints in API.

## api/account/{endpoints}
Endpoints for Account Operation.  

* `[POST]` login  
`[FromBody]`
    ```cs
    public class LoginRequest {
        public string username { get; set; }
        public string password { get; set; }
    }
    ```
    Login with username and password.  

* `[POST]` d-login  
`[FromBody]`  
    ```cs
    public class DirectLoginRequest {
        public Guid accountId { get; set; }
        public string password { get; set; }
    }
    ```
    Directly login by id and password.  
    Used when users once logged in before.  

* `[POST]` create
`[FromBody]` 
    ```cs
    public class CreateAccountRequest {
        public string email { get; set; }
        public string username { get; set; }
        public string? nickname { get; set; }
        public string? accountBio { get; set; }
        public byte genderCode { get; set; }
    }
    ```
    Create a new account.  
    **Planed to remove email.*  

