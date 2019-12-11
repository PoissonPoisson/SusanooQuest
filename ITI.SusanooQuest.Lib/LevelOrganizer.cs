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
            switch (_i)
            {
                case 0:
                    if (_context.Ennemy.Count() == 0) VagueOne();
                    _i++;
                    break;
                case 1:
                    if (_context.Ennemy.Count() == 0) VagueOne();
                    _i++;
                    break;
                case 2:
                    if (_context.Ennemy.Count() == 0) VagueTwo();
                    _i++;
                    break;
                case 3:
                    if (_context.Ennemy.Count() == 0) VagueTwo();
                    _i++;
                    break;
                case 4:
                    if (_context.Ennemy.Count() == 0) FirstBoss();
                    _i++;
                    break;
            }
        }

        private void VagueOne()
        {
            _ennemiesAlive.Add(_context.CreateEnnemy(new Vector(100, 80), 20, _context, 25, 5, "standard"));
            _ennemiesAlive.Add(_context.CreateEnnemy(new Vector(90, 70), 20, _context, 25, 5, "standard"));
            _ennemiesAlive.Add(_context.CreateEnnemy(new Vector(80, 60), 20, _context, 25, 5, "standard"));
            _ennemiesAlive.Add(_context.CreateEnnemy(new Vector(70, 50), 20, _context, 25, 5, "standard"));
            _ennemiesAlive.Add(_context.CreateEnnemy(new Vector(60, 40), 20, _context, 25, 5, "standard"));
            _context.Update();
        }
        private void VagueTwo()
        {
            _ennemiesAlive.Add(_context.CreateEnnemy(new Vector(100, 80), 20, _context, 25, 5, "standard"));
            _ennemiesAlive.Add(_context.CreateEnnemy(new Vector(90, 70), 20, _context, 25, 5, "standard"));
            _ennemiesAlive.Add(_context.CreateEnnemy(new Vector(80, 60), 20, _context, 25, 5, "standard"));
            _ennemiesAlive.Add(_context.CreateEnnemy(new Vector(70, 50), 20, _context, 25, 5, "standard"));
            _ennemiesAlive.Add(_context.CreateEnnemy(new Vector(60, 40), 20, _context, 25, 5, "standard"));
            _ennemiesAlive.Add(_context.CreateEnnemy(new Vector(80, 100), 20, _context, 25, 5, "standard"));
            _ennemiesAlive.Add(_context.CreateEnnemy(new Vector(70, 90), 20, _context, 25, 5, "standard"));
            _ennemiesAlive.Add(_context.CreateEnnemy(new Vector(60, 80), 20, _context, 25, 5, "standard"));
            _ennemiesAlive.Add(_context.CreateEnnemy(new Vector(50, 70), 20, _context, 25, 5, "standard"));
            _ennemiesAlive.Add(_context.CreateEnnemy(new Vector(40, 60), 20, _context, 25, 5, "standard"));
            _context.Update();
        }
        private void FirstBoss()
        {
            _ennemiesAlive.Add(_context.CreateEnnemy(new Vector(100, 100), 50, _context, 100, 7, "boss"));
            _context.Update();
        }

    }
}
