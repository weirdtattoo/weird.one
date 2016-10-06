using SQLite;

namespace Weird.Data
{
    public class Answer
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public bool IsCorrect { get; set; }
        public string Text { get; set; }
    }
}