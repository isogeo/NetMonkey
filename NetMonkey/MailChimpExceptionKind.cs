using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NetMonkey
{



    ////////////////////////////////////////////////////////////////////////////
    ///
    /// <summary>API exception kinds.</summary>
    ///
    ////////////////////////////////////////////////////////////////////////////

    public enum MailChimpExceptionKind
    {
        /// <summary>Unknown error.</summary>
        Unknown=0,
        /// <summary>The API Key provided is invalid, revoked, you're in the wrong data center, or whatever the error message says.</summary>
        ApiInvalidKey,
        /// <summary>The parameters passed to the API call are invalid or not provided when required.</summary>
        ApiValidationError,
        /// <summary>You didn't pay attention to <see href="http://apidocs.mailchimp.com/api/faq/#faq6">this</see>.</summary>
        ApiTooManyConnections,
        /// <summary>The account being accessed has been disabled - more detail in the actual message returned.</summary>
        UserDisabled,
        /// <summary>The account being access is currently under temporary maintenance.</summary>
        UserUnderMaintenance,
        /// <summary>The account being accessed does not have permission to access the API method.</summary>
        UserInvalidRole,
        /// <summary>The account being accessed has not been approved for some action - more detail in the actual message returned.</summary>
        UserInvalidAction
    }
}
