using System;
using System.IO;
using System.Threading;

namespace Console_tetris
{
    class Program
    {
        static Game Game = new Game();
        static object locker = new object();

        static void Main(string[] args)
        {
            /*Game preparation*/
            Console.WriteLine("Player, enter your name below");
            Game.playerName = Console.ReadLine();
            string path = @"\console tetris highscore.txt";
            if (!File.Exists(path))
            {
                StreamWriter sw = File.CreateText(path);
                {
                    sw.WriteLine("test 99");
                    sw.Close();
                }
            }
            Game.readHighScore();
            Game.SetFigStart();
            Game.DrawFig();
            Thread T1 = new Thread(SleepMoveDown);
            Thread T2 = new Thread(UpdateGameField);
            T1.Start();
            T2.Start();
        }

        public static void SleepMoveDown()
        {
            while (!Game.gameOver)
            {
                lock (locker)
                {
                    Thread.Sleep(100);
                    Game.MoveFigDown(Game.NoObstructionsCheck('S'));
                    if (Game.gameOver)
                    {
                        Console.WriteLine("\nEnter next player name and press any key to start again");
                        Game.readHighScore();
                        Game.gameOver = false;
                        Game.playerName = Console.ReadLine();
                        Game.score = 0;
                        Game.lines = 0;
                        Game.DrawFig();
                        Game.ClearField();
                    }
                }
            }
        }

        public static void UpdateGameField()
        {
            while (!Game.gameOver)
            {
                var input = Console.ReadKey(true);

                lock (locker)
                {
                    switch (input.Key)
                    {
                        case ConsoleKey.A:
                            lock (locker)
                            {
                                Game.MoveFigLeft(Game.NoObstructionsCheck('A'));
                                break;
                            }
                        case ConsoleKey.D:
                            lock (locker)
                            {
                                Game.MoveFigRight(Game.NoObstructionsCheck('D'));
                                break;
                            }
                        case ConsoleKey.S:
                            lock (locker)
                            {
                                Game.MoveFigDown(Game.NoObstructionsCheck('S'));
                                break;
                            }
                        default:
                            break;
                    }
                }
            }
        }
    }
}
