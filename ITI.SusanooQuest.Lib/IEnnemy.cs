namespace ITI.SusanooQuest.Lib
{
    public interface IEnnemy
	{
		public Game Context { get; }

		internal float Speed { get; set; }

		public string Tag { get; }

		public Vector Position { get; }

		internal void Update();
	}
}
