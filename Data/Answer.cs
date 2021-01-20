using System.Text.Json.Serialization;

namespace Data
{
    public class Answer
    {
        private string answer;
        public Answer()
        {
        }

        public Answer(string answer)
        {
            this.answer = answer;
        }

        [JsonPropertyName("answerText")]
        public string AnswerText
        {
            get
            {
                return answer;
            }
            set
            {
                answer = value;
            }
        }
        public override string ToString()
            => answer;
    }
}
