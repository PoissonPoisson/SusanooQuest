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
            Move();
        }

        protected override void Kill() {}

        protected override void Move()
        {
            throw new NotImplementedException();
        }

        internal int Life => _life;
    }
}
