using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Portable;


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
            request = new RestRequest("api/getuser", System.Net.Http.HttpMethod.Post);
            request.AddParameter("access_token", Gadget.Token.Access, ParameterType.QueryString);
            return await base.ExecuteAsync(request);
        }
    }
}
