using System;
using Data;

namespace PollManager
{
    class Menu
    {
        ManagerService manager = new ManagerService();
        public void StartMenu()
        {
            try
            {
                manager.UploadPolls();
            }
            catch
            {
                Console.WriteLine("Invalid json file");
                return;
            }

            do
            {
                ShowMenu();
                string option = manager.GetOption();
                if (option == "1")
                {
                    manager.AddPoll();
                    manager.UpdateAllPolls();
                }
                else
                if (option == "2")
                {
                    if (manager.IsAnyPoll())
                    {
                        manager.ShowPolls();
                        int pollIndex = manager.GetOrderNumber();
                        if (manager.PollIsExist(pollIndex))
                        {
                            manager.EditPoll(pollIndex);
                            manager.UpdateAllPolls();
                        }
                        else
                            Console.WriteLine("There is no such poll");
                    }
                    else
                        Console.WriteLine("The list of polls is empty");
                }
                else
                if (option == "3")
                {
                    if (manager.IsAnyPoll())
                    {
                        manager.ShowPolls();
                        int pollIndex = manager.GetOrderNumber();
                        if (manager.PollIsExist(pollIndex))
                        {
                            manager.DeletePoll(pollIndex);
                            manager.UpdateAllPolls();
                        }
                        else
                            Console.WriteLine("There is no such poll\n");
                    }
                    else
                        Console.WriteLine("The list of polls is empty");
                }
                else
                if (option == "4")
                {
                    if (manager.IsAnyPoll())
                    {
                        manager.ShowPolls();
                        int pollIndex = manager.GetOrderNumber();
                        if (manager.PollIsExist(pollIndex))
                        {
                            //вызвать метод, который приймет как параметр позицию пула в коллекции и покажет статистику по запрошенному пулу
                            manager.DisplayPollStatistic(pollIndex);
                        }
                        else
                            Console.WriteLine("There is no such poll\n");
                    }
                    else
                        Console.WriteLine("The list of polls is empty");
                }
                else
                if (option == "5")
                    return;
                else
                    Console.WriteLine("Wrong option. Please, try again\n");
            } while (true);

        }
        private void ShowMenu()
        {
            Console.WriteLine("\n1.Add new poll\n2.Edit poll\n3.Delete poll\n4.See poll's statistics\n5.Exit");
        }
    }
}
