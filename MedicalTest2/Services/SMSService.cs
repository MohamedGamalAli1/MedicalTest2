using MedicalTest2.Helpers;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace MedicalTest2.Services
{
    public class SMSService : ISMSService
    {
        private readonly TwilioSettings _twilio;

        public SMSService(IOptions<TwilioSettings> twilio)
        {
            _twilio = twilio.Value;
        }

        public async Task<MessageResource> Send(string mobileNumber, string body)
        {
            TwilioClient.Init(_twilio.AccountSID, _twilio.AuthToken);

            var result = MessageResource.CreateAsync(
                    body: body,
                    from: new Twilio.Types.PhoneNumber(_twilio.TwilioPhoneNumber),
                    to: mobileNumber
                );

            return  await  result;
        }
    }
}