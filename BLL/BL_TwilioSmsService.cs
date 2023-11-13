using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace BLL
{
    public class BL_TwilioSmsService
    {
        private readonly string accountSid;
        private readonly string authToken;
        private readonly PhoneNumber fromPhoneNumber;

        public BL_TwilioSmsService(string accountSid, string authToken, string fromPhoneNumber)
        {
            this.accountSid = accountSid;
            this.authToken = authToken;
            this.fromPhoneNumber = new PhoneNumber(fromPhoneNumber);

            TwilioClient.Init(this.accountSid, this.authToken);
        }

        public void SendSms(string toPhoneNumber, string messageBody)
        {
            var messageOptions = new CreateMessageOptions(new PhoneNumber(toPhoneNumber))
            {
                From = this.fromPhoneNumber,
                Body = messageBody
            };

            var message = MessageResource.Create(messageOptions);
            Console.WriteLine(message.Body);
        }
    }
}
