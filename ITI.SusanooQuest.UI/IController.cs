using SFML.Window;

namespace ITI.SusanooQuest.UI
{
    public interface IController
    {
        public IController GetNextMenu { get; }

        public void Update();

        public void Render();

        public void MouseButtonPressed(MouseButtonEventArgs e);

        public void KeyPressed(KeyEventArgs e);

        public void KeyReleased(KeyEventArgs e);

        public void Dispose();
    }
}