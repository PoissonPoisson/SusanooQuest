using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.SusanooQuest.Lib
{
    interface IMovement
    {
        Vector Move(Vector pos);
    }
    public class Projectile
    {
        Vector _origin;
        Vector _pos;
        double _speed;
        Entity _shooter;
        float _length;
        int _damage;
        string _tag;
        IMovement _movement;

        internal Projectile(double speed, int damage, Vector origin, string tag)
        {
            _origin = origin;
            _pos = new Vector(0, 0);
            _speed = speed;
            _damage = damage;
            _tag = tag;
        }

        internal void Update()
        {
            _pos = _movement.Move(_pos);
        }

        internal IMovement Movement
        {
            set { _movement = value; }
            get { return _movement; }
        }

        internal double Speed => _speed;

        public Vector Position
        {
            get { return new Vector(_pos.X + _origin.X, _pos.Y + _origin.Y); }
        }
        public string Tag => _tag;
    }

    public class Y : IMovement
    {
        double _step;
        internal Y (double step)
        {
            _step = step;
        }
        public Vector Move(Vector pos)
        {
            double x = pos.X;
            double y = pos.Y;

            x = 0;
            y -= _step;

            return new Vector(Convert.ToSingle(x), Convert.ToSingle(y));
        }

        
    }

    class CosY : IMovement
    {
        double _step;
        internal CosY(double step)
        {
            _step = step;
        }

        public Vector Move(Vector pos)
        {
            double x = pos.X;
            double y = pos.Y;
            
            x = Convert.ToSingle(200*Math.Cos(Convert.ToDouble(y)/50));
            y += _step;

            return new Vector(Convert.ToSingle(x), Convert.ToSingle(y));
        }
    }
}
