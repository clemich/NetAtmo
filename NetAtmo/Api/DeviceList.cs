using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Portable;

namespace NetAtmo.Api
{
    /// <summary>
    /// http://dev.netatmo.com/doc/restapi/devicelist
    /// </summary>
    public class DeviceList : Core<Entities.DeviceList>
    {
        public DeviceList(NetAtmo.Gadget gadget)
            : base(gadget)
        {
        }

        async public override Task<bool> ExecuteAsync(RestRequest request = null)
        {
            request = new RestRequest("api/devicelist", System.Net.Http.HttpMethod.Post);
            request.AddParameter("access_token", Gadget.Token.Access, ParameterType.QueryString);
            Gadget.Stations.Clear();
            Gadget.Modules.Clear();
            bool result = await base.ExecuteAsync(request);
            if (result)
            {
                foreach (var device in Executed.Result.Data.Body.Devices)
                {
                    Gadget.Stations.Add(device.Id, device.StationName);
                    Gadget.Modules.Add(device.Id, device.ModuleName);
                }

                foreach (var module in Executed.Result.Data.Body.Modules)
                    Gadget.Modules.Add(module.Id, module.ModuleName);
            }
            return result;
        }
    }
}
