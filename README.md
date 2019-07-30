# service-fabric-stateless-secure-remoting
> A simple codebase to demonstrate secure remoting between two stateless Service Fabric services

This solution has three projects
 * SecureRemoting - Service Fabric project
 * RemoteService - Stateless service which implements a Service interface defined in WebService
 * WebService - Stateless service that consumes the RemoteService through Service Fabric remoting

 The configuration for secure remoting are read implicitly from the `TransportSettings` section of the Settings.xml in both WebService and Remote Service.  
 The code has been tested in Service Fabric cluster with multiple nodetype as well.
