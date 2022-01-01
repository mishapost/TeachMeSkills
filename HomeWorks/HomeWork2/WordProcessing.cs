using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HomeWork2
{
    public class WordProcessing
    {
        public WordProcessing(string dataText)
        {
            SourceText = dataText;
        }

        public string SourceText { get; }


        /// <summary> Метод получения слов из текста </summary>
        /// <returns></returns>
        public List<string> GetWords()
        {
            if (string.IsNullOrEmpty(SourceText) || string.IsNullOrWhiteSpace(SourceText)) return null;

            var punctuation = SourceText.Where(char.IsPunctuation).Distinct().ToArray();
            return SourceText.Split().Select(x => x.Trim(punctuation)).ToList();
        }

        /// <summary> Метод возвращает предложения </summary>
        /// <returns></returns>
        public List<string> GetSuggestions()
        {
            // Считаю что предложения заканчиваются на ". ","! ","? "
            if (string.IsNullOrEmpty(SourceText) || string.IsNullOrWhiteSpace(SourceText)) return null;
            return Regex.Split(SourceText, @"(?<=[\.!\?])\s+").ToList();
        }

    }
}
