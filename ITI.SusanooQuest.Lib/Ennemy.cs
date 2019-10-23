using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.SusanooQuest.Lib
{
    public class Ennemy : Entity
    {
        public Ennemy(Vector pos, float length, Game game, ushort live)
            : base(pos, length, game, live)
        {
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

        protected override void Move()
        {
            _pos = new Vector(_game.GetNextRandomDouble(-1.0, 1.0), _game.GetNextRandomDouble(-1.0, 1.0));
        }
    }
}
