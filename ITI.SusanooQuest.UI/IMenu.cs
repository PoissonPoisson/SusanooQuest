namespace ITI.SusanooQuest.UI
{
    public interface IMenu
    {
        public IMenu GetNextMenu();

        public void Update();

        public void Render();
    }
}