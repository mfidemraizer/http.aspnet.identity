namespace IdentityApi.Controllers
{
    using Http.AspNet.Identity;
    using Microsoft.AspNet.Identity;
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
        [HttpGet, Route("{userId}/properties/lockout.count")]
        public async Task<IHttpActionResult> GetAccessFailedCountAsync(string userId)
        {
            return Ok
            (
                await UserStore.GetAccessFailedCountAsync
                (
                    await UserStore.FindByIdAsync(userId)
                )
            );
        }

        [HttpHead, Route("{userId}/properties/lockout.enabled")]
        public async Task<IHttpActionResult> GetLockoutEnabledAsync(string userId)
        {
            bool result = await UserStore.GetLockoutEnabledAsync
            (
                await UserStore.FindByIdAsync(userId)
            );

            if (result) return Ok();
            else return NotFound();
        }

        [HttpGet, Route("{userId}/properties/lockout.enddate")]
        public async Task<IHttpActionResult> GetLockoutEndDateAsync(string userId)
        {
            return Ok
            (
                await UserStore.GetLockoutEndDateAsync
                (
                    await UserStore.FindByIdAsync(userId)
                )
            );
        }

        [HttpPut, Route("{userId}/properties/lockout.failcount")]
        public async Task<IHttpActionResult> IncrementAccessFailedCountAsync(string userId)
        {
            await UserStore.IncrementAccessFailedCountAsync
            (
                await UserStore.FindByIdAsync(userId)
            );

            return Ok();
        }

        [HttpDelete, Route("{userId}/properties/lockout.failcount")]
        public async Task<IHttpActionResult> ResetAccessFailedCountAsync(string userId)
        {
            await UserStore.ResetAccessFailedCountAsync
            (
                await UserStore.FindByIdAsync(userId)
            );

            return Ok();
        }

        [HttpPut, Route("{userId}/properties/lockout.enabled")]
        public async Task<IHttpActionResult> SetLockoutEnabledAsync(string userId, dynamic dto)
        {
            await UserStore.SetLockoutEnabledAsync
            (
                await UserStore.FindByIdAsync(userId),
                dto.enabled
            );

            return Ok();
        }

        [HttpPut, Route("{userId}/properties/lockout.enddate")]
        public async Task<IHttpActionResult> SetLockoutEndDateAsync(string userId, dynamic dto)
        {
            await UserStore.SetLockoutEndDateAsync
            (
                await UserStore.FindByIdAsync(userId),
                dto.date
            );

            return Ok();
        }
    }
}