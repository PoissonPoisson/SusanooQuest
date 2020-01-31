using System;
using System.Reflection;
using System.Collections.Generic;
using ITI.SusanooQuest.Lib;
using SFML.Window;
using SFML.System;
using SFML.Graphics;
using SFML.Audio;

namespace ITI.SusanooQuest.UI
{
    public class Credit : IController, IDisposable
    {
        #region Fields

        readonly Button[] _buttons;
        readonly RenderWindow _window;
        readonly View _view;
        float _report;
        readonly RectangleShape _bg;
        IController _nextMenu;
        readonly Text[] _texts;
        readonly Font _font;
        readonly RectangleShape _panel;

        #endregion

        public Credit(RenderWindow window)
        {
            if (window == null) throw new NullReferenceException("Window is null.");

            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            _window = window;
            _nextMenu = this;
            _report =
            _report = Math.Min(_window.Size.X / 1920.0f, _window.Size.Y / 1080.0f);
            _view = new View(new FloatRect(0.0f, 0.0f, 1920.0f, 1080.0f));
            _bg = new RectangleShape(new Vector2f(1920.0f, 1080.0f))
            {
                Position = new Vector2f(0.0f, 0.0f),
                Texture = new Texture(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.bg_MainMenu.png"))
            };
            _buttons = new Button[1];
            Texture buttonTexture = new Texture(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.button_return2.png"));
            _buttons[0] = new Button(new Vector(760, 900), (int)buttonTexture.Size.X, (int)buttonTexture.Size.Y, buttonTexture);
            SoundManager mySoundManager = SoundManager.GetInstance();
            mySoundManager.LaunchMusic(nbMusic: 2);


            _font = new Font(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.THBiolinum.ttf"));
            _texts = new Text[15]
            {
                new Text("Songs", _font, 70) { Position = new Vector2f(100, 100), FillColor = Color.Black },
                new Text("Lullaby of Deserted Hell", _font, 60) { Position = new Vector2f(150, 200), FillColor = Color.Black },
                new Text("https://www.youtube.com/watch?v=aDfALl6DM7U", _font, 50) { Position = new Vector2f(800, 185), FillColor = Color.Black },
                new Text("https://en.touhouwiki.net/wiki/Touhou_Wiki:Copyrights", _font, 50) { Position = new Vector2f(800, 245), FillColor = Color.Black },
                new Text("Autres musiques", _font, 60) { Position = new Vector2f(150, 300), FillColor = Color.Black },
                new Text("Libres de droits", _font, 50) { Position = new Vector2f(800, 305), FillColor = Color.Black },
                new Text("Effets sonors des tirs", _font, 60) { Position = new Vector2f(150, 380), FillColor = Color.Black },
                new Text("Titouan Cellier", _font, 50) { Position = new Vector2f(800, 385), FillColor = Color.Black },
                new Text("Effets sonors de dégat", _font, 60) { Position = new Vector2f(150, 460), FillColor = Color.Black },
                new Text("https://www.youtube.com/watch?v=NTnaMsGryJ4", _font, 50) { Position = new Vector2f(800, 465), FillColor = Color.Black },

                new Text("Artworks", _font, 70) { Position = new Vector2f(100, 550), FillColor = Color.Black },
                new Text("Arrière plan par fjsmu", _font, 60) { Position = new Vector2f(150, 650), FillColor = Color.Black },
                new Text("https://www.pixiv.net/en/artworks/69412949", _font, 50) { Position = new Vector2f(800, 655), FillColor = Color.Black },
                new Text("Autres designs", _font, 60) { Position = new Vector2f(150, 730), FillColor = Color.Black },
                new Text("Grosman Romain", _font, 50) { Position = new Vector2f(800, 735), FillColor = Color.Black }
            };

            _panel = new RectangleShape(new Vector2f(1800f, 900))
            {
                Position = new Vector2f(1920 / 2 - 1800 / 2, 1080 / 2 - 900 / 2),
                Texture = new Texture(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.panel.png"))
            };
        }

        #region Properties

        public IController GetNextMenu => _nextMenu;

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
            if (e.Code == Keyboard.Key.Escape) _nextMenu = new MainMenu(_window);
        }

        public void KeyReleased(KeyEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public void Render()
        {
            _window.Draw(_bg, RenderStates.Default);
            _window.Draw(_panel);
            foreach (Button button in _buttons)
            {
                _window.Draw(button.Image);
            }
            foreach (Text text in _texts) _window.Draw(text);

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
            //throw new NotImplementedException();
        }

        public void Dispose()
        {
            _view.Dispose();
            _bg.Texture.Dispose();
            _panel.Dispose();
            _bg.Dispose();
            foreach (Button button in _buttons)
            {
                button.Image.Dispose();
            }

        }

        #endregion
    }
}
