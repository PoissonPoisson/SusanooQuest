using System;

namespace ITI.SusanooQuest.Lib
{
    class Double : IAttackPattern
    {
        Ennemy _context;
        Vector _target;
        DateTime _lastShot;
        public Double(Ennemy context)
        {
            _context = context;
            _lastShot = DateTime.Now;
        }

        void IAttackPattern.Update()
        {
            if (DateTime.Now > _lastShot.AddSeconds(1))
            {
                _target = _context.Context.Player.Position;
                Vector u1 = new Vector(1, 5);
                Vector u2 = new Vector(1, -5);
                float norm1 = (float)Math.Sqrt(Math.Pow(u1.X, 2) + Math.Pow(u1.Y, 2));
                float norm2 = (float)Math.Sqrt(Math.Pow(u2.X, 2) + Math.Pow(u2.Y, 2));
                Vector vu1 = new Vector(u1.X / norm1, u1.Y / norm1);
                Vector vu2 = new Vector(u2.X / norm2, u2.Y / norm2);
                _context.Context.AddProjectile(new Projectile(5, 1, new Vector(_context.Position.X, _context.Position.Y), _context, "cosY") 
                    { Movement = new Ax(5, vu1, true) });
                _context.Context.AddProjectile(new Projectile(5, 1, new Vector(_context.Position.X, _context.Position.Y), _context, "cosY") 
                    { Movement = new Ax(5, vu2, false) });
                _lastShot = DateTime.Now;
            }
        }
    }
}
