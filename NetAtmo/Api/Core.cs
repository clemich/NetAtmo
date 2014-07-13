using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;
using Newtonsoft.Json;
using RestSharp.Portable;

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
            Executed.Result = null;
            Executed.Exception = null;
            try
            {
                var s = await Gadget.Client.Execute(request);
                Debug.WriteLine(s.ToString());

                Executed.Result = await Gadget.Client.Execute<T>(request);
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

            protected IRestResponse<T> m_Result;

            public IRestResponse<T> Result
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
                    return IsResult && m_Result.StatusCode == System.Net.HttpStatusCode.OK;
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
