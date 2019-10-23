using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.SusanooQuest.Lib
{
    class Player : Entity
    {
        
        
        ushort _bombes = 3;
        
        
        public Player (Vector pos, double length, Game game)
            : base(pos, length, game, 3)
        {
        }

        internal override void Update()
        {

        }
    }
}
