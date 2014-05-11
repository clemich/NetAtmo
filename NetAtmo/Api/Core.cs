using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using PortableRest;
using System.Diagnostics;
using Newtonsoft.Json;

namespace NetAtmo.Api
{
    public class Core<T> where T: class
    {
        public NetAtmo.Gadget Gadget { get; set; }

        public Core(NetAtmo.Gadget gadget)
        {
            Gadget = gadget;
        }

        async public virtual Task<bool> ExecuteAsync(RestRequest request = null)
        {
            RestClient rest = new RestClient();
            rest.BaseUrl = Gadget.Client.BaseUrl;

            Executed.Result = null;
            Executed.Exception = null;
            try
            {
                JsonSerializerSettings jss= new JsonSerializerSettings();
                jss.Formatting= Formatting.Indented;

                string response = await rest.ExecuteAsync<string>(request);
                Debug.WriteLine(response);
                var x= JsonConvert.DeserializeObject<T>(response);
                var xs= JsonConvert.SerializeObject(x, jss);
                Executed.Result= x;
                return Executed.IsResultAndStatusCodeOk;
            }
            catch (Exception ex)
            {
                Executed.Exception = ex;
                return false;
            }
        }

        public ExecutedHelper Executed = new ExecutedHelper();

        public class ExecutedHelper
        {
            #region Result
            protected T m_Result;
            public T Result
            {
                get
                {
                    return m_Result;
                }
                internal set
                {
                    m_Result = value;
                }
            }
            public bool IsResult
            {
                get
                {
                    return m_Result != null;
                }
            }

            public bool IsResultAndStatusCodeOk
            {
                get
                {
                    return IsResult;
                }
            }
            #endregion

            #region Exception
            protected Exception m_Exception;
            public Exception Exception
            {
                get
                {
                    return m_Exception;
                }
                internal set
                {
                    m_Exception = value;
                }
            }

            public bool IsException
            {
                get
                {
                    return m_Exception != null;
                }
            }
            #endregion


        }
    }
}
