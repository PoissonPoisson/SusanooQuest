using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.SusanooQuest.Lib
{
    class FourBulletStraight : IAttackPattern
    {
        readonly Ennemy _context;
        DateTime _startShot;
        ushort _step;
        readonly Dictionary<string, string> _ennemyTypeProjectile;

        public FourBulletStraight (Ennemy context)
        {
            _context = context;
            _startShot = DateTime.Now;
            _step = 1;
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

            if (DateTime.Now > _startShot.AddSeconds(1.4) && _step == 5)
            {
                _startShot = DateTime.Now;
                _step = 1;
            }
            else if(DateTime.Now > _startShot.AddSeconds(0.4) && _step == 4)
            {
                _context.Context.CreateProjectile(2, 1, new Vector(_context.Position.X, _context.Position.Y), _context, _ennemyTypeProjectile[_context.Tag]);
                _step++;
            } else if (DateTime.Now > _startShot.AddSeconds(0.3) && _step == 3)
            {
                _context.Context.CreateProjectile(2, 1, new Vector(_context.Position.X, _context.Position.Y), _context, _ennemyTypeProjectile[_context.Tag]);
                _step++;
            }
            else if (DateTime.Now > _startShot.AddSeconds(0.2) && _step == 2)
            {
                _context.Context.CreateProjectile(2, 1, new Vector(_context.Position.X, _context.Position.Y), _context, _ennemyTypeProjectile[_context.Tag]);
                _step++;
            }
            else if (DateTime.Now > _startShot.AddSeconds(0.1) && _step == 1)
            {
                _context.Context.CreateProjectile(2, 1, new Vector(_context.Position.X, _context.Position.Y), _context, _ennemyTypeProjectile[_context.Tag]);
                _step++;
            }
            

        }
    }
}
