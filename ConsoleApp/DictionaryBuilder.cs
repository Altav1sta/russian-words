using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp
{
    internal sealed class DictionaryBuilder
    {
        public const int StatedWordFormsCount = 5074048;
        public const int TargetEncodingCodePage = 1251;

        public IDictionary<int, WordForm> Build(string sourceFilePath)
        {
            var result = new Dictionary<int, WordForm>();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            
            using var reader = new StreamReader(sourceFilePath, Encoding.GetEncoding(TargetEncodingCodePage));

            string line;
            int counter = 0;
            int percents = 0;
            bool isLemma = true;
            int currentLemmaId = -1;
            int currentBaseFormId = -1;

            Console.Write("\nProgress: 0%");

            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    isLemma = true;
                    continue;
                }

                counter++;
                var newPercentsValue = counter * 100 / StatedWordFormsCount;
                if (newPercentsValue != percents)
                {
                    percents = newPercentsValue;
                    Console.Write("\rProgress: {0}% ({1} words processed)", percents, counter);
                }

                var word = MakeWord(line, isLemma, ref currentLemmaId, ref currentBaseFormId);

                result[word.Id] = word;
                isLemma = false;
            }

            Console.WriteLine("\n");

            return result;
        }

        private static WordForm MakeWord(string line, bool isLemma, ref int currentLemmaId, ref int currentBaseFormId)
        {
            var result = new WordForm();
            var parts = line.Split(" | ");

            result.Text = parts[0].Trim();
            result.MorphologicalCharacteristics = parts[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            result.Spelling = parts[2].Trim();

            if (parts.Length > 4)
            {
                result.Frequency = string.IsNullOrWhiteSpace(parts[3]) ? default(double?) : double.Parse(parts[3]);
                result.IsName = parts[4].Equals("1") || parts[5].Length > 0;
                result.NameKind = parts[5].Length > 0 ? parts[5] : null;
            }

            result.Id = int.Parse(parts[^1]);

            if (!line.StartsWith(' '))
            {
                currentBaseFormId = result.Id;
                if (isLemma) currentLemmaId = result.Id;
            }

            result.IsLemma = isLemma;
            result.LemmaId = currentLemmaId;
            result.BaseFormId = currentBaseFormId;
            result.NotInUse = result.Text.StartsWith('*');

            return result;
        }
    }
}
