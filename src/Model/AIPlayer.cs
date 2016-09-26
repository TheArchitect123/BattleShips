using System;
using System.Collections;
using System.Collections.Generic;

using System.Diagnostics;
using SwinGameSDK;

using NUnit;
using NUnit.Framework; 

namespace BattleShips
{
	/// <summary>
	/// The AIPlayer is a type of player. It can readomly deploy ships, it also has the
	/// functionality to generate coordinates and shoot at tiles
	/// </summary>

	[TestFixture]
	public abstract class AIPlayer : Player
	{

		/// <summary>
		/// Location can store the location of the last hit made by an
		/// AI Player. The use of which determines the difficulty.
		/// </summary>
		protected class Location
		{
			private int _Row;

			private int _Column;
			/// <summary>
			/// The row of the shot
			/// </summary>
			/// <value>The row of the shot</value>
			/// <returns>The row of the shot</returns>
			public int Row
			{
				get { return _Row; }
				set { _Row = value; }
			}

			/// <summary>
			/// The column of the shot
			/// </summary>
			/// <value>The column of the shot</value>
			/// <returns>The column of the shot</returns>
			public int Column
			{
				get { return _Column; }
				set { _Column = value; }
			}

			/// <summary>
			/// Sets the last hit made to the local variables
			/// </summary>
			/// <param name="row">the row of the location</param>
			/// <param name="column">the column of the location</param>
			public Location(int row, int column)
			{
				_Column = column;
				_Row = row;
			}

			/// <summary>
			/// Check if two locations are equal
			/// </summary>
			/// <param name="thisloc">location 1</param>
			/// <param name="otherloc">location 2</param>
			/// <returns>true if location 1 and location 2 are at the same spot</returns>
			public static bool operator ==(Location thisloc, Location otherloc)
			{
				if (!ReferenceEquals (thisloc, null) && !ReferenceEquals (otherloc, null)) {
					return thisloc.Row.Equals (otherloc.Row) && thisloc.Column.Equals (otherloc.Column);
				} else {
					return false;
				}
			}

			/// <summary>
			/// Check if two locations are not equal
			/// </summary>
			/// <param name="this">location 1</param>
			/// <param name="other">location 2</param>
			/// <returns>true if location 1 and location 2 are not at the same spot</returns>
			public static bool operator !=(Location thisloc, Location otherloc)
			{
				if (ReferenceEquals (thisloc, null) || ReferenceEquals (otherloc, null)) {
					return true;
				}
				return !thisloc.Row.Equals(otherloc.Row) || !thisloc.Column.Equals(otherloc.Column);
			}
		}


		public AIPlayer(BattleShipsGame game) : base(game)
		{
		}

		/// <summary>
		/// Generate a valid row, column to shoot at
		/// </summary>
		/// <param name="row">output the row for the next shot</param>
		/// <param name="column">output the column for the next show</param>
		protected abstract void GenerateCoords(ref int row, ref int column);

		/// <summary>
		/// The last shot had the following result. Child classes can use this
		/// to prepare for the next shot.
		/// </summary>
		/// <param name="result">The result of the shot</param>
		/// <param name="row">the row shot</param>
		/// <param name="col">the column shot</param>
		protected abstract void ProcessShot(int row, int col, AttackResult result);

		/// <summary>
		/// The AI takes its attacks until its go is over.
		/// </summary>
		/// <returns>The result of the last attack</returns>

		[Test]
		public override AttackResult Attack()
		{
			AttackResult result = default(AttackResult);
			int row = 0;
			int column = 0;

			//keep hitting until a miss
			do
			{
				Delay();

				GenerateCoords(ref row, ref column);
				//generate coordinates for shot
				try {
					result = _game.Shoot (row, column);
					if (result == null) {
						throw new AssertionException ("Unit test AttackResult has failed");
					} else {
						Assert.IsTrue (result != null);
						Console.WriteLine ("AttackResult() successful");
					}
				}
				catch(AssertionException) {
					Console.WriteLine ("Unit test AttackResult has failed");
				}
				//take shot
				ProcessShot(row, column, result);
			} while (result.Value != ResultOfAttack.Miss && result.Value != ResultOfAttack.GameOver && !SwinGame.WindowCloseRequested());

			return result;
		}

		/// <summary>
		/// Wait a short period to simulate the think time
		/// </summary>
		private void Delay()
		{
			int i = 0;
			for (i = 0; i <= 50; i++) {
			//Dont delay if window is closed
				if (SwinGame.WindowCloseRequested())
				return;

			SwinGame.Delay(5); // CHANGE DELAY TO BE SUITABLE
			SwinGame.ProcessEvents();
			SwinGame.RefreshScreen();
		}
	}
}
}