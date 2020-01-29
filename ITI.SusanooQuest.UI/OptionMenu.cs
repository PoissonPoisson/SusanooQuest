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
            _volumeBarBackground = new RectangleShape(new Vector2f(1000, 50))
            {
                Position = new Vector2f(460, 515),
                FillColor = Color.Green
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

            _font = new Font(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.THBiolinum.ttf"));
            _texts = new Text[1];
            _texts[0] = new Text($"Nombre de vie", _font) { CharacterSize = 60, FillColor = Color.Red, Position = new Vector2f(600, 275) };

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
                Console.WriteLine($"X : {posInput.X}, Y : {posInput.Y}");

                //Console.WriteLine($"Button :\n - X : {_buttons[0].Pos.X} - {_buttons[0].Pos.X + _buttons[0].Width}\n - Y : {_buttons[0].Pos.Y} - {_buttons[0].Pos.Y + _buttons[0].Height}");
                if (_volumeBar.Position.X <= posInput.X && posInput.X < _volumeBar.Position.X + _volumeBar.Size.X && _volumeBar.Position.Y <= posInput.Y && posInput.Y < _volumeBar.Position.Y + _volumeBar.Size.Y)
                {
                    // SoundManager.GetInstance().GetCurrentMusic.Volume = ((((_volumeBar.Position.X + _volumeBar.Size.X)-_volumeBar.Position.X)-(e.X- (_volumeBar.Position.X + _volumeBar.Size.X))*100)/ ((_volumeBar.Position.X + _volumeBar.Size.X)-_volumeBar.Position.X));
                    //SoundManager.GetInstance().GetCurrentMusic.Volume = (100 * (posInput.X - _volumeBar.Position.X)) / (_volumeBar.Size.X + _volumeBar.Position.X);
                    Console.WriteLine((100 * (posInput.X - _volumeBar.Position.X)) / (_volumeBar.Size.X + _volumeBar.Position.X));
                    SoundManager.GetInstance().GetCurrentVolume = (100 * (posInput.X - _volumeBar.Position.X)) / (_volumeBar.Size.X + _volumeBar.Position.X);
                    _volumeBarBackground = new RectangleShape(new Vector2f(posInput.X - _volumeBar.Position.X, 50))
                    {
                        Position = new Vector2f(460, 515),   //0.0
                        FillColor = Color.Green
                    };
                                       
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
            foreach (Button button in _buttons)
            {
                _window.Draw(button.Image, RenderStates.Default);
            }
            _window.Draw(_texts[0], RenderStates.Default);
            _window.Draw(_volumeBar);
            _window.Draw(_volumeBarBackground);
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
            foreach (Button button in _buttons)
            {
                button.Image.Dispose();
            }
            _font.Dispose();
            foreach(Text text in _texts)
            {
                text.Dispose();
            }
        }

        #endregion
    }
}
