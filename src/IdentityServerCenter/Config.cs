using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServerCenter
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>() {
                new ApiResource("Api", "MyApi")
                {
                    Scopes = {
                        new string("Api")
                    }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client()
                {
                    ClientId = "client",
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { new string("Api") }
                },
                new Client()
                {
                    ClientId = "clientpwd",
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes =  { new string("Api") }
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>()
            {
                new ApiScope("Api")
            };
        }

        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser>() { 
                new TestUser()
                {
                    SubjectId = "1",
                    Username = "molw",
                    Password = "root@1234"
                }
            };
        }
    }
}
