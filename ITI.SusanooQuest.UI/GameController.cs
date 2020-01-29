using System;
using System.Reflection;
using System.Collections.Generic;
using ITI.SusanooQuest.Lib;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;

namespace ITI.SusanooQuest.UI
{
    public class GameController : IController, IDisposable
    {
        #region Fields

        readonly RenderWindow _window;
        readonly View _view;
        float _ratio;
        bool _isFirering;
        bool _isBombing;
        readonly RectangleShape _bg;
        IController _nextMenu;
        readonly Dictionary<string, Text> _texts;
        readonly Font _font;
        readonly Vector _size;
        readonly Game _game;
        readonly RectangleShape _bgMap;
        readonly RectangleShape _playerHitboxTexture;
        readonly RectangleShape _playerTexture2;
        readonly RenderTexture _drawMap;
        readonly Sprite _spriteMap;
        readonly RenderTexture _drawStats;
        readonly Sprite _spriteStates;
        readonly RectangleShape _bgStates;
        readonly Dictionary<string, RectangleShape> _projectilesTexture;
        //readonly Dictionary<string, CircleShape> _ennemiesTexture;
        readonly Dictionary<string, RectangleShape> _ennemiesTexture;
        readonly Sound _soundOfPlayerProjectil;  
        
        #endregion

        public GameController(RenderWindow window)
        {
            if (window == null) throw new NullReferenceException("Window is null.");

            Tuple<ushort, uint> data = DataManager.Reader();

            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            // general data
            
            _size = new Vector(1920, 1080);
            _window = window;
            _nextMenu = this;
            _game = new Game(data.Item1, 3, data.Item2);

            // window and view data

            _ratio = Math.Min(_window.Size.X / _size.X, _window.Size.Y / _size.Y);
            _view = new View(new FloatRect(0f, 0f, _size.X, _size.Y));
            _bg = new RectangleShape(new Vector2f(_size.X, _size.Y))
            {
                Position = new Vector2f(0f, 0f),
                Texture = new Texture(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.bg_MainMenu.png"))
            };

            // states data

            _drawStats = new RenderTexture(720, 600);
            _spriteStates = new Sprite(_drawStats.Texture) { Position = new Vector2f(1100, 40) };
            _bgStates = new RectangleShape(new Vector2f(720, 600))
            {
                Position = new Vector2f(0, 0),
                FillColor = Color.Black
            };
            
            _font = new Font(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.THBiolinum.ttf"));
            _texts = new Dictionary<string, Text>
            {
                { "MsgHighScore", new Text("Meilleur score", _font)                   { CharacterSize = 50, FillColor = Color.Red, Position = new Vector2f(20, 0) } },
                { "MsgScore",     new Text("Score", _font)                            { CharacterSize = 50, FillColor = Color.Red, Position = new Vector2f(20, 100) } },
                { "MsgNbLifes",   new Text("Nombre de vie", _font)                    { CharacterSize = 50, FillColor = Color.Red, Position = new Vector2f(20, 200) } },
                { "MsgNbBombs",   new Text("Nombre de bombes", _font)                 { CharacterSize = 50, FillColor = Color.Red, Position = new Vector2f(20, 300) } },
                { "HichScore",    new Text(ScoreToString(_game.HighScore, 10), _font) { CharacterSize = 50, FillColor = Color.Red, Position = new Vector2f(400, 0) } },
                { "Score",        new Text(ScoreToString(_game.Score, 10), _font)     { CharacterSize = 50, FillColor = Color.Red, Position = new Vector2f(400, 100) } },
                { "Life",         new Text(_game.Player.Life.ToString(), _font)       { CharacterSize = 50, FillColor = Color.Red, Position = new Vector2f(400, 200) } },
                { "Bombs",        new Text(_game.Bombes.ToString(), _font)            { CharacterSize = 50, FillColor = Color.Red, Position = new Vector2f(400, 300) } }
            };

            // map data

            _bgMap = new RectangleShape(new Vector2f(_game.Map.Width, _game.Map.Height))
            {
                Position = new Vector2f(0f, 0f),
                Texture = new Texture(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.bg_map.png"))
            };

            _playerHitboxTexture = new RectangleShape(new Vector2f(10f, 10f)) { Texture = new Texture(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.red_ball.png")) };
            
            _playerTexture2 = new RectangleShape(new Vector2f(60, 60))
            {
                Position = _playerHitboxTexture.Position,
                Texture = new Texture(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.perso.png")),
            };
            //Dictionary of projectiles texture
            //_projectilesTexture = new Dictionary<string, CircleShape>();
            //_projectilesTexture.Add("Y", new CircleShape(5) { FillColor = Color.Blue});
            //_projectilesTexture.Add("CosY", new CircleShape(5) { FillColor = Color.Red });
            //_projectilesTexture.Add("Homing", new CircleShape(5) { FillColor = Color.Magenta });

            _projectilesTexture = new Dictionary<string, RectangleShape>();
            _projectilesTexture.Add("Y", new RectangleShape(new Vector2f(20f, 20f)) { Texture = new Texture(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.red_ball.png")) });
            _projectilesTexture.Add("cosY", new RectangleShape(new Vector2f(20f, 20f)) { Texture = new Texture(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.green_ball.png")) });
            _projectilesTexture.Add("follow", new RectangleShape(new Vector2f(20f, 20f)) { Texture = new Texture(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.blue_ball.png")) });


            //Dictionary of ennemy texture
            //_ennemiesTexture = new Dictionary<string, CircleShape>();
            //_ennemiesTexture.Add("standard", new CircleShape(20));
            //_ennemiesTexture.Add("diagonal", new CircleShape(8) { FillColor = Color.Cyan});
            _ennemiesTexture = new Dictionary<string, RectangleShape>();
            _ennemiesTexture.Add("standard", new RectangleShape(new Vector2f(45f, 45f)) { Texture = new Texture(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.standard_ennemy.png")) });

            _drawMap = new RenderTexture((uint)_game.Map.Width, (uint)_game.Map.Height);
            _spriteMap = new Sprite(_drawMap.Texture) { Position = new Vector2f(100f, 40f) };           
            

            _window.SetFramerateLimit(60);
            SoundManager mySoundManager = SoundManager.GetInstance();
            mySoundManager.LaunchMusic(nbMusic: 4);

        }

        #region Properties

        public IController GetNextMenu => _nextMenu;

        #endregion

        #region Methodes

        public void MouseButtonPressed(MouseButtonEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public void KeyPressed(KeyEventArgs e)
        {
            switch(e.Code)
            {
                case Keyboard.Key.LShift:
                    _game.Player.Slow = true;
                    break;
                case Keyboard.Key.Left:
                    _game.Player.Deplacment["Right"] = false;
                    _game.Player.Deplacment["Left"] = true;
                    break;
                case Keyboard.Key.Up:
                    _game.Player.Deplacment["Down"] = false;
                    _game.Player.Deplacment["Up"] = true;
                    break;
                case Keyboard.Key.Right:
                    _game.Player.Deplacment["Left"] = false;
                    _game.Player.Deplacment["Right"] = true;
                    break;
                case Keyboard.Key.Down:
                    _game.Player.Deplacment["Up"] = false;
                    _game.Player.Deplacment["Down"] = true;
                    break;
                case Keyboard.Key.W:
                    _game.Player.StartShoot();
                    StartSoundFire();
                    break;
                case Keyboard.Key.X:
                    if (_game.Bombes > 0)
                    {
                        _game.OnClearProjectil();
                        UpdateBomb();
                        BomberSoundStart();
                    }
                    break;
                case Keyboard.Key.Escape:
                    _nextMenu = new MainMenu(_window);
                    break;
            }
        }

        public void KeyReleased(KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.LShift:
                    _game.Player.Slow = false;
                    break;
                case Keyboard.Key.Left:
                    _game.Player.Deplacment["Left"] = false;
                    break;
                case Keyboard.Key.Up:
                    _game.Player.Deplacment["Up"] = false;
                    break;
                case Keyboard.Key.Right:
                    _game.Player.Deplacment["Right"] = false;
                    break;
                case Keyboard.Key.Down:
                    _game.Player.Deplacment["Down"] = false;
                    break;
                case Keyboard.Key.W:
                    _game.Player.EndShoot();
                    StopSoundFire();
                    break;
                case Keyboard.Key.X:
                    BomberSoundStop();
                    break;
            }
        }
        public void BomberSoundStart()
        {
            if (_isBombing) return;
            _isBombing = true;
           // _game.OnClearProjectil();
            SoundManager mySoundManager = SoundManager.GetInstance();
            mySoundManager.Explode();
        }
        public void BomberSoundStop()
        {
            _isBombing = false;
        }

        public void StartSoundFire()
        {
            if (_isFirering) return;

            _isFirering = true;
            //_game.Player.StartShoot();
            SoundManager mySoundManager = SoundManager.GetInstance();
            mySoundManager.Shoot();

        }
        public void StopSoundFire()
        {
            if (!_isFirering) return;
            _isFirering = false;
            _game.Player.EndShoot();
            SoundManager mySoundManager = SoundManager.GetInstance();
            mySoundManager.StopShoot();
        }

        public void Render()
        {
            _window.Draw(_bg);

            CreateDrawMap();
            _window.Draw(_spriteMap);

            CreateDrawStates();
            _window.Draw(_spriteStates);


            _ratio = Math.Min(_window.Size.X / _size.X, _window.Size.Y / _size.Y);

            _view.Viewport = new FloatRect(
                (_window.Size.X / 2 - (_size.X / 2) * _ratio) / _window.Size.X,
                (_window.Size.Y / 2 - (_size.Y / 2) * _ratio) / _window.Size.Y,
                ((_window.Size.X / 2 + (_size.X / 2) * _ratio) / _window.Size.X) - ((_window.Size.X / 2 - (_size.X / 2) * _ratio) / _window.Size.X),
                ((_window.Size.Y / 2 + (_size.Y / 2) * _ratio) / _window.Size.Y) - ((_window.Size.Y / 2 - (_size.Y / 2) * _ratio) / _window.Size.Y)
            );

            _window.SetView(_view);
        }

        public void Update()
        {
            _game.Update();
            UpdateLife();
            _texts["Score"].DisplayedString = ScoreToString(_game.Score, 10);
            _texts["HichScore"].DisplayedString = ScoreToString(_game.HighScore, 10);
            if (_game.Player.Life <= 0)
            {
                SoundManager soundManager = SoundManager.GetInstance();
                soundManager.StopShoot();
                _nextMenu = new EndPageMenu(_window, false);
            }
            if (_game.End)
            {
                SoundManager.GetInstance().StopShoot();
                _nextMenu = new EndPageMenu(_window, true);
            }
        }

        public void Dispose()
        {
            _bgMap.Texture.Dispose();
            _bgMap.Dispose();
            _bg.Texture.Dispose();
            _bg.Dispose();
            _view.Dispose();
            foreach (Text text in _texts.Values) text.Dispose();
            _font.Dispose();
            _drawMap.Dispose();
            _bgStates.Dispose();
            _drawStats.Dispose();
        }

        void CreateDrawMap()
        {
            _drawMap.Clear();
            _drawMap.Draw(_bgMap);

            _playerHitboxTexture.Position = new Vector2f
            (
                _game.Player.Position.X - _playerHitboxTexture.Size.X / 2,
                _game.Player.Position.Y - _playerHitboxTexture.Size.Y / 2
            );
            _playerTexture2.Position = new Vector2f(
                _playerHitboxTexture.Position.X - (_playerTexture2.Size.X / 2),
                _playerHitboxTexture.Position.Y - (_playerTexture2.Size.Y / 2)
            );

            _drawMap.Draw(_playerTexture2);
            _drawMap.Draw(_playerHitboxTexture);

            foreach (Projectile p in _game.Projectiles)
            {
                _projectilesTexture.TryGetValue(p.Tag, out RectangleShape value);
                //value.Position = new Vector2f(p.Position.X - p.Movement.Length, p.Position.Y - p.Movement.Length);
                value.Position = new Vector2f(p.Position.X - value.Size.X / 2, p.Position.Y - value.Size.Y / 2);
                _drawMap.Draw(value);
            }

            foreach (Ennemy e in _game.Ennemy)
            {
                _ennemiesTexture.TryGetValue(e.Tag, out RectangleShape value);
                value.Position = new Vector2f(e.Position.X - value.Size.X / 2, e.Position.Y - value.Size.Y / 2);
                _drawMap.Draw(value);
            }

            _drawMap.Display();
        }

        void CreateDrawStates()
        {
            _drawStats.Clear();
            _drawStats.Draw(_bgStates);

            foreach (Text text in _texts.Values) _drawStats.Draw(text);

            _drawStats.Display();
        }

        string ScoreToString(uint score, ushort length)
        {
            string strScore = score.ToString();
            ushort i = 0;
            string msg = "";
            while (i < Math.Max(length, strScore.Length))
            {
                if (i < strScore.Length) msg = $"{strScore.Substring(strScore.Length - 1 - i, 1)}{msg}";
                else msg = $"0{msg}";
                i++;
                if (i % 3 == 0) msg = $",{msg}";
            }
            return msg;
        }

        void UpdateBomb()
        {
            _texts["Bombs"].DisplayedString = _game.Bombes.ToString();
        }

        void UpdateLife()
        {
            if (_texts["Life"].DisplayedString != _game.Player.Life.ToString()) 
            {
                SoundManager.GetInstance().HitSound();
                _texts["Life"].DisplayedString = _game.Player.Life.ToString(); 
            }                      

        }
        #endregion
    }
}
