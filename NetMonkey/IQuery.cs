using System;
using System.Collections.Specialized;

namespace NetMonkey
{

    /// <summary>Interface implemented by a query.</summary>
    public interface IQuery
    {

        /// <summary>Gets the query parameters for the current instance.</summary>
        /// <returns>The query parameters for the current instance.</returns>
        NameValueCollection GetQueryParameters();
    }
}
