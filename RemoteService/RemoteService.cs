using System.Collections.Generic;
using System.Fabric;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using WebService;

namespace RemoteService
{
    /// <summary>
    /// The FabricRuntime creates an instance of this class for each service type instance. 
    /// </summary>
    internal sealed class RemoteService : StatelessService, IValueService
    {
        public RemoteService(StatelessServiceContext context)
            : base(context)
        { }

        public Task<string> GetValueAsync()
        {
            return Task.FromResult("remoteValue");
        }

        /// <summary>
        /// Optional override to create listeners (like tcp, http) for this service instance.
        /// </summary>
        /// <returns>The collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            // FabricTransportServiceRemotingListener reads the x509Credentials configuration from the TransportSettings section
            // in Settings.xml
            return new[]
            {
                new ServiceInstanceListener((context) => new FabricTransportServiceRemotingListener(context, this))
            };
        }
    }
}
