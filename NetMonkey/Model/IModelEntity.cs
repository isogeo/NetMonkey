using Newtonsoft.Json;

namespace NetMonkey.Model
{

    /// <summary>Interface implemented by a model entity.</summary>
    public interface IModelEntity:
        IModelObject
    {

        /// <summary>A string that uniquely identifies this enity.</summary>
        [JsonProperty(PropertyName = "id")]
        string Id { get; }
    }
}
