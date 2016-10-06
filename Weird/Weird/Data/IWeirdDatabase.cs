using System.Collections.Generic;

namespace Weird.Data
{
    public interface IWeirdDatabase
    {
        List<Question> GetRandomQuestions(int number);
    }
}