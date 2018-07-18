 
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DispatchManager.Services.DispatchManage
{
    public class TruckAPI : ITruckAPI
    {
        public string RetrieveTruck(string vinNbr)
        {
            if (string.IsNullOrEmpty(vinNbr)) {
                return string.Empty;
            }
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string url = string.Format("https://vpic.nhtsa.dot.gov/api/vehicles/DecodeVin/{0}?format=json&modelyear=2011", vinNbr);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                var tmp = client.GetAsync(url).Result;
                if (tmp.IsSuccessStatusCode)
                {
                    string strJson = tmp.Content.ReadAsStringAsync().Result;
                    return strJson;
                }
            }
            catch (Exception err)
            {
                return  err.Message;
            }
            return string.Empty;
        }
    }

}
