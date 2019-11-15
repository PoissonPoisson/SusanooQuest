using System;
using System.Reflection;
using System.Collections.Generic;
using ITI.SusanooQuest.Lib;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace ITI.SusanooQuest.UI
{
    public class GameController : IController, IDisposable
    {
        #region Fields

        readonly RenderWindow _window;
        readonly View _view;
        float _ratio;
        readonly RectangleShape _bg;
        IController _nextMenu;
        readonly Text[] _texts;
        readonly Font _font;
        readonly Vector _size;
        readonly Game _game;
        readonly RectangleShape _bgMap;
        readonly CircleShape _playerHitboxTexture;
        readonly RectangleShape _playerTexture2;
        readonly RenderTexture _drawMap;
        readonly Sprite _spriteMap;
        readonly RenderTexture _drawStats;
        readonly Sprite _spriteStates;
        readonly RectangleShape _bgStates;

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

            _drawStats = new RenderTexture(800, 600);
            _spriteStates = new Sprite(_drawStats.Texture) { Position = new Vector2f(1010, 40) };
            _bgStates = new RectangleShape(new Vector2f(800, 600))
            {
                Position = new Vector2f(0, 0),
                FillColor = Color.Black
            };
            
            _font = new Font(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.THBiolinum.ttf"));
            _texts = new Text[6];
            _texts[0] = new Text("Meilleur score", _font) { CharacterSize = 50, FillColor = Color.Red, Position = new Vector2f(0, 0) };
            _texts[1] = new Text("Score", _font) { CharacterSize = 50, FillColor = Color.Red, Position = new Vector2f(0, 100) };
            _texts[2] = new Text("Nombre de vie", _font) { CharacterSize = 50, FillColor = Color.Red, Position = new Vector2f(0, 200) };
            _texts[3] = new Text("Nombre de bombes", _font) { CharacterSize = 50, FillColor = Color.Red, Position = new Vector2f(0, 300) };
            _texts[4] = new Text((_game.HighScore).ToString(), _font) { CharacterSize = 50, FillColor = Color.Red, Position = new Vector2f(_drawStats.Size.X / 2, 0) };
            _texts[5] = new Text(_game.Score.ToString(), _font) { CharacterSize = 50, FillColor = Color.Red, Position = new Vector2f(_drawStats.Size.X / 2, 100) };

            // map data

            _bgMap = new RectangleShape(new Vector2f(_game.Map.Width, _game.Map.Height))
            {
                Position = new Vector2f(0f, 0f),
                Texture = new Texture(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.background_map.jpg"))
            };

            _playerHitboxTexture = new CircleShape(_game.Player.Length) { FillColor = Color.Green };
            _playerHitboxTexture.Position = new Vector2f
            (
                _game.Player.Position.X - _playerHitboxTexture.Radius,
                _game.Player.Position.Y - _playerHitboxTexture.Radius
            );
            _playerTexture2 = new RectangleShape(new Vector2f(60, 60))
            {
                Position = _playerHitboxTexture.Position,
                Texture = new Texture(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.perso.png"))
            };
            

            _drawMap = new RenderTexture((uint)_game.Map.Width, (uint)_game.Map.Height);
            _spriteMap = new Sprite(_drawMap.Texture) { Position = new Vector2f(100f, 40f) };
            

            _window.SetFramerateLimit(60);
        }

        #region Properties

        public IController GetNextMenu
        {
            get { return _nextMenu; }
        }

        #endregion

        #region Methodes

        public void MouseButtonPressed(MouseButtonEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public void KeyPressed(KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.LShift)  _game.Player.Slow = true;
            if (e.Code == Keyboard.Key.Left)    _game.Player.StartMove(new Vector(-1, 0));
            if (e.Code == Keyboard.Key.Up)      _game.Player.StartMove(new Vector(0, -1));
            if (e.Code == Keyboard.Key.Right)   _game.Player.StartMove(new Vector(1, 0));
            if (e.Code == Keyboard.Key.Down)    _game.Player.StartMove(new Vector(0, 1));
            if (e.Code == Keyboard.Key.W);
            if (e.Code == Keyboard.Key.X) ;
            if (e.Code == Keyboard.Key.Escape) ;
            
            if (e.Code == Keyboard.Key.Escape) _nextMenu = new MainMenu(_window);
        }

        public void KeyReleased(KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.LShift) _game.Player.Slow = false;
            if (e.Code == Keyboard.Key.Left) _game.Player.EndMove(new Vector(1, 0));
            if (e.Code == Keyboard.Key.Up) _game.Player.EndMove(new Vector(0, 1));
            if (e.Code == Keyboard.Key.Right) _game.Player.EndMove(new Vector(-1, 0));
            if (e.Code == Keyboard.Key.Down) _game.Player.EndMove(new Vector(0, -1));
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
            
            // en faire 2 => les states et la map
            // créer le dessin de la map, des states et de la fenêtre globale
            // créer une méthode qui s'occupe de desiner chaques view
        }

        public void Update()
        {
            _game.Update();
        }

        public void Dispose()
        {
            _bgMap.Texture.Dispose();
            _bgMap.Dispose();
            _bg.Texture.Dispose();
            _bg.Dispose();
            _view.Dispose();
            foreach (Text text in _texts) text.Dispose();
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
                _game.Player.Position.X - _playerHitboxTexture.Radius,
                _game.Player.Position.Y - _playerHitboxTexture.Radius
            );
            _playerTexture2.Position = new Vector2f(
                _playerHitboxTexture.Position.X - (_playerTexture2.Size.X / 2),
                _playerHitboxTexture.Position.Y - (_playerTexture2.Size.Y / 2)
            );

            _drawMap.Draw(_playerTexture2);
            _drawMap.Draw(_playerHitboxTexture);

            _drawMap.Display();
        }

        void CreateDrawStates()
        {
            _drawStats.Clear();
            _drawStats.Draw(_bgStates);

            foreach (Text text in _texts) _drawStats.Draw(text);

            _drawStats.Display();
        }

        #endregion
    }
}
