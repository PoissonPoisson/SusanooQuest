using System;
using System.Collections.Generic;
using ITI.SusanooQuest.Lib;
using SFML.Window;
using SFML.System;
using SFML.Graphics;

namespace ITI.SusanooQuest.UI
{
    public class Credit : IMenu
    {
        readonly Button[] _buttons;
        readonly RenderWindow _window;
        bool _isUpdate;
        IMenu _nextMenu;
        public IMenu GetNextMenu
        {
            get { return _nextMenu; }
        }

        public Credit(RenderWindow window)
        {
            if (window == null) throw new NullReferenceException("Window is null");
            _window = window;
            _nextMenu = this;
            _isUpdate = true;

            _buttons = new Button[1]
            {
                new Button(new Vector(1400,900), 400, 50)
            };
        }
        public bool IsUpdate
        {
            get { return _isUpdate; }
            set { _isUpdate = value; }
        }

        public void MouseButtonPressed(MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left)
            {
                Vector posInput = new Vector(e.X, e.Y);
                if (_buttons[0].Selected(posInput)) _nextMenu = new MainMenu(_window);
            }
        }

        public void KeyPressed(KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape) _nextMenu = new MainMenu(_window);
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

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
