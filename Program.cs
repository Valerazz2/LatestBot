using Telegram.Bot;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace LordsBotWar
{
    class Program
    {
       
        static void Main(string[] args)
        {
            var client = new TelegramBotClient("5769854630:AAHgb7dWOjYS278rwM0qQMPxQvBoLdrtsdE"); 
            client.StartReceiving(Update, Error);
            Console.ReadLine();
        }

        private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }

        private static async Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            var message = update.Message;
            if (message?.Text != null)
            {
                var count = message.Text.Count(w => w == '/');
                var nums = message.Text.Split('/');
                
                if (count != 2 || nums.Length != 3 || !nums.All(StringIsOnlyDigit) || !char.IsDigit(message.Text[0]) || 
                    !char.IsDigit(message.Text[0]) || nums.Any(e => e == ""))
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Неверный формат пропопорции");
                    return;
                }
                var sum = nums.Sum(Convert.ToInt32);
                var answer = $"Чтобы выбить {message.Text} надо использовать: {nums[1]}/{nums[2]}/{nums[0]}";
                await botClient.SendTextMessageAsync(message.Chat.Id, answer);
            }
        }

        private static bool StringIsOnlyDigit(string str)
        {
            return str.All(char.IsDigit);
        }
    }
}