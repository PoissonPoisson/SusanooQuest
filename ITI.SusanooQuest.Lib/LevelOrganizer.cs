using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.SusanooQuest.Lib
{
    public class LevelOrganizer
    {
        public List<Ennemy> _ennemiesAlive;
        public List<Ennemy> _ennemiesDeath;
        Player _player;
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

        public void LevelOne()
        {
            Ennemy e = new Ennemy(new Vector(100, 100), 25, _context, 100, 25, "1");
             
           _ennemiesAlive.Add(e);
           _ennemiesAlive.Add(e);
           _ennemiesAlive.Add(e);
           _ennemiesAlive.Add(e);
           _ennemiesAlive.Add(e);


        while (_i == 5)
            if (e.Life == 0 )
            {
              _ennemiesAlive.Remove(e);
              _ennemiesDeath.Add(e);
                    _i++;
            }
            _ennemiesDeath.Clear();
        }
       
    }
}
