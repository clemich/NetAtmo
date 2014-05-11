using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortableRest;
using System.Net.Http;

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
            request = new RestRequest("api/devicelist?access_token={access_token}", HttpMethod.Post);
            request.ContentType = ContentTypes.FormUrlEncoded;
            request.AddUrlSegment("access_token", Gadget.Token.Access);
            bool result = await base.ExecuteAsync(request);
            return result;
        }
    }
}
