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

        #endregion

        public GameController(RenderWindow window)
        {
            if (window == null) throw new NullReferenceException("Window is null.");

            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            _size = new Vector(1920, 1080);
            _window = window;
            _nextMenu = this;
            _ratio = Math.Min(_window.Size.X / _size.X, _window.Size.Y / _size.Y);
            _view = new View(new FloatRect(0.0f, 0.0f, _size.X, _size.Y));
            _bg = new RectangleShape(new Vector2f(_size.X, _size.Y))
            {
                Position = new Vector2f(0.0f, 0.0f),
                Texture = new Texture(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.bg_MainMenu.png"))
            };

            _font = new Font(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.THBiolinum.ttf"));
            _texts = new Text[3];
            _texts[0] = new Text($"Meilleur score", _font) { CharacterSize = 50, FillColor = Color.Red, Position = new Vector2f(1100, 100) };
            _texts[1] = new Text($"Score", _font) { CharacterSize = 50, FillColor = Color.Red, Position = new Vector2f(1100, 200) };
            _texts[2] = new Text($"Nombre de vie", _font) { CharacterSize = 50, FillColor = Color.Red, Position = new Vector2f(1100, 300) };
            _game = new Game();
        }

        public IController GetNextMenu
        {
            get { return _nextMenu; }
        }

        public void MouseButtonPressed(MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void KeyPressed(KeyEventArgs e)
        {
            Vector vector = new Vector(0, 0);
            bool slow = false;
            if (e.Code == Keyboard.Key.Left)    _game.Player.StartMove(-2, 0);
            if (e.Code == Keyboard.Key.Up)      _game.Player.StartMove(0, -2);
            if (e.Code == Keyboard.Key.Right)   _game.Player.StartMove(2, 0);
            if (e.Code == Keyboard.Key.Down)    _game.Player.StartMove(0, 2);
            if (e.Shift) slow = true ;
            if (e.Code == Keyboard.Key.W);
            if (e.Code == Keyboard.Key.X) ;
            if (e.Code == Keyboard.Key.Escape) ;

            //_game.AssignPlayerMotion(vector, slow);

            if (e.Code == Keyboard.Key.Escape) _nextMenu = new MainMenu(_window);
        }

        void OnKeyReleased(object Sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Right || e.Code == Keyboard.Key.Left || e.Code == Keyboard.Key.Up || e.Code == Keyboard.Key.Down)
            {
                _game.Player.EndMove();
            }
        }

        public void Render()
        {
            _window.Draw(_bg);
            foreach (Text text in _texts) _window.Draw(text);
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _bg.Texture.Dispose();
            _bg.Dispose();
            _view.Dispose();
            foreach (Text text in _texts) text.Dispose();
            _font.Dispose();
        }
    }
}
