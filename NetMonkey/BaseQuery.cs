using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Web;

namespace NetMonkey
{

    /// <summary>Base class for a query.</summary>
    public abstract class BaseQuery:
        IQuery
    {

        /// <summary>Creates a new instance of the <see cref="BaseQuery"/> type.</summary>
        protected BaseQuery()
        { }

        /// <summary>Converts the value of this instance to a query string representation.</summary>
        /// <returns>The query string representation of this instance.</returns>
        public override string ToString()
        {
            NameValueCollection parameters = GetQueryParameters();
            if (parameters!=null)
                return parameters.ToString();

            return string.Empty;
        }

        /// <summary>Gets the query parameters for the current instance.</summary>
        /// <returns>The query parameters for the current instance.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This clearly belongs in a method.")]
        protected virtual NameValueCollection GetQueryParameters()
        {
            return HttpUtility.ParseQueryString("");
        }

        /// <summary>Gets the query parameters for the current instance.</summary>
        /// <returns>The query parameters for the current instance.</returns>
        NameValueCollection IQuery.GetQueryParameters()
        {
            return GetQueryParameters();
        }
    }
}
