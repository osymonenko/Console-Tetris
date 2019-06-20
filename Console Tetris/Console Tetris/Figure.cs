using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console_tetris;

namespace Console_tetris
{
    abstract class Figure
    {
        int[,] Field { get; set; }
        public int[,] Geometry { get; set; }
        public bool IsVertical { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int LengthRowsHorisontal { get; set; }
        public int LengthRowsVertical { get; set; }
        public int LengthColumnsHorizontal { get; set; }
        public int LengthColumnsVertical { get; set; }
    }

    class O : Figure
    {
        public O()
        {
            Geometry = new int[2, 4] {{ 8, 8, 0, 0 },
                                      { 8, 8, 0, 0 } };

            LengthRowsHorisontal = 2;
            LengthColumnsHorizontal = 2;
            LengthRowsVertical = 2;
            LengthColumnsVertical = 2;
        }
    }

    class I : Figure
    {
        public I()
        {
            Geometry = new int[2, 4] {{ 8, 8, 8, 8 },
                                      { 0, 0, 0, 0 } };

            LengthRowsHorisontal = 1;
            LengthColumnsHorizontal = 4;
            LengthRowsVertical = 4;
            LengthColumnsVertical = 1;
        }
    }

    class L : Figure
    {
        public L()
        {
            Geometry = new int[2, 4] {{ 0, 0, 8, 0 },
                                      { 8, 8, 8, 0 } };

            LengthRowsHorisontal = 2;
            LengthColumnsHorizontal = 3;
            LengthRowsVertical = 3;
            LengthColumnsVertical = 2;
        }
    }

    class J : Figure
    {
        public J()
        {
            Geometry = new int[2, 4] {{ 8, 8, 8, 0 },
                                      { 0, 0, 8, 0 } };

            LengthRowsHorisontal = 2;
            LengthColumnsHorizontal = 3;
            LengthRowsVertical = 3;
            LengthColumnsVertical = 2;
        }
    }


    class S : Figure
    {
        public S()
        {
            Geometry = new int[2, 4] {{ 0, 8, 8, 0 },
                                      { 8, 8, 0, 0 } };

            LengthRowsHorisontal = 2;
            LengthColumnsHorizontal = 3;
            LengthRowsVertical = 3;
            LengthColumnsVertical = 2;
        }
    }

    class Z : Figure
    {
        public Z()
        {
            Geometry = new int[2, 4] {{ 8, 8, 0, 0 },
                                      { 0, 8, 8, 0 } };

            LengthRowsHorisontal = 2;
            LengthColumnsHorizontal = 3;
            LengthRowsVertical = 3;
            LengthColumnsVertical = 2;
        }
    }

    class T : Figure
    {
        public T()
        {
            Geometry = new int[2, 4] {{ 8, 8, 8, 0 },
                                      { 0, 8, 0, 0 } };

            LengthRowsHorisontal = 2;
            LengthColumnsHorizontal = 3;
            LengthRowsVertical = 3;
            LengthColumnsVertical = 2;
        }
    }
}
