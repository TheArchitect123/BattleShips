using System;
using SwinGameSDK;

namespace BattleShips
{
	/// <summary>
	/// The battle phase is handled by the DiscoveryController.
	/// </summary>
	public static class DiscoveryController
	{
		/// <summary>
		/// The exit button dimensions.
		/// </summary>
		private const int EXIT_BUTTON_LEFT = 693;
		private const int TOP_BUTTONS_TOP = 72;
		private const int TOP_BUTTONS_HEIGHT = 46;
		private const int PLAY_BUTTON_WIDTH = 80;
		/// <summary>
		/// Handles input during the discovery phase of the game.
		/// </summary>
		/// <remarks>
		/// Escape opens the game menu. Clicking the mouse will
		/// attack a location.
		/// </remarks>
		public static void HandleDiscoveryInput()
		{
			if (SwinGame.KeyTyped(KeyCode.vk_ESCAPE))
			{
				GameController.AddNewState(GameState.ViewingGameMenu);
			}

			if (SwinGame.MouseClicked(MouseButton.LeftButton))
			{
				if (UtilityFunctions.IsMouseInRectangle (EXIT_BUTTON_LEFT, TOP_BUTTONS_TOP, PLAY_BUTTON_WIDTH, TOP_BUTTONS_HEIGHT)) {
					GameController.AddNewState (GameState.ViewingGameMenu);
				}

				DoAttack();
			}
		}

		/// <summary>
		/// Attack the location that the mouse if over.
		/// </summary>
		private static void DoAttack()
		{
			Point2D mouse = default(Point2D);

			mouse = SwinGame.MousePosition();

			//Calculate the row/col clicked
			int row = 0;
			int col = 0;
			row = Convert.ToInt32(Math.Floor((mouse.Y - UtilityFunctions.FIELD_TOP) / (UtilityFunctions.CELL_HEIGHT + UtilityFunctions.CELL_GAP)));
			col = Convert.ToInt32(Math.Floor((mouse.X - UtilityFunctions.FIELD_LEFT) / (UtilityFunctions.CELL_WIDTH + UtilityFunctions.CELL_GAP)));

			if (row >= 0 & row < GameController.HumanPlayer.EnemyGrid.Height)
			{
				if (col >= 0 & col < GameController.HumanPlayer.EnemyGrid.Width)
				{
					GameController.Attack(row, col);
				}
			}
		}

		/// <summary>
		/// Draws the game during the attack phase.
		/// </summary>s
		public static void DrawDiscovery()
		{
			const int SCORES_LEFT = 172;
			const int SHOTS_TOP = 157;
			const int HITS_TOP = 206;
			const int SPLASH_TOP = 256;

			if ((SwinGame.KeyDown(KeyCode.vk_SPACE))
			{
				UtilityFunctions.DrawField(GameController.ComputerPlayer.PlayerGrid, GameController.ComputerPlayer, true);
			}
			else {
				UtilityFunctions.DrawField(GameController.HumanPlayer.EnemyGrid, GameController.ComputerPlayer, false);
			}

			UtilityFunctions.DrawSmallField(GameController.HumanPlayer.PlayerGrid, GameController.HumanPlayer);
			UtilityFunctions.DrawMessage();

			SwinGame.DrawText(GameController.HumanPlayer.Shots.ToString(), Color.White, GameResources.GameFont("Menu"), SCORES_LEFT, SHOTS_TOP);
			SwinGame.DrawText(GameController.HumanPlayer.Hits.ToString(), Color.White, GameResources.GameFont("Menu"), SCORES_LEFT, HITS_TOP);
			SwinGame.DrawText(GameController.HumanPlayer.Missed.ToString(), Color.White, GameResources.GameFont("Menu"), SCORES_LEFT, SPLASH_TOP);

			SwinGame.DrawBitmap(GameResources.GameImage("ExitButton"), EXIT_BUTTON_LEFT, TOP_BUTTONS_TOP);
		}

	}
}