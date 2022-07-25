using Prism.Events;
using ProjectManagement.Domain.EventArgs;

namespace ProjectManagement.UI.Events
{
    public class AfterDetailClosedEvent : PubSubEvent<AfterDetailClosedEventArg>
    { }
}