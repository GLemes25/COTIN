using Common.Api.Logic.Business.Notification;

namespace Common.Api.Logic.Business.Intefaces
{
    public interface INotification
    {
        IList<object> List { get; }
        bool HasNotifications { get; }
        bool Includes(Description error);
        void Add(Description error);
    }
}
