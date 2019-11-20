using System;
using System.Collections.Generic;

namespace ITI.SusanooQuest.Lib
{
    public class Player : Entity
    {
        #region Fields

        //Vector _delta;
        bool _slow;
        bool _onShoot;
        readonly Dictionary<string, bool> _deplacement;

        #endregion

        public Player (Vector pos, float length, Game game, ushort life, float speed)
            : base(pos, length, game, life,  speed)
        {
            //_delta = new Vector(0, 0);
            _slow = false;
            _onShoot = false;
            _deplacement = new Dictionary<string, bool>
            {
                { "Left" , false },
                { "Up"   , false },
                { "Right", false },
                { "Down" , false }
            };
        }

        #region Properties

        internal int Life => _life;

        public float Length => _length;

        public bool Slow
        {
            get { return _slow; }
            set { _slow = value; }
        }

        internal bool OnShoot => _onShoot;

        public Dictionary<string, bool> Deplacment => _deplacement;

        //public Vector Delta => _delta;

        #endregion

        #region Methodes

        internal override void Update()
        {
            Move();
        }

        protected override void Kill()
        {
            throw new NotImplementedException();
        }

        //public void StartMove (Vector deplacement)
        //{
        //    float x;
        //    float y;
        //    if (deplacement.X != 0 && _delta.X == 0) x = deplacement.X;
        //    else x = 0;
        //    if (deplacement.Y != 0 && _delta.Y == 0) y = deplacement.Y;
        //    else y = 0;

        //    _delta = _delta.Add(x * _speed, y * _speed);
        //}

        //public void EndMove(Vector direction)
        //{
        //    if (direction.X != 0) _delta = _delta.Add(-_delta.X, direction.Y);
        //    if (direction.Y != 0) _delta = _delta.Add(direction.X, -_delta.Y);
        //}

        public void Move()
        {
            float x = (_deplacement["Left"]) ? -1 : 0;
            x += (_deplacement["Right"]) ? 1 : 0;
            float y = (_deplacement["Up"]) ? -1 : 0;
            y += (_deplacement["Down"]) ? 1 : 0;

            if (Math.Abs(x) + Math.Abs(y) == 2.0)
            {
                x = (float)Math.Cos(Math.Atan2(y, x));
                y = (float)Math.Sin(Math.Atan2(y, x));
            }

            //float movSpeedPerMs = 1;
            //float x = _delta.X;
            //float y = _delta.Y;

            //if (Math.Sqrt(x * x + y * y) > 1)
            //{
            //    x = (float)Math.Cos(Math.Atan2(y, x));
            //    y = (float)Math.Sin(Math.Atan2(y, x));
            //}

            x = _pos.X + ((_slow) ? x * (_speed / 2) : x * _speed);
            if (x - _length < 0) x = 0 + _length;
            else if (_game.Map.Width < x + _length) x = _game.Map.Width - _length;

            y = _pos.Y + ((_slow) ? y * (_speed / 2) : y * _speed);
            if (y - _length < 0) y = 0 + _length;
            else if (_game.Map.Height < y + _length) y = _game.Map.Height - _length;

            _pos = new Vector(x, y);
        }

        public void StartShoot()
        {
            _onShoot = true;            
            Console.WriteLine("je tire");
        }

        public void EndShoot()
        {
            _onShoot = false;
            Console.WriteLine("je ne tire plus");
        }

        #endregion
    }
}
