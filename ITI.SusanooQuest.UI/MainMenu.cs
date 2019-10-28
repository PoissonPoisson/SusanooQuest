using System;
using ITI.SusanooQuest.Lib;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace ITI.SusanooQuest.UI
{
    class MainMenu : IMenu
    {
        #region Fields

        readonly Button[] _buttons;
        readonly RenderWindow _window;
        bool _isUpdate;
        IMenu _nextMenu;

        #endregion

        public MainMenu(RenderWindow window)
        {
            if (window == null) throw new NullReferenceException("Window is null.");

            _buttons = new Button[5]
            {
                new Button(new Vector(1200, 315), 400, 50),
                new Button(new Vector(1200, 415), 400, 50),
                new Button(new Vector(1200, 515), 400, 50),
                new Button(new Vector(1200, 615), 400, 50),
                new Button(new Vector(1200, 715), 400, 50)
            };
            _window = window;
            _nextMenu = this;
            _isUpdate = true;
        }

        #region Properties

        public IMenu GetNextMenu
        {
            get { return _nextMenu; }
        }

        public bool IsUpdate
        {
            get { return _isUpdate; }
            set { _isUpdate = value; }
        }

        #endregion

        #region Methodes

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void Render()
        {
            RectangleShape rectangle = new RectangleShape();
            foreach (Button button in _buttons)
            {
                rectangle.Size = new Vector2f(button.Width, button.Height);
                rectangle.Position = new Vector2f((int)button.Pos.X, (int)button.Pos.Y);
                rectangle.FillColor = Color.White;
                _window.Draw(rectangle);
            }
            rectangle.Dispose();
        }

        public void MouseButtonPressed(MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left)
            {
                Vector posInput = new Vector(e.X, e.Y);

                if (_buttons[0].Selected(posInput)) { _isUpdate = true; _nextMenu = new Game(); }
                if (_buttons[1].Selected(posInput)) { _isUpdate = true; _nextMenu = new GameMulti(); }
                if (_buttons[2].Selected(posInput)) { _isUpdate = true; _nextMenu = new OptionMenu(_window); }
                if (_buttons[3].Selected(posInput)) { _isUpdate = true; _nextMenu = new Credit(); }
                if (_buttons[4].Selected(posInput)) { _isUpdate = true; _window.Close(); }
            }
        }

        public void KeyPressed(KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape)
            {
                _window.Close();
            }
        }

        #endregion
    }
}
