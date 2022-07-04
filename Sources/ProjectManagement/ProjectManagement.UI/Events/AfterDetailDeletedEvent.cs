using Prism.Events;
using ProjectManagement.Domain.EventArgs;

namespace ProjectManagement.UI.Events
{
    public class AfterDetailDeletedEvent : PubSubEvent<AfterDetailDeletedEventArg>
    { }
}