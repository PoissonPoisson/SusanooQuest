using System;
using System.Reflection;
using System.Collections.Generic;
using ITI.SusanooQuest.Lib;
using SFML.Window;
using SFML.System;
using SFML.Graphics;

namespace ITI.SusanooQuest.UI
{
    public class OptionMenu : IController, IDisposable
    {
        #region Fields

        readonly Button[] _buttons;
        readonly RenderWindow _window;
        readonly View _view;
        float _report;
        readonly RectangleShape _bg;
        IController _nextMenu;

        #endregion

        public OptionMenu(RenderWindow window)
        {
            if (window == null) throw new NullReferenceException("Window is null");

            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            _window = window;
            _nextMenu = this;
            _report = Math.Min(_window.Size.X / 1920.0f, _window.Size.Y / 1080.0f);
            _view = new View(new FloatRect(0.0f, 0.0f, 1920.0f, 1080.0f));
            _bg = new RectangleShape(new Vector2f(1920.0f, 1080.0f))
            {
                Position = new Vector2f(0.0f, 0.0f),
                Texture = new Texture(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.bg_MainMenu.png"))
            };

            _buttons = new Button[1];
            Texture buttonTexture = new Texture(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.button_return.png"));
            _buttons[0] = new Button(new Vector(760, 515), (int)buttonTexture.Size.X, (int)buttonTexture.Size.Y, buttonTexture);
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
            if (e.Button == Mouse.Button.Left)
            {
                Vector posInput = new Vector(
                    (e.X - (_window.Size.X / 2 - (1920.0f / 2) * _report)) / _report,
                    (e.Y - (_window.Size.Y / 2 - (1080.0f / 2) * _report)) / _report
                );

                //Console.WriteLine($"X : {posInput.X}, Y : {posInput.Y}");
                //Console.WriteLine($"Button :\n - X : {_buttons[0].Pos.X} - {_buttons[0].Pos.X + _buttons[0].Width}\n - Y : {_buttons[0].Pos.Y} - {_buttons[0].Pos.Y + _buttons[0].Height}");

                if (_buttons[0].Selected(posInput)) _nextMenu = new MainMenu(_window);
            }
        }

        public void KeyPressed(KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape)
            {
                _nextMenu = new MainMenu(_window);
                Dispose();
            }
        }

        public void Render()
        {
            _window.Draw(_bg, RenderStates.Default);
            foreach (Button button in _buttons)
            {
                _window.Draw(button.Image);
            }

            _report = Math.Min(_window.Size.X / 1920.0f, _window.Size.Y / 1080.0f);

            _view.Viewport = new FloatRect(
                (_window.Size.X / 2 - (1920.0f / 2) * _report) / _window.Size.X,
                (_window.Size.Y / 2 - (1080.0f / 2) * _report) / _window.Size.Y,
                ((_window.Size.X / 2 + (1920.0f / 2) * _report) / _window.Size.X) - ((_window.Size.X / 2 - (1920.0f / 2) * _report) / _window.Size.X),
                ((_window.Size.Y / 2 + (1080.0f / 2) * _report) / _window.Size.Y) - ((_window.Size.Y / 2 - (1080.0f / 2) * _report) / _window.Size.Y)
            );

            _window.SetView(_view);
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
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
