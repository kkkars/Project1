using System;
using System.Collections.Generic;
using System.Text;
using Data;

namespace PollParticipant
{
    class Menu
    {
        ParticipantService pollService = new ParticipantService();
        public void StartMenu()
        {
            try
            {
                pollService.UploadPolls();
            }
            catch (System.Text.Json.JsonException)
            {
                //итак, если джейсон битый, удален и прочее и не может быть считан, то у нас "проблемы на сервере"
                Console.WriteLine("Some problems on the server. We apologize for the inconvenience. Please, try again later\n");
                return;
            }
            catch (Exception e)
            {
                //этот кетч он для перехвата ошибки, которую кинет вызванный нами метод, а эта ошибка будет выкинута если файл пустой, и значит пулов у нас нету, просто считаем переденный месседж
                Console.WriteLine($"{e.Message}");
                return;
            }

            do
            {
                ShowMenu();
                string option = GetOption();
                if (option == "1")
                {
                    pollService.ShowPolls();
                    int pollIndex = pollService.GetOrderNumber();
                    pollService.StartPoll(pollIndex);
                    // pollService.UpdateAllPolls();
                }
                else
                if (option == "2")
                {
                    Console.WriteLine("Have a nice day :)");
                    return;
                }
                else
                    Console.WriteLine("Wrong option. Try again\n");

                Console.WriteLine();
            } while (true);

        }
        private void ShowMenu()
        {
            Console.WriteLine("1.Pass poll\n2.Exit\n");
        }
        private string GetOption()
        {
            Console.Write("\nOption: ");
            return Console.ReadLine().Trim();
        }
    }
}
