using System;

namespace ITI.SusanooQuest.Lib
{
    public class Player : Entity
    {
        
        ushort _bombes = 3;
        Vector _delta;
        readonly int _width;
        readonly int _height;

        public Player (Vector pos, float length, Game game, int width, int height)
            : base(pos, length, game, 3)
        {
            _delta = new Vector(0, 0);
            _width = width;
            _height = height;
        }

        internal override void Update()
        {
            Move();
        }

        protected override void Kill() {}

        //protected override void Move(float x, float y)
        //{
        //    _pos = new Vector(_pos.X + x, _pos.Y + y);
        //}

        public void StartMove (Vector deplacement)
        {
            float x;
            float y;
            if (deplacement.X != 0 && _delta.X == 0) x = deplacement.X;
            else x = 0;
            if (deplacement.Y != 0 && _delta.Y == 0) y = deplacement.Y;
            else y = 0;

            _delta = _delta.Add(x, y);
        }

        public void EndMove(Vector vector)
        {
            if (vector.X != 0) _delta = _delta.Add(-_delta.X, vector.Y);
            if (vector.Y != 0) _delta = _delta.Add(vector.X, -_delta.Y);
        }

        public void Move()
        {
            float x = Math.Max(Math.Min(_pos.X + _delta.X, _game.Map.Width), 0);
            float y = Math.Max(Math.Min(_pos.Y + _delta.Y, _game.Map.Height), 0);
            _pos = new Vector(x, y);
        }
        
        internal int Life => _life;
    }
}
