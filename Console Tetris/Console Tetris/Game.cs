﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Console_tetris
{
    class Game
    {
        public int[,] Field = new int[22, 10];
        public Figure Fig = new O();
        public int boomNum;

        public void SetFigStart()
        {
            Fig.IsVertical = false;
            Fig.Y = 0;
            Fig.X = Field.GetLength(1) / 2 - 1;
        }

        public void DrawFig(Figure figure)
        {
            for (int yc = 0; yc < Fig.Geometry.GetLength(0); yc++)
            {
                for (int xc = 0; xc < Fig.Geometry.GetLength(1); xc++)
                {
                    Field[Fig.Y + yc, Fig.X + xc] = Fig.Geometry[yc, xc];
                }
            }
        }

        public void UpdateField()
        {
            Console.Clear();
            for (int yc = 0; yc < Field.GetLength(0); yc++)
            {
                for (int xc = 0; xc < Field.GetLength(1); xc++)
                {
                    Console.Write(Field[yc, xc]);
                }
                Console.WriteLine();
            }
        }

        //fixed
        public void MoveFigDown(bool NoObstructions)
        {
            if (NoObstructions)
            {
                if (!Fig.IsVertical)
                {
                    for (int xc = 0; xc < Fig.Geometry.GetLength(1); xc++)
                    {
                        for (int yc = 0; yc < Fig.Geometry.GetLength(0); yc++)
                        {
                            //if geometry got "1". Also "0" checking should be inside "1", because only figure should be moved (other close geometry should stay untouchable)
                            if (Fig.Geometry[yc, xc] == 1)
                            {
                                Field[Fig.Y + yc + 1, Fig.X + xc] = 1;
                                //also if it was 0 yc...
                                if (yc == 0)
                                {
                                    Field[Fig.Y + yc, Fig.X + xc] = 0;
                                }
                                //save from OutOfBounds exception
                                if (yc != 0)
                                {
                                    if (Fig.Geometry[yc - 1, xc] == 0)
                                    {
                                        Field[Fig.Y + yc, Fig.X + xc] = 0;
                                    }
                                }
                            }
                        }
                    }
                }
                Fig.Y++;
                UpdateField();
            }
            else
            {
                CheckFullLines();
                SetFigStart();
                DrawFig(Fig);
            }
        }

        public void CheckFullLines()
        {
            //maximum amount of rows to destroy can be 4 (after placing "I" figure)
            int[] rowsToDestroy = new int[4];
            //to avoid understanding that rowsToDestroy[0] should destroy "-1" row (int array got everywhere zeros after creating)
            int z = 0;
            rowsToDestroy[z] = 999;

            for (int row = 0; row < Field.GetLength(0); row++)
            {
                for (int column = 0; column < Field.GetLength(1); column++)
                {
                    if (Field[row, column] == 0) break;
                    if (Field[row, column] == 1 & column == Field.GetLength(1) - 1) rowsToDestroy[z++] = row;
                    else continue;
                }
            }
            if (rowsToDestroy[0] != 999)
            {
                for (int rowNum = 0; rowNum < rowsToDestroy.GetLength(0); rowNum++)
                {
                    if (rowsToDestroy[rowNum] != 0)
                    {
                        //destroying the rows
                        for (int column = 0; column < Field.GetLength(1); column++)
                        {
                            Console.WriteLine("MAKING CELL " + Field[rowsToDestroy[rowNum], column] + "= 0");
                            Thread.Sleep(100);
                            Field[rowsToDestroy[rowNum], column] = 0;
                        }
                        UpdateField();
                        Console.WriteLine("\nBOOM! #" + ++boomNum);
                        Thread.Sleep(1000);
                    }
                }
                //moving rows
                for (int rowNum = 0; rowNum < rowsToDestroy.GetLength(0); rowNum++)
                {
                    for (int row = 0; row < Field.GetLength(0); row++)
                    {
                        if (row == rowsToDestroy[rowNum])
                        {
                            for (int rowRebuild = row; rowRebuild != 0; rowRebuild--)
                            {
                                for (int column = 0; column < Field.GetLength(1); column++)
                                {
                                    if (row == 0)
                                    {
                                        Field[rowRebuild, column] = 0;
                                    }
                                    else
                                    {
                                        Field[rowRebuild, column] = Field[rowRebuild - 1, column];
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        //fixing.................................................................
        public void MoveFigLeft(bool NoObstructions)
        {
            if (NoObstructions)
            {
                if (!Fig.IsVertical)
                {
                    for (int xc = 0; xc < Fig.Geometry.GetLength(1); xc++)
                    {
                        for (int yc = 0; yc < Fig.Geometry.GetLength(0); yc++)
                        {
                            //if geometry got "1"
                            if (Fig.Geometry[yc, xc] == 1)
                            {
                                Field[Fig.Y + yc, Fig.X + xc - 1] = 1;
                                //also if it was 0 yc... TO NOT AFFECT ON OTHER EMPTY field zeros
                                if (xc == Fig.Geometry.GetLength(1) - 1 | (xc < Fig.Geometry.GetLength(1) - 1 && Fig.Geometry[yc, xc + 1] == 0))
                                {
                                    Field[Fig.Y + yc, Fig.X + xc] = 0;
                                }
                            }
                        }
                    }
                }
                Fig.X--;
                UpdateField();
            }
        }

        public void MoveFigRight(bool NoObstructions)
        {
            if (NoObstructions)
            {
                if (!Fig.IsVertical)
                {
                    for (int xc = 0; xc < Fig.Geometry.GetLength(1); xc++)
                    {
                        for (int yc = 0; yc < Fig.Geometry.GetLength(0); yc++)
                        {
                            //if geometry got "1"
                            if (Fig.Geometry[yc, xc] == 1)
                            {
                                Field[Fig.Y + yc, Fig.X + xc + 1] = 1;
                                //also if it was 0 yc... TO NOT AFFECT ON OTHER EMPTY field zeros
                                if (xc == 0 | (xc > 0 && Fig.Geometry[yc, xc - 1] == 0))
                                {
                                    Field[Fig.Y + yc, Fig.X + xc] = 0;
                                }
                            }
                        }
                    }
                }
                Fig.X++;
                UpdateField();
            }
        }

        public bool NoObstructionsCheck(char KeyPressed)
        {
            int counter = 2;
            switch (KeyPressed)
            {
                case 'A':
                    {
                        //fixed for O object
                        if (Fig.X != 0)
                        {
                            if (!Fig.IsVertical && Fig.X != 0)
                            {
                                for (int index = 0; index < Fig.CheckCellsLeftHorizontal.GetLength(0);)
                                {
                                    if (Field[Fig.Y + Fig.CheckCellsLeftHorizontal[index], Fig.X + Fig.CheckCellsLeftHorizontal[index + 1]] != 1)
                                    {
                                        Field[Fig.Y + Fig.CheckCellsLeftHorizontal[index], Fig.X + Fig.CheckCellsLeftHorizontal[index + 1]] = 2;
                                    }
                                    UpdateField();
                                    Thread.Sleep(10);
                                    if (Field[Fig.Y + Fig.CheckCellsLeftHorizontal[index], Fig.X + Fig.CheckCellsLeftHorizontal[index + 1]] == 1)
                                    {
                                        return false;
                                    }
                                    index = index + 2;
                                }
                                return true;
                            }
                            if (Fig.IsVertical && Fig.X != 0)
                            {

                            }
                        }
                        return false;
                    }
                case 'D':
                    {
                        //fixed for O object
                        if (Fig.X != Field.GetLength(1) - 1)
                        {
                            if (!Fig.IsVertical && Fig.X != Field.GetLength(1) - Fig.LenghtHorizontal)
                            {
                                for (int index = 0; index < Fig.CheckCellsRightHorizontal.GetLength(0);)
                                {
                                    if (Field[Fig.Y + Fig.CheckCellsRightHorizontal[index], Fig.X + Fig.CheckCellsRightHorizontal[index + 1]] != 1)
                                    {
                                        Field[Fig.Y + Fig.CheckCellsRightHorizontal[index], Fig.X + Fig.CheckCellsRightHorizontal[index + 1]] = counter++;
                                    }
                                    UpdateField();
                                    Thread.Sleep(10);
                                    if (Field[Fig.Y + Fig.CheckCellsRightHorizontal[index], Fig.X + Fig.CheckCellsRightHorizontal[index + 1]] == 1)
                                    {
                                        return false;
                                    }
                                    index = index + 2;
                                }
                                return true;
                            }
                            if (Fig.IsVertical && Fig.X != 0)
                            {

                            }
                        }
                        return false;
                    }
                case 'S':
                    {
                        //fixed for O object
                        if (!Fig.IsVertical && Fig.Y != Field.GetLength(0) - Fig.LengthVertical)
                        {
                            for (int index = 0; index < Fig.CheckCellsLeftHorizontal.GetLength(0);)
                            {
                                if (Field[Fig.Y + Fig.CheckCellsBottomHorizontal[index], Fig.X + Fig.CheckCellsBottomHorizontal[index + 1]] != 1)
                                {
                                    Field[Fig.Y + Fig.CheckCellsBottomHorizontal[index], Fig.X + Fig.CheckCellsBottomHorizontal[index + 1]] = counter++;
                                }
                                UpdateField();
                                Thread.Sleep(10);
                                if (Field[Fig.Y + Fig.CheckCellsBottomHorizontal[index], Fig.X + Fig.CheckCellsBottomHorizontal[index + 1]] == 1)
                                {
                                    return false;
                                }
                                index = index + 2;
                            }
                            return true;
                        }
                        if (Fig.IsVertical && Fig.Y != Field.GetLength(0) - 1)
                        {

                        }
                        return false;
                    }
                case 'R':
                    {
                        return false;
                    }
                default:
                    {
                        Console.WriteLine("just moving down");
                        return false;
                    }
            }
        }

        public void RotateFig()
        {
            //Fig.Rotate(Field);
        }


        /*use it later
* 
Figure Generate()
{
   Figure fig;
   Random num = new Random();
   int x = num.Next(7);
   switch (x)
   {
       case 1: 
           fig = new I();
           break;
       case 2:
           fig = new L();
           break;
       case 3:
           fig = new J();
           break;
       case 4:
           fig = new O();
           break;
       case 5:
           fig = new S();
           break;
       case 6:
           fig = new Z();
           break;
       default:
           fig = new T();
           break;
   }
   return fig;
}*/

    }
}