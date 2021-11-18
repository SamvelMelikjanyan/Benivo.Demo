using Benivo.Demo.Models.Infrastructure;

namespace Benivo.Demo.Models.Inputs
{
    public class RegisterInput : BaseInput
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
