namespace Http.AspNet.Identity
{
    using Microsoft.AspNet.Identity;
    using System.Threading.Tasks;

    public partial class HttpUserStore<TUser> : IUserSecurityStampStore<TUser>
    {
        public virtual Task<string> GetSecurityStampAsync(TUser user)
        {
            return Task.FromResult(user.SecurityStamp);
        }

        public virtual Task SetSecurityStampAsync(TUser user, string stamp)
        {
            user.SecurityStamp = stamp;

            return Task.FromResult(true);
        }
    }
}
