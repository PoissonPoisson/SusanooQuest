﻿using Newtonsoft.Json.Linq;
using System;


namespace ITI.SusanooQuest.Lib
{
    interface IMovementEn
    {
        Vector Move(Vector pos);
        internal UInt16 Type { get; }
    }

    public class Ennemy : Entity
    {
        #region fields
        IMovementEn _movement;
        IAttackPattern _attack;
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

        internal void Update()
        {
            Attack.Update();

            if (_life <= 0) Kill();
            //else _pos = _movement.Move(_pos);

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

        internal IMovementEn Movement
        {
            set { _movement = value; }
            get { return _movement; }
        }

        internal float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        internal IAttackPattern Attack
        {
            get { return _attack; }
            set { _attack = value; }
        }
        public string Tag => _tag;

        public Vector Pos => _pos;
        public Game Context => _game;
    }

    public class Standard : IMovementEn
    {
        float _speed;
        Game _game;
        UInt16 _type;

        internal Standard(float speed, Game game)
        {
            _speed = Convert.ToSingle(speed);
            _game = game;
            _type = 1000;
        }

        //public Vector Move(Vector pos)
        //{
        //    //Make the ennemy go the other way when it bumps against the edge of the map
        //    if (pos.X < 0 || pos.X > _game.Map.Width) _speed = -(_speed);
        //    return new Vector(pos.X + _speed, pos.Y);
        //}

        Vector IMovementEn.Move(Vector pos)
        {
            //Make the ennemy go the other way when it bumps against the edge of the map
            if (pos.X < 0 || pos.X > _game.Map.Width) _speed = -(_speed);
            return new Vector(pos.X + _speed, pos.Y);
        }

        ushort IMovementEn.Type => _type;
    }

    public class Diagonal : IMovementEn
    {
        float _speed;
        Game _game;
        UInt16 _type;

        internal Diagonal(double speed, Game game)
        {
            _speed = Convert.ToSingle(speed);
            _game = game;
            _type = 1000;
        }

        ushort IMovementEn.Type => _type;

        //public Vector Move(Vector pos)
        //{
        //    //Make the ennemy go the other way when it bumps against the edge of the map
        //    if (pos.X < 0 || pos.X > _game.Map.Width || pos.Y < 0 || pos.Y > _game.Map.Height) _speed = -(_speed);
        //    return new Vector(pos.X + _speed, pos.Y + _speed);
        //}

        Vector IMovementEn.Move(Vector pos)
        {
            //Make the ennemy go the other way when it bumps against the edge of the map
            if (pos.X < 0 || pos.X > _game.Map.Width || pos.Y < 0 || pos.Y > _game.Map.Height) _speed = -(_speed);
            return new Vector(pos.X + _speed, pos.Y + _speed);
        }
    }



        
}
