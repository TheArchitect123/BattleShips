
using System;
using System.Collections;
using System.Collections.Generic;

using System.Diagnostics;
using SwinGameSDK;

namespace BattleShips
{
	public class GameLogic
	{
		public static void Main()
		{
			//Opens a new Graphics Window
			SwinGame.OpenGraphicsWindow("Battle Ships", 800, 600);

			//Load Resources
			GameResources.LoadResources();

			//plays the background music
			SwinGame.PlayMusic(GameResources.GameMusic("Background"));

			//Game Loop
			do
			{
				GameController.HandleUserInput();
				GameController.DrawScreen();
			} while (!(SwinGame.WindowCloseRequested() == true | GameController.CurrentState == GameState.Quitting));

			SwinGame.StopMusic();

			//Free Resources and Close Audio, to end the program.
			GameResources.FreeResources();
		}
	}
}