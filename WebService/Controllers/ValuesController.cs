using System;
using System.Collections.Generic;
using System.Fabric;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Remoting.FabricTransport;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Client;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            ServiceProxyFactory serviceProxyFactory = new ServiceProxyFactory(
                (c) => new FabricTransportServiceRemotingClientFactory());

            // Factory reads the x509Credentials configuration from the TransportSettings section
            // in Settings.xml
            var remoteClient = serviceProxyFactory.CreateServiceProxy<IValueService>(
                new Uri("fabric:/SecureRemoting/RemoteService"));

            var result = await remoteClient.GetValueAsync();
            return new string[] { "value1", "value2", result };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private static SecurityCredentials GetSecurityCredentials()
        {
            // Provide certificate details.
            var x509Credentials = new X509Credentials
            {
                FindType = X509FindType.FindByThumbprint,
                FindValue = "fb1a3dfbb66074311a70c5c461b4c807f29f3e2a",
                StoreLocation = StoreLocation.LocalMachine,
                StoreName = "My",
                ProtectionLevel = ProtectionLevel.EncryptAndSign
            };
            x509Credentials.RemoteCommonNames.Add("ServiceFabric-Test-Cert");
            x509Credentials.RemoteCertThumbprints.Add("fb1a3dfbb66074311a70c5c461b4c807f29f3e2a");
            return x509Credentials;
        }
    }
}
