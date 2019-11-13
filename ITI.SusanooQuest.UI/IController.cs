using SFML.Window;
using System;

namespace ITI.SusanooQuest.UI
{
    public interface IController : IDisposable
    {
        public IController GetNextMenu { get; }

        public void Update();

        public void Render();

        public void MouseButtonPressed(MouseButtonEventArgs e);

        public void KeyPressed(KeyEventArgs e);

        public void KeyReleased(KeyEventArgs e);
    }
}