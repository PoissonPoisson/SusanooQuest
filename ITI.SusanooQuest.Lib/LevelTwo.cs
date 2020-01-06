using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITI.SusanooQuest.Lib
{
    class LevelTwo : ILevels
    {
        readonly List<Ennemy> _ennemiesAlive;
        readonly List<Ennemy> _ennemiesDeath;

        Game _context;
        int _i = 0;
        bool _nextLevel = false;
        public List<Ennemy> Alive => _ennemiesAlive;

        public List<Ennemy> Dead => _ennemiesDeath;
        DateTime _startExecutionDate = DateTime.Now;
        
        public LevelTwo(List<Ennemy> ennemiesAlive, List<Ennemy> ennemiesDeath, Game ctx)
        {
            _ennemiesAlive = ennemiesAlive;
            _ennemiesDeath = ennemiesDeath;
            _context = ctx;
        }

           
               

        public void NextVague()
        {
           
                    if (_context.Ennemy.Count() == 0)
                    {
                        _context.CreateEnnemy(new Vector(100, 80), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(90, 70), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(80, 60), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(70, 50), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(60, 40), 20, _context, 25, 5, "standard");
                        _context.Update();
                    };
            TimeSpan duration = DateTime.Now.Subtract(_startExecutionDate);
            string _executionTime = string.Format(
                "Temps d'exécution : {0}h, {0}m, {12}s et {0}ms.",
                duration.Hours,
                duration.Minutes,
                duration.Seconds,
                duration.Milliseconds);
            
        }


        public void Boss()
        {
             _context.CreateEnnemy(new Vector(100, 100), 75, _context, 500, 8, "boss");
            _context.Update();
        }

        public void FirstBoss()
        {
             _context.CreateEnnemy(new Vector(100, 100), 50, _context, 100, 7, "boss");
            _context.Update();
        }

        public void NextLevel()
        {
            
        }

       
    }
}
