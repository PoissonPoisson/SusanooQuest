namespace ITI.SusanooQuest.Lib
{
    public interface IPattern
	{
		public IPattern NextPatern { get; }

		public void Update();
	}
}
