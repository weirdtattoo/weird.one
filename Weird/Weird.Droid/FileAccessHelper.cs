using System;
using System.IO;
using Android.Content.Res;

namespace Weird.Droid
{
    public class FileAccessHelper
    {

        public static string GetLocalFilePath(AssetManager assets, string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string dbPath = Path.Combine(path, filename);

            CopyDatabaseIfNotExists(assets, dbPath);

            return dbPath;
        }

        private static void CopyDatabaseIfNotExists(AssetManager assets, string dbPath)
        {
            //for now replace db...
            if (File.Exists(dbPath))
            {
                File.Delete(dbPath);
            }


            if (!File.Exists(dbPath))
            {
                using (var br = new BinaryReader(assets.Open("bank.db")))
                {
                    using (var bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
                    {
                        byte[] buffer = new byte[2048];
                        int length = 0;
                        while ((length = br.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            bw.Write(buffer, 0, length);
                        }
                    }
                }
            }
        }
    }
}