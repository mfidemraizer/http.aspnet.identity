# What is *Http.AspNet.Identity*?

Short answer: it's a custom user store provider to ASP.NET Identity 2.2.x and above.

### Long answer...

Think about the following scenario:

- You've a central API which works as the *security server*, and it's hosted either using OWIN or IIS hosting, but in both cases the so-called API is a standalone application.
- Other product-specific APIs are federated to the *security server* and these rely on the authentication and authorization approach.
- Whole security server implements *OAuth2*.


###...let me make a question...

Since federated APIs will be receiving OAuth 2 *access tokens*, they'll need to unencrypt them and, the whole token may contain claims like owner user id, its security role...

I suspect you'll need to access your security database somewhere to validate that the OAuth client is valid and I also guess you'll want to be able to get an instance of the *user for the which the access token was generated for*.

Wait, there's a security hole here: federated APIs are able to validate and access security database. **Aren't these federated APIs meant to rely on *security server/API?*** 

### Now you should be able to understand why *Http.AspNet.Identity* provider exists!

This custom user store provider to ASP.NET Identity doesn't call user store's CRUD operations directly to a database, but it just implement HTTP/RESTful calls to some RESTful API. *Who will be that target RESTful API?* ***Yes, you're correct: it'll be the security API***.

That is, this custom provider is just like a proxy which delegates the actual execution of an ASP.NET Identity operation to some remote RESTful API.

This project provides a possible implementation to the so-called Security REST API. Just check the ***IdentityApi*** project here as part of the GitHub repository. You can use it in your own ASP.NET WebAPI project. Since the controller requires authorization, you'll need to configure ASP.NET Identity to use some kind of authentication (I strongly recommend [OWIN OAuth2 middleware](https://www.nuget.org/packages/Microsoft.Owin.Security.OAuth)).