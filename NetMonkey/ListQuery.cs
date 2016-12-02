using System.Globalization;
using System.Web;

namespace NetMonkey
{

    /// <summary>Parameters for a qury about lists.</summary>
    public class ListQuery
    {

        /// <summary>Converts the value of this instance to a query string representation.</summary>
        /// <returns>The query string representation of this instance.</returns>
        public override string ToString()
        {
            var query = HttpUtility.ParseQueryString("");

            if (Count.HasValue)
                query["count"]=Count.Value.ToString("G", CultureInfo.InvariantCulture);
            if (Offset.HasValue)
                query["offset"]=Offset.Value.ToString("G", CultureInfo.InvariantCulture);
            if (!string.IsNullOrWhiteSpace(Email))
                query["email"]=Email;

            return query.ToString();
        }

        /// <summary>The number of records to return.</summary>
        public int? Count { get; set; }

        /// <summary>The number of records from a collection to skip. Iterating over large collections with this parameter can be slow.</summary>
        public int? Offset { get; set; }

        /// <summary>Restrict results to lists that include a specific subscriber’s email address.</summary>
        public string Email { get; set; }
    }
}
