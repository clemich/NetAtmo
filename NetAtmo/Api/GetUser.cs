using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortableRest;
using System.Net.Http;


namespace NetAtmo.Api
{
    /// <summary>
    /// http://dev.netatmo.com/doc/restapi/getuser
    /// </summary>
    public class GetUser : Core<Entities.GetUser>
    {
        public GetUser(NetAtmo.Gadget gadget)
            : base(gadget)
        {
        }

        async public override Task<bool> ExecuteAsync(RestRequest request = null)
        {
            request = new RestRequest("api/getuser");
            request.ContentType = ContentTypes.FormUrlEncoded;
            request.Method= HttpMethod.Post;
            request.AddQueryString("access_token", Gadget.Token.Access);
            return await base.ExecuteAsync(request);
        }
    }
}
