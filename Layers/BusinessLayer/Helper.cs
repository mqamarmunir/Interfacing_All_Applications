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
        public static async Task<string> CallCliqApi(string Address,string Parameters="",string HTTPMethod="GET")
        {
            if (HTTPMethod == "GET")
            {
                HttpClient client = new HttpClient();
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
            else
            {
                return "";
            }

        }
    }

}
