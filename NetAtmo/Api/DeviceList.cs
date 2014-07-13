using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
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
            request = new RestRequest("api/devicelist?access_token={access_token}", System.Net.Http.HttpMethod.Post);
            request.AddParameter("access_token", Gadget.Token.Access, ParameterType.QueryString);
            bool result = await base.ExecuteAsync(request);
            return result;
        }
    }
}
