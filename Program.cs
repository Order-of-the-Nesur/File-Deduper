using System;
using System.IO;
using System.Text.RegularExpressions;

namespace File_Deduper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the Current Location of the files:");
            string filesCurrentLocation = Console.ReadLine();
            Console.WriteLine();
           
            Console.WriteLine("Please enter the Location you wish to move the duplicate files to:");
            string filesMoveToLocation = Console.ReadLine();
            Console.WriteLine();

            //Check to see if the folders even exist.
            if (!Directory.Exists(filesCurrentLocation))
                throw new DirectoryNotFoundException("Current Location of Files Doesn't Exist");
            if (!Directory.Exists(filesMoveToLocation))
                throw new DirectoryNotFoundException("Move To Location of Files Doesn't Exist");

            string[] filesToMove = Directory.GetFiles(filesCurrentLocation);

            foreach (string filePath in filesToMove)
            {

                var nonCopyExists = false;
                if (filePath.Contains("- Copy"))
                {
                    if (Array.Exists(filesToMove, element => element == filePath.Replace(" - Copy", "")))
                    {
                        nonCopyExists = true;
                    }
                }


                string lastTenCharactersOfFile = filePath.Substring(filePath.Length - 10, 10);

                var regex = new Regex(@"( - Copy )?(\(\d{1,3}\))(.mp3)$");

                if (regex.IsMatch(lastTenCharactersOfFile) || nonCopyExists)
                {
                    Console.WriteLine("Now Moving " + Path.GetFileName(filePath));
                    File.Move(filePath, filesMoveToLocation + @"\" + Path.GetFileName(filePath));
                }
            }


        }
    }
}
