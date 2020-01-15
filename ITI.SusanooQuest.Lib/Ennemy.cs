using Newtonsoft.Json.Linq;
using System;


namespace ITI.SusanooQuest.Lib
{
    
    public class Ennemy : Entity, IEnnemy
    {
        #region fields
        IMovement _movement;
        readonly string _tag;
        string _path = @"...\ITI.SusanooQuest.Lib\leveltest.json";
        ushort _cd;
        #endregion


        public Ennemy(Vector pos, float length, Game game, int life, float speed, string tag)
            : base(pos, length, game, life, speed)
        {
            _tag = tag;
            _cd = 10;
        }

        void IEnnemy.Update()
        {
            //Same as the player, make the ennemy shoot evry 10 updates
            _cd--;
            if (_cd == 0)
            {
                _game.CreateProjectile(2, 1, new Vector(_pos.X + _length, _pos.Y + _length), this, "CosY");
                _cd = 10;
            }

            if (_life <= 0) Kill();
            else _pos = _movement.Move(_pos);
            
        }

        private void Kill()
        {
            _game.OnKill(this);
            _game = null;

        }

        internal JToken Serialize()
        {

            return new JObject(
                new JProperty("Life", _life),
                new JProperty("Speed", _speed)
                );

        }

        internal IMovement Movement
        {
            set { _movement = value; }
            get { return _movement; }
        }

        float IEnnemy.Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }
        public string Tag => _tag;

        public Game Context => _game;
    }

    public class Standard : IMovement
    {
        float _speed;
        Game _game;

        internal Standard(float speed, Game game)
        {
            _speed = Convert.ToSingle(speed);
            _game = game;
        }

        public Vector Move(Vector pos)
        {
            //Make the ennemy go the other way when it bumps against the edge of the map
            if (pos.X < 0 || pos.X > _game.Map.Width) _speed = -(_speed);
            return new Vector(pos.X + _speed, pos.Y); ;
        }
    }

    public class Diagonal : IMovement
    {
        float _speed;
        Game _game;

        internal Diagonal(double speed, Game game)
        {
            _speed = Convert.ToSingle(speed);
            _game = game;
        }

        public Vector Move(Vector pos)
        {
            //Make the ennemy go the other way when it bumps against the edge of the map
            if (pos.X < 0 || pos.X > _game.Map.Width) _speed = -(_speed);
            return new Vector(pos.X + _speed, pos.Y + _speed); ;
        }
    }


    
}
