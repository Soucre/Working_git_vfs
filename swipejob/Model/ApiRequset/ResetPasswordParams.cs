
using SwipeJob.Model.Extra;

namespace SwipeJob.Model.ApiRequset
{
    public class ResetPasswordParams
    {
        public string ConfirmationCode { get; set; }

        public string Password { get; set; }
    }
}
