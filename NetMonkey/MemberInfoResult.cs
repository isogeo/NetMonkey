using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NetMonkey
{



    ////////////////////////////////////////////////////////////////////////////
    ///
    /// <summary>Result of the operation including valid data and any errors.</summary>
    ///
    ////////////////////////////////////////////////////////////////////////////

    [JsonObject(MemberSerialization.OptIn)]
    public class MemberInfoResult<TMergeVariables>
        where TMergeVariables:
            MergeVariables
    {

        /// <summary>The number of subscribers successfully found on the list.</summary>
        [JsonProperty("success_count")]
        public int SuccessCount { get; set; }

        /// <summary>The number of subscribers who were not found on the list.</summary>
        [JsonProperty("success_count")]
        public int ErrorCount { get; set; }

        /// <summary>The members found.</summary>
        [JsonProperty("data")]
        public List<MemberInfoData<TMergeVariables>> Data { get; set; }
    }
}
