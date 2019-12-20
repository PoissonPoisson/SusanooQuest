using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ITI.SusanooQuest.Lib
{
    
    public class Ennemy : Entity
    {
        #region fields
        IMovement _movement;
        readonly string _tag;
        string _path = @"...\ITI.SusanooQuest.Lib\leveltest.json";
        ushort _cd;
        #endregion


        public Ennemy(Vector pos, float length, Game game, ushort life, float speed, string tag)
            : base(pos, length, game, life, speed)
        {
            _tag = tag;
            _cd = 10;
        }

        internal override void Update()
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

        internal double Speed => _speed;

        public string Tag => _tag;
    }

    public class Standard : IMovement
    {
        float _speed;
        Game _game;

        internal Standard(double speed, Game game)
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

    
    //public class Ennemy : Entity
    //{
    //    string _path = @"...\ITI.SusanooQuest.Lib\leveltest.json";
    //    string _tag;
    //    ushort _cd;
    //    public Ennemy(Vector pos, float length, Game game, ushort life, float speed, string tag)
    //        : base(pos, length, game, life, speed)
    //    {
    //        _tag = tag;
    //        _cd = 10;
    //    }

    //    private void Kill()
    //    {
    //        _game.OnKill(this);
    //        _game = null;

    //    }

    //    internal override void Update()
    //    {

    //        if (_life <= 0) Kill();
    //        else Move();

    //    }

    //    protected void Move()
    //    {
    //        //Same as the player, make the ennemy shoot evry 10 updates
    //        _cd--;
    //        if (_cd == 0)
    //        {
    //            _game.CreateProjectile(2, 1, new Vector(_pos.X + _length, _pos.Y + _length), this, "CosY");
    //            _cd = 10;
    //        }

    //        //Make the ennemy go the other way when it bumps against the edge of the map
    //        if (_pos.X < 0 || _pos.X > _game.Map.Width) _speed = -(_speed);

    //        //Move the ennemy
    //        Vector newpos = new Vector(_pos.X + _speed, _pos.Y);
    //        _pos = newpos;
    //        //Console.WriteLine(_pos);
    //    }

    //    internal JToken Serialize()
    //    {

    //        return new JObject(
    //            new JProperty("Life", _life),
    //            new JProperty("Speed", _speed)
    //            );

    //    }

    //    public string Tag => _tag;
    //}
}
