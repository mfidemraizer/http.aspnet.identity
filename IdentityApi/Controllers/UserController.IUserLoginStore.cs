namespace IdentityApi.Controllers
{
    using Http.AspNet.Identity;
    using Microsoft.AspNet.Identity;
    using System.Linq;
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
        [HttpPost, Route("{userId}/logins")]
        public async Task<IHttpActionResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            await UserStore.AddLoginAsync(await UserStore.FindByIdAsync(userId), login);

            return Ok();
        }

        [HttpGet, Route("{userId}/logins")]
        public async Task<IHttpActionResult> GetLoginsAsync(string userId)
        {
            return Ok
            (
                await UserStore.GetLoginsAsync(await UserStore.FindByIdAsync(userId))
            );
        }

        [HttpDelete, Route("{userId}/logins/{providerKey}")]
        public async Task<IHttpActionResult> RemoveLoginAsync(string userId, string providerKey)
        {
            TUser user = await UserStore.FindByIdAsync(userId);
            UserLoginInfo login = (await UserStore.GetLoginsAsync(user))
                                                    .SingleOrDefault(l => l.ProviderKey == providerKey);

            if (login == null) return NotFound();

            await UserStore.RemoveLoginAsync(await UserStore.FindByIdAsync(userId), login);

            return Ok();
        }

        [HttpGet, Route("login:{providerKey}")]
        public async Task<IHttpActionResult> FindAsync(string providerKey)
        {
            return Ok(await UserStore.FindAsync(new UserLoginInfo("unknown", providerKey)));
        }
    }
}