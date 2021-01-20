using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Data
{
    public class ParticipantService : PollService
    {
        public void UploadPolls()
        {
            var json = File.ReadAllText(pollsPath);
            if (json.Length == 0)
                throw new Exception("No poll has been created yet.\nTurn back to pass the poll when they will be ready!\n");
            polls = JsonSerializer.Deserialize<List<Poll>>(json);
        }
        public void StartPoll(int pollIndex)
        {
            if (PollIsExist(pollIndex))
            {
                Console.WriteLine($"Poll:  {polls[pollIndex].PollName}\n");
                polls[pollIndex].PassPoll();
                UpdateAllPolls();
            }
            else
                Console.WriteLine("That poll doesn't exist.\n");
        }
    }
}
