using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace consulta_Ejecutiva.REST
{
    public static class Cliente
    {

        public async static Task<T> GetRequest<T>(this string url)
        {
            try
            {

                HttpClient client = new HttpClient();
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json);

            }
            catch
            {
                return default(T);
            }
        }

        public async static Task<string> GetRequestUSER<T>(this string url)
        {
            try
            {

                HttpClient client = new HttpClient();
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();
                string var = json.ToString();
                return var;

            }
            catch
            {
                return default(string);
            }
        }

        public async static Task<T> PostRequest<T>(string url, string data)
        {
            try
            {
                HttpContent httpContent = new StringContent(data, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                var response = await client.PostAsync(url, httpContent);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(jsonString);
                }
            }
            catch
            {
                return default(T);
            }

            return default(T);


        }

    }
}