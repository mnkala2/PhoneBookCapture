using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookCapture.Data
{
    public class Helper
    {
        public static async Task<string> GetData(string baseUrl)
        {
            string data = string.Empty;
            try
            {

                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                    {
                        using (HttpContent content = res.Content)
                        {
                            data = await content.ReadAsStringAsync();

                        }
                    }
                }

                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<bool> PostData(string url, object obj)
        {

            try
            {
                var json = JsonConvert.SerializeObject(obj);
                var data = new StringContent(json, Encoding.UTF8, "application/json");


                using var client = new HttpClient();

                var response = await client.PostAsync(url, data);

                if ((int)response.StatusCode == 201)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }


        public static async Task<bool> PutData(string url, object obj)
        {

            try
            {
                var json = JsonConvert.SerializeObject(obj);
                var data = new StringContent(json, Encoding.UTF8, "application/json");


                using var client = new HttpClient();

                var response = await client.PutAsync(url, data);

                if ((int)response.StatusCode == 204)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
