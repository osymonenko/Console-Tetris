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
        static Figure Fig = new O();
        static object locker = new object();
        //for better view

        static void Main(string[] args)
        {
            Game.SetFigStart();
            //Game.DrawFig(Fig);
            Game.UpdateField();
            Thread T1 = new Thread(SleepMoveDown);
            Thread T2 = new Thread(UpdateGameField);
            T1.Name = "THREAD: Drop every 1 sec";
            T2.Name = "THREAD: Readkey";
            T1.Start();
            T2.Start();
        }

        public static void SleepMoveDown()
        {
            while (true)
            {
                Thread.Sleep(10);
                lock (locker)
                {
                    Game.MoveFigDown(Game.NoObstructionsCheck('S'));
                    DateTime now = DateTime.Now;
                    Console.WriteLine("\n{0:T}", now);
                }
            }
        }

        public static void UpdateGameField()
        {
            while (true)
            {
                Console.WriteLine("THREAD READKEY ENTERED");
                var input = Console.ReadKey(true);

                lock (locker)
                {
                    switch (input.Key)
                    {
                        case ConsoleKey.A:
                            Console.WriteLine("'A' KEY DETECTED");
                            lock (locker)
                            {
                                Game.MoveFigLeft(Game.NoObstructionsCheck('A'));
                                break;
                            }
                        case ConsoleKey.D:
                            Console.WriteLine("'D' KEY DETECTED");
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
                        case ConsoleKey.R:
                            lock (locker)
                            {
                                Game.RotateFig();
                                break;
                            }

                        default:
                            Console.WriteLine("...NOTHING...");
                            break;
                    }
                }
            }
        }
    }
}
