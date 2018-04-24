using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace HangMan
{
    class Program
    {
        enum Difficulty
        {
            Babby = 0,
            Easy,
            Normal,
            Hard,
            Brutal,
        };

        static string[] words_babby =
        {
            "cat",
            "dog",
            "fish",
            "ball",
            "duck",
            "toys",
            "baby",
            "doll",
            "park",
            "sun",
            "sky",
            "tree",
            "grass",
        };

        static string[] words_easy =
        {
            "silly",
            "border",
            "advice",
            "candy",
            "beef",
            "monkey",
            "image",
            "garage",
            "Library",
            "School",
            "Hospital",
            "Playground",
            "The World",
            "Rainbow",
        };
        static string[] words_normal =
        {
            "Cabbage",
            "Deep-fried",
            "smoky",
            "filling",
            "flaked",
            "fried",
            "arrangement",
            "attempt",
            "possibly",
            "Computer",
            "Keyboard",
            "Automobile",
            "potatoes",
            "snack",
            "soaked",
        };
        static string[] words_hard =
        {
            "Wildebeest",
            "Phlegm",
            "Zealous",
            "Klutz",
            "Jazzy",
            "Haphazard",
            "Dwarves",
            "Croquet",
            "Awkward",
            "Squawk",
            "Zippy",
            "Zombie",
            "Yacht",
            "Sphinx",
            "Oxygen",
            "Memento",
            "Kayak",
            "Hyphen",
            "Fizz",
            "Jazz",
            "Buzz",
            "Fuzz",
        };

        static string Guesses;
        static string characters = "\"!#£@$%&/{([)]=}?+´`|'*<>\\,;.:-_½§ 1234567890";

        static string theWord, wordGuess;

        static string[] gallow0 =
        {
            "   _____ ",
            "   |   | ",
            "   |     ",
            "   |     ",
            "   |     ",
            "   |     ",
            " /'''\\  ",
            "/     \\ ",
        };

        static string[] gallow1 =
        {
            "   _____ ",
            "   |   | ",
            "   |   O ",
            "   |     ",
            "   |     ",
            "   |     ",
            " /'''\\  ",
            "/     \\ ",
        };

        static string[] gallow2 =
        {
            "   _____ ",
            "   |   | ",
            "   |   O ",
            "   |   | ",
            "   |     ",
            "   |     ",
            " /'''\\  ",
            "/     \\ ",
        };

        static string[] gallow3 =
        {
            "   _____ ",
            "   |   | ",
            "   |   O ",
            "   |  /| ",
            "   |     ",
            "   |     ",
            " /'''\\  ",
            "/     \\ ",
        };

        static string[] gallow4 =
        {
            "   _____ ",
            "   |   | ",
            "   |   O ",
            "   |  /|\\",
            "   |     ",
            "   |     ",
            " /'''\\  ",
            "/     \\ ",
        };

        static string[] gallow5 =
        {
            "   _____ ",
            "   |   | ",
            "   |   O ",
            "   |  /|\\",
            "   |  /  ",
            "   |     ",
            " /'''\\  ",
            "/     \\ ",
        };

        static string[] gallow6 =
        {
            "   _____ ",
            "   |   | ",
            "   |   O ",
            "   |  /|\\",
            "   |  / \\",
            "   |     ",
            " /'''\\  ",
            "/     \\ ",
        };

        static Difficulty difficulty;
        static Random rnd;

        static void Main(string[] args)
        {
            List<string[]> gallows = new List<string[]>
            {
                gallow0,
                gallow1,
                gallow2,
                gallow3,
                gallow4,
                gallow5,
                gallow6
            };

            rnd = new Random(DateTime.Now.Millisecond);

            bool gameOver = false;

            while (!gameOver)
            {
                int lives = 6;
                int selectedGallow = 0;
                List<string> screen = new List<string>();
                string guess = "";
                Guesses = "";

                Console.Clear();
                NewGame();

                wordGuess = WordCipher(theWord.ToUpper());

                while (true)
                {
                    screen.Clear();
                    screen.AddRange(gallows[selectedGallow]);
                    screen.Add("");
                    screen.Add(wordGuess);
                    Console.Clear();
                    Draw(screen);

                    if (wordGuess.ToUpper() == theWord.ToUpper())
                        break;

                    guess = Console.ReadLine();

                    if (guess.Length == 1)
                    {
                        if (Guesses.Contains(guess.ToUpper()))
                        {
                            Console.WriteLine("You have already tried {0}!", guess.ToUpper());
                            Console.Beep(250, 1000);
                            Thread.Sleep(1000);
                        }
                        else if (CheckGuess(guess))
                        {
                            wordGuess = WordDecipher(guess.ToUpper());
                            Console.WriteLine("Yay!");
                            Console.Beep(300, 250);
                            Console.Beep(400, 250);
                            Console.Beep(500, 250);
                            Console.Beep(600, 250);
                            Thread.Sleep(1000);
                            Guesses += guess.ToUpper() + " ";
                        }
                        else
                        {
                            if (lives == 0)
                                break;

                            lives--;
                            selectedGallow++;
                            Console.WriteLine("The word does not contain the letter {0}!", guess);
                            Console.Beep(300, 250);
                            Console.Beep(200, 750);
                            Thread.Sleep(1000);
                            Guesses += guess.ToUpper() + " ";
                        }
                    }
                    else if (guess.Length < 1)
                    {
                        Console.WriteLine("Type a letter first!");
                        Console.Beep(250, 1000);
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        if (guess.ToLower() == "exit" || guess.ToLower() == "qq" || guess.ToLower() == "quit")
                            break;
                        Console.WriteLine("{0} is not valid. Try again!", guess);
                        Console.Beep(250, 1000);
                        Thread.Sleep(1000);
                    }
                }

                Console.WriteLine("\n\nGAME OVER\n");
                if (lives > 0)
                {
                    if (wordGuess.ToUpper() == theWord.ToUpper())
                    {
                        Console.WriteLine("A WINNER IS YOU!");
                        Console.Beep(830, 100);
                        Console.Beep(830, 900);
                    }
                }
                else
                {
                    Console.WriteLine("YOU LOSE!\n\nThe word was \"{0}\"", theWord.ToUpper());
                    Console.Beep(250, 100);
                    Console.Beep(150, 900);
                }

                Console.Write("\n[R]etry or [Q]uit? ");
                switch(Console.ReadLine().ToLower())
                {
                    case "r":
                        continue;
                    case "q":
                        Environment.Exit(0);
                        break;
                }
            }
        }

        static void NewGame()
        {
            bool ready = false;
            while(!ready)
            {
                Console.WriteLine("    __  __                                          ____ ");
                Console.WriteLine("   / / / /___  _____ ____ _____ ___  ____ _____     |  | ");
                Console.WriteLine("  / /_/ / __ `/ __ \\/ __ `/ __ `__ \\/ __ `/ __ \\    |  O ");
                Console.WriteLine(" / __  / /_/ / / / / /_/ / / / / / / /_/ / / / /    |    ");
                Console.WriteLine("/_/ /_/\\__,_/_/ /_/\\__, /_/ /_/ /_/\\__,_/_/ /_/   /'''\\ ");
                Console.WriteLine("           by b   /____/  ©2018                  /     \\");
                Console.WriteLine("");
                Console.WriteLine(" NEW GAME\n\n   1) Babby's first game\n   2) Easy game\n   3) Normal game\n   4) Hard game\n");
                Console.Write("\n Select difficulty: ");
                if(Int32.TryParse(Console.ReadLine(), out int diff) && diff > 0 && diff < 5)
                {
                    difficulty = (Difficulty)diff - 1;
                    switch(diff - 1)
                    {
                        case 0:
                            {
                                theWord = words_babby[rnd.Next(0, words_babby.Length)].ToUpper();
                                break;
                            }
                        case 1:
                            {
                                theWord = words_easy[rnd.Next(0, words_easy.Length)].ToUpper();
                                break;
                            }
                        case 2:
                            {
                                theWord = words_normal[rnd.Next(0, words_normal.Length)].ToUpper();
                                break;
                            }
                        case 3:
                            {
                                theWord = words_hard[rnd.Next(0, words_hard.Length)].ToUpper();
                                break;
                            }
                        default:
                            {
                                theWord = words_normal[rnd.Next(0, words_normal.Length)].ToUpper();
                                break;
                            }
                    }
                    ready = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("That is not a valid difficulty. Try again!\n");
                }
            }
        }

        static string WordCipher(string word)
        {
            string retval = "";
            foreach(char c in word)
            {
                if (characters.Contains(c))
                    retval += c;
                else
                    retval += "_";
            }
            return retval;
        }

        static string WordDecipher(string guess)
        {
            string retval = "";
            for(int i = 0; i < theWord.Length; i++)
            {
                if(theWord[i] == guess[0])
                    retval += guess;

                else
                    retval += wordGuess[i];

            }
            return retval;
        }

        static bool CheckGuess(string guess)
        {
            if(theWord.ToUpper().Contains(guess.ToUpper()))
                return true;

            return false;
        }

        static void Draw(List<string> input)
        {
            Console.WriteLine("Letters guessed: {0}", Guesses.ToUpper());
            foreach (string s in input)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 - s.Length / 2, Console.CursorTop);
                Console.WriteLine(s);
            }
            Console.Write("Guess a letter: ");
        }
    }
}
