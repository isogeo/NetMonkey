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
        [MailChimpException("Invalid_ApiKey", ExceptionType=typeof(ApiException))]
        ApiInvalidKey,
        /// <summary>The parameters passed to the API call are invalid or not provided when required.</summary>
        [MailChimpException("ValidationError", ExceptionType=typeof(ApiException))]
        ApiValidationError,
        /// <summary>You didn't pay attention to <see href="http://apidocs.mailchimp.com/api/faq/#faq6">this</see>.</summary>
        [MailChimpException("Too_Many_Connections", ExceptionType=typeof(ApiException))]
        ApiTooManyConnections,
        /// <summary />
        [MailChimpException("List_AlreadySubscribed", ExceptionType=typeof(ListException))]
        ListAlreadySubscribed,
        /// <summary />
        [MailChimpException("List_DoesNotExist", ExceptionType=typeof(ListException))]
        ListDoesNotExist,
        /// <summary>The account being accessed has been disabled - more detail in the actual message returned.</summary>
        [MailChimpException("User_Disabled", ExceptionType=typeof(UserException))]
        UserDisabled,
        /// <summary>The account being access is currently under temporary maintenance.</summary>
        [MailChimpException("User_UnderMaintenance", ExceptionType=typeof(UserException))]
        UserUnderMaintenance,
        /// <summary>The account being accessed does not have permission to access the API method.</summary>
        [MailChimpException("User_InvalidRole", ExceptionType=typeof(UserException))]
        UserInvalidRole,
        /// <summary>The account being accessed has not been approved for some action - more detail in the actual message returned.</summary>
        [MailChimpException("User_InvalidAction", ExceptionType=typeof(UserException))]
        UserInvalidAction
    }
}
