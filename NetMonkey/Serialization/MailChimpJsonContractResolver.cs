using System;
using System.Globalization;
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

            if (typeof(CultureInfo).IsAssignableFrom(objectType))
                contract.Converter=new Serialization.CultureInfoJsonConverter();

            if ((objectType==typeof(DateTime)) || (objectType==typeof(DateTime?)))
                contract.Converter=new DateTimeJsonConverter();

            if (objectType.IsEnum || (objectType.IsGenericType && (objectType.GetGenericTypeDefinition()==typeof(Nullable<>)) && objectType.GetGenericArguments()[0].IsEnum))
                contract.Converter=new StringEnumConverter() { CamelCaseText=true };

            return contract;
        }
    }
}
