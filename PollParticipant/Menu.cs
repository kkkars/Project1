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
                Console.WriteLine("Some problems on the server. We apologize for the inconvenience. Please, try again later\n");
                return;
            }
            catch (Exception e)
            {
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
            =>Console.WriteLine("1.Pass poll\n2.Exit\n");
        private string GetOption()
        {
            Console.Write("\nOption: ");
            return Console.ReadLine().Trim();
        }
    }
}
