namespace Http.AspNet.Identity
{
    public interface IIdentityUser
    {
        string Id { get; set; }
        string Email { get; set; }
        string PhoneNumber { get; set; }
        string PasswordHash { get; set; }
        string SecurityStamp { get; set; }
    }
}