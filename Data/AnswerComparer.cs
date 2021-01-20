using System;
using System.Collections.Generic;

namespace Data
{
    class AnswerComparer : IEqualityComparer<Answer>
    {
        public bool Equals(Answer a1, Answer a2)
        {
            if (a1 == null && a2 == null)
                return true;
            else
            if (a1 == null || a2 == null)
                return false;
            else
            if (a1.AnswerText == a2.AnswerText)
                return true;
            else
                return false;
        }
        public int GetHashCode(Answer obj)
        {
            if (Object.ReferenceEquals(obj, null)) return 0;
            return obj.AnswerText.GetHashCode();
        }
    }
}
