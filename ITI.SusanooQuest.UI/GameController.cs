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
        readonly CircleShape _playerTexture;
        readonly RectangleShape _playerTexture2;
        readonly RenderTexture _drawMap;
        readonly Sprite _spriteMap;

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
            _game = new Game(data.Item1, 3);

            // window and view data

            _ratio = Math.Min(_window.Size.X / _size.X, _window.Size.Y / _size.Y);
            _view = new View(new FloatRect(0f, 0f, _size.X, _size.Y));
            _bg = new RectangleShape(new Vector2f(_size.X, _size.Y))
            {
                Position = new Vector2f(0f, 0f),
                Texture = new Texture(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.bg_MainMenu.png"))
            };

            _font = new Font(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.THBiolinum.ttf"));
            _texts = new Text[3];
            _texts[0] = new Text($"Meilleur score", _font) { CharacterSize = 50, FillColor = Color.Red, Position = new Vector2f(1100, 100) };
            _texts[1] = new Text($"Score", _font) { CharacterSize = 50, FillColor = Color.Red, Position = new Vector2f(1100, 200) };
            _texts[2] = new Text($"Nombre de vie", _font) { CharacterSize = 50, FillColor = Color.Red, Position = new Vector2f(1100, 300) };

            // map data

            _bgMap = new RectangleShape(new Vector2f(_game.Map.Width, _game.Map.Height))
            {
                Position = new Vector2f(0f, 0f),
                Texture = new Texture(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.background_map.jpg"))
            };

            _playerTexture = new CircleShape(_game.Player.Length) { FillColor = Color.Green };
            _playerTexture.Position = new Vector2f
            (
                _game.Player.Position.X - _playerTexture.Radius,
                _game.Player.Position.Y - _playerTexture.Radius
            );
            _playerTexture2 = new RectangleShape(new Vector2f(60, 60))
            {
                Position = _playerTexture.Position,
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
            if (e.Code == Keyboard.Key.W)       _game.Player.StartShoot();
            if (e.Code == Keyboard.Key.X)       _game.OnClearProjectil();
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
            if (e.Code == Keyboard.Key.W) _game.Player.EndShoot();
            if (e.Code == Keyboard.Key.X) _game.EndClearProjectil();
        }

        public void Render()
        {
            _window.Draw(_bg);

            CreateDrawMap();

            _window.Draw(_spriteMap);
            
            foreach (Text text in _texts) _window.Draw(text);

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
        }

        void CreateDrawMap()
        {
            _drawMap.Clear();
            _drawMap.Draw(_bgMap);

            _playerTexture.Position = new Vector2f
            (
                _game.Player.Position.X - _playerTexture.Radius,
                _game.Player.Position.Y - _playerTexture.Radius
            );
            _playerTexture2.Position = new Vector2f(
                _playerTexture.Position.X - (_playerTexture2.Size.X / 2),
                _playerTexture.Position.Y - (_playerTexture2.Size.Y / 2)
            );

            _drawMap.Draw(_playerTexture2);
            _drawMap.Draw(_playerTexture);

            _drawMap.Display();
        }

        #endregion
    }
}
