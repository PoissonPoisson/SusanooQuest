using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.SusanooQuest.Lib
{
    public class Map //: IRectangleSurface
    {
        readonly int _width;
        readonly int _height;

        internal Map(int width, int height)
        {
            if (width <= 0) throw new ArgumentException("Width can't be negative.", nameof(width));
            if (height <= 0) throw new ArgumentException("Height can't be negative.", nameof(height));

            _width = width;
            _height = height;
        }

        public int Width
        {
            get { return _width; }
        }

        public int Height
        {
            get { return _height; }
        }
    }
}
