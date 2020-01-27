using System;

namespace ITI.SusanooQuest.Lib
{
    internal class Pattern1 : IPattern
    {
        readonly ILevel _context;
        IPattern _nextPattern;
        readonly DateTime _startPattern;
        ushort _step;
        Random _random;

        internal Pattern1(ILevel context)
        {
            _context = context ?? throw new NullReferenceException("Context is null.");
            _nextPattern = this;
            _startPattern = DateTime.Now;
            _step = 0;
            _random = new Random();
        }

        public IPattern NextPatern => _nextPattern;

        public void Update()
        {
            DateTime now = DateTime.Now;

            if (_step == 0 && now >= _startPattern.AddSeconds(3))
            {
                for (int i = 1; i <= 4; i++)
                {
                    _context.Context.AddEnnemy(new Ennemy(new Vector(-i * 100, 90 - i * 10), 20, _context.Context, 25, 4, "standard"));
                    _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(1100, 90 - i * 10), 1000);
                }
                _step++;
            }
            else if (_step == 1 && now >= _startPattern.AddSeconds(8))
            {
                for (int i = 0; i <= 3; i++)
                {
                    _context.Context.AddEnnemy(new Ennemy(new Vector(1000 + i * 100, 150 - i * 10), 20, _context.Context, 25, 4, "standard"));
                    _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(-200, 150 -i * 10), 1000);
                }
                _step++;
            }
            else if (_step == 2 && now >= _startPattern.AddSeconds(12))
            {
                for (int i = 0; i < 5; i++)
                {
                    _context.Context.AddEnnemy(new Ennemy(new Vector(-100 - (5.5f * 10 * i), 100 - i * 20), 20, _context.Context, 25, 4, "standard"));
                    _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(1100, 500), 1000);
                    _context.Context.AddEnnemy(new Ennemy(new Vector(-100 - (5.5f * 10 * i), 100 - 50 - i * 20), 20, _context.Context, 25, 4, "standard"));
                    _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(1100, 450), 1000);
                }
                _step++;
            }
            else if (_step == 3 && now >= _startPattern.AddSeconds(17))
            {
                for (int i = 0; i < 5; i++)
                {
                    _context.Context.AddEnnemy(new Ennemy(new Vector(1000 + (5.5f * 10 * i), 100 - i * 20), 20, _context.Context, 25, 4, "standard"));
                    _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(-200, 500), 1000);
                    _context.Context.AddEnnemy(new Ennemy(new Vector(1000 + (5.5f * 10 * i), 100 - 50 - i * 20), 20, _context.Context, 25, 4, "standard"));
                    _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(-200, 450), 1000);
                }
                _step++;
            }
            else if (_step == 4 && now >= _startPattern.AddSeconds(25))
            {
                for (int i = 1; i <= 5; i++)
                {
                    _context.Context.AddEnnemy(new Ennemy(new Vector(200, -i * 100), 20, _context.Context, 25, 4, "standard"));
                    _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(200, 1200), 1000);
                    _context.Context.AddEnnemy(new Ennemy(new Vector(700, -i * 100), 20, _context.Context, 25, 4, "standard"));
                    _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(700, 1200), 1000);
                }
                _step++;
            }
            else if (_step == 5 && now >= _startPattern.AddSeconds(30))
            {
                _context.Context.AddEnnemy(new Ennemy(new Vector(200, -100), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(200, 100), 1000);
                _step++;
            }
            else if (_step == 6 && now >= _startPattern.AddSeconds(30))
            {
                _context.Context.AddEnnemy(new Ennemy(new Vector(200, -100), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(200, 400), 1000);
                _step++;
            }
            else if (_step == 7 && now >= _startPattern.AddSeconds(31))
            {
                _context.Context.AddEnnemy(new Ennemy(new Vector(800, -100), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(800, 300), 1000);

                   for (int i = 0; i < 6; i++)
                {
                    _context.Context.AddEnnemy(new Ennemy(new Vector(-100 - i * 50, 450), 20, _context.Context, 25, 4, "standard"));
                    _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(1100, 450), 1000);
                }
                _step++;
            }
            else if (_step == 8 && now >= _startPattern.AddSeconds(32))
            {
                _context.Context.AddEnnemy(new Ennemy(new Vector(500, -100), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(500, 200), 1000);
                _step++;
            }
            else if (_step == 9 && now >= _startPattern.AddSeconds(33))
            {
                _context.Context.AddEnnemy(new Ennemy(new Vector(300, -100), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(300, 150), 1000);
                _step++;
            }
            else if (_step == 10 && now >= _startPattern.AddSeconds(34))
            {
                _context.Context.AddEnnemy(new Ennemy(new Vector(700, -100), 20, _context.Context, 25, 4, "standard"));
                _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(700, 500), 1000);

                for (int i = 0; i < 6; i++)
                {
                    _context.Context.AddEnnemy(new Ennemy(new Vector(1000 + i * 50, 350), 20, _context.Context, 25, 4, "standard"));
                    _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(-200, 350), 1000);
                } 
                _step++;
            }
            else if (_step == 11 && now >= _startPattern.AddSeconds(40))
            {
                for (int i = 0; i < 8; i++)
                {
                    int posX = _random.Next(100, 800);
                    _context.Context.AddEnnemy(new Ennemy(new Vector(posX, _random.Next(-700, -100)), 20, _context.Context, 25, 4, "standard"));
                    _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(posX, _random.Next(100, 300)), 1000);
                }
                _step++;
            }
            else if (_step == 12 && now >= _startPattern.AddSeconds(45))
            {
                for (int i = 0; i < 800; i += 100)
                {
                    _context.Context.AddEnnemy(new Ennemy(new Vector(50 + i, -100 - i), 20, _context.Context, 25, 4, "standard"));
                    _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(50 + i, 1300), 1000);
                }
                for (int i = 0; i < 800; i += 100)
                {
                    _context.Context.AddEnnemy(new Ennemy(new Vector(850 - i, -900 - i), 20, _context.Context, 25, 4, "standard"));
                    _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(850 - i, 1300), 1000);
                }
                for (int i = 0; i < 800; i += 100)
                {
                    _context.Context.AddEnnemy(new Ennemy(new Vector(50 + i, -1700 - i), 20, _context.Context, 25, 4, "standard"));
                    _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(50 + i, 1300), 1000);
                }
                for (int i = 0; i < 800; i += 100)
                {
                    _context.Context.AddEnnemy(new Ennemy(new Vector(850 - i, -2500 - i), 20, _context.Context, 25, 4, "standard"));
                    _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(850 - i, 1300), 1000);
                }
                for (int i = 0; i < 800; i += 100)
                {
                    _context.Context.AddEnnemy(new Ennemy(new Vector(50 + i, -3300 - i), 20, _context.Context, 25, 4, "standard"));
                    _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(50 + i, 1300), 1000);
                }
                for (int i = 0; i <= 800; i += 100)
                {
                    _context.Context.AddEnnemy(new Ennemy(new Vector(850 - i, -4100 - i), 20, _context.Context, 25, 4, "standard"));
                    _context.Context.Ennemy[_context.Context.Ennemy.Count - 1].Movement = new Directional(_context.Context.Ennemy[_context.Context.Ennemy.Count - 1], 4, new Vector(850 - i, 1300), 1000);
                }
                _step++;
            }
        }
    }
}