using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.SusanooQuest.Lib
{
    public abstract class Entity
    {
        readonly float _length;
        protected Game _game;
        float _speed;
        protected ushort _life;
        protected Vector _pos;

        protected Entity(Vector pos, float length, Game game, ushort life)
        {
            _length = length;
            _game = game;
            _pos = pos;
            _life = life;
        }

        internal Vector Position
        {
            get { return _pos; }
        }

        internal abstract void Update();

        protected abstract void Kill();

    }
}
