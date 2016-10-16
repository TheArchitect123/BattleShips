using NUnit.Framework;
using System;
using BattleShips;

namespace BattleShipTest
{
	[TestFixture ()]
	public class GameControllerTest
	{
		[Test ()]
		public static void AddNewStateTest ()
		{
			GameController.AddNewState (GameState.ViewingGameMenu);
			GameController.AddNewState (GameState.Discovering);
			GameController.AddNewState (GameState.Deploying);

			GameState state = GameController.CurrentState;

			Assert.AreEqual ("Deploying", state.ToString (), "Fail to check the Game status after add a new status");
		}
	}
}
