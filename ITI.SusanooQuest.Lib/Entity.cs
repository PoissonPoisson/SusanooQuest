using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.SusanooQuest.Lib
{
    public abstract class Entity
    {
        protected readonly float _length;
        protected Game _game;
        protected float _speed;
        protected ushort _life;
        protected Vector _pos;

        protected Entity(Vector pos, float length, Game game, ushort life, float speed)
        {
            if (length < 0) throw new IndexOutOfRangeException("Length can't be nagative.");
            if (speed < 0) throw new IndexOutOfRangeException("Speed can't be nagative.");
            if (game == null) throw new NullReferenceException("Game is null.");

            _length = length;
            _game = game;
            _pos = pos;
            _life = life;
            _speed = speed;
        }

        public Vector Position
        {
            get { return _pos; }
        }

        internal abstract void Update();

        protected abstract void Kill();

    }
}
