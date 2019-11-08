using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.SusanooQuest.Lib
{
    internal class Map //: IRectangleSurface
    {
        readonly Vector _pos;
        readonly int _width;
        readonly int _height;

        internal Map(int width, int height)
        {
            if (width <= 0) throw new ArgumentException("Width can't be negative.", nameof(width));
            if (height <= 0) throw new ArgumentException("Height can't be negative.", nameof(height));

            _width = width;
            _height = height;
        }

        internal Vector Pos
        {
            get { return _pos; }
        }

        internal int Width
        {
            get { return _width; }
        }

        internal int Height
        {
            get { return _height; }
        }
    }
}
