namespace Http.AspNet.Identity
{
    using Microsoft.AspNet.Identity;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Net.Http;
    using System.Threading.Tasks;

    public partial class HttpUserStore<TUser> : IUserLoginStore<TUser>, IUserStore<TUser>
    {
        public virtual Task AddLoginAsync(TUser user, UserLoginInfo login)
        {
            Contract.Requires(user != null, "Given user must be a non-null reference");
            Contract.Requires(login != null, "Given login must be a non-null reference");
            Contract.Ensures(Contract.Result<Task>() != null);

            // for example: /users/{userId}/logins
            return HttpClient.PostAsJsonAsync
            (
                UserLoginStoreResourceUri.Replace("{userId}", ((IIdentityUser)user).Id),
                login
            );
        }

        public virtual async Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
        {
            Contract.Requires(user != null, "Given user must be a non-null reference");
            Contract.Ensures(Contract.Result<Task<IList<UserLoginInfo>>>() != null);

            // for example: /users/{userId}/logins
            HttpResponseMessage response = await HttpClient.GetAsync
            (
                UserLoginStoreResourceUri.Replace("{userId}", ((IIdentityUser)user).Id)
            );

            return await response.Content.ReadAsAsync<IList<UserLoginInfo>>();
        }

        public virtual Task RemoveLoginAsync(TUser user, UserLoginInfo login)
        {
            Contract.Requires(user != null, "Given user must be a non-null reference");
            Contract.Requires(login != null, "Given login must be a non-null reference");
            Contract.Ensures(Contract.Result<Task>() != null);

            // for example: /users/{userId}/logins/{providerKey}
            HttpRequestMessage request = new HttpRequestMessage
            (
                HttpMethod.Delete,
                $"{UserLoginStoreResourceUri.Replace("{userId}", ((IIdentityUser)user).Id)}/{login.ProviderKey}"
            );

            return HttpClient.SendAsync(request);
        }

        public Task<TUser> FindAsync(UserLoginInfo login)
        {
            Contract.Requires(login != null, "Given login must be a non-null reference");
            Contract.Ensures(Contract.Result<Task<TUser>>() != null);

            return FindByIdAsync(login.ProviderKey);
        }
    }
}
