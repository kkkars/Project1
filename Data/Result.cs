using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace Data
{

    public class Result
    {

        List<Answer> answers;
        public Result()
        {

        }
        public Result(int amountOfAnswers)
        {
            Answers = new List<Answer>(amountOfAnswers);
        }


        [JsonPropertyName("answers")]
        public List<Answer> Answers
        {
            get
            {
                return answers;
            }
            set
            {
                answers = value;
            }
        }

        public void AddAnswer(string answer)
            =>answers.Add(new Answer(answer));
        public string GetUserAnswer(Question question)
        {
            do
            {
                Console.Write("Your answer: ");
                string answer = Console.ReadLine().Trim();

                if (!string.IsNullOrEmpty(answer))
                    return answer.ToLower();
                else
                    Console.WriteLine("Wrong answer: answer can't be empty\n");
            } while (true);

        }
        public void DeleteAnswer(int answerIndex)
            =>answers.RemoveAt(answerIndex);
        
    }

}
