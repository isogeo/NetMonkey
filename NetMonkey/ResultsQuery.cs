using System.Collections.Specialized;
using System.Globalization;

namespace NetMonkey
{

    /// <summary>Parameters for a query about results.</summary>
    /// <typeparam name="TResults">The type of results that will be returned by the query.</typeparam>
    public class ResultsQuery<TResults>:
        FieldsQuery<TResults>
        where TResults:
            Model.IResults
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

            return parameters;
        }

        /// <summary>The number of records to return.</summary>
        public int? Count { get; set; }

        /// <summary>The number of records from a collection to skip. Iterating over large collections with this parameter can be slow.</summary>
        public int? Offset { get; set; }
    }
}
