using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;

namespace NetMonkey
{

    /// <summary>Interface implemented by a query.</summary>
    public interface IQuery
    {

        /// <summary>Gets the query parameters for the current instance.</summary>
        /// <returns>The query parameters for the current instance.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This clearly belongs in a method.")]
        NameValueCollection GetQueryParameters();
    }
}
