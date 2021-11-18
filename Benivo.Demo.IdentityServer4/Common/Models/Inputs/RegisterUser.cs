namespace Benivo.Demo.IdentityServer4.Common.Models.Inputs
{
    public class RegisterUser
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public long ExternalId { get; set; }
    }
}
