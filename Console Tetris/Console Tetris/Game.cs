using System;
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
        //public Figure Fig;
        public static int boomNum;
        public int highScore = 999;
        public string highScorePlayer = "TestHighScorePlayer";
        public string playerName = "TestPlayerName";
        public int score;
        public int lines;

        public void SetFigStart()
        {
            //Fig = Generate();
            Fig.IsVertical = true;
            Fig.Y = 0;
            Fig.X = Field.GetLength(1) / 2 - 1;
        }

        public void DrawFig(Figure figure)
        {
            if (!Fig.IsVertical)
            {
                for (int figRows = 0; figRows < Fig.Geometry.GetLength(0); figRows++)
                {
                    for (int figColumns = 0; figColumns < Fig.Geometry.GetLength(1); figColumns++)
                    {
                        Field[Fig.Y + figRows, Fig.X + figColumns] = Fig.Geometry[figRows, figColumns];
                    }
                }
            }
            else
            {
                /*vynos mozga*/
                for (int figRows = 0; figRows < Fig.Geometry.GetLength(0); figRows++)
                {
                    for (int figColumns = 0; figColumns < Fig.Geometry.GetLength(1); figColumns++)
                    {
                        Field[Fig.Y + figColumns, Fig.X + 1 - figRows] = Fig.Geometry[figRows, figColumns];
                        UpdateField();
                        Thread.Sleep(10);
                    }
                }
            }
            UpdateField();
        }

        public void UpdateField()
        {
            Console.Clear();
            for (int yc = 0; yc < Field.GetLength(0); yc++)
            {
                for (int xc = 0; xc < Field.GetLength(1); xc++)
                {
                    Console.Write(Field[yc, xc]);

                    /*highscore print (1 from 2)*/
                    if (yc == 1 && xc == Field.GetLength(1) - 1)
                    {
                        Console.Write("\t\t HIGHSCORE:");
                    }
                    /*highscore print (2 from 2)*/
                    if (yc == 2 && xc == Field.GetLength(1) - 1)
                    {
                        Console.Write("\t -= " + highScorePlayer + " with " + highScore + " points =-");
                    }
                    /*playerinfo print (1 from 4)*/
                    if (yc == 4 && xc == Field.GetLength(1) - 1)
                    {
                        Console.Write("\t    CURRENT PLAYER: ");
                    }
                    /*playerinfo print (2 from 4)*/
                    if (yc == 5 && xc == Field.GetLength(1) - 1)
                    {
                        Console.Write("\t    name: " + playerName);
                    }
                    /*playerinfo print (3 from 4)*/
                    if (yc == 6 && xc == Field.GetLength(1) - 1)
                    {
                        Console.Write("\t   lines: " + lines);
                    }
                    /*playerinfo print (4 from 4)*/
                    if (yc == 7 && xc == Field.GetLength(1) - 1)
                    {
                        Console.Write("\t   score: " + score);
                    }
                }
                Console.WriteLine();
            }
        }

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
                if (Fig.IsVertical)
                {
                    for (int figRows = 0; figRows < Fig.Geometry.GetLength(0); figRows++)
                    {
                        for (int figColumns = 0; figColumns < Fig.Geometry.GetLength(1); figColumns++)
                        {
                            Field[Fig.Y + figColumns + 1, Fig.X + 1 - figRows] = Fig.Geometry[figRows, figColumns];
                            UpdateField();
                            Thread.Sleep(10);
                        }
                    }
                    Field[Fig.Y, Fig.X] = 0;
                    Field[Fig.Y, Fig.X + 1] = 0;
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
                            //Console.WriteLine("MAKING CELL " + Field[rowsToDestroy[rowNum], column] + "= 0");
                            //Thread.Sleep(100);
                            Field[rowsToDestroy[rowNum], column] = 0;
                        }
                        lines++;
                        boomNum++;
                    }
                }
                /*score adding*/
                {
                    if (boomNum == 1)
                    {
                        score += 100;
                    }
                    if (boomNum == 2)
                    {
                        score += 300;
                    }
                    if (boomNum == 3)
                    {
                        score += 700;
                    }
                    if (boomNum == 4)
                    {
                        score += 1500;
                    }
                    boomNum = 0;
                    UpdateField();
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
            int cellCheckedCounter = 2;
            int correctorForI = 0;
            if (Fig.ToString() == "Console_tetris.I")
            {
                correctorForI = 1;
            }
            switch (KeyPressed)
            {
                case 'A':
                    {
                        /*if figure is located not on the leftmost point*/
                        if (Fig.X != 0)
                        {
                            /*if figure is horizontal*/
                            if (!Fig.IsVertical)
                            {
                                for (int index = 0; index < Fig.CheckCellsLeftHorizontal.GetLength(0); index = index + 2)
                                {
                                    if (Field[Fig.Y + Fig.CheckCellsLeftHorizontal[index], Fig.X + Fig.CheckCellsLeftHorizontal[index + 1]] != 1)
                                    {
                                        Field[Fig.Y + Fig.CheckCellsLeftHorizontal[index], Fig.X + Fig.CheckCellsLeftHorizontal[index + 1]] = cellCheckedCounter++;
                                    }
                                    UpdateField();
                                    Thread.Sleep(10);
                                    if (Field[Fig.Y + Fig.CheckCellsLeftHorizontal[index], Fig.X + Fig.CheckCellsLeftHorizontal[index + 1]] == 1)
                                    {
                                        return false;
                                    }
                                }
                                return true;
                            }
                            /*if figure is horizontal*/
                            if (Fig.IsVertical)
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
                            if (!Fig.IsVertical && Fig.X != Field.GetLength(1) - Fig.LenghtColumnsHorizontal)
                            {
                                for (int index = 0; index < Fig.CheckCellsRightHorizontal.GetLength(0); index = index + 2)
                                {
                                    if (Field[Fig.Y + Fig.CheckCellsRightHorizontal[index], Fig.X + Fig.CheckCellsRightHorizontal[index + 1]] != 1)
                                    {
                                        Field[Fig.Y + Fig.CheckCellsRightHorizontal[index], Fig.X + Fig.CheckCellsRightHorizontal[index + 1]] = cellCheckedCounter++;
                                    }
                                    UpdateField();
                                    Thread.Sleep(10);
                                    if (Field[Fig.Y + Fig.CheckCellsRightHorizontal[index], Fig.X + Fig.CheckCellsRightHorizontal[index + 1]] == 1)
                                    {
                                        return false;
                                    }
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
                        /*if figure is horizontal AND != on the end of the rows*/
                        if (!Fig.IsVertical && Fig.Y != Field.GetLength(0) - Fig.LengthRowsHorisontal)
                        {
                            /*checking every cell of CheckCellsBottomHorizontal, index point at column and row coordinate*/
                            for (int index = 0; index < Fig.CheckCellsBottomHorizontal.GetLength(0); index = index + 2)
                            {
                                /*next code for checking visualization only*/
                                if (Field[Fig.Y + Fig.CheckCellsBottomHorizontal[index], Fig.X + Fig.CheckCellsBottomHorizontal[index + 1]] != 1)
                                {
                                    Field[Fig.Y + Fig.CheckCellsBottomHorizontal[index], Fig.X + Fig.CheckCellsBottomHorizontal[index + 1]] = cellCheckedCounter++;
                                }
                                UpdateField();
                                Thread.Sleep(10);
                                /*if obstruction found - returns false*/
                                if (Field[Fig.Y + Fig.CheckCellsBottomHorizontal[index], Fig.X + Fig.CheckCellsBottomHorizontal[index + 1]] == 1)
                                {
                                    return false;
                                }
                            }
                            return true;
                        }
                        /*for vertical figures*/
                        if (Fig.IsVertical && Fig.Y != Field.GetLength(0) - Fig.LengthRowsVertical)
                        {
                            /*checking every cell of CheckCellsBottomHorizontal, index point at column and row coordinate*/
                            for (int index = 0; index < Fig.CheckCellsBottomVertical.GetLength(0); index = index + 2)
                            {
                                /*next code for checking visualization only*/
                                if (Field[Fig.Y + Fig.CheckCellsBottomVertical[index], Fig.X + correctorForI + Fig.CheckCellsBottomVertical[index + 1]] != 1)
                                {
                                    Field[Fig.Y + Fig.CheckCellsBottomVertical[index], Fig.X + correctorForI + Fig.CheckCellsBottomVertical[index + 1]] = cellCheckedCounter++;
                                }
                                UpdateField();
                                Thread.Sleep(10);
                                /*if obstruction found - returns false*/
                                if (Field[Fig.Y + Fig.CheckCellsBottomVertical[index], Fig.X + correctorForI + Fig.CheckCellsBottomVertical[index + 1]] == 1)
                                {
                                    return false;
                                }
                            }
                            return true;
                        }
                        return false;
                    }
                default:
                    {
                        Console.WriteLine("just moving down");
                        return false;
                    }
            }
        }

        /*use it later*/

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
        }

    }
}
