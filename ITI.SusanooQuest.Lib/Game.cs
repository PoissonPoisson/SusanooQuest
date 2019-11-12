using System;
using System.Collections.Generic;

namespace ITI.SusanooQuest.Lib
{
    public class Game
    {
        readonly List<Ennemy> _ennemies;
        readonly Player _player;
        readonly Random _random;
        Vector _direction;
        Map _map;

        public Game()
        {
            _ennemies = new List<Ennemy>();
            _map = new Map(900, 1000);
            _player = new Player(new Vector(_map.Width / 2, _map.Height - 100), 3, this, _map.Width, _map.Height);
            _random = new Random();
        }

        internal void OnKill(Ennemy ennemy)
        {
            _ennemies.Remove(ennemy);
        }

        public Ennemy Create_Ennemy(Vector pos, float length, Game game, ushort live)
        {
            Ennemy ennemy = new Ennemy(pos, length, game, live);
            _ennemies.Add(ennemy);
            return ennemy;
        }

        public bool Update()
        {
            foreach (Ennemy ennemy in _ennemies)
            {
                //ennemy.Update();
            }

            _player.Update();
            return _player.Life == 0;


        }


        public List<Ennemy> Ennemy
        {
            get { return _ennemies; }
        }

        public Vector PlayerDirection
        {
            get { return _direction; }
            set
            {
                _direction = value;

            }
        }

        public Player Player => _player;

        public Map Map => _map;
    }
}