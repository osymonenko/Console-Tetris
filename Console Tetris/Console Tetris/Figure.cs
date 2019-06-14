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
        public int[] CheckCellsLeftHorizontal { get; set; }
        public int[] CheckCellsRightHorizontal { get; set; }
        public int[] CheckCellsBottomHorizontal { get; set; }
        public int[] CheckCellsLeftVertical { get; set; }
        public int[] CheckCellsRightVertical { get; set; }
        public int[] CheckCellsBottomVertical { get; set; }
        public bool IsVertical { get; set; } //0-horisontal, 1 - vertical
        public int X { get; set; }
        public int Y { get; set; }
        public int LengthRowsHorisontal { get; set; }
        public int LengthRowsVertical { get; set; }
        public int LenghtColumnsHorizontal { get; set; }
        public int LenghtColumnsVertical { get; set; }

        public virtual void Rotate(int[,] Field) { }
    }

    class O : Figure
    {
        public O()
        {
            Geometry = new int[2, 4] {{ 1, 1, 0, 0 },
                                      { 1, 1, 0, 0 } };

            CheckCellsLeftHorizontal = new int[] { 0, -1, 1, -1 };
            CheckCellsRightHorizontal = new int[] { 0, 2, 1, 2 };
            CheckCellsBottomHorizontal = new int[] { 2, 0, 2, 1 };
            CheckCellsLeftVertical = CheckCellsLeftHorizontal;
            CheckCellsRightVertical = CheckCellsRightHorizontal;
            CheckCellsBottomVertical = CheckCellsBottomHorizontal;

            LengthRowsHorisontal = 2;
            LenghtColumnsHorizontal = 2;
            LengthRowsVertical = 2;
            LenghtColumnsVertical = 2;
        }
    }

    class I : Figure
    {
        public I()
        {
            Geometry = new int[2, 4] {{ 1, 1, 1, 1 },
                                      { 0, 0, 0, 0 } };

            CheckCellsLeftHorizontal = new int[] { 0, -1 };
            CheckCellsRightHorizontal = new int[] { 0, 4 };
            CheckCellsBottomHorizontal = new int[] { 1, 0, 1, 1, 1, 2, 1, 3 };
            CheckCellsLeftVertical = new int[] { 0, -1, 1, -1, 2, -1, 3, -1 };
            CheckCellsRightVertical = new int[] { 0, 1, 1, 1, 2, 1, 3, 1 };
            CheckCellsBottomVertical = new int[] { 4, 0 };

            LengthRowsHorisontal = 1;
            LenghtColumnsHorizontal = 4;
            LengthRowsVertical = 4;
            LenghtColumnsVertical = 1;
        }
    }

    class L : Figure
    {
        public L()
        {
            Geometry = new int[2, 4] {{ 0, 0, 1, 0 },
                                      { 1, 1, 1, 0 } };

            CheckCellsLeftHorizontal = new int[] { 0, 1, 1, -1 };
            CheckCellsRightHorizontal = new int[] { 0, 3, 1, 3 };
            CheckCellsBottomHorizontal = new int[] { 2, 0, 2, 1, 2, 2 };
            CheckCellsLeftVertical = new int[] { 0, -1, 1, -1, 2, -1 };
            CheckCellsRightVertical = new int[] { 0, 1, 1, 1, 2, 2 };
            CheckCellsBottomVertical = new int[] { 0, 3, 1, 3 };

            LengthRowsHorisontal = 2;
            LenghtColumnsHorizontal = 3;
            LengthRowsVertical = 3;
            LenghtColumnsVertical = 2;
        }
    }

    class J : Figure
    {
        public J()
        {
            Geometry = new int[2, 4] {{ 1, 1, 1, 0 },
                                      { 0, 0, 1, 0 } };

            CheckCellsLeftHorizontal = new int[] { 0, -1, 1, 1 };
            CheckCellsRightHorizontal = new int[] { 0, 3, 1, 3 };
            CheckCellsBottomHorizontal = new int[] { 1, 0, 1, 1, 2, 2 };
            CheckCellsLeftVertical = new int[] { 0, 0, 1, 0, 2, -1 };
            CheckCellsRightVertical = new int[] { 0, 2, 1, 2, 2, 2 };
            CheckCellsBottomVertical = new int[] { 0, 3, 1, 3 };

            LengthRowsHorisontal = 2;
            LenghtColumnsHorizontal = 3;
            LengthRowsVertical = 3;
            LenghtColumnsVertical = 2;
        }
    }


    class S : Figure
    {
        public S()
        {
            Geometry = new int[2, 4] {{ 0, 1, 1, 0 },
                                      { 1, 1, 0, 0 } };

            CheckCellsLeftHorizontal = new int[] { 0, 0, 1, -1 };
            CheckCellsRightHorizontal = new int[] { 0, 3, 1, 2 };
            CheckCellsBottomHorizontal = new int[] { 2, 0, 2, 1, 1, 2 };
            CheckCellsLeftVertical = new int[] { 0, -1, 1, -1, 2, 0 };
            CheckCellsRightVertical = new int[] { 0, 1, 1, 2, 2, 2 };
            CheckCellsBottomVertical = new int[] { 0, 2, 1, 3 };

            LengthRowsHorisontal = 2;
            LenghtColumnsHorizontal = 3;
            LengthRowsVertical = 3;
            LenghtColumnsVertical = 2;
        }
    }

    class Z : Figure
    {
        public Z()
        {
            Geometry = new int[2, 4] {{ 1, 1, 0, 0 },
                                      { 0, 1, 1, 0 } };

            CheckCellsLeftHorizontal = new int[] { 0, -1, 1, 0 };
            CheckCellsRightHorizontal = new int[] { 0, 2, 1, 3 };
            CheckCellsBottomHorizontal = new int[] { 1, 0, 2, 1, 2, 2 };
            CheckCellsLeftVertical = new int[] { 0, 0, 1, -1, 2, -1 };
            CheckCellsRightVertical = new int[] { 0, 2, 1, 2, 2, 1 };
            CheckCellsBottomVertical = new int[] { 0, 3, 1, 2 };

            LengthRowsHorisontal = 2;
            LenghtColumnsHorizontal = 3;
            LengthRowsVertical = 3;
            LenghtColumnsVertical = 2;
        }
    }

    class T : Figure
    {
        public T()
        {
            Geometry = new int[2, 4] {{ 1, 1, 1, 0 },
                                      { 0, 1, 0, 0 } };
            CheckCellsLeftHorizontal = new int[] { 0, -1, 1, 0 };
            CheckCellsRightHorizontal = new int[] { 0, 3, 1, 2 };
            CheckCellsBottomHorizontal = new int[] { 1, 0, 2, 1, 1, 2 };
            CheckCellsLeftVertical = new int[] { 0, 0, 1, -1, 2, 0 };
            CheckCellsRightVertical = new int[] { 0, 2, 1, 2, 2, 2 };
            CheckCellsBottomVertical = new int[] { 0, 2, 1, 3 };

            LengthRowsHorisontal = 2;
            LenghtColumnsHorizontal = 3;
            LengthRowsVertical = 3;
            LenghtColumnsVertical = 2;
        }
    }
}
