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

        public Game()
        {
            _ennemies = new List<Ennemy>();
            _player = new Player(new Vector(0, 0), 3, this);
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
                ennemy.Update();
            }

            _player.Update(taille, Vector direction);
            return _player.Life == 0;


        }

        internal Vector GetRandomPosition()
        {
            return new Vector(GetNextRandomDouble(0, 900), GetNextRandomDouble(0, 1000));
        }

        internal double GetNextRandomDouble(double min, double max)
        {
            return _random.NextDouble() * (max - min) + min;
        }

        public List<Ennemy> Ennemy
        {
            get { return _ennemies; }
        }

        public Vector PlayerDirection
        {
            get { return _direction}
            set
            {
                _direction = value;

            }
        }

        public Player Player => _player;
    }
}