using System;

namespace ITI.SusanooQuest.Lib
{
    public class LevelOne : ILevel
    {
        readonly Game _context;
        ILevel _nextLevel;
        IPattern _pattern;

        internal LevelOne(Game context)
        {
            _context = context ?? throw new NullReferenceException("Context is null.");
            _nextLevel = this;
            _pattern = new Pattern1(this);
        }

        public Game Context => _context;

        public ILevel NextLevel => _nextLevel;

        public IPattern Pattern => _pattern;

        public void Update()
        {
            _pattern = _pattern.NextPatern;
            _pattern.Update();
        }
    }
}
