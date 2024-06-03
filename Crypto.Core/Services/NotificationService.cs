namespace Crypto.Core.Services;

public class NotificationService : INotificationService
{
    //TODO: Replace string message by object to have more details (logo, priority depending on variation value)
    public Task Notify(string message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public  interface INotificationService
{
    Task Notify(string message, CancellationToken cancellationToken);
}
