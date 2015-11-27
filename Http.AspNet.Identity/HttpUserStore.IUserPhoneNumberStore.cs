namespace Http.AspNet.Identity
{
    using Microsoft.AspNet.Identity;
    using System.Diagnostics.Contracts;
    using System.Net.Http;
    using System.Threading.Tasks;

    public partial class HttpUserStore<TUser> : IUserPhoneNumberStore<TUser>
    {
        public virtual Task<string> GetPhoneNumberAsync(TUser user)
        {
            Contract.Requires(user != null, "Given user must be a non-null reference");
            Contract.Ensures(Contract.Result<Task<string>>() != null);

            return Task.FromResult(user.PhoneNumber);
        }

        public async virtual Task<bool> GetPhoneNumberConfirmedAsync(TUser user)
        {
            Contract.Requires(user != null, "Given user must be a non-null reference");
            Contract.Ensures(Contract.Result<Task<bool>>() != null);

            // for example: /users/{userId}/properties/phonenumber.confirmed
            HttpRequestMessage request = new HttpRequestMessage
            (
                HttpMethod.Head,
                UserPropertiesResourceUri.Replace("{userId}", ((IIdentityUser)user).Id).Replace("{propertyName}", "phoneNumber.confirmed")
            );

            return (await HttpClient.SendAsync(request)).IsSuccessStatusCode;
        }

        public virtual Task SetPhoneNumberAsync(TUser user, string phoneNumber)
        {
            Contract.Requires(user != null, "Given user must be a non-null reference");
            Contract.Requires(!string.IsNullOrEmpty(phoneNumber), "Given phone number must be a non-null reference and must not be empty");
            Contract.Ensures(Contract.Result<Task>() != null);

            user.PhoneNumber = phoneNumber;

            return Task.FromResult(true);
        }

        public virtual Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed)
        {
            Contract.Requires(user != null, "Given user must be a non-null reference");
            Contract.Ensures(Contract.Result<Task>() != null);

            // for example: /users/{userId}/properties/phonenumber.confirmed
            return HttpClient.PutAsJsonAsync
            (
                UserPropertiesResourceUri.Replace("{userId}", ((IIdentityUser)user).Id).Replace("{propertyName}", "phoneNumber.confirmed"),
                new { confirmed = confirmed }
            );
        }
    }
}