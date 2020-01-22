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

            if (_step == 0 && now >= _startPattern.AddSeconds(3))
            {
                _context.Context.AddEnnemy(new Ennemy(new Vector(-100, 80), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(1100, 80));
                _context.Context.AddEnnemy(new Ennemy(new Vector(-200, 70), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(1100, 70));
                _context.Context.AddEnnemy(new Ennemy(new Vector(-300, 60), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(1100, 60));
                _context.Context.AddEnnemy(new Ennemy(new Vector(-400, 50), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(1100, 50));
                _step++;
            }
            else if (_step == 1 && now >= _startPattern.AddSeconds(8))
            {
                _context.Context.AddEnnemy(new Ennemy(new Vector(1000, 150), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(-200, 150));
                _context.Context.AddEnnemy(new Ennemy(new Vector(1100, 140), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(-200, 140));
                _context.Context.AddEnnemy(new Ennemy(new Vector(1200, 130), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(-200, 130));
                _context.Context.AddEnnemy(new Ennemy(new Vector(1300, 120), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(-200, 120));
                _step++;
            }
            else if (_step == 2 && now >= _startPattern.AddSeconds(12))
            {
                _context.Context.AddEnnemy(new Ennemy(new Vector(-100, 100), 20, _context.Context, 25, 4,"standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(1100, 500));
                _context.Context.AddEnnemy(new Ennemy(new Vector(-155, 80), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(1100, 500));
                _context.Context.AddEnnemy(new Ennemy(new Vector(-210, 60), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(1100, 500));
                _context.Context.AddEnnemy(new Ennemy(new Vector(-265, 40), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(1100, 500));
                _context.Context.AddEnnemy(new Ennemy(new Vector(-100, 50), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(1100, 450));
                _context.Context.AddEnnemy(new Ennemy(new Vector(-155, 30), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(1100, 450));
                _context.Context.AddEnnemy(new Ennemy(new Vector(-210, 10), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(1100, 450));
                _context.Context.AddEnnemy(new Ennemy(new Vector(-265, -10), 20, _context.Context, 25, 4,"standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(1100, 450));
                _step++;
            }
            else if (_step == 3 && now >= _startPattern.AddSeconds(17))
            {
                _context.Context.AddEnnemy(new Ennemy(new Vector(1000, 100), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(-200, 500));
                _context.Context.AddEnnemy(new Ennemy(new Vector(1055, 80), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(-200, 500));
                _context.Context.AddEnnemy(new Ennemy(new Vector(1110, 60), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(-200, 500));
                _context.Context.AddEnnemy(new Ennemy(new Vector(1165, 40), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(-200, 500));
                _context.Context.AddEnnemy(new Ennemy(new Vector(1000, 50), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(-200, 450));
                _context.Context.AddEnnemy(new Ennemy(new Vector(1055, 30), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(-200, 450));
                _context.Context.AddEnnemy(new Ennemy(new Vector(1110, 10), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(-200, 450));
                _context.Context.AddEnnemy(new Ennemy(new Vector(1165, -10), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(-200, 450));
                _step++;
            }
            else if (_step == 4 && now >= _startPattern.AddSeconds(25))
            {
                _context.Context.AddEnnemy(new Ennemy(new Vector(200, -100), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(200, 1200));
                _context.Context.AddEnnemy(new Ennemy(new Vector(200, -200), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(200, 1200));
                _context.Context.AddEnnemy(new Ennemy(new Vector(200, -300), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(200, 1200));
                _context.Context.AddEnnemy(new Ennemy(new Vector(200, -400), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(200, 1200));
                _context.Context.AddEnnemy(new Ennemy(new Vector(200, -500), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(200, 1200));
                _context.Context.AddEnnemy(new Ennemy(new Vector(700, -100), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(700, 1200));
                _context.Context.AddEnnemy(new Ennemy(new Vector(700, -200), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(700, 1200));
                _context.Context.AddEnnemy(new Ennemy(new Vector(700, -300), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(700, 1200));
                _context.Context.AddEnnemy(new Ennemy(new Vector(700, -400), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(700, 1200));
                _context.Context.AddEnnemy(new Ennemy(new Vector(700, -500), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(700, 1200));
                _step++;
            }
            else if (_step == 5 && now >= _startPattern.AddSeconds(30))
            {
                _context.Context.AddEnnemy(new Ennemy(new Vector(200, -100), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(200, 100));
                _step++;
            }
            else if (_step == 6 && now >= _startPattern.AddSeconds(30))
            {
                _context.Context.AddEnnemy(new Ennemy(new Vector(200, -100), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(200, 400));
                _step++;
            }
            else if (_step == 7 && now >= _startPattern.AddSeconds(31))
            {
                _context.Context.AddEnnemy(new Ennemy(new Vector(800, -100), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(800, 300));
                _context.Context.AddEnnemy(new Ennemy(new Vector(-100, 450), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(1100, 450));
                _context.Context.AddEnnemy(new Ennemy(new Vector(-150, 450), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(1100, 450));
                _context.Context.AddEnnemy(new Ennemy(new Vector(-200, 450), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(1100, 450));
                _context.Context.AddEnnemy(new Ennemy(new Vector(-250, 450), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(1100, 450));
                _context.Context.AddEnnemy(new Ennemy(new Vector(-300, 450), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(1100, 450));
                _context.Context.AddEnnemy(new Ennemy(new Vector(-350, 450), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(1100, 450));

                _step++;
            }
            else if (_step == 8 && now >= _startPattern.AddSeconds(32))
            {
                _context.Context.AddEnnemy(new Ennemy(new Vector(500, -100), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(500, 200));
                _step++;
            }
            else if (_step == 9 && now >= _startPattern.AddSeconds(33))
            {
                _context.Context.AddEnnemy(new Ennemy(new Vector(300, -100), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(300, 150));
                _step++;
            }
            else if (_step == 10 && now >= _startPattern.AddSeconds(34))
            {
                _context.Context.AddEnnemy(new Ennemy(new Vector(700, -100), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(700, 500));
                _context.Context.AddEnnemy(new Ennemy(new Vector(1000, 350), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(-200, 350));
                _context.Context.AddEnnemy(new Ennemy(new Vector(1050, 350), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(-200, 350));
                _context.Context.AddEnnemy(new Ennemy(new Vector(1100, 350), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(-200, 350));
                _context.Context.AddEnnemy(new Ennemy(new Vector(1150, 350), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(-200, 350));
                _context.Context.AddEnnemy(new Ennemy(new Vector(1200, 350), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(-200, 350));
                _context.Context.AddEnnemy(new Ennemy(new Vector(1250, 350), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(-200, 350));
                _step++;
            }
            else if (_step == 11) _nextPattern = new Pattern1(_context);
        }
    }
}
