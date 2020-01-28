using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.SusanooQuest.Lib
{
    public interface IMovement
    {
        Vector Move(Vector pos);
        public float Length { get; }
    }
    public class Projectile
    {
        #region fields
        IMovement _movement;
        Vector _pos;
        readonly Vector _origin;
        readonly Entity _shooter;
        readonly double _speed;
        readonly int _damage;
        readonly string _tag;
        #endregion

        public Projectile(double speed, int damage, Vector origin, Entity shooter, string tag)
        {
            if (speed < 0 || damage < 0) throw new ArgumentException("Must be superior to 0");
            if (shooter == null) throw new ArgumentNullException();

            _origin = origin;
            _pos = new Vector(0, 0);
            _speed = speed;
            _damage = damage;
            _tag = tag;
            _shooter = shooter;
        }

        internal void Update()
        {
            _pos = _movement.Move(_pos);
        }

        public IMovement Movement
        {
            internal set { _movement = value; }
            get { return _movement; }
        }

        internal double Speed => _speed;

        public Vector Position
        {
            get
            {
                if (_tag == "CosY") return new Vector(_pos.X + _origin.X, _pos.Y + _origin.Y + 79);
                return new Vector(_pos.X + _origin.X, _pos.Y + _origin.Y);
            }
        }
        public string Tag => _tag;

        public Entity Shooter => _shooter;

        internal Vector Origin => _origin;

        internal int Damage => _damage;
    }

    public class Y : IMovement
    {
        readonly float _length;
        double _step;

        internal Y(double step)
        {
            _length = 30;
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

        float IMovement.Length => _length;
    }

    class CosY : IMovement
    {
        readonly float _length;
        double _step;

        internal CosY(double step)
        {
            _length = 5;
            _step = step;
        }

        Vector IMovement.Move(Vector pos)
        {

            double x = pos.X;
            double y = pos.Y;
            //Décalage pour que le projectile apparaisse bien devant le tireur
            if (y == 0) y = -79;
            x = Convert.ToSingle(200 * Math.Cos(Convert.ToDouble(y) / 50));
            y += _step;
            return new Vector(Convert.ToSingle(x), Convert.ToSingle(y));
        }

        float IMovement.Length => _length;
    }

    class Ax : IMovement
    {
        readonly float _length;
        float _step;
        Vector _u;
        bool _dir;
        

        internal Ax(float step, Vector u, bool dir)
        {
            _dir = dir;
            _length = 5;
            _step = step;
            _u = u.Multiply(_step);
        }

        Vector IMovement.Move(Vector pos)
        {
            if (_dir)
            {
                pos = pos.Add(_u);
            } else
            {
                pos = pos.Sub(_u);
            }
            return pos;
        }

        float IMovement.Length => _length;
    }
}
