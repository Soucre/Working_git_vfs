
using SwipeJob.Model.Extra;

namespace SwipeJob.Model.ApiRequset
{
    public class LoginParams
    {
        public bool IsKeepSignIn { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }

        public AccountType AccountType { get; set; }

        public UserType UserType { get; set; }
    }
}
