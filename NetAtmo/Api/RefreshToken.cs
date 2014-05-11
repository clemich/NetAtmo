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
    /// http://dev.netatmo.com/doc/authentication/refreshtoken
    /// </summary>
    public class RefreshToken : Core<Entities.RefreshToken>
    {
        public RefreshToken(NetAtmo.Gadget gadget)
            : base(gadget)
        {
        }

        async public override Task<bool> ExecuteAsync(RestRequest request = null)
        {
            request = new RestRequest("oauth2/token");
            request.ContentType = ContentTypes.FormUrlEncoded;
            request.Method= HttpMethod.Post;
            request.AddParameter("grant_type", "refresh_token");
            request.AddParameter("refresh_token", Gadget.Token.Refresh);
            request.AddParameter("client_id", Gadget.Config.Id);
            request.AddParameter("client_secret", Gadget.Config.Secret);

            Gadget.Token.Reset();
            bool result = await base.ExecuteAsync(request);
            if (result)
            {
                Gadget.Token.Access = Executed.Result.AccessToken;
                Gadget.Token.Refresh = Executed.Result.RefreshToken;
            }
            return result;
        }
    }
}
