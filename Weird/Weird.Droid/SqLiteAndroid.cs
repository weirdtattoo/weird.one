using Weird.Droid;
using Xamarin.Forms;
using Weird;
using SQLite;

[assembly: Dependency(typeof(SqLiteAndroid))]

namespace Weird.Droid
{
    public class SqLiteAndroid : ISqLite
    {
      

        #region ISQLite implementation

        

        SQLiteConnection ISqLite.GetConnection(string dbPath)
        {
          
            var connection = new SQLiteConnection(dbPath);

            return connection;
        }

        #endregion
    }
}