using System;

namespace ITI.SusanooQuest.Lib
{
    public class Player : Entity
    {
        
        ushort _bombes = 3;
        float _deltaX;
        float _deltaY;
        readonly int _width;
        readonly int _height;

        public Player (Vector pos, float length, Game game, int width, int height)
            : base(pos, length, game, 3)
        {
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

        public void StartMove (float x, float y)
        {
            _deltaX = x;
            _deltaY = y;
        }

        public void EndMove()
        {
            _deltaX = 0;
            _deltaY = 0;
        }

        public void Move()
        {
            float x = Math.Max(Math.Min(_pos.X + _deltaX, _game.Map.Width), 0);
            float y = Math.Max(Math.Min(_pos.Y + _deltaY, _game.Map.Height), 0);
            _pos = new Vector(x, y);
        }
        
        internal int Life => _life;
    }
}
