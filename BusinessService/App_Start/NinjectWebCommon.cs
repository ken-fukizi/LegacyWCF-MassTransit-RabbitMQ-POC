[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(BusinessService.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(BusinessService.App_Start.NinjectWebCommon), "Stop")]

namespace BusinessService.App_Start
{
    using System;
    using System.Web;
    using BusinessService.ServiceBus;
    using BusinessService.ServiceBus.Commands;
    using BusinessService.ServiceBus.Consumers;
    using BusinessService.ServiceBus.Observers;
    using GreenPipes;
    using MassTransit;
    using Microsoft.Extensions.Logging;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();                

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ILoggerFactory>().ToConstant(LoggerFactory.Create(builder => { builder.AddConsole(); }));
            kernel.Bind<ILogger>().ToProvider(typeof(Logger<>));
            kernel.Bind<ITester>().To<Tester>();
            

            kernel.Bind<IConsumer<ISaveCustomerLeadCommand>>().To<SaveCustomerLeadCommandConsumer>();

            //var busControl =  Bus.Factory.CreateUsingRabbitMq(cfg =>
            //{
            //    var host = cfg.Host(new Uri("rabbitmq://localhost"), h =>
            //    {
            //        h.Username("guest");
            //        h.Password("guest");
            //    });
            //    cfg.AutoDelete = false;
            //    cfg.Durable = true;


            //    cfg.ReceiveEndpoint(host, "save_customer_lead", e =>
            //    {
            //        e.AutoDelete = false;
            //        e.PrefetchCount = 5;
            //        e.UseRetry(retry =>
            //        {
            //            retry.Interval(3, TimeSpan.FromMinutes(5));
            //            retry.Handle<Exception>();
            //        });
            //        e.UseCircuitBreaker(breaker =>
            //        {
            //            breaker.Handle<Exception>();
            //            breaker.TrackingPeriod = TimeSpan.FromMinutes(1);
            //            breaker.TripThreshold = 10;
            //            breaker.ActiveThreshold = 10;
            //            breaker.ResetInterval = TimeSpan.FromMinutes(1);
            //        });
            //        e.UseRateLimit(
            //                rateLimit: 10,
            //                interval: TimeSpan.FromSeconds(1)
            //            );
            //        e.Consumer<SaveCustomerLeadCommandConsumer>(kernel);
            //        EndpointConvention.Map<ISaveCustomerLeadCommand>(e.InputAddress);
            //    });
            //});

            //busControl.Start();

            // The concrete values will have to be moved to appsettings
            kernel.Bind<IBus>().ToMethod(ctx => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.AutoDelete = false;
                cfg.Durable = true;


                cfg.ReceiveEndpoint(host, "save_customer_lead", e =>
                {
                    e.AutoDelete = false;
                    e.PrefetchCount = 5;
                    e.UseRetry(retry =>
                    {
                        retry.Interval(3, TimeSpan.FromMinutes(5));
                        retry.Handle<Exception>();
                    });
                    e.UseCircuitBreaker(breaker =>
                    {
                        breaker.Handle<Exception>();
                        breaker.TrackingPeriod = TimeSpan.FromMinutes(1);
                        breaker.TripThreshold = 10;
                        breaker.ActiveThreshold = 10;
                        breaker.ResetInterval = TimeSpan.FromMinutes(1);
                    });
                    e.UseRateLimit(
                            rateLimit: 10,
                            interval: TimeSpan.FromSeconds(1)
                        );
                    e.Consumer<SaveCustomerLeadCommandConsumer>(kernel);
                    EndpointConvention.Map<ISaveCustomerLeadCommand>(e.InputAddress);
                });
            })).InSingletonScope();

            kernel.Bind<ISendObserver>().To<SendObserver>();
            kernel.Bind<IReceiveObserver>().To<ReceiveObserver>();
            kernel.Bind<IPublishObserver>().To<PublishObserver>();
            kernel.Bind<IBusObserver>().To<BusObserver>();            
        }        
    }
}
