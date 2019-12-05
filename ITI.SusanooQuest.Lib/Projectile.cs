﻿using System;
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
        IMovement _movement;
        Vector _pos;
        readonly Vector _origin;
        readonly Entity _shooter;
        readonly double _speed;
        readonly float _length;
        readonly int _damage;
        readonly string _tag;
        

        internal Projectile(double speed, int damage, Vector origin, Entity shooter, string tag)
        {
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

        internal IMovement Movement
        {
            set { _movement = value; }
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
        Vector _origin;
        internal CosY(double step, Vector origin)
        {
            _step = step;
            _origin = origin;
        }

        public Vector Move(Vector pos)
        {
            
            double x = pos.X;
            double y = pos.Y;
            if (y == 0) y = -79;
            x = Convert.ToSingle(200*Math.Cos(Convert.ToDouble(y)/50));
            //Console.WriteLine("x : "+x+" || y: "+y);
            y += _step;
            return new Vector(Convert.ToSingle(x), Convert.ToSingle(y));
        }
    }
}
