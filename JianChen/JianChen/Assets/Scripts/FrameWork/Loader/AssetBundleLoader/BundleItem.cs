namespace game.main
{
	public class BundleItem
	{
		public string Name;

		public BundleType Type;

		public string[] Dependencies;
	}

	public enum BundleType
	{
		Module,
		Image,
		Atlas
	}
}