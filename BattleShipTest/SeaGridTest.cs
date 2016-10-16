using NUnit.Framework;
using System;
using BattleShips;
using System.Collections;
using System.Collections.Generic;


namespace BattleShipTest
{
	[TestFixture]
	public class SeaGridTest
	{
		[Test]
		public void MoveShipTest ()
		{
			Dictionary<ShipName, Ship> ships = new Dictionary<ShipName, Ship> ();
			ships.Add (ShipName.Destroyer, new Ship (ShipName.Destroyer));

			SeaGrid sg = new SeaGrid (ships);


			//sg.AddShip(0, 0,Direction.LeftRight,sh);

			sg.MoveShip (2, 0, ShipName.Destroyer, Direction.UpDown);

			Assert.AreEqual ("Ship", sg.Item(2,0).ToString() , "Fail to check the SeaGrid after move a ship");
		}
	}
}
