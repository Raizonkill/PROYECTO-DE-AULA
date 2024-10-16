using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types.Enums;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    private static ITelegramBotClient botClient;

    static async Task Main()
    {
        botClient = new TelegramBotClient("7080715683:AAH4X0VODcjIeH6PRxdU_DllhwidiFfIxhs");

        var me = await botClient.GetMeAsync();
        Console.WriteLine($"Hola, Mundo! Soy el usuario {me.Id} y mi nombre es {me.FirstName}.");

        using var cts = new CancellationTokenSource();

        botClient.StartReceiving(
            new DefaultUpdateHandler(HandleUpdateAsync, HandleErrorAsync),
            cts.Token
        );

        Console.WriteLine("Presiona Enter para salir");
        Console.ReadLine();

        cts.Cancel();
    }

    private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Telegram.Bot.Types.Update update, CancellationToken cancellationToken)
    {
        if (update.Type == UpdateType.Message && update.Message.Text != null)
        {
            var message = update.Message;
            Console.WriteLine($"Recibido un mensaje de texto en el chat {message.Chat.Id}.");

            // Responder a mensajes específicos
            switch (message.Text)
            {
                case "/start":
                    await botClient.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: "¡Bienvenido a CryptoBot! Usa /balance para ver tu saldo."
                    );
                    break;

                case "/balance":
                    await botClient.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: "Tu saldo actual es 0.00 BTC."
                    );
                    break;

                case "/convert":
                    await botClient.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: "Por favor, especifica la moneda a la que deseas convertir (por ejemplo, /convert USD)."
                    );
                    break;

                case "/trends":
                    await botClient.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: "Obteniendo tendencias para tu portafolio..."
                    );
                    break;

                case "/trade":
                    await botClient.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: "Por favor, especifica la acción de comercio (por ejemplo, /buy BTC 0.01)."
                    );
                    break;

                default:
                    await botClient.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: "Lo siento, no entendí ese comando."
                    );
                    break;
            }
        }
    }

    private static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Error: {exception.Message}");
        return Task.CompletedTask;
    }
}
