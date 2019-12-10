using System;
using System.Collections.Generic;

namespace ITI.SusanooQuest.Lib
{
    public class Game
    {
        #region Fields
        //readonly Dictionary<string,Dictionary<string,>>
        public List<Ennemy> _ennemies;
        public List<Ennemy> _death;
        readonly List<Projectile> _projectiles;
        readonly List<Projectile> _projectilesToDel;
        Player _player;
        readonly Random _random;
        Map _map;
        uint _highScore;
        uint _score;
        ushort _bombes;
        ushort _cd;

        #endregion

        public Game(ushort playerLife ,ushort bombes, uint highScore)
        {
            _ennemies = new List<Ennemy>();
            _death = new List<Ennemy>();
            _map = new Map(900, 1000);
            _player = new Player(new Vector(_map.Width / 2, _map.Height - 100), 5, this, playerLife, 5);
            _random = new Random();
            _highScore = highScore;
            _projectiles = new List<Projectile>();
            _projectilesToDel = new List<Projectile>();
            _bombes = bombes;
            _score = 0;
            _cd = 1;

        }

        

        

        public bool Update()
        {
            if (_ennemies.Count < 1) CreateEnnemy(new Vector(100, 80), 8, this, 100, 5, "standard");
            foreach (Ennemy ennemy in _ennemies) ennemy.Update();
            
            if (_player.OnShoot)
            {
                _cd--;
                if (_cd == 0)
                {
                    _cd = 10;
                    CreateProjectile(10, _player.Strength, new Vector(_player.Position.X + _player.Length, _player.Position.Y + _player.Length), _player, "Y");
                }
            }

            foreach (Projectile projectile in _projectiles)
            {
                projectile.Update();
                if (projectile.Position.Y > _map.Height || projectile.Position.Y < -20) _projectilesToDel.Add(projectile);
            }
            if (_projectilesToDel.Count != 0)
            {
                foreach (Projectile projectile in _projectilesToDel) _projectiles.Remove(projectile);
                _projectilesToDel.Clear();
            }
            _player.Update();
            if (_highScore < _score) _highScore = _score;
            return _player.Life == 0;
        }

        public Ennemy CreateEnnemy(Vector pos, float length, Game game, ushort life, float speed, string tag)
        {
            Ennemy ennemy = new Ennemy(pos, length, game, life, speed, tag);
            _ennemies.Add(ennemy);
            return ennemy;
        }



        public LevelOrganizer CreateLevel()
        {
            LevelOrganizer levelone = new LevelOrganizer(_ennemies, _death, Player, this);
            levelone.LevelOne();
            return levelone;
        }
               

        internal void CreateProjectile(double speed, int damage, Vector origin, Entity shooter, string type)
        {
            Projectile projec = new Projectile(speed, damage, origin, shooter, type);

            switch (type)
            {
                case "Y":
                    projec.Movement = new Y(projec.Speed);
                    break;
                case "CosY":
                    projec.Movement = new CosY(projec.Speed, projec.Origin);
                    break;
            }

            _projectiles.Add(projec);
        }

        internal void OnKill(Ennemy ennemy)
        {
            _ennemies.Remove(ennemy);
        }
        public void OnClearProjectil()
        {
            for (int i = _projectiles.Count-1; i >= 0; i--) if (_projectiles[i].Shooter != _player) _projectiles.Remove(_projectiles[i]);
            //foreach (Projectile projectile in _projectiles) if (projectile.Shooter != _player) _projectiles.Remove(projectile);
            _bombes--;
            Console.WriteLine("BOOOOOOOOOM!");
        }
        public void EndClearProjectil()
        {
            Console.WriteLine("a plus de projectiles");
        }

       
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

        public List<Projectile> Projectiles => _projectiles;

        internal ushort Cd
        {
            set { _cd = value; }
        }

        #endregion
    }
}