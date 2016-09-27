
using NUnit.Framework;
using SwinGameSDK;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace BattleShips
{
    [TestFixture]
    /// <summary>
    /// Controls displaying and collecting high score data.
    /// </summary>
    /// <remarks>
    /// Data is saved to a file.
    /// </remarks>
    public static class HighScoreController
    {
        private const int NAME_WIDTH = 3;

        private const int SCORES_LEFT = 490;
        /// <summary>
        /// The score structure is used to keep the name and
        /// score of the top players together.
        /// </summary>
        private struct Score : IComparable
        {
            public string Name;

            public int Value;
            /// <summary>
            /// Allows scores to be compared to facilitate sorting
            /// </summary>
            /// <param name="obj">the object to compare to</param>
            /// <returns>a value that indicates the sort order</returns>
            public int CompareTo(object obj)
            {
                if (obj is Score)
                {
                    Score other = (Score)obj;

                    return other.Value - this.Value;
                }
                else {
                    return 0;
                }
            }
        }


        private static List<Score> _Scores = new List<Score>();
  
        [Test]
        public static void FileFoundScoresTest()
        {
            string filename = null;
            filename = SwinGame.PathToResource("highscores.txt");

            StreamReader output = default(StreamReader);
            output = new StreamReader(filename);
            Assert.NotNull(output, "File not found");

            output.Close();
        }
        [Test]
        public static void LoadScoresTest()
        {
            Score A = new Score();
            A.Name = "AAA";
            A.Value = 100;
            Score B = new Score();
            B.Name = "BBB";
            B.Value = 90;
            Score C = new Score();
            C.Name = "CCC";
            C.Value = 80;
            Score D = new Score();
            D.Name = "DDD";
            D.Value = 70;
            Score E = new Score();
            E.Name = "EEE";
            E.Value = 60;
            Score F = new Score();
            F.Name = "FFF";
            F.Value = 50;
            Score G = new Score();
            G.Name = "GGG";
            G.Value = 40;
            Score H = new Score();
            H.Name = "HHH";
            H.Value = 30;
            Score I = new Score();
            I.Name = "III";
            I.Value = 20;
            Score J = new Score();
            J.Name = "JJJ";
            J.Value = 10;
            List<Score> testScore = new List<Score>()
            { A,B,C,D,E,F,G,H,I,J };

            string filename = null;
            filename = SwinGame.PathToResource("highscores.txt");

            StreamReader input = default(StreamReader);
            input = new StreamReader(filename);

            //Read in the # of scores
            int numScores = 0;
            numScores = Convert.ToInt32(input.ReadLine());
  
            _Scores.Clear();

            int i = 0;

            for (i = 1; i <= numScores; i++)
            {
                Score s = default(Score);
                string line = null;

                line = input.ReadLine();

                s.Name = line.Substring(0, NAME_WIDTH);
                s.Value = Convert.ToInt32(line.Substring(NAME_WIDTH));
                _Scores.Add(s);
            }
            input.Close();
            Assert.AreEqual(_Scores, testScore, "High Score Not Equal");
        }
        /// <summary>
        /// Loads the scores from the highscores text file.
        /// </summary>
        /// <remarks>
        /// The format is
        /// # of scores
        /// NNNSSS
        /// 
        /// Where NNN is the name and SSS is the score
        /// </remarks>

        private static void LoadScores()
        {
            string filename = null;
            filename = SwinGame.PathToResource("highscores.txt");

            StreamReader input = default(StreamReader);
            input = new StreamReader(filename);

            //Read in the # of scores
            int numScores = 0;
            numScores = Convert.ToInt32(input.ReadLine());

            _Scores.Clear();

            int i = 0;

            for (i = 1; i <= numScores; i++)
            {
                Score s = default(Score);
                string line = null;

                line = input.ReadLine();

                s.Name = line.Substring(0, NAME_WIDTH);
                s.Value = Convert.ToInt32(line.Substring(NAME_WIDTH));
                _Scores.Add(s);
            }
            input.Close();
        }

        [Test]
        public static void SaveScoresTest()
        {
            List<String> testScoreFormat = new List<string>()
            {"10","AAA100","BBB90","CCC80","DDD70","EEE60","FFF50","GGG40","HHH30","III20","JJJ10"};

            List<String> testScoreActual = new List<string>();

            string filename = null;
            filename = SwinGame.PathToResource("highscores.txt");

            StreamWriter output = default(StreamWriter);
            output = new StreamWriter(filename);

            output.WriteLine(_Scores.Count);
            testScoreActual.Add(_Scores.Count.ToString());

            foreach (Score s in _Scores)
            {
                output.WriteLine(s.Name + s.Value);
                testScoreActual.Add(s.Name + s.Value.ToString());
            }

            output.Close();
                    
            Assert.AreEqual(testScoreActual, testScoreFormat, "High Score Not Equal");
        }
        /// <summary>
        /// Saves the scores back to the highscores text file.
        /// </summary>
        /// <remarks>
        /// The format is
        /// # of scores
        /// NNNSSS
        /// 
        /// Where NNN is the name and SSS is the score
        /// </remarks>
        private static void SaveScores()
        {
            string filename = null;
            filename = SwinGame.PathToResource("highscores.txt");

            StreamWriter output = default(StreamWriter);
            output = new StreamWriter(filename);

            output.WriteLine(_Scores.Count);

            foreach (Score s in _Scores)
            {
                output.WriteLine(s.Name + s.Value);
            }

            output.Close();
        }

        /// <summary>
        /// Draws the high scores to the screen.
        /// </summary>
        public static void DrawHighScores()
        {
            const int SCORES_HEADING = 40;
            const int SCORES_TOP = 80;
            const int SCORE_GAP = 30;

            if (_Scores.Count == 0)
                LoadScores();

            SwinGame.DrawText("   High Scores   ", Color.White, GameResources.GameFont("Courier"), SCORES_LEFT, SCORES_HEADING);

            //For all of the scores
            int i = 0;
            for (i = 0; i <= _Scores.Count - 1; i++)
            {
                Score s = default(Score);

                s = _Scores[i];

                //for scores 1 - 9 use 01 - 09
                if (i < 9)
                {
                    SwinGame.DrawText(" " + (i + 1) + ":   " + s.Name + "   " + s.Value, Color.White, GameResources.GameFont("Courier"), SCORES_LEFT, SCORES_TOP + i * SCORE_GAP);
                }
                else {
                    SwinGame.DrawText(i + 1 + ":   " + s.Name + "   " + s.Value, Color.White, GameResources.GameFont("Courier"), SCORES_LEFT, SCORES_TOP + i * SCORE_GAP);
                }
            }
        }

        /// <summary>
        /// Handles the user input during the top score screen.
        /// </summary>
        /// <remarks></remarks>
        public static void HandleHighScoreInput()
        {
            if (SwinGame.MouseClicked(MouseButton.LeftButton) || SwinGame.KeyTyped(KeyCode.vk_ESCAPE) || SwinGame.KeyTyped(KeyCode.vk_RETURN))
            {
                GameController.EndCurrentState();
            }
        }

        /// <summary>
        /// Read the user's name for their highsSwinGame.
        /// </summary>
        /// <param name="value">the player's sSwinGame.</param>
        /// <remarks>
        /// This verifies if the score is a highsSwinGame.
        /// </remarks>
        public static void ReadHighScore(int value)
        {
            const int ENTRY_TOP = 500;

            if (_Scores.Count == 0)
                LoadScores();

            //is it a high score
            if (value > _Scores[_Scores.Count - 1].Value)
            {
                Score s = new Score();
                s.Value = value;

                GameController.AddNewState(GameState.ViewingHighScores);

                int x = 0;
                x = SCORES_LEFT + SwinGame.TextWidth(GameResources.GameFont("Courier"), "Name: ");

                SwinGame.StartReadingText(Color.White, NAME_WIDTH, GameResources.GameFont("Courier"), x, ENTRY_TOP);

                //Read the text from the user
                while (SwinGame.ReadingText())
                {
                    SwinGame.ProcessEvents();

                    UtilityFunctions.DrawBackground();
                    DrawHighScores();
                    SwinGame.DrawText("Name: ", Color.White, GameResources.GameFont("Courier"), SCORES_LEFT, ENTRY_TOP);
                    SwinGame.RefreshScreen();
                }

                s.Name = SwinGame.TextReadAsASCII();

                if (s.Name.Length < 3)
                {
                    s.Name = s.Name + new string(Convert.ToChar(" "), 3 - s.Name.Length);
                }

                _Scores.RemoveAt(_Scores.Count - 1);
                _Scores.Add(s);
                _Scores.Sort();

                GameController.EndCurrentState();
            }
        }
    }
}