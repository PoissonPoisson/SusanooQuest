using System;
using ITI.SusanooQuest.Lib;

namespace ITI.SusanooQuest.UI
{
    public class Button : IRectangleSurface
    {
        readonly Vector _pos;
        readonly int _width;
        readonly int _height;

        public Button(Vector pos, int width, int height)
        {
            if (width <= 0) throw new ArgumentException("Width can't be negative.", nameof(width));
            if (height <= 0) throw new ArgumentException("Height can't be negative.", nameof(height));
            if (int.MaxValue - pos.X < width) throw new ArgumentOutOfRangeException("Width is superior of int values.", nameof(width));
            if (int.MaxValue - pos.Y < height) throw new ArgumentOutOfRangeException("Height is superior of int values.", nameof(height));

            _pos = pos;
            _width = width;
            _height = height;
        }

        public Vector Pos
        {
            get { return _pos; }
        }

        public int Width
        {
            get { return _width; }
        }

        public int Height
        {
            get { return _height; }
        }

        public bool Selected(Vector pos)
        {
            return _pos.X <= pos.X && pos.X < _pos.X + _width && _pos.Y <= pos.Y && pos.Y < _pos.Y + _height;
        }
    }
}