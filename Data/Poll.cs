using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
namespace Data
{
    public class Poll
    {
        List<Question> questions = new List<Question>();
        List<Result> results = new List<Result>();
        private string nameOfPoll;

        public Poll()
        {
        }

        public Poll(string nameOfPoll)
        {
            this.nameOfPoll = nameOfPoll;
        }

        [JsonPropertyName("pollName")]
        public string PollName
        {
            get
            {
                return nameOfPoll;
            }
            set
            {
                nameOfPoll = value;
            }
        }

        [JsonPropertyName("questions")]
        public List<Question> Questions
        {
            get
            {
                return questions;
            }
            set
            {
                questions = value;
            }
        }

        [JsonPropertyName("results")]
        public List<Result> Results
        {
            get
            {
                return results;
            }
            set
            {
                results = value;
            }
        }

        public void AddQuestionToPoll()
        {
            do
            {
                Console.WriteLine("Choose the type of the question:\n1.Simple question\n2.Question with variants\n");
                string userChoice = Console.ReadLine();
                if (userChoice == "1")
                {
                    questions.Add(new Question(CreateQuestion()));
                    return;
                }
                else
                if (userChoice == "2")
                {
                    string newQuestion = CreateQuestion();
                    questions.Add(new Question(newQuestion, CreateAnswerVariants()));
                    return;
                }
                else
                    Console.WriteLine("Wrong option. Try again.\n");
            } while (true);
        }
        public void EditPollName()
        {
            do
            {
                Console.Write("Enter new poll's name: ");
                string newPollName = Console.ReadLine().Trim();
                if (!string.IsNullOrEmpty(newPollName))
                {
                    PollName = newPollName;
                    return;
                }
                else
                    Console.WriteLine("Poll's name can't be empty. Try again\n");
            } while (true);
        }
        public void EditQuestion(int questionIndex)
        {
            if (questions[questionIndex].AnswerVariants == null)
            {
                RewriteQuestion(questionIndex);
            }
            else
            {
                do
                {
                    Console.WriteLine("1.Edit question content\n2.Edit question answer\n3.Stop editing the question\n");
                    string option = GetOption();
                    if (option == "1")
                    {
                        RewriteQuestion(questionIndex);
                    }
                    else
                    if (option == "2")
                    {
                        questions[questionIndex].ShowAnswerVariants();
                        int answerVariantIndex = GetOrderNumber();
                        if (questions[questionIndex].AnswerVariantIsExist(answerVariantIndex))
                            RewriteQuestionVariant(questionIndex, answerVariantIndex);
                        else
                            Console.WriteLine("There in no such answer variant.\n");
                    }
                    else
                    if (option == "3")
                        return;
                    else
                        Console.WriteLine("Wrong option. Try again\n");
                } while (true);
            }

        }
        public void DeleteQuestion(int questionIndex)
        {
            questions.RemoveAt(questionIndex);
            results.ForEach(result => result.DeleteAnswer(questionIndex));
        }
        public void DisplayAllQuestionStatistic()
        {
            for (int i = 0; i < questions.Count(); i++)
            {
                Console.WriteLine("\n" + questions[i].Issue);
                var allAnswers = results.Select(result => result.Answers[i]).ToList();
                var uniqueAnswers = allAnswers.Distinct(new AnswerComparer()).ToList();
                for (int j = 0; j < uniqueAnswers.Count; j++)
                {
                    decimal amountOfСoincidence = 0;
                    foreach (Answer answer in allAnswers)
                    {
                        if (answer.AnswerText == uniqueAnswers[j].AnswerText)
                            ++amountOfСoincidence;
                    }
                    Console.WriteLine($"{uniqueAnswers[j]}: {Math.Round((decimal)(amountOfСoincidence / allAnswers.Count) * 100, 2)} (Responses amount: {amountOfСoincidence})");
                }
            }
        }
        public void PassPoll()
        {
            int count = 1;
            var result = new Result(questions.Count);

            foreach (Question question in questions)
            {
                Console.WriteLine($"{count++}. {question.Issue}\n");
                if (question.AnswerVariants != null)
                    question.ShowAnswerVariants();
                result.AddAnswer(result.GetUserAnswer(question));
            }
            results.Add(result);

            if (SavingIsConfirmed())
                SavePollResult(result);
        }
        public void ShowAllQuestions()
        {
            int count = 0;
            questions.ForEach(question => Console.WriteLine($"{++count}. {question.Issue}"));
        }
        public bool QuestionIsExist(int indexOfQuestion)
        {
            if (indexOfQuestion >= 0 && indexOfQuestion < questions.Count)
                return true;
            else
            {
                Console.WriteLine("There is no question with such index\n");
                return false;
            }
        }
        private void SavePollResult(Result result)
        {
            string path = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\PollResults\result-{DateTime.Now.ToString("HH-mm-dd-MM-yyyy")}.txt";

            FileInfo file = new FileInfo(path);
            if (!file.Directory.Exists)
            {
                System.IO.Directory.CreateDirectory(file.DirectoryName);
            }

            using (StreamWriter sw = File.AppendText(path))
            {
                int counter = 0;
                foreach (var answer in result.Answers)
                {
                    sw.WriteLine($"{++counter}. {answer.AnswerText}");
                }
            }

            Console.WriteLine($"\nResults saved to path {path}");
        }
        private bool SavingIsConfirmed()
        {
            Console.Write("Do you want to save your result on the computer? (y/n)");
            var saveKey = Console.ReadKey(true).Key;

            while (saveKey != ConsoleKey.Y && saveKey != ConsoleKey.N)
            {
                saveKey = Console.ReadKey(true).Key;
            }
            if (saveKey == ConsoleKey.Y)
                return true;
            else
                return false;
        }
        private string CreateQuestion()
        {
            do
            {
                Console.Write("Enter the question: ");
                string newQuestion = Console.ReadLine().Trim();
                if (!string.IsNullOrEmpty(newQuestion))
                    return newQuestion;
                else
                    Console.WriteLine("Question can't be empty. Try again\n");

            } while (true);
        }
        private List<Answer> CreateAnswerVariants()
        {
            List<Answer> answers = new List<Answer>();
            int answerCount = 0;

            while (answerCount < 30)
            {
                Console.Write("Add answer variant: ");
                string answerVariant = Console.ReadLine().Trim();
                if (!string.IsNullOrEmpty(answerVariant))
                {
                    answers.Add(new Answer(answerVariant));
                    answerCount++;

                    if (answerCount > 1 && answerCount < 30)
                    {
                        Console.Write("Do you want to add one more variant? (y/n)");

                        var exitKey = Console.ReadKey(true).Key;

                        while (exitKey != ConsoleKey.Y && exitKey != ConsoleKey.N)
                        {
                            exitKey = Console.ReadKey(true).Key;
                        }

                        Console.WriteLine();

                        if (exitKey == ConsoleKey.N)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Variant can't be empty");
                }
            }

            return answers;
        }
        private void RewriteQuestion(int questionIndex)
        {
            do
            {
                Console.Write("Enter new question: ");
                string newQuestion = Console.ReadLine().Trim();
                if (!string.IsNullOrEmpty(newQuestion))
                {
                    questions[questionIndex].question = newQuestion;
                    return;
                }
                else
                    Console.WriteLine("Question can't be empty. Try again\n");
            } while (true);
        }
        private void RewriteQuestionVariant(int questionIndex, int answerVariantIndex)
        {
            questions[questionIndex].RewriteAnswerVariant(answerVariantIndex);
        }
        private string GetOption()
        {
            Console.Write("\nOption: ");
            return Console.ReadLine().Trim();
        }
        private int GetOrderNumber()
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
            } while (true);
        }
    }
}
