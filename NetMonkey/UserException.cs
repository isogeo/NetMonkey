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
    /// <summary>MailChimp user related exception.</summary>
    ///
    ////////////////////////////////////////////////////////////////////////////

    public sealed class UserException:
        MailChimpException
    {

        private UserException()
        {
        }

        /// <summary>Creates a new instance of the <see cref="UserException" /> class.</summary>
        /// <param name="message">The message of the exception.</param>
        /// <param name="code">The MailChimp code related to the exception.</param>
        /// <param name="kind">The MailChimp error kind.</param>
        internal UserException(string message, int code, MailChimpExceptionKind kind) :
            base(message, code, kind)
        {
        }
    }
}
