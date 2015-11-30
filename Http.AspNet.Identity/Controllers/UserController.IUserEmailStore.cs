namespace Http.AspNet.Identity
{
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
        [HttpHead, Route("{userId}/properties/email.confirmed")]
        public async Task<IHttpActionResult> GetEmailConfirmedAsync(string userId)
        {
            bool result = await UserStore.GetEmailConfirmedAsync(await UserStore.FindByIdAsync(userId));

            if (result) return Ok();
            else return NotFound();
        }

        [HttpPut, Route("{userId}/properties/email.confirmed")]
        public async Task<IHttpActionResult> SetEmailConfirmedAsync(string userId, dynamic dto)
        {
            await UserStore.SetEmailConfirmedAsync(await UserStore.FindByIdAsync(userId), dto.confirmed);

            return Ok();
        }
    }
}