using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Data
{
    public class ManagerService : PollService
    {
        public void AddPoll()
        {
            string pollName = GetPollName();
            var newPoll = new Poll(pollName);

            int questionAmount = GetQuestionAmount();
            for (int i = 0; i < questionAmount; i++)
                newPoll.AddQuestionToPoll();
            polls.Add(newPoll);
        }
        public void EditPoll(int pollIndex)
        {
            var poll = polls[pollIndex];
            do
            {
                Console.WriteLine("1.Change poll's name\n2.Edit questions\n3. Stop editing");
                string option = GetOption();
                if (option == "1")
                {
                    poll.EditPollName();
                }
                else
                if (option == "2")
                {
                    poll.ShowAllQuestions();
                    int questionIndex = GetOrderNumber();
                    if (poll.QuestionIsExist(questionIndex))
                    {
                        do
                        {
                            Console.WriteLine("1.Edit question\n2.Delete question\n3. Stop editing");
                            option = GetOption();
                            if (option == "1")
                            {
                                poll.EditQuestion(questionIndex);
                                break;
                            }
                            else
                            if (option == "2")
                            {
                                poll.DeleteQuestion(questionIndex);
                                return;
                            }
                            else
                                Console.WriteLine("Wrong option.\nTry again\n");
                        } while (true);
                    }
                    else
                        Console.WriteLine("There in no such question\n");
                }
                else
                if (option == "3")
                    return;
                else
                    Console.WriteLine("Wrong option.\nTry again\n");

            } while (true);
        }
        public void DeletePoll(int pollIndex)
            => polls.RemoveAt(pollIndex);
        public void DisplayPollStatistic(int pollIndex)
            => polls[pollIndex].DisplayAllQuestionStatistic();
        public bool IsAnyPoll()
        {
            if (polls.Count != 0)
                return true;
            else
                return false;
        }
        public void UploadPolls()
        {
            if (!File.Exists(pollsPath))
            {
                File.Create(pollsPath).Close();
            }
            var json = File.ReadAllText(pollsPath);
            if (json.Length == 0)
                return;
            polls = JsonSerializer.Deserialize<List<Poll>>(json);
        }
        public string GetOption()
        {
            Console.Write("\nOption: ");
            return Console.ReadLine().Trim();
        }
        private string GetPollName()
        {
            do
            {
                Console.Write("Name of the poll: ");
                string pollName = Console.ReadLine().Trim();
                if (!string.IsNullOrEmpty(pollName))
                    return pollName;
                else
                    Console.WriteLine("Wrong poll name. Try again\n");
            } while (true);
        }
        private int GetQuestionAmount()
        {
            do
            {
                Console.Write("Enter the amount of question that poll will contain to create a poll: ");
                try
                {
                    return Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Wrong format. Try again\n");
                }
            } while (true);
        }
    }
}
