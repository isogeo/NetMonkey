using System;
using System.Diagnostics;

namespace NetMonkey
{




    ////////////////////////////////////////////////////////////////////////////
    ///
    /// <summary>Links a MailChimp API error to an exception.</summary>
    ///
    ////////////////////////////////////////////////////////////////////////////

    [AttributeUsage(AttributeTargets.Field, AllowMultiple=false)]
    internal class MailChimpExceptionAttribute:
        Attribute
    {

        public MailChimpExceptionAttribute(string error)
        {
            Debug.Assert(!string.IsNullOrEmpty(error));
            if (string.IsNullOrEmpty(error))
                throw new ArgumentNullException("error");

            _Error=error;
        }

        public string Error
        {
            get
            {
                return _Error;
            }
        }

        public Type ExceptionType
        {
            get
            {
                return _ExceptionType;
            }
            set
            {
                Debug.Assert(value!=null);
                if (value==null)
                    throw new ArgumentNullException("exceptionType");

                _ExceptionType=value;
            }
        }

        private string _Error;
        private Type _ExceptionType;
    }
}
