using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.SusanooQuest.Lib
{
    class HomingPattern : IAttackPattern
    {
        readonly Ennemy _context;
        Vector _target;
        DateTime _lastShot;
        readonly Dictionary<string, string> _ennemyTypeProjectile;

        public HomingPattern(Ennemy context)
        {
            _context = context;
            _lastShot = DateTime.Now;
            _ennemyTypeProjectile = new Dictionary<string, string>();
            _ennemyTypeProjectile.Add("fireL", "fire");
            _ennemyTypeProjectile.Add("fireR", "fire");
            _ennemyTypeProjectile.Add("waterL", "water");
            _ennemyTypeProjectile.Add("waterR", "water");
            _ennemyTypeProjectile.Add("dirtL", "dirt");
            _ennemyTypeProjectile.Add("dirtR", "dirt");
        }

        void IAttackPattern.Update()
        {
            if(DateTime.Now > _lastShot.AddSeconds(0.5))
            {
                _target = _context.Context.Player.Position;
                float a = (_target.Y - _context.Position.Y) / (_target.X - _context.Position.X);
                bool dir = _context.Position.X > _context.Context.Player.Position.X;
                Vector u = new Vector(_target.X - _context.Position.X, _target.Y - _context.Position.Y);
                float norm = (float)Math.Sqrt(Math.Pow(u.X, 2) + Math.Pow(u.Y, 2));
                Vector vu = new Vector(u.X / norm, u.Y / norm);
                _context.Context.AddProjectile(new Projectile(5, 1, new Vector(_context.Position.X, _context.Position.Y), _context, _ennemyTypeProjectile[_context.Tag]) { Movement = new Ax(5, vu, true) });
                _lastShot = DateTime.Now;
            }
        }
    }
}
