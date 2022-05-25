using Prism.Events;
using System;

namespace ProjectManagement.UI.Events
{
    public class AfterDeveloperDeletedEvent : PubSubEvent<Guid>
    { }
}