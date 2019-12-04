using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ITI.SusanooQuest.Lib
{
  
    public class Ennemy : Entity
    {
        string _pathtest = @"...\ITI.SusanooQuest.Lib\leveltest.json";
        public Ennemy( Vector pos, float length, Game game, ushort life, float speed)
            : base(pos, length, game, life, speed)
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

        protected void Move()
        {
            
        }

        internal JToken Serialize()
        {

            return new JObject(
                new JProperty("Life", _life),
                new JProperty("Speed", _speed)
                ) ;
            
        }
    }
}
