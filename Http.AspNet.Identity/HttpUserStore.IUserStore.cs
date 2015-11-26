namespace Http.AspNet.Identity
{
    using Microsoft.AspNet.Identity;
    using System.Net.Http;
    using System.Threading.Tasks;

    public partial class HttpUserStore<TUser> : IUserStore<TUser>
        where TUser : class, IUser, IIdentityUser
    {
        public Task CreateAsync(TUser user) => HttpClient.PostAsJsonAsync(UserResourceUri, user);

        public Task DeleteAsync(TUser user) =>
            HttpClient.DeleteAsync($"{UserResourceUri}/{((IIdentityUser)user).Id}");

        public async Task<TUser> FindByIdAsync(string userId)
        {
            HttpResponseMessage response = await HttpClient.GetAsync($"{UserResourceUri}/{userId}");

            return await response.Content.ReadAsAsync<TUser>();
        }

        public Task<TUser> FindByNameAsync(string userName) => FindByIdAsync(userName);

        public Task UpdateAsync(TUser user) =>
            HttpClient.PutAsJsonAsync<TUser>($"{UserResourceUri}/{((IIdentityUser)user).Id}", user);

        public void Dispose()
        {
        }
    }
}