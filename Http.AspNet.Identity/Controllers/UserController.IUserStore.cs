namespace Http.AspNet.Identity
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Threading.Tasks;
    using System.Web.Http;

    [Authorize, RoutePrefix("api/v1/users")]
    public sealed partial class UserController<TUser, TStore> : ApiController
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
        private readonly TStore _userStore;

        public UserController(TStore userStore)
        {
            _userStore = userStore;
        }

        private TStore UserStore => _userStore;

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> CreateAsync(TUser user)
        {
            await UserStore.CreateAsync(user);

            return Ok();
        }

        [HttpDelete, Route("{id}")]
        public async Task<IHttpActionResult> DeleteAsync(string userId)
        {
            await UserStore.DeleteAsync(await UserStore.FindByIdAsync(userId));

            return Ok();
        }

        [HttpGet, Route("{id}")]
        public async Task<IHttpActionResult> FindByIdAsync(string userId)
        {
            return Ok(await UserStore.FindByIdAsync(userId));
        }

        public Task<IdentityUser> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        [HttpPut, Route("{id}")]
        public async Task UpdateAsync(string id)
        {
            await UserStore.UpdateAsync(await UserStore.FindByIdAsync(id));
        }
    }
}
