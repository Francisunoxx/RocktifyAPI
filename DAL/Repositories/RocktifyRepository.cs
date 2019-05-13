using DAL.Interfaces;
using ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class RocktifyRepository : IRocktifyRepository
    {
        const string client_id = "f9e175275fce4fefbc7de713e8e2c601";
        const string client_secret = "ea2f175d15524e25a8e45bece72cfe8c";
        const string grant_type = "client_credentials";

        public async Task<object> AccessToken()
        {
            using (HttpClient client = new HttpClient())
            {
                Uri uri = new Uri("https://accounts.spotify.com/api/token");

                HttpContent content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("client_id", client_id),
                    new KeyValuePair<string, string>("client_secret", client_secret),
                    new KeyValuePair<string, string>("grant_type", grant_type)
                });
                
                HttpResponseMessage response = await client.PostAsync(uri, content).ConfigureAwait(false);

                return JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
            }
        }
    }
}
