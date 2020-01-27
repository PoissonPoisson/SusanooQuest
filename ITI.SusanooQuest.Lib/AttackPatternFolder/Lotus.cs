using System;

namespace ITI.SusanooQuest.Lib.AttackPatternFolder
{
    internal class Lotus : IAttackPattern
    {
        readonly IEnnemy _context;
        DateTime _lastShoot;

        internal Lotus(IEnnemy context)
        {
            _context = context ?? throw new NullReferenceException("Context is null.");
            _lastShoot = DateTime.Now;
        }

        void IAttackPattern.Update()
        {
            if (DateTime.Now >= _lastShoot.AddSeconds(1))
            {
                for (int i = 0; i < 360; i += 30)
                {
                    _context.Context.AddProjectile(new Projectile(5, 1, _context.Position, (Ennemy)_context, "standar"));
                }
            }
        }
    }
}
