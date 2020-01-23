namespace ITI.SusanooQuest.Lib
{
    public interface ILevel
    {
        public Game Context { get; }

        public ILevel NextLevel { get; }

        public IPattern Pattern { get; }

        public void Update();
    }
}
