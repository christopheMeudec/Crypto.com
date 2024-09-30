using Crypto.Core.Settings;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Crypto.Core.Services;

public class TelegramNotificationService(TelegramSettings telegramSettings) : INotificationService
{
    private readonly TelegramBotClient _telegramBotClient = new(telegramSettings.Token);

    //TODO: Replace string message by object to have more details (logo, priority depending on variation value)
    public async Task Notify(string message, CancellationToken cancellationToken)
    {
        try
        {
            await _telegramBotClient.SendTextMessageAsync(telegramSettings.ChatId, message, parseMode: ParseMode.Markdown, cancellationToken: cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}

public interface INotificationService
{
    Task Notify(string message, CancellationToken cancellationToken);
}
