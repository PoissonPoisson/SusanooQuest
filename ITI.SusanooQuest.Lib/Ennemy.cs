using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.SusanooQuest.Lib
{
    public class Ennemy : Entity
    {
        string _tag;
        ushort _cd;
        public Ennemy(Vector pos, float length, Game game, ushort life, float speed, string tag)
            : base(pos, length, game, life, speed)
        {
            _tag = tag;
            _cd = 10;
        }

        protected override void Kill()
        {
            _game.OnKill(this);
            _game = null;
        }

        internal override void Update()
        {

            if (_life == 0) Kill();
            else Move();
            
        }

        protected void Move()
        {
            _cd--;
            if (_cd == 0)
            {
                _game.CreateProjectile(2, 1, new Vector(_pos.X + _length, _pos.Y + _length), this, "CosY");
                _cd = 10;
            }
            if (_pos.X < 0 || _pos.X > _game.Map.Width) _speed = -(_speed);
            Vector newpos = new Vector(_pos.X + _speed, _pos.Y);
            _pos = newpos;
            //Console.WriteLine(_pos);
        }

        public string Tag => _tag;
    }
}
