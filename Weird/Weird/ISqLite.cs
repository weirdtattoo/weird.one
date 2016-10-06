using SQLite;

namespace Weird
{
    public interface ISqLite
    {
        SQLiteConnection GetConnection(string dbPath);
    }
}