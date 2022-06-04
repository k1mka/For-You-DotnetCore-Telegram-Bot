using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;

namespace TelegramBotExperiments
{

    class Program
    {
        private static ITelegramBotClient bot = new TelegramBotClient("5539229150:AAEKqeMyt1oAPfdN471CDGGiEEXtaETu8Wc");

        private static string[] stringArray = { "���� ���� ����� ������ �������, ����� ������, " +
                    "��� � ����� � � ������ ���� ��������.", "�� ����� ������!", "��������!",
                "�� ������ ����������� � �������", "����� � ���� ���� ������, ��� �������� �������� �� ����!",
                "����� ������ ����� ������������ ���� ����� ������!", "���� ������ ����� ������� ��� �����, � ���������� ������� ����� �� ���� ���������",
                "���� ������ ����� ������� ��� �����, � ���������� ������� ����� �� ���� ���������",
                "�� ������ ���������, ������ ��������� ���������.",
                "������� � ��������� � ���, ��� ���� �� ������ ���� ���������.",
                "���� ����� �������� ��� �� � �������� ������������!", "� ���� ������� ��� �������, � � ���� ������!",
                  "�� ����� �� �����, ������� ������� �� ����� ���� ��� ���������.", "���� ����� � ����� �� ���, � ��� ��� � �� ���� �����������.",
                  "� ����� ����� � ������ � ����� ������.", "�� � ������� ������������ � �� �� ���������� �� ������.",
                  "������ �����, �� ���������!", "���� ���� ����� ������ �������, ����� ������, ��� � ����� � � ������ ���� ��������.",
                  "�� ��������� ������, �� ���������� ���� ����� ��������.", "�� �������� ��������", "���� ���� ��������",
                   "��������� ����� ���� ���� ����� �����", "���� ������� ��������, ��� �� ����� �����!", "�� �����!",};
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {


            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                await HandleMessage(botClient, update);
            }
            else if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {
                await HandleCallbackQuesty(botClient, update);
            }
        }

        private static Task HandleCallbackQuesty(ITelegramBotClient botClient, Update update)
        {
            throw new NotImplementedException();
        }

        private static async Task HandleMessage(ITelegramBotClient botClient, Update update)
        {
            var message = update.Message;
            if (message.Text != null)
            {

                if (message.Text.ToLower() == "/start")
                {

                    Random random = new Random();

                    await botClient.SendTextMessageAsync(message.Chat, stringArray[new Random().Next(0, stringArray.Length)]);


                }

            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
            });


        }


        static void Main(string[] args)
        {
            Console.WriteLine("������� ��� " + bot.GetMeAsync().Result.FirstName);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            Console.ReadLine();
        }
    }
}