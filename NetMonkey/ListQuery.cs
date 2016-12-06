using System.Collections.Specialized;

namespace NetMonkey
{

    /// <summary>Parameters for a query about lists.</summary>
    public class ListQuery:
        ResultsQuery<Model.ListResults>
    {

        /// <summary>Gets the query parameters for the current instance.</summary>
        /// <returns>The query parameters for the current instance.</returns>
        protected override NameValueCollection GetQueryParameters()
        {
            var parameters=base.GetQueryParameters();

            if (!string.IsNullOrWhiteSpace(Email))
                parameters.Add("email", Email);

            return parameters;
        }

        /// <summary>Restrict results to lists that include a specific subscriber’s email address.</summary>
        public string Email { get; set; }
    }
}
