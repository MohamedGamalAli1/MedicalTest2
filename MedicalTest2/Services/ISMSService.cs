using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio.Rest.Api.V2010.Account;

namespace MedicalTest2.Services
{
    public interface ISMSService
    {
         Task<MessageResource> Send(string mobileNumber, string body);
    }
}
