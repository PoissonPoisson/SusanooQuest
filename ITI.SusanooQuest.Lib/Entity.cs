using System;

namespace ITI.SusanooQuest.Lib
{
    public abstract class Entity
    {
        #region Fields

        protected readonly float _length;
        protected Game _game;
        protected float _speed;
        protected int _life;
        protected Vector _pos;
        protected ushort _strength;

        #endregion

        protected Entity(Vector pos, float length, Game game, int life, float speed)
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

        #region Properties

        public Vector Position => _pos;

        public int Life
        {
            get { return _life; }
            internal set { _life = value; }
        }
        public float Length => _length;

        internal ushort Strength => _strength;

        #endregion
    }
}
