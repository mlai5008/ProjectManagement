using Prism.Events;
using ProjectManagement.Domain.EventArgs;

namespace ProjectManagement.UI.Events
{
    public class OpenDetailViewEvent : PubSubEvent<OpenDetailViewEventArg>
    { }
}