using System;

namespace ITI.SusanooQuest.Lib
{
    public class Player : Entity
    {
        
        ushort _bombes = 3;
        Vector _delta;
        bool _slow;

        public Player (Vector pos, float length, Game game, ushort life, float speed, ushort bombes)
            : base(pos, length, game, life,  speed)
        {
            _delta = new Vector(0, 0);
            _slow = false;
            _bombes = bombes;
        }

        internal override void Update()
        {
            Move();
        }

        protected override void Kill()
        {
            throw new NotImplementedException();
        }

        public void StartMove (Vector deplacement)
        {
            float x;
            float y;
            if (deplacement.X != 0 && _delta.X == 0) x = deplacement.X;
            else x = 0;
            if (deplacement.Y != 0 && _delta.Y == 0) y = deplacement.Y;
            else y = 0;

            _delta = _delta.Add(x * _speed, y * _speed);
        }

        public void EndMove(Vector vector)
        {
            if (vector.X != 0) _delta = _delta.Add(-_delta.X, vector.Y);
            if (vector.Y != 0) _delta = _delta.Add(vector.X, -_delta.Y);
        }

        public void Move()
        {
            float x = _delta.X;
            float y = _delta.Y;

            if (Math.Sqrt(x * x + y * y) > 1)
            {
                x = (float)Math.Cos(Math.Atan2(y, x));
                y = (float)Math.Sin(Math.Atan2(y, x));
            }

            x = _pos.X + ((_slow) ? x * (_speed / 2) : x * _speed);
            if (x - _length < 0) x = 0 + _length;
            else if (_game.Map.Width < x + _length) x = _game.Map.Width - _length;

            y = _pos.Y + ((_slow) ? y * (_speed / 2) : y * _speed);
            if (y - _length < 0) y = 0 + _length;
            else if (_game.Map.Height < y + _length) y = _game.Map.Height - _length;

            _pos = new Vector(x, y);
        }
        
        internal int Life => _life;

        public float Length => _length;

        public ushort Bombes
        {
            get { return _bombes; }
            internal set { _bombes = value; }
        }

        public bool Slow
        {
            get { return _slow; }
            set { _slow = value; }
        }
    }
}
