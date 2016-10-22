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
		}

		[Test]
		public void Test1Shoot ()
		{
			Assert.AreEqual (false, testTile.Shot);
			testTile.Shoot ();
			Assert.AreEqual (true, testTile.Shot);
		}

		[Test]
		public void Test2Ship ()
		{
			Assert.AreEqual (null, testTile.Ship);
			testTile.Ship = new Ship (ShipName.Tug);
			Assert.AreEqual ("BattleShips.Ship" , testTile.Ship.ToString());
			testTile.ClearShip ();
			Assert.AreEqual (null, testTile.Ship);
		}
	}
}