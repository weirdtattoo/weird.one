using System.Collections.Generic;
using SQLite;

namespace Weird.Data
{
    public class Question
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public string Text { get; set; }
        public string Explanation { get; set; }
        public List<Answer> Answers { get; set; }

        public bool IsLastQuestion { get; set; }
    }
}