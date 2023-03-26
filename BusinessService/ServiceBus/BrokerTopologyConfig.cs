using BusinessService.ServiceBus.Consumers;
using MassTransit;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessService.ServiceBus
{
    public static class BrokerTopologyConfig
    {
        // More generic bus configurator outside of the scope of this repository purpose

        //public static IBusControl ConfigureBus(IKernel kernel, IPublishObserver publishObserver, IReceiveObserver receiveObserver, IBusObserver busObserver, ISendObserver sendObserver)
        //{
        //    var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
        //    {
        //        var host = cfg.Host(
        //            host: new Uri("rabbitmq://localhost"), // put these in appSettings
        //            port: 5672,
        //            virtualHost: "/",
        //            configure: h =>
        //            {
        //                h.Username("guest");
        //                h.Password("guest");
        //            }
        //        );
        //        cfg.AutoDelete = true;
        //        cfg.Durable = true;
        //        cfg.ReceiveEndpoint(host, "save_customer_lead", e =>
        //        {
        //            e.Consumer<SaveCustomerLeadCommandConsumer>(kernel);
        //        });
        //    });                    
        //}
    }
}