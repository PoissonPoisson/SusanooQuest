using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.SusanooQuest.Lib
{
    public interface IRectangleSurface
    {
        public Vector Pos { get; }

        public int Width { get; }

        public int Height { get; }
    }
}
