using System;
using System.Collections.Generic;

namespace ITI.SusanooQuest.Lib
{
    public class Game
    {
        #region Fields

        readonly List<Ennemy> _ennemies;
        readonly List<IProjectile> _projectiles;
        readonly Player _player;
        readonly Random _random;
        Map _map;
        uint _highScore;
        uint _score;
        ushort _bombes;

        #endregion

        public Game(ushort playerLife ,ushort bombes, uint highScore)
        {
            _ennemies = new List<Ennemy>();
            _map = new Map(900, 1000);
            _player = new Player(new Vector(_map.Width / 2, _map.Height - 100), 5, this, playerLife, 5);
            _random = new Random();
            _highScore = highScore;
            _projectiles = new List<IProjectile>();
            _bombes = bombes;
            _score = 0;
        }

        #region Methodes

        internal void OnKill(Ennemy ennemy)
        {
            _ennemies.Remove(ennemy);
        }

        public Ennemy Create_Ennemy(Vector pos, float length, Game game, ushort live, float speed)
        {
            Ennemy ennemy = new Ennemy(pos, length, game, live, speed);
            _ennemies.Add(ennemy);
            return ennemy;
        }

        public bool Update()
        {
            foreach (Ennemy ennemy in _ennemies) ennemy.Update();

            _player.Update();
            if (_highScore < _score) _highScore = _score;
            return _player.Life == 0;
        }
        public void OnClearProjectil()
        {
            _projectiles.Clear();
            _bombes--;
            Console.WriteLine("BOOOOOOOOOM!");
        }
        public void EndClearProjectil()
        {
            Console.WriteLine("a plus de projectiles");
        }

        #endregion

        #region Properties

        public List<Ennemy> Ennemy
        {
            get { return _ennemies; }
        }


        public Player Player => _player;

        public Map Map => _map;

        public uint HighScore => _highScore;

        public uint Score => _score;

        public ushort Bombes => _bombes;

        #endregion
    }
}