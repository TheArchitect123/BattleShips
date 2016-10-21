using NUnit.Framework;

namespace BattleShips
{
	[TestFixture]
	public class ZaneUnitTests
	{
		private Tile testTile;

		[SetUp]
		public void Init ()
		{
			testTile = new Tile (0, 0, null);
			//Initialise variables here. DO give variables a value here
			//This is called automatically before each test
		}

		[Test]
		public void Test1Shoot ()
		{
			Assert.AreEqual (false, testTile.Shot);
			testTile.Shoot ();
			Assert.AreEqual (true, testTile.Shot);
		}
	}
}