using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace NetMonkey.Model
{

    /// <summary>Interface implemented by a model object.</summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = "We need an interface here")]
    public interface IModelObject
    { }
}
