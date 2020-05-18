using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Ordgåte
{
    class Program
    {
        private static readonly Random Random = new Random();
        static void Main(string[] args)
        {
            // velge tilfeldig ord ( hoppe over tillfeldig antall ors.)- eks abonnement
            // Lete videre etter ord som begynner på siste del av dette ordet. - mental
            var words = GetWords();
            var wordCount = 200;
            while (wordCount > 0)
            {
                var hasFoundMatch = GetWordRiddle(words);
                if (hasFoundMatch) wordCount--;
            }

        }

        private static bool GetWordRiddle(string[] words)
        {
            var randomWordIndex = Random.Next(words.Length);
            var selectedWord = words[randomWordIndex];
            Console.Write(selectedWord + " - ");
            foreach (var word in words)
            {
                if (!IsLastPartOfFirstWordEqualToFirstPartOfSecondWord(selectedWord, word)) continue;
                Console.WriteLine(word);
                return true;
            }
            Console.WriteLine(" Fant ingen match!");
            return false;
        }

        private static bool IsLastPartOfFirstWordEqualToFirstPartOfSecondWord(string word1, string word2)
        {
            return
                   IsLastPartOfFirstWordEqualToFirstPartOfSecondWord(3, word1, word2)
                   || IsLastPartOfFirstWordEqualToFirstPartOfSecondWord(4, word1, word2)
                   || IsLastPartOfFirstWordEqualToFirstPartOfSecondWord(5, word1, word2);

        }
        private static bool IsLastPartOfFirstWordEqualToFirstPartOfSecondWord(int commonLength, string word1, string word2)
        {
            var lastPartOfFirstWord = word1.Substring(word1.Length - commonLength, commonLength);

            var firstPartOfSecondWord = word2.Substring(0, commonLength);
            return lastPartOfFirstWord == firstPartOfSecondWord;
        }

        static string[] GetWords()
        {
            var lastWord = string.Empty;
            var filePath = @"C:\Users\TG90\OneDrive\Documents\Modul 3\WordRiddle\WordRiddlee\OrdListe.txt";
            var wordList = new List<string>();
            foreach (var line in File.ReadLines(filePath, Encoding.UTF8))
            {
                var parts = line.Split('\t');
                var word = parts[1];
                if (word != lastWord
                    && word.Length > 6
                    && word.Length < 11
                    && !word.Contains("-"))
                {
                    wordList.Add(word);//yield return word; //returnerer en og en
                }
                lastWord = word;
            }

            return wordList.ToArray();
        }

    }
}
