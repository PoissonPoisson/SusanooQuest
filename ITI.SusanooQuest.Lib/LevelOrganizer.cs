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
        ILevels _level;
        Game _context;
        int _i = 0;



        public List<Ennemy> Alive => _ennemiesAlive;
        public List<Ennemy> Die => _ennemiesDeath;

   
        public LevelOrganizer(List<Ennemy> ennemiesAlive, List<Ennemy> ennemiesDeath, Player player, Game ctx)
        {
            _ennemiesAlive = ennemiesAlive;
            _ennemiesDeath = ennemiesDeath;
  
            _player = player;
            _context = ctx;

        }

        public void Level()
        {
            LevelOne levelone = new LevelOne(_ennemiesAlive, _ennemiesDeath, _context);
            levelone.NextVague();
            if (levelone.NextLevel() == true)
            {
                
            }
        }
    }
}

