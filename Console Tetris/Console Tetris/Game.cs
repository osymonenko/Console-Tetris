using System;
using System.IO;

namespace Console_tetris
{
    class Game
    {
        public int[,] Field = new int[15, 12];
        public Figure Fig = new O();
        //public Figure Fig;
        public static int boomNum;
        public int highScore = 99;
        public string highScorePlayer;
        public string playerName;
        public int score;
        public int lines;
        public bool gameOver;

        public void SetFigStart()
        {
            Fig.IsVertical = true;
            //Fig = Generate();
            Fig.Y = 0;
            Fig.X = Field.GetLength(1) / 2 - 1;
        }

        public void DrawFig()
        {
            if (!Fig.IsVertical)
            {
                for (int figRows = 0; figRows < Fig.Geometry.GetLength(0); figRows++)
                {
                    for (int figColumns = 0; figColumns < Fig.Geometry.GetLength(1); figColumns++)
                    {
                        if (Fig.Geometry[figRows, figColumns] == 0)
                        {
                            continue;
                        }
                        if (Field[Fig.Y + figRows, Fig.X + figColumns] == 1)
                        {
                            gameOver = true;
                        }
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
                        if (Fig.Geometry[figRows, figColumns] == 0)
                        {
                            continue;
                        }
                        if (Field[Fig.Y + figColumns, Fig.X + 1 - figRows] == 1)
                        {
                            gameOver = true;
                        }
                        /*only for I vertical dude*/
                        if (Fig.LengthRowsVertical == 4 && figRows != 1)
                        {
                            if (Field[Fig.Y + figColumns, Fig.X] == 1)
                            {
                                gameOver = true;
                            }
                            Field[Fig.Y + figColumns, Fig.X] = Fig.Geometry[figRows, figColumns];
                            continue;
                        }
                        /*only for I vertical dude*/
                        if (Fig.LengthRowsVertical == 4 && figRows == 1)
                        {
                            break;
                        }
                        else
                        {
                            Field[Fig.Y + figColumns, Fig.X + 1 - figRows] = Fig.Geometry[figRows, figColumns];
                        }
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
                    if (!gameOver)
                    {
                        if (yc == 1 && xc == Field.GetLength(1) - 1)
                        {
                            Console.Write("\tHIGHSCORE:");
                        }
                        if (yc == 2 && xc == Field.GetLength(1) - 1)
                        {
                            Console.Write("\t" + highScorePlayer + " with " + highScore + " points");
                        }
                        if (yc == 4 && xc == Field.GetLength(1) - 1)
                        {
                            Console.Write("\tPLAYER ");
                        }
                        if (yc == 5 && xc == Field.GetLength(1) - 1)
                        {
                            Console.Write("\t  name: " + playerName);
                        }
                        if (yc == 6 && xc == Field.GetLength(1) - 1)
                        {
                            Console.Write("\t lines: " + lines);
                        }
                        if (yc == 7 && xc == Field.GetLength(1) - 1)
                        {
                            Console.Write("\t score: " + score);
                        }
                    }

                    if (gameOver && score <= highScore)
                    {
                        if (yc == 1 && xc == Field.GetLength(1) - 1)
                        {
                            Console.Write("\tHIGHSCORE:");
                        }
                        if (yc == 2 && xc == Field.GetLength(1) - 1)
                        {
                            Console.Write("\t" + highScorePlayer + " with " + highScore + " points");
                        }
                        if (yc == 4 && xc == Field.GetLength(1) - 1)
                        {
                            Console.Write("\t" + playerName + " PLAYER SCORE: ");
                        }
                        if (yc == 5 && xc == Field.GetLength(1) - 1)
                        {
                            Console.Write("\t" + score + " points");
                        }
                        if (yc == 7 && xc == Field.GetLength(1) - 1)
                        {
                            Console.Write("\t    NOT BAD... ;)");
                        }
                    }

                    if (gameOver && score > highScore)
                    {
                        if (yc == 1 && xc == Field.GetLength(1) - 1)
                        {
                            Console.Write("\tNEW HIGHSCORE!!!");
                        }
                        if (yc == 2 && xc == Field.GetLength(1) - 1)
                        {
                            Console.Write("\t" + playerName + " WITH " + score + " POINTS");
                        }
                        if (yc == 4 && xc == Field.GetLength(1) - 1)
                        {
                            Console.Write("\t    WOW, YOU ARE AWESOME ;)");
                        }
                        writeHighScore(playerName, score);
                    }
                }
                Console.WriteLine();
            }
        }

        public void writeHighScore(String player1, int score1)
        {
            StreamWriter sw = new StreamWriter(@"\console tetris highscore.txt");
            sw.WriteLine(player1 + " " + score1);
            sw.Close();
        }

        public void readHighScore()
        {
            StreamReader sr = new StreamReader(@"\console tetris highscore.txt");
            string s = sr.ReadLine();
            string[] text = s.Split(' ');
            highScorePlayer = text[0];
            highScore = int.Parse(text[1]);
            sr.Close();
        }

        public void ClearField()
        {
            for(int i = 0; i < Field.GetLength(0); i++)
            {
                for (int j = 0; j < Field.GetLength(1); j++)
                {
                    Field[i, j] = 0;
                }
            }
        }

        public void MoveFigDown(bool NoObstructions)
        {
            if (NoObstructions)
            {
                for (int column = 0; column < Field.GetLength(1); column++)
                {
                    for (int row = Field.GetLength(0) - 1; row > -1; row--)
                    {
                        if (row == 0 && Field[row,column] == 8)
                        {
                            Field[row, column] = 0;
                            Field[row + 1, column] = 8;
                        }
                        else if(Field[row, column] == 8 && row != Field.GetLength(0)-1 && row != 0)
                        {
                            Field[row + 1, column] = 8;
                            /*if end of the field on the bottom side or this is end of figure*/
                            if (row == Field.GetLength(0) - 1 || (Field[row, column] == 8 && Field[row - 1, column] != 8))
                            {
                                Field[row, column] = 0;
                            }
                        }
                    }
                }
                Fig.Y++;
                UpdateField();
            }
            else
            {
                ConvertFigure();
                CheckFullLines();
                SetFigStart();
                DrawFig();
            }
        }

        public void ConvertFigure()
        {
            for (int i = 0; i < Field.GetLength(0); i++)
            {
                for (int j = 0; j < Field.GetLength(1); j++)
                {
                    if (Field[i, j] == 8)
                    {
                        Field[i, j] = 1;
                    }
                }
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
                /*moving rows*/
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
                for (int row = 0; row < Field.GetLength(0); row++)
                {
                    for (int column = 0; column < Field.GetLength(1); column++)
                    {
                        if (Field[row, column] == 8)
                        {
                            Field[row, column - 1] = 8;
                            /*if end of the field on the right side or this is end of figure*/
                            if (column == Field.GetLength(1) - 1 || (Field[row,column] == 8 && Field[row,column + 1] != 8))
                            {
                                Field[row, column] = 0;
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
                for (int row = 0; row < Field.GetLength(0); row++)
                {
                    for (int column = Field.GetLength(1) - 1; column > -1; column--)
                    {
                        if (Field[row, column] == 8)
                        {
                            /*if end of the field on the left side or this is end of figure*/
                            if (column == 0 || (Field[row, column] == 8 && Field[row, column - 1] != 8))
                            {
                                Field[row, column] = 0;
                            }
                            Field[row, column + 1] = 8;
                        }
                    }
                }
                Fig.X++;
                UpdateField();
            }
        }

        public bool NoObstructionsCheck(char KeyPressed)
        {
            switch (KeyPressed)
            {
                case 'A':
                    {
                        /*if figure is located not on the leftmost point*/
                        if (Fig.X != 0)
                        {
                            for(int row = 0; row < Field.GetLength(0); row++)
                            {
                                for (int column = 0; column < Field.GetLength(1); column++)
                                {
                                    if (Field[row,column] == 8)
                                    {
                                        //save from OutOfBounds exception
                                        if (column == 0)
                                        {
                                            return false;
                                        }
                                        if (Field[row, column - 1] == 1)
                                        {
                                            return false;
                                        }
                                    }
                                }
                            }
                            return true;
                        }
                        return false;
                    }
                case 'D':
                    {
                        if ((!Fig.IsVertical &&
                            (Fig.X + Fig.LengthColumnsHorizontal != Field.GetLength(1))) || 
                             (Fig.IsVertical &&
                            (Fig.X + Fig.LengthColumnsVertical != Field.GetLength(1))))
                        {
                            for (int row = 0; row < Field.GetLength(0); row++)
                            {
                                for (int column = 0; column < Field.GetLength(1); column++)
                                {
                                    if (Field[row, column] == 8)
                                    {
                                        if (Field[row, column + 1] == 1)
                                        {
                                            return false;
                                        }
                                    }
                                }
                            }
                            return true;
                        }
                        return false;
                    }
                case 'S':
                    {
                        if ((!Fig.IsVertical &&
                        (Fig.Y + Fig.LengthRowsHorisontal != Field.GetLength(0))) ||
                         (Fig.IsVertical &&
                        (Fig.Y + Fig.LengthRowsVertical != Field.GetLength(0))))
                        {
                            for (int row = 0; row < Field.GetLength(0); row++)
                            {
                                for (int column = 0; column < Field.GetLength(1); column++)
                                {
                                    if (Field[row, column] == 8)
                                    {
                                        if (Field[row + 1, column] == 1)
                                        {
                                            return false;
                                        }
                                    }
                                }
                            }
                            return true;
                        }
                        return false;
                    }
                default:
                    {
                        return false;
                    }
            }
        }

        Figure Generate()
        {
           Figure fig;
           Random num = new Random();
           int x = num.Next(14);
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
                case 7:
                    fig = new T();
                    break;
                case 8:
                    fig = new I();
                    fig.IsVertical = true;
                    break;
                case 9:
                    fig = new L();
                    fig.IsVertical = true;
                    break;
                case 10:
                    fig = new J();
                    fig.IsVertical = true;
                    break;
                case 11:
                    fig = new O();
                    fig.IsVertical = true;
                    break;
                case 12:
                    fig = new S();
                    fig.IsVertical = true;
                    break;
                case 13:
                    fig = new Z();
                    fig.IsVertical = true;
                    break;
                case 14:
                    fig = new T();
                    fig.IsVertical = true;
                    break;
                default:
                    return Generate();
            }
           return fig;
        }

    }
}
