using Newtonsoft.Json;

namespace NetMonkey.Model
{

    /// <summary>Interface implemented by a model object.</summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public interface IModelObject
    { }
}
