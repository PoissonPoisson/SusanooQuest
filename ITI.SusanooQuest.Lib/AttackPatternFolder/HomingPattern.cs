using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.SusanooQuest.Lib.AttackPatternFolder
{
    class HomingPattern : IAttackPattern
    {
        Ennemy _context;
        Vector _target;
        DateTime _lastShot;
        public HomingPattern(Ennemy context)
        {
            _context = context;
            _lastShot = DateTime.Now;
        }

        void IAttackPattern.Update()
        {
            if(DateTime.Now > _lastShot.AddSeconds(5))
            {
                _target = _context.Context.Player.Position;
                float a = (_target.Y - _context.Position.Y) / (_target.X - _context.Position.X);
                bool dir = _context.Position.X > _context.Context.Player.Position.X;
                Vector u = new Vector(_target.X - _context.Position.X, _target.Y - _context.Position.X);
                float norm = (float)Math.Sqrt(Math.Pow(u.X, 2) + Math.Pow(u.Y, 2));
                Vector vu = new Vector(u.X / norm, u.Y / norm);
                _context.Context.AddProjectile(new Projectile(5, 1, new Vector(_context.Position.X, _context.Position.Y), _context, "Homing") { Movement = new Homing(5, a, dir, _context, vu) });
                _lastShot = DateTime.Now;
            }
        }
    }
}
