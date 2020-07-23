using System;
using System.Linq;
using System.Text;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var path = @"..\..\..\..\dictionary-source.txt";
            var builder = new DictionaryBuilder();

            Console.WriteLine("Press ENTER to build a dictionary\n");
            Console.ReadLine();

            var dictionary = builder.Build(path);

            Console.WriteLine("Building finished. By pressing ENTER you will display new word chosen by random");
            Console.ReadLine();

            var rnd = new Random();
            var keysArray = dictionary.Keys.ToArray();

            while (true)
            {
                var key = keysArray[rnd.Next(dictionary.Count)];
                var notInUseText = dictionary[key].NotInUse ? " (not in use)" : string.Empty;
                Console.WriteLine(dictionary[key].Text + notInUseText);
                Console.ReadLine();
            }
        }
    }
}
