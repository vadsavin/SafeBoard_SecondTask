using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace SafeBoard_ScanAPI.Utils
{
    public class RequireObjectPropertiesContractResolver : DefaultContractResolver
    {
        protected override JsonObjectContract CreateObjectContract(Type objectType)
        {
            var contract = base.CreateObjectContract(objectType);
            contract.ItemRequired = Required.AllowNull;
            return contract;
        }
    }
}
