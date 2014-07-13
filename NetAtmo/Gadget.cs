using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Portable;

namespace NetAtmo
{
    /// <summary>
    /// http://dev.netatmo.com/doc/
    /// http://dev.netatmo.com/dev/createapp
    /// </summary>
    public class Gadget
    {
        public Gadget(string id, string secret, string userName, string password, string scope = null)
        {
            Config.Id = id;
            Config.Secret = secret;
            Config.UserName = userName;
            Config.Password = password;

            // read_station 
            // read_thermostat 
            // write_thermostat
            if (scope == null)
                Config.Scope = "read_station";
            else
                Config.Scope = scope;
        }

        #region Client
        protected RestClient m_Client;

        public const string RootUrl = "http://api.netatmo.net";
        public RestClient Client
        {
            get
            {
                if (m_Client == null)
                {
                    m_Client = new RestClient(RootUrl);
                }
                return m_Client;
            }
        }
        #endregion

        #region Config
        public class ConfigHelper
        {
            #region Id
            protected string m_Id = "";
            public string Id
            {
                get
                {
                    return m_Id;
                }
                internal set
                {
                    m_Id = value;
                }
            }
            #endregion

            #region Secret
            protected string m_Secret = "";
            public string Secret
            {
                get
                {
                    return m_Secret;
                }
                internal set
                {
                    m_Secret = value;
                }
            }
            #endregion

            #region UserName
            protected string m_UserName = "";
            public string UserName
            {
                get
                {
                    return m_UserName;
                }
                internal set
                {
                    m_UserName = value;
                }
            }
            #endregion

            #region Password
            protected string m_Password = "";
            public string Password
            {
                get
                {
                    return m_Password;
                }
                internal set
                {
                    m_Password = value;
                }
            }
            #endregion

            #region Scope
            protected string m_Scope = "";
            public string Scope
            {
                get
                {
                    return m_Scope;
                }
                internal set
                {
                    m_Scope = value;
                }
            }
            #endregion
        }

        private ConfigHelper m_Config = new ConfigHelper();
        public ConfigHelper Config
        {
            get { return m_Config; }
        }
        #endregion

        #region Api
        private Api.ClientCredentials m_ClientCredentials = null;
        public Api.ClientCredentials ClientCredentials
        {
            get
            {
                if (m_ClientCredentials == null)
                    m_ClientCredentials = new Api.ClientCredentials(this);
                return m_ClientCredentials;
            }
        }

        private Api.DeviceList m_DeviceList = null;
        public Api.DeviceList DeviceList
        {
            get
            {
                if (m_DeviceList == null)
                    m_DeviceList = new Api.DeviceList(this);
                return m_DeviceList;
            }
        }

        private Api.GetUser m_GetUser = null;
        public Api.GetUser GetUser
        {
            get
            {
                if (m_GetUser == null)
                    m_GetUser = new Api.GetUser(this);
                return m_GetUser;
            }
        }

        private Api.RefreshToken m_RefreshToken = null;
        public Api.RefreshToken RefreshToken
        {
            get
            {
                if (m_RefreshToken == null)
                    m_RefreshToken = new Api.RefreshToken(this);
                return m_RefreshToken;
            }
        }
        #endregion

        #region Token
        public class TokenHelper
        {
            public TokenHelper()
            {
                Reset();
            }
            public string Access { get; set; }
            public string Refresh { get; set; }

            public void Reset()
            {
                Access = "";
                Refresh = "";
            }
        }

        private TokenHelper m_Token = new TokenHelper();
        public TokenHelper Token
        {
            get { return m_Token; }
        }
        #endregion
    }


}
