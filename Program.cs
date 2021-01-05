using System;
using System.IO;
using System.Text.RegularExpressions;

namespace File_Deduper
{
    class Program
    {
        static void Main(string[] args)
        {
            string filesCurrentLocation = @"C:\Users\ariel\Music\Monstercat Music\Dubstep";
            string filesMoveToLocation = @"C:\Users\ariel\Music\Monstercat Music\Dubstep\Dupes";

            //Check to see if the folders even exist.
            if (!Directory.Exists(filesCurrentLocation))
                throw new DirectoryNotFoundException("Current Location of Files Doesn't Exist");
            if (!Directory.Exists(filesMoveToLocation))
                throw new DirectoryNotFoundException("Move To Location of Files Doesn't Exist");

            string[] filesToMove = Directory.GetFiles(filesCurrentLocation);

            foreach (string filePath in filesToMove)
            {
                string lastTenCharactersOfFile = filePath.Substring(filePath.Length - 10, 10);

                var regex = new Regex(@"( - Copy )?(\(\d{1,3}\))(.mp3)$");

                if (regex.IsMatch(lastTenCharactersOfFile))
                {
                    Console.WriteLine("Now Moving " + Path.GetFileName(filePath));
                    File.Move(filePath, filesMoveToLocation + @"\" + Path.GetFileName(filePath));
                }
            }


        }
    }
}
