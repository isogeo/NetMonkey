using Newtonsoft.Json;

namespace NetMonkey.Model
{

    /// <summary>Interface implemented by results.</summary>
    public interface IResults:
        IModelObject
    {

        /// <summary>The total number of items matching the query regardless of pagination.</summary>
        [JsonProperty(PropertyName = "total_items")]
        int TotalItems { get; }
    }
}
