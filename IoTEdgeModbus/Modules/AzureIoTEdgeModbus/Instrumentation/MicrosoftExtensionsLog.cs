namespace AzureIoTEdgeModbus.Instrumentation
{
    using Microsoft.Azure.Devices.Shared;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Conventions Used for Event Ids-
    /// 1xxx- Trace Events
    /// 2xxx- Informational Events
    /// 3xxx- Warning Events
    /// 4xxx- Error Events
    /// </summary>
    public class MicrosoftExtensionsLog : EventLog
    {
        private readonly ILogger logger;
        private readonly Dictionary<string, object> moduleIdentify;

        public MicrosoftExtensionsLog(ILogger logger, Dictionary<string, object> moduleIdentify)
        {
            this.logger = logger;
            this.moduleIdentify = moduleIdentify;
        }

        [Event(1000, Description = "Modbus sessions available: {sessionCount}")]
        public void ModbusSessionCount(int sessionCount)
        {
            this.LogTrace(MethodBase.GetCurrentMethod(), new Dictionary<string, object>() { { "sessionCount", sessionCount } });
        }

        [Event(2000, Description = "Connection to remote IoT Hub opened.")]
        public void IoTHubConnectionOpened()
        {
            this.LogInformation(MethodBase.GetCurrentMethod(), null);
        }

        [Event(2002, Description = "New desired properties received: {twinCollection}.")]
        public void DesiredPropertiesReceivedFromTwin(TwinCollection twinCollection)
        {
            this.LogInformation(MethodBase.GetCurrentMethod(), new Dictionary<string, object>() { { "twinCollection", twinCollection } });
        }

        [Event(2003, Description = "New desired properties received: {desiredProperties}.")]
        public void DesiredPropertiesReceivedFromFile(string desiredProperties)
        {
            this.LogInformation(MethodBase.GetCurrentMethod(), new Dictionary<string, object>() { { "desiredProperties", desiredProperties } });
        }

        [Event(2004, Description = "Attempting to retrieve desired configuration from provider: {providerType}.")]
        public void RetrieveDesiredConfigurationFrom(string configurationProviderTypeName)
        {
            this.LogInformation(MethodBase.GetCurrentMethod(), new Dictionary<string, object>() { { "configurationProviderTypeName", configurationProviderTypeName } });
        }

        [Event(3000, Description = "Configuration received is invalid, JSON schema validation error: {errorMessage}.")]
        public void ConfigurationValidationError(string errorMessage)
        {
            this.LogWarning(MethodBase.GetCurrentMethod(), new Dictionary<string, object>() { { "errorMessage", errorMessage } });
        }

        [Event(4000, Description = "Could not retrieve desired properties from store.")]
        public void ConfigurationRetrievalError(Exception exception)
        {
            this.LogError(MethodBase.GetCurrentMethod(), exception, new Dictionary<string, object>() { { "exception", exception } });
        }

        protected override void LogTrace(int id, string name, string description, IDictionary<string, object> properties)
        {
            this.logger.Log(LogLevel.Trace, new EventId(id, name), this.moduleIdentify, null, (object obj, Exception ex) => description);
        }

        protected override void LogInformation(int id, string name, string description, IDictionary<string, object> properties)
        {
            this.logger.Log(LogLevel.Information, new EventId(id, name), this.moduleIdentify, null, (object obj, Exception ex) => description);
        }

        protected override void LogWarning(int id, string name, string description, IDictionary<string, object> properties)
        {
            this.logger.Log(LogLevel.Information, new EventId(id, name), this.moduleIdentify, null, (object obj, Exception ex) => description);
        }

        protected override void LogError(int id, string name, Exception exception, string description, IDictionary<string, object> properties)
        {
            this.logger.Log(LogLevel.Error, new EventId(id, name), this.moduleIdentify, exception, (object obj, Exception ex) => description);
        }
    }
}