using ITI.SusanooQuest.Lib.AttackPatternFolder;
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
        List<Ennemy> _death;
        readonly List<Projectile> _projectiles;
        readonly List<Projectile> _projectilesToDel;
        readonly Player _player;
        readonly Random _random;
        Map _map;
        uint _highScore;
        uint _score;
        ushort _bombes;
        double _cd;
        DateTime _lastShot;
        ushort _playerLifeAtBegining;

        // game_structure part
        ILevel _level;

        #endregion

        public Game(ushort playerLife ,ushort bombes, uint highScore)
        {
            _ennemies = new List<Ennemy>();
            _death = new List<Ennemy>();
            _map = new Map(900, 1000);
            _player = new Player(new Vector(_map.Width / 2, _map.Height - 100), 5, this, playerLife, 8);
            _random = new Random();
            _highScore = highScore;
            _projectiles = new List<Projectile>();
            _projectilesToDel = new List<Projectile>();
            _bombes = bombes;
            _score = 0;
            _cd = 0.1;
            _playerLifeAtBegining = playerLife;

            _level = new LevelOne(this);
        }

        public bool Update()
        {
            _level = _level.NextLevel;
            _level.Update();

            //Update all the ennemies
            for (int i = _ennemies.Count() - 1; i >= 0; i--)
            {
                _ennemies[i].Update();
            }
            if (_player.OnShoot)
            {
                //Is here to make the player shoot evry 0.5 seconds
                if (DateTime.Now >= _lastShot.AddSeconds(_cd))
                {
                    AddProjectile(new Projectile(10, _player.Strength, new Vector(_player.Position.X, _player.Position.Y), _player, "Y") { Movement = new Y(10) });
                    _lastShot = DateTime.Now;
                }
            }
            //Update all the projectiles
            foreach (Projectile projectile in _projectiles)
            {
                ushort a;
                //Make the projectile move
                if (projectile.Tag == "Homing") a = 5;
                else a = 1;

                for (ushort i = 0; i != a; i++)
                {
                    projectile.Update();
                    //If the projectiles belong to an ennemy, compare the position with the player and explode if collision
                    if (projectile.Shooter != _player)
                    {
                        double distance = Math.Sqrt(Math.Pow(_player.Position.X - projectile.Position.X, 2) + Math.Pow(_player.Position.Y - projectile.Position.Y, 2));
                        float sumR = projectile.Movement.Length + _player.Length;
                        if (sumR > distance)
                        {
                            ProjectileExplode(projectile, _player);
                            break;
                        }
                    }
                    else
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
            }
            //Del all the projectile that expload or went out of bond 
            if (_projectilesToDel.Count != 0)
            {
                foreach (Projectile projectile in _projectilesToDel) _projectiles.Remove(projectile);
                _projectilesToDel.Clear();
            }

            _player.Update();

            if (_highScore < _score) _highScore = _score;
            if (_player.Life == 0) DataManager.Writer(_playerLifeAtBegining, _highScore);

            return _player.Life == 0;
        }

        //Inflict the damage of a projectile to the target
        private void ProjectileExplode(Projectile projectile, Entity target)
        {
            if (target is Ennemy) _score += 100;
            if (target is Player) 
            {
                OnClearProjectil();
                _bombes++;
            } 
            target.Life -= projectile.Damage;
            //Console.WriteLine(_player.Life);
            _projectilesToDel.Add(projectile);

        }

        internal void AddEnnemy(Ennemy ennemy)
        {
            if (ennemy == null) throw new NullReferenceException("Ennemy is null.");
            if (ennemy.Context != this) throw new ArgumentException("Context is an another game.");
            if (_ennemies.Contains(ennemy)) throw new ArgumentException("This ennemy is already in the game.");
            ennemy.Attack = new Triple(ennemy);
            _ennemies.Add(ennemy);
        }

        internal void AddProjectile(Projectile projectile)
        {
            _projectiles.Add(projectile);
        }

        internal void CreateProjectile(double speed, int damage, Vector origin, Entity shooter, string type)
        {
            if (speed < 0 || damage < 0) throw new ArgumentException("Must be superior to 0");
            if (shooter == null) throw new ArgumentNullException();

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
            _score += ennemy.Movement.Type;
            //Console.WriteLine( _ennemies.Count());
        }
        
        //Clear all projectile when a bomb is used
        public void OnClearProjectil()
        {
            for (int i = _projectiles.Count-1; i >= 0; i--) if (_projectiles[i].Shooter != _player && _projectilesToDel.IndexOf(_projectiles[i]) == -1) _projectilesToDel.Add(_projectiles[i]);
            _bombes--;
            Console.WriteLine("BOOOOOOOOOM!");
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

        #endregion
    }
}