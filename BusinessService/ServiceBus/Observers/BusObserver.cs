using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;


namespace BusinessService.ServiceBus.Observers
{
    public class BusObserver : IBusObserver
    {
        private readonly ILogger<BusObserver> _logger;
        public BusObserver(ILogger<BusObserver> logger)
        {
            _logger = logger;
        }

        public Task CreateFaulted(Exception exception)
        {
            _logger.LogError(exception, "Bus create faulted");
            return Task.CompletedTask;
        }

        public Task PostCreate(IBus bus)
        {
            _logger.LogInformation("Bus created");
            return Task.CompletedTask;
        }
        public Task PostStart(IBus bus, Task<BusReady> busReady)
        {
            _logger.LogInformation("Bus started");
            return Task.CompletedTask;
        }
        public Task PostStop(IBus bus)
        {
            _logger.LogInformation("Bus stopped");
            return Task.CompletedTask;  
        }
        public Task PreCreate(IBus bus)
        {
            _logger.LogInformation($"bus pre-create {bus.GetType().Name}");
            return Task.CompletedTask;
        }
        public Task PreStart(IBus bus)
        {
            _logger.LogInformation($"bus pre-start {bus.GetType().Name}");
            return Task.CompletedTask;
        }
        public Task PreStop(IBus bus)
        {
            _logger.LogInformation($"bus pre-stop {bus.GetType().Name}");
            return Task.CompletedTask;
        }

        public Task StartFaulted(IBus bus, Exception exception)
        {
            _logger.LogInformation($"Bus failed to start {exception}");
            return Task.CompletedTask;
        }

        public Task StopFaulted(IBus bus, Exception exception)
        {
            _logger.LogError(exception, "Bus create faulted"); 
            return Task.CompletedTask;
        }
    }
}