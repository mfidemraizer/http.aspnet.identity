namespace Http.AspNet.Identity
{
    using System.Security.Claims;

    public class IdentityUserClaim
    {
        public virtual string Id { get; set; }
        public virtual string UserId { get; set; }
        public virtual string ClaimType { get; set; }
        public virtual string ClaimValue { get; set; }

        public static implicit operator IdentityUserClaim(Claim claim)
        {
            return new IdentityUserClaim { ClaimType = claim.Type, ClaimValue = claim.Value };
        }
    }
}