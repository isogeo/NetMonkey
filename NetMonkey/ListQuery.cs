using System.Collections.Specialized;
using System.Globalization;

namespace NetMonkey
{

    /// <summary>Parameters for a qury about lists.</summary>
    public class ListQuery:
        FieldsQuery<Model.ListResults>
    {

        /// <summary>Gets the query parameters for the current instance.</summary>
        /// <returns>The query parameters for the current instance.</returns>
        protected override NameValueCollection GetQueryParameters()
        {
            var parameters=base.GetQueryParameters();

            if (Count.HasValue)
                parameters.Add("count", Count.Value.ToString("G", CultureInfo.InvariantCulture));
            if (Offset.HasValue)
                parameters.Add("offset", Offset.Value.ToString("G", CultureInfo.InvariantCulture));
            if (!string.IsNullOrWhiteSpace(Email))
                parameters.Add("email", Email);

            return parameters;
        }

        /// <summary>The number of records to return.</summary>
        public int? Count { get; set; }

        /// <summary>The number of records from a collection to skip. Iterating over large collections with this parameter can be slow.</summary>
        public int? Offset { get; set; }

        /// <summary>Restrict results to lists that include a specific subscriber’s email address.</summary>
        public string Email { get; set; }
    }
}
