using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

class Program
{
    private static ITelegramBotClient botClient;

    static void Main()
    {
        botClient = new TelegramBotClient("YOUR_BOT_TOKEN_HERE");

        var me = botClient.GetMeAsync().Result;
        Console.WriteLine($"Hola, Mundo! Soy el usuario {me.Id} y mi nombre es {me.FirstName}.");

        botClient.OnMessage += Bot_OnMessage;
        botClient.StartReceiving(new UpdateType[] { UpdateType.Message });

        Console.ReadLine();
        botClient.StopReceiving();
    }

    private static void Bot_OnMessage(object sender, MessageEventArgs e)
    {
        if (e.Message.Text != null)
        {
            Console.WriteLine($"Recibido un mensaje de texto en el chat {e.Message.Chat.Id}.");

            switch (e.Message.Text)
            {
                case "/start":
                    botClient.SendTextMessageAsync(
                        chatId: e.Message.Chat.Id,
                        text: "¡Bienvenido a CryptoBot! Usa /balance para ver tu saldo."
                    );
                    break;

                case "/balance":
                    botClient.SendTextMessageAsync(
                        chatId: e.Message.Chat.Id,
                        text: "Tu saldo actual es 0.00 BTC."
                    );
                    break;

                case "/convert":
                    botClient.SendTextMessageAsync(
                        chatId: e.Message.Chat.Id,
                        text: "Por favor, especifica la moneda a la que deseas convertir (por ejemplo, /convert USD)."
                    );
                    break;

                case "/trends":
                    botClient.SendTextMessageAsync(
                        chatId: e.Message.Chat.Id,
                        text: "Obteniendo tendencias para tu portafolio..."
                    );
                    break;

                case "/trade":
                    botClient.SendTextMessageAsync(
                        chatId: e.Message.Chat.Id,
                        text: "Por favor, especifica la acción de comercio (por ejemplo, /buy BTC 0.01)."
                    );
                    break;

                default:
                    botClient.SendTextMessageAsync(
                        chatId: e.Message.Chat.Id,
                        text: "Lo siento, no entendí ese comando."
                    );
                    break;
            }
        }
    }
}
 