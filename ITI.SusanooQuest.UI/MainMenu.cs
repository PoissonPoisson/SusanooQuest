using System;
using System.Reflection;
using ITI.SusanooQuest.Lib;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
using SFML.Audio;
using System.IO;

namespace ITI.SusanooQuest.UI
{
    class MainMenu : IController, IDisposable
    {
        #region Fields

        readonly Button[] _buttons;
        readonly RenderWindow _window;
        readonly View _view;
        float _ratio;
        readonly RectangleShape _bg;
        IController _nextMenu;
        readonly Vector _size;
        readonly Music _music;

        #endregion

        public MainMenu(RenderWindow window)
        {
            if (window == null) throw new NullReferenceException("Window is null.");

            _size = new Vector(1920, 1080);

            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            _window = window;
            _nextMenu = this;
            _ratio = Math.Min(_window.Size.X / _size.X, _window.Size.Y / _size.Y);
            _view = new View(new FloatRect(0.0f, 0.0f, _size.X, _size.Y));
            _bg = new RectangleShape(new Vector2f(_size.X, _size.Y))
            {
                Position = new Vector2f(0.0f, 0.0f),
                Texture = new Texture(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.bg_MainMenu.png"))
            };

            _buttons = new Button[5];
            Texture buttonTexture;
            buttonTexture = new Texture(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.button_play.png"));
            _buttons[0] = new Button(new Vector(1200, 315), (int)buttonTexture.Size.X, (int)buttonTexture.Size.Y, buttonTexture);
            buttonTexture = new Texture(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.button_multiplayer.png"));
            _buttons[1] = new Button(new Vector(1200, 415), (int)buttonTexture.Size.X, (int)buttonTexture.Size.Y, buttonTexture);
            buttonTexture = new Texture(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.button_option.png"));
            _buttons[2] = new Button(new Vector(1200, 515), (int)buttonTexture.Size.X, (int)buttonTexture.Size.Y, buttonTexture);
            buttonTexture = new Texture(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.button_credit.png"));
            _buttons[3] = new Button(new Vector(1200, 615), (int)buttonTexture.Size.X, (int)buttonTexture.Size.Y, buttonTexture);
            buttonTexture = new Texture(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.button_quit.png"));
            _buttons[4] = new Button(new Vector(1200, 715), (int)buttonTexture.Size.X, (int)buttonTexture.Size.Y, buttonTexture);

            //_music = new Music(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.Lullaby_of_Deserted_Hell.wav"));
            //_music.Play();
        }

        #region Properties

        public IController GetNextMenu
        {
            get { return _nextMenu; }
        }

        #endregion

        #region Methodes

        public void Update()
        {
        }
        
        public void Render()
        {
            _window.Draw(_bg, RenderStates.Default);
            foreach (Button button in _buttons)
            {
                _window.Draw(button.Image, RenderStates.Default);
            }

            _ratio = Math.Min(_window.Size.X / _size.X, _window.Size.Y / _size.Y);

            _view.Viewport = new FloatRect(
                (_window.Size.X / 2 - (_size.X / 2) * _ratio) / _window.Size.X,
                (_window.Size.Y / 2 - (_size.Y / 2) * _ratio) / _window.Size.Y,
                ((_window.Size.X / 2 + (_size.X / 2) * _ratio) / _window.Size.X) - ((_window.Size.X / 2 - (_size.X / 2) * _ratio) / _window.Size.X),
                ((_window.Size.Y / 2 + (_size.Y / 2) * _ratio) / _window.Size.Y) - ((_window.Size.Y / 2 - (_size.Y / 2) * _ratio) / _window.Size.Y)
            );

            _window.SetView(_view);
        }

        public void MouseButtonPressed(MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left)
            {
                Vector posInput = new Vector(
                    (e.X - (_window.Size.X / 2 - (_size.X / 2) * _ratio)) / _ratio,
                    (e.Y - (_window.Size.Y / 2 - (_size.Y / 2) * _ratio)) / _ratio
                );

                //Console.WriteLine($"X : {posInput.X}, Y : {posInput.Y}");
                //Console.WriteLine($"Button :\n - X : {_buttons[2].Pos.X} - {_buttons[2].Pos.X + _buttons[2].Width}\n - Y : {_buttons[2].Pos.Y} - {_buttons[2].Pos.Y + _buttons[2].Height}");

                if (_buttons[0].Selected(posInput)) _nextMenu = new GameController(_window);
                if (_buttons[1].Selected(posInput)) ;// _nextMenu = new GameMulti();
                if (_buttons[2].Selected(posInput)) _nextMenu = new OptionMenu(_window);
                if (_buttons[3].Selected(posInput)) _nextMenu = new Credit(_window);
                if (_buttons[4].Selected(posInput)) _window.Close();
            }
        }

        public void KeyPressed(KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape)
            {
                _window.Close();
            }
        }

        public void KeyReleased(KeyEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public void Dispose()
        {
            //_music.Stop();
            //_music.Dispose();

            _view.Dispose();
            _bg.Texture.Dispose();
            _bg.Dispose();
            foreach (Button button in _buttons)
            {
                button.Image.Dispose();
            }
        }

        #endregion
    }
}
