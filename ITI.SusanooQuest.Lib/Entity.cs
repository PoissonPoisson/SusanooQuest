using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.SusanooQuest.Lib
{
    abstract class Entity
    {
        double _length;
        double _speed = double.MinValue;
        Vector _pos;
        Game _game;
        ushort _live;

        protected Entity (Vector pos, double length, Game game, ushort live)
        {
            _length = length;
            _game = game;
            _pos = pos;
            _live = live;
        }

        internal abstract void Update();
    }
}
