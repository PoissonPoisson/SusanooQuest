using System;
using System.IO;
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
        float _ratio;
        readonly RectangleShape _bg;
        IController _nextMenu;
        readonly Text[] _texts;
        readonly Font _font;
        readonly Vector _size;
        ushort _maxLive;
        uint _highScore;
        RectangleShape _selectCircl;
        readonly RectangleShape _volumeBar;
        RectangleShape _volumeBarBackground;

        #endregion

        public OptionMenu(RenderWindow window)
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
            _volumeBar = new RectangleShape(new Vector2f(1000, 50))
            {
                Position = new Vector2f(460, 515),
                FillColor = Color.Black
            };
            _buttons = new Button[6];
            Texture buttonTexture;
            buttonTexture = new Texture(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.button_return.png"));
            _buttons[0] = new Button(new Vector(760, 800), (int)buttonTexture.Size.X, (int)buttonTexture.Size.Y, buttonTexture);
            buttonTexture = new Texture(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.button_1.png"));
            _buttons[1] = new Button(new Vector(900, 300), (int)buttonTexture.Size.X, (int)buttonTexture.Size.Y, buttonTexture);
            buttonTexture = new Texture(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.button_2.png"));
            _buttons[2] = new Button(new Vector(1000, 300), (int)buttonTexture.Size.X, (int)buttonTexture.Size.Y, buttonTexture);
            buttonTexture = new Texture(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.button_3.png"));
            _buttons[3] = new Button(new Vector(1100, 300), (int)buttonTexture.Size.X, (int)buttonTexture.Size.Y, buttonTexture);
            buttonTexture = new Texture(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.button_4.png"));
            _buttons[4] = new Button(new Vector(1200, 300), (int)buttonTexture.Size.X, (int)buttonTexture.Size.Y, buttonTexture);
            buttonTexture = new Texture(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.button_5.png"));
            _buttons[5] = new Button(new Vector(1300, 300), (int)buttonTexture.Size.X, (int)buttonTexture.Size.Y, buttonTexture);


            Tuple<ushort, uint> data = DataManager.Reader();
            _maxLive = data.Item1;
            _highScore = data.Item2;

            //_selectCircl = new CircleShape(25) { Position = _buttons[_maxLive].Image.Position, FillColor = Color.Yellow};
            _selectCircl = new RectangleShape(new Vector2f(50f, 50f))
            {
                Texture = new Texture(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.select_nb_life.png")),
                Position = _buttons[_maxLive].Image.Position
            };
            SoundManager mySoundManager = SoundManager.GetInstance();            
            mySoundManager.LaunchMusic(nbMusic: 1);

            _font = new Font(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.THBiolinum.ttf"));
            _texts = new Text[2]
            {
                new Text($"Nombre de vie", _font, 60) { FillColor = Color.White, Position = new Vector2f(600, 275) },
                new Text($"Volume : {Math.Round(mySoundManager.GetCurrentVolume, 0)}%", _font, 50) {FillColor = Color.White, Position = new Vector2f(850f, 500f) }
            };
            _volumeBarBackground = new RectangleShape(new Vector2f(1000 * mySoundManager.GetCurrentVolume / 100, 50))
            {
                Position = new Vector2f(460, 515),
                FillColor = Color.Green
            };
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
                    (e.X - (_window.Size.X / 2 - (_size.X / 2) * _ratio)) / _ratio,
                    (e.Y - (_window.Size.Y / 2 - (_size.Y / 2) * _ratio)) / _ratio
                );

                if (_volumeBar.Position.X <= posInput.X && posInput.X <= _volumeBar.Position.X + _volumeBar.Size.X && _volumeBar.Position.Y < posInput.Y && posInput.Y <= _volumeBar.Position.Y + _volumeBar.Size.Y)
                {
                    float result = (100 * (posInput.X - _volumeBar.Position.X)) / _volumeBar.Size.X;

                    SoundManager.GetInstance().GetCurrentVolume = result;

                    _volumeBarBackground.Size = new Vector2f(posInput.X - _volumeBar.Position.X, 50);
                    _texts[1].DisplayedString = $"Volume : {Math.Round(result, 0)}%";
                }

                if (_buttons[0].Selected(posInput))
                {
                    DataManager.Writer(_maxLive, _highScore);
                    _nextMenu = new MainMenu(_window);
                }
                else
                {
                    for (ushort i = 1; i <= 5; i++)
                    {
                        if (_buttons[i].Selected(posInput))
                        {
                            _selectCircl.Position = _buttons[i].Image.Position;
                            _maxLive = i;
                            break;
                        }
                    }
                }
            }
        }
        
        public void KeyPressed(KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape)
            {
                DataManager.Writer(_maxLive, _highScore);
                _nextMenu = new MainMenu(_window);
            }
        }

        public void KeyReleased(KeyEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public void Render()
        {
            _window.Draw(_bg, RenderStates.Default);
            _window.Draw(_selectCircl, RenderStates.Default);
            foreach (Button button in _buttons) _window.Draw(button.Image, RenderStates.Default);
            _window.Draw(_volumeBar);
            _window.Draw(_volumeBarBackground);
            foreach (Text text in _texts) _window.Draw(text);
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
            //throw new NotImplementedException();
        }

        public void Dispose()
        {
            _view.Dispose();
            _bg.Texture.Dispose();
            _bg.Dispose();
            _volumeBar.Dispose();
            _volumeBarBackground.Dispose();
            _selectCircl.Dispose();
            foreach (Button button in _buttons) button.Image.Dispose();
            _font.Dispose();
            foreach(Text text in _texts) text.Dispose();
        }

        #endregion
    }
}
