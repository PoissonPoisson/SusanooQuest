using System;
using System.Collections.Generic;

namespace ITI.SusanooQuest.Lib.EnnemyPattern
{
    internal class PatternBoss1 : IPattern
    {
        Ennemy _boss;
        readonly ILevel _context;
        IPattern _nextPattern;
        DateTime _startPattern;
        ushort _step;

        internal PatternBoss1(ILevel context)
        {
            _context = context ?? throw new NullReferenceException("Context is null.");
            _nextPattern = this;
            _step = 0;
            _startPattern = DateTime.Now;
        }

        public IPattern NextPatern => _nextPattern;

        public void Update()
        {
            DateTime now = DateTime.Now;

            if (_step == 0 && now >= _startPattern.AddSeconds(5))
            {
                _boss = new Ennemy(new Vector(450, -100), 10, _context.Context, 10000, 5, "boss");
                _boss.Movement = new Directional(_boss, 5, new Vector(150, 300), 200);
                _step++;
                _startPattern = now;
            }
            else if (_step == 0 && (now >= _startPattern.AddSeconds(20) || _boss.Life <= 9000))
            {
                _boss.Movement.Direction = new Vector(200, 300);
            }
        }
    }
}