namespace Http.AspNet.Identity
{
    using Microsoft.AspNet.Identity;
    using System.Diagnostics.Contracts;
    using System.Net.Http;
    using System.Threading.Tasks;

    public partial class HttpUserStore<TUser> : IUserStore<TUser>
        where TUser : class, IUser, IIdentityUser
    {
        public Task CreateAsync(TUser user)
        {
            Contract.Requires(user != null, "Given user must be a non-null reference");
            Contract.Ensures(Contract.Result<Task>() != null);

            return HttpClient.PostAsJsonAsync(UserResourceUri, user);
        }

        public Task DeleteAsync(TUser user)
        {
            Contract.Requires(user != null, "Given user must be a non-null reference");
            Contract.Ensures(Contract.Result<Task>() != null);

            return HttpClient.DeleteAsync($"{UserResourceUri}/{((IIdentityUser)user).Id}");
        }

        public async Task<TUser> FindByIdAsync(string userId)
        {
            Contract.Requires(!string.IsNullOrEmpty(userId), "Given user id must not be null or empty string");
            Contract.Ensures(Contract.Result<Task<TUser>>() != null);

            HttpResponseMessage response = await HttpClient.GetAsync($"{UserResourceUri}/{userId}");

            return await response.Content.ReadAsAsync<TUser>();
        }

        public Task<TUser> FindByNameAsync(string userName)
        {
            Contract.Requires(!string.IsNullOrEmpty(userName), "Given user name must not be null or empty string");
            Contract.Ensures(Contract.Result<Task<TUser>>() != null);

            return FindByIdAsync(userName);
        }

        public Task UpdateAsync(TUser user)
        {
            Contract.Requires(user != null, "Given user must be a non-null reference");
            Contract.Ensures(Contract.Result<Task>() != null);

            return HttpClient.PutAsJsonAsync($"{UserResourceUri}/{((IIdentityUser)user).Id}", user);
        }

        public void Dispose()
        {
        }
    }
}