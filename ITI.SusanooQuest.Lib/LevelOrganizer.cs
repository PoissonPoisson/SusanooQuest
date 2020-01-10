using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITI.SusanooQuest.Lib
{
    public class LevelOrganizer
    {
        readonly List<Ennemy> _ennemiesAlive;
        readonly List<Ennemy> _ennemiesDeath;
        Player _player;
        Game _context;
        int _i = 0;
        Ennemy _ennemy;


        public List<Ennemy> Alive => _ennemiesAlive;
        public List<Ennemy> Die => _ennemiesDeath;

        public LevelOrganizer(List<Ennemy> ennemiesAlive, List<Ennemy> ennemiesDeath, Player player, Game ctx)
        {
            _ennemiesAlive = ennemiesAlive;
            _ennemiesDeath = ennemiesDeath;
            _player = player;
            _context = ctx;

        }

        public void LevelOne()
        {
            if (_context.Ennemy.Count() == 0) VagueOne() ;           
        }

        private void VagueOne()
        {
            _context.CreateEnnemy(new Vector(100, 80), 8, _context, 25, 3, "standard");
            _context.CreateEnnemy(new Vector(90, 70), 8, _context, 25, 3, "diagonal");
            _context.CreateEnnemy(new Vector(80, 60), 8, _context, 25, 3, "standard");
            _context.CreateEnnemy(new Vector(70, 50), 8, _context, 25, 3, "standard");
            _context.CreateEnnemy(new Vector(60, 40), 8, _context, 25, 3, "standard");
        }
    }
}
