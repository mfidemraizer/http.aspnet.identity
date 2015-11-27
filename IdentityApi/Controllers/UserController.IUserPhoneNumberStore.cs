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
        [HttpHead, Route("{userId}/properties/phonenumber.confirmed")]
        public async Task<IHttpActionResult> GetPhoneNumberConfirmedAsync(string userId)
        {
            bool result = await UserStore.GetPhoneNumberConfirmedAsync(await UserStore.FindByIdAsync(userId));

            if (result) return Ok();
            else return NotFound();
        }

        [HttpPut, Route("{userId}/properties/phonenumber.confirmed")]
        public async Task<IHttpActionResult> SetPhoneNumberConfirmedAsync(string userId, dynamic dto)
        {
            await UserStore.SetPhoneNumberConfirmedAsync(await UserStore.FindByIdAsync(userId), dto.confirmed);

            return Ok();
        }
    }
}