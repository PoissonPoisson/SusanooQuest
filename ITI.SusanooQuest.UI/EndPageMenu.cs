using ITI.SusanooQuest.Lib;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Reflection;

namespace ITI.SusanooQuest.UI
{
    public class EndPageMenu : IController
    {
        #region Fields

        IController _nextMenu;
        readonly RenderWindow _window;
        readonly Vector _size;
        float _ratio;
        readonly View _view;
        readonly RectangleShape _bg;
        readonly Font _font;
        readonly Text[] _texts;        
        readonly Button _button;
        readonly RenderTexture _credit;
        readonly Sprite _spriteCredit;
        readonly Text[] _creditTexts;

        #endregion

        public EndPageMenu(RenderWindow window, bool win)
        {
            if (window == null) throw new ArgumentNullException("Window is null.");

            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            // general data

            _window = window;
            _nextMenu = this;
            _size = new Vector(1920, 1080);

            // window and view

            _ratio = Math.Min(_window.Size.X / _size.X, _window.Size.Y / _size.Y);
            _view = new View(new FloatRect(0f, 0f, _size.X, _size.Y));
            _bg = new RectangleShape(new Vector2f(_size.X, _size.Y))
            {
                Position = new Vector2f(0f, 0f),
                Texture = new Texture(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.bg_MainMenu.png"))
            };
            _font = new Font(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.THBiolinum.ttf"));

            Texture buttonTexture = new Texture(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.button_Menu.png"));

            if (win)
            {
                SoundManager.GetInstance().LaunchMusic(5);
                _texts = new Text[0];

                _credit = new RenderTexture(1000, 3500);
                _spriteCredit = new Sprite(_credit.Texture) { Position = new Vector2f(_size.X / 2 - _credit.Size.X / 2, _size.Y) };
                _creditTexts = new Text[19]
                {
                    new Text("END", _font) { CharacterSize = 200, FillColor = Color.White, Position = new Vector2f(350, 0) },
                    new Text("Susanoo's Quest", _font) { CharacterSize = 150, FillColor = Color.White, Position = new Vector2f(100, 1100) },
                    new Text("Un projet porté par :", _font) { CharacterSize = 80, FillColor = Color.White, Position = new Vector2f(150, 1500) },
                    new Text("IN'TECH", _font) { CharacterSize = 50, FillColor = Color.White, Position = new Vector2f(200, 1600) },
                    new Text("Développé par :", _font) { CharacterSize = 80, FillColor = Color.White, Position = new Vector2f(150, 1700) },
                    new Text("Delorme-Glorieux Romain", _font) { CharacterSize = 50, FillColor = Color.White, Position = new Vector2f(200, 1800) },
                    new Text("Picotin Paul", _font) { CharacterSize = 50, FillColor = Color.White, Position = new Vector2f(200, 1860) },
                    new Text("Roussin Antoine", _font) { CharacterSize = 50, FillColor = Color.White, Position = new Vector2f(200, 1920) },
                    new Text("Encadré par :", _font) { CharacterSize = 80, FillColor = Color.White, Position = new Vector2f(150, 2020) },
                    new Text("Raquillet Antoine", _font) { CharacterSize = 50, FillColor = Color.White, Position = new Vector2f(200, 2120) },
                    new Text("Dchimir Rachid", _font) { CharacterSize = 50, FillColor = Color.White, Position = new Vector2f(200, 2180) },
                    new Text("Grapgismes par :", _font) { CharacterSize = 80, FillColor = Color.White, Position = new Vector2f(150, 2280) },
                    new Text("fjsmu", _font) { CharacterSize = 50, FillColor = Color.White, Position = new Vector2f(200, 2380) },
                    new Text("Grosman Romain", _font) { CharacterSize = 50, FillColor = Color.White, Position = new Vector2f(200, 2440) },
                    new Text("Song par :", _font) { CharacterSize = 80, FillColor = Color.White, Position = new Vector2f(150, 2540) },
                    new Text("Williatico", _font) { CharacterSize = 50, FillColor = Color.White, Position = new Vector2f(200, 2640) },
                    new Text("Takeshi Saito", _font) { CharacterSize = 50, FillColor = Color.White, Position = new Vector2f(200, 2700) },
                    new Text("Titouan Cellier", _font) { CharacterSize = 50, FillColor = Color.White, Position = new Vector2f(200, 2760) },
                    new Text("Merci à vous !", _font) { CharacterSize = 100, FillColor = Color.White, Position = new Vector2f(280, 3060) }
                };
                //new Text("", _font) { CharacterSize = 50, FillColor = Color.White, Position = new Vector2f() }
                foreach (Text text in _creditTexts) _credit.Draw(text);
                _credit.Display();
                
            }
            else
            {
                SoundManager mySoundManager = SoundManager.GetInstance();
                mySoundManager.LaunchMusic(nbMusic: 3);

                _button = new Button(
                new Vector(_size.X / 2 - buttonTexture.Size.X / 2, 800),
                (int)buttonTexture.Size.X,
                (int)buttonTexture.Size.Y,
                buttonTexture
                );

                _texts = new Text[1]
                {
                    new Text("Game Over", _font) { CharacterSize = 120, FillColor = Color.Red, Position = new Vector2f(750, 350) }

                };
            }
            

        }


        #region Properties

        public IController GetNextMenu => _nextMenu;

        #endregion

        #region Methodes

        public void Dispose()
        {
            _view.Dispose();
            _bg.Dispose();
            _font.Dispose();
            foreach (Text text in _texts) text.Dispose();
            if (_creditTexts != null)
            {
                foreach (Text text in _creditTexts) text.Dispose();
            }
            if (_button != null) _button.Image.Dispose();
            
        }

        public void KeyPressed(KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape || e.Code == Keyboard.Key.Space) _nextMenu = new MainMenu(_window);
        }

        public void KeyReleased(KeyEventArgs e)
        { }

        public void MouseButtonPressed(MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left)
            {
                Vector posInput = new Vector(
                    (e.X - (_window.Size.X / 2 - (_size.X / 2) * _ratio)) / _ratio,
                    (e.Y - (_window.Size.Y / 2 - (_size.Y / 2) * _ratio)) / _ratio
                );

                if (_button != null && _button.Selected(posInput)) _nextMenu = new MainMenu(_window);
            }
                
        }

        public void Render()
        {
            _window.Draw(_bg, RenderStates.Default);
            if (_credit != null) _window.Draw(_spriteCredit);
            if (_button != null) _window.Draw(_button.Image, RenderStates.Default);
            foreach (Text text in _texts) _window.Draw(text, RenderStates.Default);

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
            if (_spriteCredit != null)
            {
                _spriteCredit.Position -= new Vector2f(0, 2f);
                if (_spriteCredit.Position.Y <= -_credit.Size.Y) _nextMenu = new MainMenu(_window);
            }
        }

        #endregion
    }
}
