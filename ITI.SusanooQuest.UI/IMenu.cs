using SFML.Window;

namespace ITI.SusanooQuest.UI
{
    public interface IMenu
    {
        public IMenu GetNextMenu { get; }

        public bool IsUpdate { get; set; }

        public void Update();

        public void Render();

        void MouseButtonPressed(MouseButtonEventArgs e);

        void KeyPressed(KeyEventArgs e);
    }
}