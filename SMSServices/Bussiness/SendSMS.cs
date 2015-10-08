using System;
using System.Collections.Generic;

using System.Text;

namespace Bussiness
{
    public class SendSMS
    {
        public string userName { get; set; }
        public string password { get; set; }

        public SendSMS(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }

        public int SendSPAM(string receiver, String messageContent)
        {
            int returnValue;
            MTSpam.MTSender mTSender = new MTSpam.MTSender();

            returnValue = mTSender.doSendMTSPAM(receiver, messageContent, this.userName, this.password);

            //if (returnValue == (int)SMSCommandStatus.SuccessAndFinish)


            return returnValue;
        }
        public enum SMSCommandStatus
        {
            ErrorOnMessage = 0, //0
            SuccessAndFinish = 1, //1
            InvalidUserNameOrPass = -1,
            InvalidMessageContent = -2,
            InvalidPhoneNumber = -3,
            ExcessNumberOfMessagesPerday = -4,
            Other = -5
        }
    }
}
