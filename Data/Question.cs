using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Data
{

    public class Question
    {
        public string question;
        public Question()
        {
        }
        public Question(string question, List<Answer> answerVariants = null)
        {
            this.question = question;
            this.AnswerVariants = answerVariants;
        }


        [JsonPropertyName("answerVariants")]
        public List<Answer> AnswerVariants { get; set; } = new List<Answer>();

        [JsonPropertyName("issue")]
        public string Issue
        {
            get
            {
                return question;
            }
            set
            {
                question = value;
            }
        }

        public void RewriteAnswerVariant(int answerVariantIndex)
        {
            do
            {
                Console.Write($"Enter the variant(Old variant: {AnswerVariants[answerVariantIndex].AnswerText}): ");
                string variant = Console.ReadLine().Trim();
                if (!string.IsNullOrEmpty(variant))
                {
                    AnswerVariants[answerVariantIndex] = new Answer(variant);
                    break;
                }
                else
                    Console.WriteLine("Answer variant can't be empty. Try again\n");

            } while (true);
        }
        public void ShowAnswerVariants()
        {
            int count = 0;
            AnswerVariants.ForEach(answer => Console.WriteLine($"{++count}. {answer}"));
        }
        public bool AnswerVariantIsExist(int answerVariantIndex)
        {
            if (answerVariantIndex >= 0 && answerVariantIndex < AnswerVariants.Count)
                return true;
            else
                return false;
        }
    }

}
