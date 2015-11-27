namespace Http.AspNet.Identity
{
    using Microsoft.AspNet.Identity;
    using System.Diagnostics.Contracts;
    using System.Net.Http;
    using System.Threading.Tasks;

    public partial class HttpUserStore<TUser> : IUserEmailStore<TUser>
    {
        public virtual Task<TUser> FindByEmailAsync(string email)
        {
            return FindByNameAsync(email);
        }

        public virtual Task<string> GetEmailAsync(TUser user)
        {
            return Task.FromResult(user.Email);
        }

        public virtual async Task<bool> GetEmailConfirmedAsync(TUser user)
        {
            Contract.Requires(user != null, "Given user must be a non-null reference");
            Contract.Ensures(Contract.Result<Task<bool>>() != null);

            // for example: /users/{userId}/properties/email.confirmed
            HttpRequestMessage request = new HttpRequestMessage
            (
                HttpMethod.Head,
                UserPropertiesResourceUri.Replace("{userId}", ((IIdentityUser)user).Id).Replace("{propertyName}", "email.confirmed")
            );

            return (await HttpClient.SendAsync(request)).IsSuccessStatusCode;
        }

        public virtual Task SetEmailAsync(TUser user, string email)
        {
            user.Email = email;

            return Task.FromResult(true);
        }

        public virtual Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {
            Contract.Requires(user != null, "Given user must be a non-null reference");
            Contract.Ensures(Contract.Result<Task>() != null);

            // for example: /users/{userId}/properties/email.confirmed
            return HttpClient.PutAsJsonAsync
            (
                UserPropertiesResourceUri.Replace("{userId}", ((IIdentityUser)user).Id).Replace("{propertyName}", "email.confirmed"),
                new { confirmed = confirmed }
            );
        }
    }
}