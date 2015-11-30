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
        [HttpPost, Route("{userId}/roles")]
        public async Task<IHttpActionResult> AddToRoleAsync(string userId, dynamic dto)
        {
            await UserStore.AddToRoleAsync
            (
                await UserStore.FindByIdAsync(userId),
                dto.roleName
            );

            return Ok();
        }

        [HttpGet, Route("{userId}/roles")]
        public async Task<IHttpActionResult> GetRolesAsync(string userId)
        {
            return Ok
            (
                await UserStore.GetRolesAsync
                (
                    await UserStore.FindByIdAsync(userId)
                )
            );
        }

        [HttpHead, Route("{userId}/roles/{roleName}")]
        public async Task<IHttpActionResult> IsInRoleAsync(string userId, string roleName)
        {
            bool result = await UserStore.IsInRoleAsync
            (
                await UserStore.FindByIdAsync(userId),
                roleName
            );

            if (result) return Ok();
            else return NotFound();
        }

        [HttpDelete, Route("{userId}/roles/{roleName}")]
        public async Task<IHttpActionResult> RemoveFromRoleAsync(string userId, string roleName)
        {
            await UserStore.RemoveFromRoleAsync
            (
                await UserStore.FindByIdAsync(userId),
                roleName
            );

            return Ok();
        }
    }
}