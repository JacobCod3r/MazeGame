using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Data;

namespace MazeGame
{
    internal class Program
    {
        static bool isBlinking = true;
        static void BlinkText()
        {
            while (isBlinking)
            {
                Console.Clear(); 
                Console.WriteLine(" ____   ____      ___      __________  _______\r\n|    \\_/    |    /   \\    |______   _||   ____|\r\n|  |\\   /|  |   /  _  \\        _/ _/  |  |____\r\n|  | \\_/ |  |  /  /_\\  \\     _/ _/    |   ____|\r\n|  |     |  | /   ___   \\  _/  /_____ |  |____\r\n|__|     |__|/___/   \\___\\|__________||_______|  \r\n  _______        ___      ____   ____  _______\r\n /  _____\\      /   \\    |    \\_/    ||   ____|\r\n|  /  _____    /  _  \\   |  |\\   /|  ||  |____\r\n|  | |_   _|  /  /_\\  \\  |  | \\_/ |  ||   ____|\r\n \\  \\_/  /   /   ___   \\ |  |     |  ||  |____\r\n  \\_____/   /___/   \\___\\|__|     |__||_______|  \r\n\r\n              Press ENTER to start!");

                Thread.Sleep(500);

                Console.Clear(); 
                Console.WriteLine(" ____   ____      ___      __________  _______\r\n|    \\_/    |    /   \\    |______   _||   ____|\r\n|  |\\   /|  |   /  _  \\        _/ _/  |  |____\r\n|  | \\_/ |  |  /  /_\\  \\     _/ _/    |   ____|\r\n|  |     |  | /   ___   \\  _/  /_____ |  |____\r\n|__|     |__|/___/   \\___\\|__________||_______|  \r\n  _______        ___      ____   ____  _______\r\n /  _____\\      /   \\    |    \\_/    ||   ____|\r\n|  /  _____    /  _  \\   |  |\\   /|  ||  |____\r\n|  | |_   _|  /  /_\\  \\  |  | \\_/ |  ||   ____|\r\n \\  \\_/  /   /   ___   \\ |  |     |  ||  |____\r\n  \\_____/   /___/   \\___\\|__|     |__||_______|  \r\n\r\n");

                Thread.Sleep(500);
            }
        }

        static int[] coords = { 0, 0 };
        static char[,] tablicaDwuWymiarowa;
        static bool changeLevel = false;

        static void move(int direction)
        {
            int rows = tablicaDwuWymiarowa.GetLength(0);
            int cols = tablicaDwuWymiarowa.GetLength(1);
            int[] tempcoords = { coords[0], coords[1] };
            if (direction == 0)
            {
                tempcoords[0]--;
            }
            else if (direction == 1)
            {
                tempcoords[0]++;
            }
            else if (direction == 2)
            {
                tempcoords[1]--;
            }
            else if (direction == 3)
            {
                tempcoords[1]++;
            }
            try
            {
                if (tablicaDwuWymiarowa[tempcoords[0], tempcoords[1]] == ' ')
                {
                    tablicaDwuWymiarowa[tempcoords[0], tempcoords[1]] = 'X';
                }
                else
                {
                    tablicaDwuWymiarowa[coords[0], coords[1]] = 'X';
                }
            }
            catch
            {
                changeLevel = true;
            }
        }


        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Thread thread = new Thread(BlinkText);
            thread.Start();
            Console.ReadLine();

            isBlinking = false;
            Thread.Sleep(500);
            Console.Clear();
            Console.WriteLine(" ____   ____      ___      __________  _______\r\n|    \\_/    |    /   \\    |______   _||   ____|\r\n|  |\\   /|  |   /  _  \\        _/ _/  |  |____\r\n|  | \\_/ |  |  /  /_\\  \\     _/ _/    |   ____|\r\n|  |     |  | /   ___   \\  _/  /_____ |  |____\r\n|__|     |__|/___/   \\___\\|__________||_______|  \r\n  _______        ___      ____   ____  _______\r\n /  _____\\      /   \\    |    \\_/    ||   ____|\r\n|  /  _____    /  _  \\   |  |\\   /|  ||  |____\r\n|  | |_   _|  /  /_\\  \\  |  | \\_/ |  ||   ____|\r\n \\  \\_/  /   /   ___   \\ |  |     |  ||  |____\r\n  \\_____/   /___/   \\___\\|__|     |__||_______|  \r\n\r\n Press right arrow or D to open in-game levels\r\n  Press left arrow or A to open custom levels");

            bool choose = true;
            bool checkForMaps = false;

            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    choose = true;
                    break;
                }
                else if (keyInfo.Key == ConsoleKey.D)
                {
                    choose = true;
                    break;
                }
                else if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    choose = false;
                    break;
                }
                else if (keyInfo.Key == ConsoleKey.A)
                {
                    choose = false;
                    break;
                }
            }

            Console.Clear();
            String folderName = "";

            int currentLevel = 0;
            if (choose)
            {
                folderName = "BaseGameLevels";
            }

            else
            {
                folderName = "CustomLevels";
            }

            while (true)
            {
                changeLevel = false;
                currentLevel++;
                try
                {
                    using (var reader = new StreamReader(folderName + "/level" + currentLevel + ".txt"))
                    {
                        checkForMaps = true;
                        var lines = new List<string>();

                        while (!reader.EndOfStream)
                        {
                            lines.Add(reader.ReadLine());
                        }

                        int r = lines.Count;
                        int c = lines[0].Length;

                        tablicaDwuWymiarowa = new char[r, c];

                        for (int i = 0; i < r; i++)
                        {
                            for (int j = 0; j < lines[i].Length; j++)
                            {
                                tablicaDwuWymiarowa[i, j] = lines[i][j];
                            }
                        }
                    }

                    int rows = tablicaDwuWymiarowa.GetLength(0);
                    int cols = tablicaDwuWymiarowa.GetLength(1);
                    while (!changeLevel)
                    {
                        for (int i = 0; i < rows; i++)
                        {
                            for (int j = 0; j < cols; j++)
                            {
                                Console.Write(tablicaDwuWymiarowa[i, j]);
                                if (tablicaDwuWymiarowa[i, j] == 'X')
                                {
                                    coords[0] = i;
                                    coords[1] = j;
                                    tablicaDwuWymiarowa[i, j] = ' ';
                                }
                            }
                            Console.WriteLine();
                        }

                        while (true)
                        {
                            ConsoleKeyInfo keyInfo = Console.ReadKey();
                            if (keyInfo.Key == ConsoleKey.UpArrow)
                            {
                                move(0);
                                break;
                            }
                            if (keyInfo.Key == ConsoleKey.W)
                            {
                                move(0);
                                break;
                            }
                            else if (keyInfo.Key == ConsoleKey.DownArrow)
                            {
                                move(1);
                                break;
                            }
                            if (keyInfo.Key == ConsoleKey.S)
                            {
                                move(1);
                                break;
                            }
                            else if (keyInfo.Key == ConsoleKey.LeftArrow)
                            {
                                move(2);
                                break;
                            }
                            else if (keyInfo.Key == ConsoleKey.A)
                            {
                                move(2);
                                break;
                            }
                            else if (keyInfo.Key == ConsoleKey.RightArrow)
                            {
                                move(3);
                                break;
                            }
                            else if (keyInfo.Key == ConsoleKey.D)
                            {
                                move(3);
                                break;
                            }
                        }

                        Console.Clear();

                    }
                }
                catch
                {
                    Console.Clear();
                    if (checkForMaps)
                    {
                        if (choose)
                        {
                            Console.WriteLine("Congratulations!\nYou have completed all the main levels!\nPress ENTER to leave.");
                        }
                        else
                        {
                            Console.WriteLine("You have completed all custom levels!\nPress ENTER to leave.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Couldn't find any levels here! Maybe create your own?\nCheck MazeGame\\bin\\Debug\\CustomLevels\nPress ENTER to leave.");
                    }
                    Console.ReadLine();
                    break;
                }
            }
        }
    }
}
