using System;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace NetMonkey.Serialization
{



    ////////////////////////////////////////////////////////////////////////////
    ///
    /// <summary>Resolves member mappings for domain model types.</summary>
    ///
    ////////////////////////////////////////////////////////////////////////////

    public class MailChimpJsonContractResolver:
        CamelCasePropertyNamesContractResolver
    {

        /// <summary>Determines which contract type is created for the given <paramref name="objectType" />. </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>A <see cref="JsonContract" /> for the given type.</returns>
        protected override JsonContract CreateContract(Type objectType)
        {
            JsonContract contract=base.CreateContract(objectType);

            if ((objectType==typeof(DateTime)) || (objectType==typeof(DateTime?)))
                contract.Converter=new DateTimeJsonConverter();

            return contract;
        }
    }
}
