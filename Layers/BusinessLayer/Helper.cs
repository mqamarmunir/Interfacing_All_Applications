using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public static class Helper
    {
        public static async Task<string> CallCliqApi(string Address, string Parameters = "", string HTTPMethod = "GET")
        {
            if (HTTPMethod == "GET")
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = new TimeSpan(0, 0, 20);
                    try
                    {
                        HttpResponseMessage res = await client.GetAsync(Address);

                        if (res.IsSuccessStatusCode)
                        {
                            var content = await res.Content.ReadAsStringAsync();
                            return content.ToString();
                        }
                        else
                        {
                            return "";
                        }

                    }
                    catch (Exception ee)
                    {
                        throw ee;
                    }
                }
            }
            else
            {
                return "";
            }

        }

        public static IRestResponse PostResultsToCliq(string Address, string json)
        {
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls11;
                var client = new RestClient(Address);
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");

                request.AddHeader("Authorization", System.Configuration.ConfigurationSettings.AppSettings["AuthorizationHeader"].ToString().Trim());
                request.AddParameter("value", json, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                return response;

            }
            catch (Exception)
            {
                throw;
            }
        }


    }

}
