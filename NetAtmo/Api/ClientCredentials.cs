using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using RestSharp.Portable;

namespace NetAtmo.Api
{
    /// <summary>
    /// http://dev.netatmo.com/doc/authentication/usercred
    /// </summary>
    public class ClientCredentials : Core<Entities.ClientCredentials>
    {
        public ClientCredentials(NetAtmo.Gadget gadget)
            : base(gadget)
        {
        }

        async public override Task<bool> ExecuteAsync(RestRequest request = null)
        {
            request = new RestRequest("oauth2/token", System.Net.Http.HttpMethod.Post);
            request.AddParameter("grant_type", "password");
            request.AddParameter("client_id", Gadget.Config.Id);
            request.AddParameter("client_secret", Gadget.Config.Secret);
            request.AddParameter("username", Gadget.Config.UserName);
            request.AddParameter("password", Gadget.Config.Password);
            request.AddParameter("scope", Gadget.Config.Scope);

            Gadget.Token.Reset();
            bool result = await base.ExecuteAsync(request);

            if (result)
            {
                Gadget.Token.Access = Executed.Result.Data.AccessToken;
                Gadget.Token.Refresh = Executed.Result.Data.RefreshToken;
            }
            return result;
        }
    }
}
