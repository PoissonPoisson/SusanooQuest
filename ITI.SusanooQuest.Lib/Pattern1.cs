using System;

namespace ITI.SusanooQuest.Lib
{
    public class Pattern1 : IPattern
    {
        readonly ILevel _context;
        IPattern _nextPattern;
        readonly DateTime _startPattern;
        ushort _step;

        internal Pattern1(ILevel context)
        {
            _context = context ?? throw new NullReferenceException("Context is null.");
            _nextPattern = this;
            _startPattern = DateTime.Now;
            _step = 0;
        }

        public IPattern NextPatern => _nextPattern;

        public void Update()
        {
            DateTime now = DateTime.Now;

            if (_step == 0 && now >= _startPattern.AddSeconds(5))
            {
                _context.Context.AddEnnemy(new Ennemy(new Vector(100, 80), 20, _context.Context, 25, 3, "standard") { Movement = new Standard(3, _context.Context) });
                _context.Context.AddEnnemy(new Ennemy(new Vector(100, 80), 20, _context.Context, 25, 3, "standard") { Movement = new Diagonal(3, _context.Context) });
                _step++;
            }
            else if (_step == 1 && now >= _startPattern.AddSeconds(10))
            {
                _step++;
            }
            else if (_step == 2 && now >= _startPattern.AddSeconds(15))
            {
                _step++;
            }
            else if (_step == 3 && now >= _startPattern.AddSeconds(20))
            {
                _step++;
            }
        }
    }
}
