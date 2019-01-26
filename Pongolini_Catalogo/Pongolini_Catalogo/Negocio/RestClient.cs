using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pongolini_Catalogo.Negocio
{
    public class RestClient
    {
        public async Task<List<T>> Get<T>(string url)  //Cualquier peticion de tipo GET entra aca.
        {
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(url); //Le pasamos la URL del JSON 
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    
                    var jsonstring = await response.Content.ReadAsStringAsync();
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(jsonstring);
                }
            }
            catch (Exception)
            {

                throw;
            }


            return default(List<T>);
        }
    }
}
