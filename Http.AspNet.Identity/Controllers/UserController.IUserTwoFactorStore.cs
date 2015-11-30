namespace Http.AspNet.Identity
{
    using Microsoft.AspNet.Identity;
    using System.Threading.Tasks;
    using System.Web.Http;

    [Authorize]
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
        [HttpHead, Route("{userId}/properties/twofactorauth.enabled")]
        public async Task<IHttpActionResult> GetTwoFactorEnabledAsync(string userId)
        {
            return Ok(await UserStore.GetTwoFactorEnabledAsync(await UserStore.FindByIdAsync(userId)));
        }

        [HttpPut, Route("{userId}/properties/twofactorauth.enabled")]
        public async Task<IHttpActionResult> SetTwoFactorEnabledAsync(string userId, dynamic dto)
        {
            return Ok
            (
                await UserStore.SetTwoFactorEnabledAsync
                (
                    await UserStore.FindByIdAsync(userId),
                    dto.enabled
                )
            );
        }
    }
}