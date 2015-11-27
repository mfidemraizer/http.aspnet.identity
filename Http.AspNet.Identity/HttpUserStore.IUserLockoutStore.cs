namespace Http.AspNet.Identity
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Diagnostics.Contracts;
    using System.Net.Http;
    using System.Threading.Tasks;

    public partial class HttpUserStore<TUser> : IUserLockoutStore<TUser, string>
    {
        public virtual async Task<int> GetAccessFailedCountAsync(TUser user)
        {
            Contract.Requires(user != null, "Given user must be a non-null reference");
            Contract.Ensures(Contract.Result<Task<int>>() != null);

            HttpResponseMessage response = await HttpClient.GetAsync
            (
                UserPropertiesResourceUri.Replace("{userId}", ((IIdentityUser)user).Id).Replace("{propertyName}", "lockout.failcount")
            );

            return await response.Content.ReadAsAsync<int>();
        }

        public async virtual Task<bool> GetLockoutEnabledAsync(TUser user)
        {
            Contract.Requires(user != null, "Given user must be a non-null reference");
            Contract.Ensures(Contract.Result<Task<bool>>() != null);

            HttpRequestMessage request = new HttpRequestMessage
            (
                HttpMethod.Head,
                UserPropertiesResourceUri.Replace("{userId}", ((IIdentityUser)user).Id).Replace("{propertyName}", "lockout.enabled")
            );

            return (await HttpClient.SendAsync(request)).IsSuccessStatusCode; 
        }

        public virtual async Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user)
        {
            Contract.Requires(user != null, "Given user must be a non-null reference");
            Contract.Ensures(Contract.Result<Task<DateTimeOffset>>() != null);

            HttpResponseMessage response = await HttpClient.GetAsync
            (
                UserPropertiesResourceUri.Replace("{userId}", ((IIdentityUser)user).Id).Replace("{propertyName}", "lockout.enddate")
            );

            return await response.Content.ReadAsAsync<DateTimeOffset>();
        }

        public virtual async Task<int> IncrementAccessFailedCountAsync(TUser user)
        {
            Contract.Requires(user != null, "Given user must be a non-null reference");
            Contract.Ensures(Contract.Result<Task<int>>() != null);

            HttpRequestMessage request = new HttpRequestMessage
            (
                HttpMethod.Put,
                UserPropertiesResourceUri.Replace("{userId}", ((IIdentityUser)user).Id).Replace("{propertyName}", "lockout.failcount")
            );

            HttpResponseMessage response = await HttpClient.SendAsync(request);

            return await response.Content.ReadAsAsync<int>();
        }

        public virtual Task ResetAccessFailedCountAsync(TUser user)
        {
            Contract.Requires(user != null, "Given user must be a non-null reference");
            Contract.Ensures(Contract.Result<Task>() != null);

            HttpRequestMessage request = new HttpRequestMessage
            (
                HttpMethod.Delete,
                UserPropertiesResourceUri.Replace("{userId}", ((IIdentityUser)user).Id).Replace("{propertyName}", "lockout.failcount")
            );

            return HttpClient.SendAsync(request);
        }

        public virtual Task SetLockoutEnabledAsync(TUser user, bool enabled)
        {
            Contract.Requires(user != null, "Given user must be a non-null reference");
            Contract.Ensures(Contract.Result<Task>() != null);

            return HttpClient.PutAsJsonAsync
            (
                UserPropertiesResourceUri.Replace("{userId}", ((IIdentityUser)user).Id).Replace("{propertyName}", "lockout.enabled"),
                new { enabled = enabled }
            );
        }

        public virtual Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd)
        {
            Contract.Requires(user != null, "Given user must be a non-null reference");
            Contract.Ensures(Contract.Result<Task>() != null);

            return HttpClient.PutAsJsonAsync
            (
                UserPropertiesResourceUri.Replace("{userId}", ((IIdentityUser)user).Id).Replace("{propertyName}", "lockout.enddate"),
                new { date = lockoutEnd }
            );
        }
    }
}