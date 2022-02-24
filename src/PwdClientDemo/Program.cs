using System;
using System.Net.Http;
using IdentityModel.Client;
using System.Threading;
using System.Threading.Tasks;

namespace PwdClientDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            HttpClient client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");

            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest() { 
                Address = disco.TokenEndpoint,
                UserName = "molw",
                Password = "root@1234",
                ClientId = "clientpwd",
                ClientSecret = "secret",
                Scope = "Api"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:5001/WeatherForecast");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }

            Console.ReadKey();
        }
    }
}
