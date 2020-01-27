using Newtonsoft.Json.Linq;
using System;


namespace ITI.SusanooQuest.Lib
{
    interface IMovementEn
    {
        Vector Move(Vector pos);
        internal ushort Type { get; }
        internal Vector Direction { get; set; }
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
            if (Attack != null) Attack.Update();
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

    public class Directional : IMovementEn
    {
        float _speed;
        readonly Ennemy _context;
        readonly ushort _type;
        Vector _destination;

        internal Directional(Ennemy context, float speed, Vector destination, ushort points)
        {
            _speed = speed;
            _context = context ?? throw new NullReferenceException("Context is null.");
            _type = points;
            _destination = destination;
        }

        ushort IMovementEn.Type => _type;

        Vector IMovementEn.Direction
        {
            get => _destination;
            set => _destination = value;
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
