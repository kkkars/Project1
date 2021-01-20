using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Data
{
    public class PollService
    {
        protected string pollsPath = $@"{Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName}\Config\polls.json";
        protected List<Poll> polls = new List<Poll>();

        public void UpdateAllPolls()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            var json = JsonSerializer.Serialize(polls, options);

            File.WriteAllText(pollsPath, json);
        }
        public int GetOrderNumber()
        {
            do
            {
                Console.Write($"Number: ");
                try
                {
                    return Convert.ToInt32(Console.ReadLine()) - 1;
                }
                catch
                {
                    Console.WriteLine("Wrong format. Try again");
                }
            } while (true) ;
        }
        public bool PollIsExist(int pollIndex)
        {
            if (pollIndex >= 0 && pollIndex < polls.Count)
                return true;
            else
                return false;
        }
        public void ShowPolls()
        {
            int count = 0;
            polls.ForEach(poll => Console.WriteLine($"{++count}. {poll.PollName}\n"));
        }
    }
}
