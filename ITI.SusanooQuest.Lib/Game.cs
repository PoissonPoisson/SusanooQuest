using System;
using System.Collections.Generic;
using System.Linq;

namespace ITI.SusanooQuest.Lib
{
    public class Game
    {
        #region Fields
        //readonly Dictionary<string,Dictionary<string,>>
        readonly List<Ennemy> _ennemies;
        List<Ennemy> _ennemiesToDel;
        List<Ennemy> _death;
        readonly List<Projectile> _projectiles;
        readonly List<Projectile> _projectilesToDel;
        readonly Player _player;
        readonly Random _random;
        LevelOrganizer _Level;
        Map _map;
        uint _highScore;
        uint _score;
        ushort _bombes;
        ushort _cd;

        #endregion

        public Game(ushort playerLife ,ushort bombes, uint highScore)
        {
            _ennemies = new List<Ennemy>();
            _ennemiesToDel = new List<Ennemy>();
            _death = new List<Ennemy>();
            _map = new Map(900, 1000);
            _player = new Player(new Vector(_map.Width / 2, _map.Height - 100), 5, this, playerLife, 8);
            _random = new Random();
            _highScore = highScore;
            _projectiles = new List<Projectile>();
            _projectilesToDel = new List<Projectile>();
            _bombes = bombes;
            _score = 0;
            _cd = 1;
            _Level = new LevelOrganizer(_ennemies, _death, Player, this);

        }

        public bool Update()
        {
            if (_ennemies.Count < 1)
            {
                _Level.LevelOne();
            }

            //Update all the ennemies
            for (int i = _ennemies.Count() - 1; i >= 0; i--)
            {
                _ennemies[i].Update();
            }

            if (_player.OnShoot)
            {
                //Is here to make the player shoot evry 10 turn
                //cd is reset to 1 evry time the key is released
                _cd--;
                if (_cd == 0)
                {
                    _cd = 10;
                    CreateProjectile(10, _player.Strength, new Vector(_player.Position.X - 5, _player.Position.Y - 5), _player, "Y");
                }
            }

            //Update all the projectiles
            foreach (Projectile projectile in _projectiles)
            {
                //Make the projectile move
                projectile.Update();

                //If the projectiles belong to an ennemy, compare the position with the player and explode if collision
                if (projectile.Shooter != _player)
                {
                    double distance = Math.Sqrt(Math.Pow(_player.Position.X - projectile.Position.X, 2) + Math.Pow(_player.Position.Y - projectile.Position.Y, 2));
                    float sumR = projectile.Movement.Length + _player.Length;
                    if (sumR > distance) ProjectileExplode(projectile, _player);
                } else
                //Else, compare the projectile to all the ennemies and explode 
                {
                    foreach (Ennemy e in _ennemies)
                    {
                        float distance = Convert.ToSingle(Math.Sqrt(Math.Pow(e.Position.X - projectile.Position.X, 2) + Math.Pow(e.Position.Y - projectile.Position.Y, 2)));
                        float sumR = projectile.Movement.Length + e.Length;
                        if (sumR > distance) ProjectileExplode(projectile, e);
                    }
                }

                //Del projectile if out of bond
                if (projectile.Position.Y > _map.Height || projectile.Position.Y < -20) _projectilesToDel.Add(projectile);
            }
            //Del all the projectile that expload or went out of bond 
            if (_projectilesToDel.Count != 0)
            {
                foreach (Projectile projectile in _projectilesToDel) _projectiles.Remove(projectile);
                _projectilesToDel.Clear();
            }

            _player.Update();

            if (_highScore < _score) _highScore = _score;
            return _player.Life == 0;
        }

        //Inflict the damage of a projectile to the target
        private void ProjectileExplode(Projectile projectile, Entity target)
        {
            target.Life -= projectile.Damage;
            //Console.WriteLine(_player.Life);
            _projectilesToDel.Add(projectile);

        }
        
        public LevelOrganizer CreateLevel()
        {
            LevelOrganizer levelone = new LevelOrganizer(_ennemies, _death, Player, this);
            levelone.LevelOne();
            return levelone;
        }

        internal Ennemy CreateEnnemy(Vector pos, float length, Game game, ushort life, float speed, string tag)
        {
            if (pos.X < 0 || pos.Y < 0) throw new ArgumentOutOfRangeException("out of bond", nameof(pos));
            if (length <= 0) throw new ArgumentException("Length must be positive", nameof(length));
            if (life <= 0) throw new ArgumentException("Life must be positive", nameof(life));
            if (speed < 0) throw new ArgumentException("Speed must be positive", nameof(speed));
            if (game == null) throw new ArgumentNullException(nameof(game));

            //Creat the incomplete ennemy
            Ennemy ennemy = new Ennemy(pos, length, game, life, speed, tag);

            //Complete it with his movement methode (designe pattern strategy)
            switch (tag)
            {
                case "standard":
                    ennemy.Movement = new Standard(ennemy.Speed, this);
                    break;
                case "diagonal":
                    ennemy.Movement = new Diagonal(ennemy.Speed, this);
                    break;
                default:
                    throw new ArgumentException("Does not match any ennemy type", nameof(tag));
            }

            _ennemies.Add(ennemy);
            return ennemy;
        }

        internal void CreateProjectile(double speed, int damage, Vector origin, Entity shooter, string type)
        {
            if (speed < 0 || damage < 0) throw new ArgumentException("Must be superior to 0");
            if (shooter == null) throw new ArgumentNullException();
            if (origin.X < 0 || origin.Y < 0) throw new ArgumentOutOfRangeException("out of Bound");

            //Creat the incomplete projectile
            Projectile projec = new Projectile(speed, damage, origin, shooter, type);

            //Complete it with his movement methode (designe pattern strategy)
            switch (type)
            {
                case "Y":
                    projec.Movement = new Y(projec.Speed);
                    break;
                case "CosY":
                    projec.Movement = new CosY(projec.Speed);
                    break;
            }

            _projectiles.Add(projec);
        }

        internal void OnKill(Ennemy ennemy)
        {
            _ennemies.Remove(ennemy);
            //Console.WriteLine( _ennemies.Count());
        }
        
        //Clear all projectile when a bomb is used
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