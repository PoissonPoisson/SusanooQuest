using System;

namespace ITI.SusanooQuest.Lib
{
    public class StandardEnnemy : Entity, IEnnemy
    {
        readonly Game _context;
        Vector _destination;
        Vector _position;
        //IAttackPattern _attackPattern;

        internal StandardEnnemy(Game context, float length, int life, Vector position, Vector destination, float speed)//, IPatternAttack patternAttack)
            : base (position, length, context, life, speed)
        {
            _context = context ?? throw new NullReferenceException("Context is null.");
            _life = life;
            _position = position;
            _destination = destination;
            _speed = speed;
        }

        public Game Context => _context;

        public string Tag => "standard";

        internal Vector Destination
        {
            get { return _destination; }
            set { _destination = value; }
        }

        float IEnnemy.Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public void ChangeDirection(Vector direction)
        {
            _destination = direction;
        }

        void IEnnemy.Update()
        {
            float x = _destination.X - _position.X;
            float y = _destination.Y - _position.Y;

            if (Math.Sqrt(x * x + y * y) > _speed) _position = _destination;
            else _position = _position.Add(new Vector((float)Math.Cos(Math.Atan2(y, x)) * _speed, (float)Math.Sin(Math.Atan2(y, x)) * _speed));
        }
    }
}
