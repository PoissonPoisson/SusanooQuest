using System;

namespace ITI.SusanooQuest.Lib
{
    public class Player : Entity
    {
        
        ushort _bombes = 3;
        
        public Player (Vector pos, float length, Game game)
            : base(pos, length, game, 3)
        {
        }

        internal override void Update()
        {

        }

        protected override void Kill() {}

        //protected override void Move(float x, float y)
        //{
        //    _pos = new Vector(_pos.X + x, _pos.Y + y);
        //}

        
        internal int Life => _life;
    }
}
