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
        public int LengthVertical;
        public int LenghtHorizontal;

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
            LenghtHorizontal = 2;
            LengthVertical = 2;
        }

        public override void Rotate(int[,] Field)
        {

        }
    }

    class I : Figure
    {
        public I()
        {
            Geometry = new int[2, 4] {{ 1, 1, 1, 1 },
                                      { 0, 0, 0, 0 } };
        }

        public override void Rotate(int[,] Field)
        {

        }
    }

    class L : Figure
    {
        public L()
        {
            Geometry = new int[2, 4] {{ 0, 0, 1, 0 },
                                      { 1, 1, 1, 0 } };
        }

        public override void Rotate(int[,] Field)
        {

        }
    }

    class J : Figure
    {
        public J()
        {
            Geometry = new int[2, 4] {{ 1, 1, 1, 0 },
                                      { 0, 0, 1, 0 } };
        }

        public override void Rotate(int[,] Field)
        {

        }
    }


    class S : Figure
    {
        public S()
        {
            Geometry = new int[2, 4] {{ 0, 1, 1, 0 },
                                      { 1, 1, 0, 0 } };
        }

        public override void Rotate(int[,] Field)
        {

        }
    }

    class Z : Figure
    {
        public Z()
        {
            Geometry = new int[2, 4] {{ 1, 1, 0, 0 },
                                      { 0, 1, 1, 0 } };
        }

        public override void Rotate(int[,] Field)
        {

        }
    }

    class T : Figure
    {
        public T()
        {
            Geometry = new int[2, 4] {{ 1, 1, 1, 0 },
                                      { 0, 1, 0, 0 } };
        }

        public override void Rotate(int[,] Field)
        {

        }
    }
}
