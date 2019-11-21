using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.SusanooQuest.Lib
{
    interface IMovement
    {
        void move();
    }
    public class Projectile
    {
        Vector _origin;
        Vector _pos;
        Vector _speed;
        Entity _shooter;
        float _length;
        int _damage;
        IMovement _movement;

        Projectile (Vector speed, int damage, float length, Vector origin)
        {
            _origin = origin;
            _pos = new Vector(0, 0);
            _speed = speed;
            _length = length;
            _damage = damage;
        }
    }
}
