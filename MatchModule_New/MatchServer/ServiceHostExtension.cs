using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;

namespace Games.NB_Match.MatchServer
{
    public static class ServiceHostExtension
    {
        public static string Status(this ServiceHost host)
        {
            StringBuilder sb = new StringBuilder();

            foreach (Uri uri in host.BaseAddresses)
            {
                sb.AppendFormat("BaseAddresses:{0} \n", uri);
            }

            foreach (ServiceEndpoint se in host.Description.Endpoints)
            {
                sb.AppendFormat("ServiceEndpoint: Address:{0}, Binding:{1}:, Contract:{2} \n", se.Address, se.Binding.Name, se.Contract.Name);
            }

            sb.AppendFormat("servicehost has {0} ChannelDispatchers \n", host.ChannelDispatchers.Count);
            foreach (ChannelDispatcher disp in host.ChannelDispatchers)
            {
                sb.AppendFormat("\t Binding name:{0} \n", disp.BindingName);
                ServiceThrottle serviceThrottle = disp.ServiceThrottle;
                if (serviceThrottle != null)
                {
                    sb.AppendFormat("\t\t Max Calls: {0}, Max Sessions:{1}:, Max Instances:{2} \n",
                        serviceThrottle.MaxConcurrentCalls, serviceThrottle.MaxConcurrentSessions, serviceThrottle.MaxConcurrentInstances);
                }
            }

            KeyedByTypeCollection<IServiceBehavior> sbCol = host.Description.Behaviors;
            foreach (IServiceBehavior behavior in sbCol)
            {
                sb.AppendFormat("Behaviore:{0} \n", behavior.ToString());
            }

            return sb.ToString();
        }

       
    }
}
