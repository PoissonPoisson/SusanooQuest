using Newtonsoft.Json.Linq;
using System;


namespace ITI.SusanooQuest.Lib
{
    interface IMovementEn
    {
        Vector Move(Vector pos);
        internal ushort Type { get; }
    }

    public class Ennemy : Entity
    {
        #region fields

        IMovementEn _movement;
        IAttackPattern _attack;
        readonly string _tag;
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
            else _pos = _movement.Move(_pos);
            
        }

        internal void Kill()
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
        public Game Context => _game;
    }

    public class Standard : IMovementEn
    {
        float _speed;
        readonly Game _game;
        readonly ushort _type;

        internal Standard(float speed, Game game)
        {
            _speed = speed;
            _game = game ?? throw new NullReferenceException("Game is null.");
            _type = 1000;
        }

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
        readonly Game _game;
        readonly ushort _type;

        internal Diagonal(float speed, Game game)
        {
            _speed = speed;
            _game = game ?? throw new NullReferenceException("Game is null.");
            _type = 1000;
        }

        ushort IMovementEn.Type => _type;

        Vector IMovementEn.Move(Vector pos)
        {
            //Make the ennemy go the other way when it bumps against the edge of the map
            if (pos.X < 0 || pos.X > _game.Map.Width || pos.Y < 0 || pos.Y > _game.Map.Height) _speed = -(_speed);
            return new Vector(pos.X + _speed, pos.Y + _speed);
        }
    }

    public class Directional : IMovementEn
    {
        float _speed;
        readonly Ennemy _context;
        readonly ushort _type;
        Vector _destination;

        internal Directional(Ennemy context, float speed, Vector destination)
        {
            _speed = speed;
            _context = context ?? throw new NullReferenceException("Context is null.");
            _type = 1000;
            _destination = destination;
        }

        ushort IMovementEn.Type => _type;

        internal Vector Direction
        {
            get { return _destination; }
            set { _destination = value; }
        }

        Vector IMovementEn.Move(Vector pos)
        {
            float x = _destination.X - pos.X;
            float y = _destination.Y - pos.Y;

            if (Math.Sqrt(x * x + y * y) <= _speed)
                if (pos.X <= 100 || _context.Context.Map.Width + 100 <= pos.X || _context.Context.Map.Height + 100 <= pos.Y)
                {
                    _context.Kill();
                    return _destination;
                }
                else return _destination;
            else return new Vector(pos.X + (float)Math.Cos(Math.Atan2(y, x)) * _speed, pos.Y + (float)Math.Sin(Math.Atan2(y, x)) * _speed);
        }
    }    
}
