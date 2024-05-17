// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            // Huvudloopen för spelet
            bool playAgain = true;
            while (playAgain)
            {
                PlayGame();

                // Fråga spelaren om de vill spela igen
                Console.Write("Do you want to play again? (y/n): ");
                string response = Console.ReadLine().ToLower();

                // Kontrollera spelarens svar
                if (response != "y")
                {
                    playAgain = false;
                }
                Console.WriteLine();
            }
        }

        static void PlayGame()
        {
            // Välkomstmeddelande till spelaren
            Console.WriteLine("Welcome to Hangman!");
            Console.WriteLine("===================");

            // Lista med ord att gissa
            List<string> listwords = new List<string>
            {
                "sheep", "goat", "computer", "america", "watermelon",
                "icecream", "jasmine", "pineapple", "orange", "mango", "lexicon"
            };

            // Skapa en ny instans av Random för att slumpa ord
            Random randGen = new Random();
            string mysteryWord = listwords[randGen.Next(listwords.Count)];

            // Skapa en array av '*' som representerar de ogissade bokstäverna
            char[] guess = new string('*', mysteryWord.Length).ToCharArray();

            // Ställ in antal försök baserat på ordets längd + 5 extra försök
            int attempts = mysteryWord.Length + 5;
            bool isWordGuessed = false;

            // Huvudloopen för spelet
            while (attempts > 0 && !isWordGuessed)
            {
                // Visa hur många försök som återstår
                Console.WriteLine($"You have {attempts} attempts left.");
                // Visa det aktuella gissade ordet
                Console.WriteLine("Current guess: " + new string(guess));
                Console.Write("Please enter your guess: ");

                // Läs användarens inmatning
                string input = Console.ReadLine();

                // Validera att inmatningen är en giltig bokstav
                if (string.IsNullOrWhiteSpace(input) || input.Length != 1 || !char.IsLetter(input[0]))
                {
                    Console.WriteLine("Invalid input. Please enter a single letter.");
                    continue;
                }

                // Omvandla inmatningen till en liten bokstav
                char playerGuess = char.ToLower(input[0]);
                bool correctGuess = false;

                // Kontrollera om gissningen finns i mysteryWord
                for (int j = 0; j < mysteryWord.Length; j++)
                {
                    if (playerGuess == mysteryWord[j])
                    {
                        guess[j] = playerGuess; // Uppdatera gissningsarrayen med rätt bokstav
                        correctGuess = true;
                    }
                }

                // Minska antalet försök om gissningen var fel
                if (!correctGuess)
                {
                    attempts--;
                }

                // Kontrollera om hela ordet har gissats
                isWordGuessed = guess.All(c => c != '*');

                Console.WriteLine();
            }

            // Avslutningsmeddelande beroende på om spelaren vann eller förlorade
            if (isWordGuessed)
            {
                Console.WriteLine("Congratulations! You've guessed the word: " + mysteryWord);
            }
            else
            {
                Console.WriteLine("Sorry, you've run out of attempts. The word was: " + mysteryWord);
            }
        }
    }
}