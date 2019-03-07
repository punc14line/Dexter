using System;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Dexter
{
    class Program
    {

        static string Timestamp2Readable(string s) => 
            new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(int.Parse(s)).ToString("r", CultureInfo.CreateSpecificCulture("en-US"));

        static string FindTimestamp(string s)
        {
            string pattern = "\\d{10}";
            Match match = Regex.Match(s, pattern);

            while (match.Success)
            {
                s = Regex.Replace(s, pattern, Timestamp2Readable(match.Value));
                match = Regex.Match(s, pattern);
            }

            return s;
        }
        

        static void Main(string[] args)
        {
            try
            {
                string line = string.Empty;
                string input = string.Empty;
                string output = string.Empty;

                if (args.Length < 2)
                {
                    throw new MissingFieldException();
                }

                input  = (args[0].IndexOf("-i:") != -1) ? args[0].Substring(3) : input;
                input  = (args[1].IndexOf("-i:") != -1) ? args[1].Substring(3) : input;
                output = (args[0].IndexOf("-o:") != -1) ? args[0].Substring(3) : output;
                output = (args[1].IndexOf("-o:") != -1) ? args[1].Substring(3) : output;

                if (input == string.Empty || output == string.Empty)
                {
                    throw new MissingFieldException();
                }

                StreamReader f = new StreamReader(input);
                StreamWriter file = new StreamWriter(output);

                while ((line = f.ReadLine()) != null)
                {
                    file.WriteLine(FindTimestamp(line));
                }

                file.Close();
                f.Close();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Error: was not able to locate \"" + e.FileName + "\"");
            }
            catch (MissingFieldException)
            {
                Console.WriteLine("Error: Missing parameters");
                Console.WriteLine("\nRequired Parameters:");
                Console.WriteLine(" - \"-i:<Input filename>\"");
                Console.WriteLine(" - \"-o:<Output filename>\"");
                Console.WriteLine("Example: Dexter.exe \"-i:Log.txt\" \"-o:OutLog.txt\"");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
            }
        }
    }
}
