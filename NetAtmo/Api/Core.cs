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
            Executed.Result = null;
            Executed.Exception = null;
            try
            {
                string responseAsString = await Gadget.Client.ExecuteAsync<string>(request);
//                Debug.WriteLine(responseAsString);
                var responseAsObject= JsonConvert.DeserializeObject<T>(responseAsString);

//                var responseAsStringJsonFormated= JsonConvert.SerializeObject(responseAsObject, new JsonSerializerSettings(){Formatting= Formatting.Indented});
//                Debug.WriteLine(responseAsStringJsonFormated);

                Executed.Result= responseAsObject;
                return Executed.IsResult;
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
