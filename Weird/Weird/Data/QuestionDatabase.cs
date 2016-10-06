using System.Collections.Generic;
using System.Linq;
using SQLite;
using Xamarin.Forms;

namespace Weird.Data
{
    public class WeirdDatabase : IWeirdDatabase
    {
        private readonly SQLiteConnection _connection;

        public WeirdDatabase(string dbPath)
        {
            _connection = DependencyService.Get<ISqLite>().GetConnection(dbPath);
       }

        public List<Question> GetRandomQuestions(int number)
        {
            return (from t in _connection.Table<Question>() select t).ToList();
        }
    }
}