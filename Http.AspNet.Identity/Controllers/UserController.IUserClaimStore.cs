namespace Http.AspNet.Identity
{
    using Microsoft.AspNet.Identity;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web.Http;

    public sealed partial class UserController<TUser, TStore>
        where TUser : class, IUser, IIdentityUser
        where TStore : class,
                IUserStore<TUser>,
                IUserTwoFactorStore<TUser, string>,
                IUserSecurityStampStore<TUser>,
                IUserRoleStore<TUser>,
                IUserPhoneNumberStore<TUser>,
                IUserPasswordStore<TUser>,
                IUserLoginStore<TUser>,
                IUserLockoutStore<TUser, string>,
                IUserEmailStore<TUser>,
                IUserClaimStore<TUser>
    {
        [HttpPost, Route("{userId}/claims")]
        public async Task<IHttpActionResult> AddClaimAsync(string userId, Claim claim)
        {
            await UserStore.AddClaimAsync
            (
                await UserStore.FindByIdAsync(userId),
                claim
            );

            return Ok();
        }

        [HttpGet, Route("{userId}/claims")]
        public async Task<IHttpActionResult> GetClaimsAsync(string userId)
        {
            return Ok
            (
                await UserStore.GetClaimsAsync
                (
                    await UserStore.FindByIdAsync(userId)
                )
            );
        }

        [HttpDelete, Route("{userId}/claims/{claimType}")]
        public async Task<IHttpActionResult> RemoveClaimAsync(string userId, string claimType)
        {
            await UserStore.RemoveClaimAsync
            (
                await UserStore.FindByIdAsync(userId),
                new Claim(claimType, string.Empty)
            );

            return Ok();
        }
    }
}