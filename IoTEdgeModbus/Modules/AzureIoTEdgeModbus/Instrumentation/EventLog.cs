namespace AzureIoTEdgeModbus.Instrumentation
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public abstract class EventLog
    {
        public void LogTrace(MemberInfo mi, IDictionary<string, object> properties)
        {
            var eventAttribute = (EventAttribute) Attribute.GetCustomAttribute(mi, typeof(EventAttribute));
            this.LogTrace(eventAttribute.Id, eventAttribute.Name??mi.Name, eventAttribute.Description, properties);
        }

        protected abstract void LogTrace(int id, string name, string description, IDictionary<string, object> properties);

        public void LogInformation(MemberInfo mi, IDictionary<string, object> properties)
        {
            var eventAttribute = (EventAttribute)Attribute.GetCustomAttribute(mi, typeof(EventAttribute));
            this.LogInformation(eventAttribute.Id, eventAttribute.Name ?? mi.Name, eventAttribute.Description, properties);
        }

        protected abstract void LogInformation(int id, string name, string description, IDictionary<string, object> properties);

        public void LogWarning(MemberInfo mi, IDictionary<string, object> properties)
        {
            var eventAttribute = (EventAttribute)Attribute.GetCustomAttribute(mi, typeof(EventAttribute));
            this.LogInformation(eventAttribute.Id, eventAttribute.Name ?? mi.Name, eventAttribute.Description, properties);
        }

        protected abstract void LogWarning(int id, string name, string description, IDictionary<string, object> properties);


        public void LogError(MemberInfo mi, Exception exception, IDictionary<string, object> properties)
        {
            var eventAttribute = (EventAttribute)Attribute.GetCustomAttribute(mi, typeof(EventAttribute));
            this.LogError(eventAttribute.Id, eventAttribute.Name ?? mi.Name, exception, eventAttribute.Description, properties);
        }

        protected abstract void LogError(int id, string name, Exception exception, string description, IDictionary<string, object> properties);
    }
}