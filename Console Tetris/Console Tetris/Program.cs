using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Threading;

namespace Console_tetris
{
    class Program
    {
        static Game Game = new Game();
        static object locker = new object();
        //for better view

        static void Main(string[] args)
        {
            /*Game preparation*/
            Console.WriteLine("Player, enter your name below");
            Game.playerName = Console.ReadLine();

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
                    Thread.Sleep(250);
                    Game.MoveFigDown(Game.NoObstructionsCheck('S'));
                }
            }
            if (Game.gameOver)
            {
                Console.WriteLine("Press any key to start again");
                Console.ReadKey();
                Game.ClearField();
                Game.UpdateField();
                Game.gameOver = false;
            }
        }

        public static void UpdateGameField()
        {
            while (true)
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
