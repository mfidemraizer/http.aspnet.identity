namespace Http.AspNet.Identity
{
    using Microsoft.AspNet.Identity;
    using System.Diagnostics.Contracts;
    using System.Threading.Tasks;

    public partial class HttpUserStore<TUser> : IUserPasswordStore<TUser>
    {
        public virtual Task<string> GetPasswordHashAsync(TUser user)
        {
            Contract.Requires(user != null, "Given user must be a non-null reference");
            Contract.Ensures(Contract.Result<Task<string>>() != null);

            return Task.FromResult(user.PasswordHash);
        }

        public virtual Task<bool> HasPasswordAsync(TUser user)
        {
            Contract.Requires(user != null, "Given user must be a non-null reference");
            Contract.Ensures(Contract.Result<Task<bool>>() != null);

            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public virtual Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            Contract.Requires(user != null, "Given user must be a non-null reference");
            Contract.Requires(!string.IsNullOrEmpty(passwordHash), "Given password hash must be a non-null reference and must not be an empty string");
            Contract.Ensures(Contract.Result<Task>() != null);

            user.PasswordHash = passwordHash;

            return Task.FromResult(true);
        }
    }
}
