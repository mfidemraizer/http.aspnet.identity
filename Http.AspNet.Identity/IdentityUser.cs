namespace Http.AspNet.Identity
{
    using Microsoft.AspNet.Identity;

    public class IdentityUser : IUser, IIdentityUser
    {
        public IdentityUser() { }

        public IdentityUser(string userName)
            : this()
        {
            UserName = userName;
        }

        public virtual string Id { get; set; }
        public virtual string UserName { get; set; }

        public virtual string Email
        {
            get { return UserName; }
            set { UserName = value; }
        }
        public virtual string PhoneNumber { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual string SecurityStamp { get; set; }
    }
}