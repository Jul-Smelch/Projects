using System;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;


namespace TelegramBot
{
    internal class Program
    {
      
        
        private static ReceiverOptions _receiverOptions;
        static int poemIndex = -1;
        static string[] poemList =
        {
            "*** \r\nДевочка в море каталась на лодке,\r\nВместе с прибоем вернулись колготки.\r\n\r\nРассказать еще смешное?",
            "*** \r\nБабушка козлика очень любила -\r\nЩи две недели из мяса варила.\r\n\r\nРассказать еще смешное?",
            "*** \r\nДорогой, ты с чем картошечку будешь на ужин?\r\n— С мясом.\r\nЯ как знала и купила чипсы с беконом.\r\n\r\nРассказать еще смешное?",
            "*** \r\nМальчик кормил в зоопарке пантеру...\r\nТуго теперь без руки пионеру.\r\n\r\nРассказать еще смешное?",
            "*** \r\nМаленький мальчик зашел в интернет,\r\nБольше не видел с тех пор его дед!\r\n\r\nРассказать еще смешное?",
            "*** \r\nВ полицию пришла заплаканная женщина:\r\n— Найдите моего мужа, он исчез.\r\nКогда это произошло ?\r\n— Неделю назад.\r\nНо почему вы только сейчас об этом заявляете?\r\n— У него сегодня зарплата.\r\n\r\nРассказать еще смешное?",
            "*** \r\nМадам, вы позволите вами по восхищаться?\r\n— А руками трогать будете?\r\nНет что вы.\r\n— Ну, а смысл тогда?\r\n\r\nРассказать еще смешное?"
        };

        static async Task Main()
        {
            // Создаем экземпляр бота, подключаем его по ключу АПИ.
            var botClient = new TelegramBotClient("Token");
                       
            // В этом объекте указываем какие типы Update будем получать - указали Сообщения (текст, фото/видео, голосовые/видео сообщения и т.д.)
            _receiverOptions = new ReceiverOptions 
            {
                AllowedUpdates = new[] 
                {
                    UpdateType.Message, 
                },
                // True - т.е. не будем обрабатывать сообщения, которые пришли пока бот был в оффлайне
                ThrowPendingUpdates = true,
            };

            using var cts = new CancellationTokenSource();

            // UpdateHander - обработчик приходящих Update`ов
            // Error - обработчик ошибок, связанных с Bot API
            botClient.StartReceiving(UpdateHandler, Error, _receiverOptions, cts.Token); // Запускаем бота

            var me = await botClient.GetMeAsync(); 
            Console.WriteLine($"Бот '{me.FirstName}' запущен, можно общаться.");

            // Устанавливаем бесконечную задержку, чтобы наш бот работал постоянно
            await Task.Delay(-1);

            
        }

        private static async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                // Начинаем обрабатывать входящие сообщения по типу, фильтруя на тип Message с текстовыми сообщениями и все остальное.
                switch (update.Type)
                {
                    case UpdateType.Message:
                        {
                            var message = update.Message;
                            Console.WriteLine("Получено сообщение: " + message.Text);

                            //Создаем кнопки клавиатуры для ответа
                            var replyKeyboard = new ReplyKeyboardMarkup(
                                        new List<KeyboardButton[]>()
                                        {
                                           new KeyboardButton[]
                                           {
                                                new KeyboardButton("Да"),
                                                new KeyboardButton("Нет")
                                           }
                                        })
                            {
                                // Указываем true, чтобы размер клавиатуры не менялся автоматически
                                ResizeKeyboard = true,
                            };

                            switch (message.Type)
                            {
                                case MessageType.Text:
                                    {
                                        // При запуске бота, либо переходу по ссылке "/start" отправляем приветственное сообщение, иначе предлагаем просто послушать стишки с кнопками Да/нет.
                                        if (message.Text == "/start")
                                        {
                                            await botClient.SendTextMessageAsync(
                                                chatId: message.Chat.Id,
                                                text: "Привет! Я бот, который поднимет твое настроение. Я умею рассказывать короткие смешные стихи.",
                                                replyMarkup: new ReplyKeyboardRemove() //удалили клавиатуру, т.к. не нужна
                                                );
                                            await botClient.SendTextMessageAsync(
                                                chatId: message.Chat.Id,
                                                text: "Хочешь расскажу смешное? Нажми на вариант ответа:",
                                                replyMarkup: replyKeyboard // передаем кнопки для ответа 
                                                );
                                            return;
                                        }
                                        //Добавляем обработчик для кнопки - вывод рандомного стиха, и с выбором продолжить еще со стихами или все.
                                        else if (message.Text == "Да") 
                                        {                                            
                                            var rand = new Random();
                                            await botClient.SendTextMessageAsync(
                                                 chatId: message.Chat.Id,
                                                 //text: randomPoem(rand.Next(7))
                                                 text: nextRandomPoem()
                                                 );
                                            await botClient.SendTextMessageAsync(
                                                chatId: message.Chat.Id,
                                                text: "Нажми на вариант ответа:",
                                                replyMarkup: replyKeyboard // передаем кнопки для ответа 
                                                );
                                            return;
                                        }
                                        //Добавляем второй обработчик для кнопки
                                        else if (message.Text == "Нет") 
                                        {
                                            await botClient.SendTextMessageAsync(
                                                 chatId: message.Chat.Id,
                                                 text: "Тогда просто улыбнись 🙂",
                                                 replyMarkup: new ReplyKeyboardRemove() //удалили клавиатуру, т.к. не нужна
                                                 );
                                            return;
                                        }
                                        else
                                        {
                                            await botClient.SendTextMessageAsync(
                                                 chatId: message.Chat.Id,
                                                 text: "Рассказать тебе короткие смешные стихи?"
                                                 );
                                            await botClient.SendTextMessageAsync(
                                                chatId: message.Chat.Id,
                                                text: "Нажми на вариант ответа:",
                                                replyMarkup: replyKeyboard // передаем кнопки для ответа 
                                                ); 
                                            return;
                                        }
                                        //break;
                                    }
                                default:
                                    {
                                        await botClient.SendTextMessageAsync(
                                             chatId: message.Chat.Id,
                                             text: "Я тебя не понимаю. Напиши что-нибудь.",
                                             replyMarkup: new ReplyKeyboardRemove() //удалили клавиатуру, т.к. не нужна
                                             );
                                        break;
                                    }
                                    
                            }
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        
        //Метод, возвращающий рандом стихи
        static string nextRandomPoem()
        {
            poemIndex++;
            if (poemIndex >= poemList.Length)
            {
                poemIndex = 0;
            }
            return poemList[poemIndex];
        }


        async static Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
         {
             throw new NotImplementedException();
         }
    }
}

