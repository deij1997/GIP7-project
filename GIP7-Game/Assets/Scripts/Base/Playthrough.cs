using Assets.Scripts.Base.Exceptions;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Assets.Scripts.Base
{
    [Serializable]
    public class Playthrough
    {
        private List<int> levels = new List<int>();

        public List<int> Levels
        {
            get
            {
                return levels;
            }
        }

        public bool HasLevels
        {
            get
            {
                return levels.Count != 0;
            }
        }

        public bool LacksLevels
        {
            get
            {
                return !HasLevels;
            }
        }

        public bool NoMoreLevels
        {
            get
            {
                return currentLevel == levels.Count - 1;
            }
        }

        public int CurrentLevel
        {
            get
            {
                return currentLevel == -1 ? levels[0] : levels[currentLevel];
            }
        }

        private int currentLevel = -1;

        //coins
        [NonSerialized]
        private int levelGainedCoins = 0;
        private int playthroughCoins = 0;

        public int LevelGainedCoins { get { return levelGainedCoins; } }
        public int PlaythroughCoins { get { return playthroughCoins; } }

        //question part
        [OptionalField]
        private int goodQuestionsPlaythrough = 0;
        [OptionalField]
        private int badQuestionsPlaythrough = 0;

        [NonSerialized]
        private int goodQuestionsLevel = 0;
        [NonSerialized]
        private int badQuestionsLevel = 0;

        public int GoodQuestionsLevel { get { return goodQuestionsLevel; } }
        public int BadQuestionsLevel { get { return badQuestionsLevel; } }
        public int GoodQuestionsPlaythrough { get { return goodQuestionsPlaythrough; } }
        public int BadQuestionsPlaythrough {  get { return badQuestionsPlaythrough; } }

        public Playthrough()
        {

        }

        /// <summary>
        /// Raises the level number and returns it
        /// </summary>
        /// <returns></returns>
        public int NextLevel()
        {
            if (NoMoreLevels)
            {
                throw new NotEnoughLevelsException();
            }
            return levels[++currentLevel];
        }

        /// <summary>
        /// Increases the coins in the current level.
        /// Can be used when a player gives the right answer to a NPC in a level.
        /// </summary>
        /// <param name="coins">The amount of coins you want to give the player</param>
        public void IncreaseCoinsCurrentLevel(int coins)
        {
            playthroughCoins += coins;
        }

        /// <summary>
        /// This method is used to move the currently gained coins from the level to the gained coins from the playthrough.
        /// After the levelgainedcoins have been added to the playthroughcoins the levelgainedcoins will be reset.
        /// </summary>
        public void IncreaseCoinsAfterLevelEnded()
        {
            playthroughCoins += levelGainedCoins;
            levelGainedCoins = 0;
        }

        /// <summary>
        /// This method is used to increase the amount of good playthrough questions after the level has ended.
        /// </summary>
        public void IncreaseGoodQuestionsAfterLevelEnded()
        {
            goodQuestionsPlaythrough += goodQuestionsLevel;
            goodQuestionsLevel = 0;
        }

        /// <summary>
        /// This method increases the current amount of good questions in the level.
        /// </summary>
        /// <param name="amount">Increases the amount of good questions in the current level.</param>
        public void IncreaseGoodQuestionCurrentLevel(int amount)
        {
            goodQuestionsLevel += amount;
        }

        /// <summary>
        /// This method increases the current amount of bad questions in the level.
        /// </summary>
        /// <param name="amount">Increases the amount of bad questions in the current level.</param>
        public void IncreaseBadQuestionsCurrentLevel(int amount)
        {
            badQuestionsLevel += amount;
        }

        /// <summary>
        /// This method is used to increase the amount of bad playthrough questions after the level has ended.
        /// </summary>
        public void IncreaseBadQuestionsAfterLevelEnded()
        {
            badQuestionsPlaythrough += badQuestionsLevel;
            badQuestionsLevel = 0;
        }

        /// <summary>
        /// This method is used to increase the amount of good playthrough questions after the level has ended.
        /// </summary>
        /// <param name="amount">The amount of questions you want to increase.</param>
        public void IncreaseGoodQuestionsAfterLevelEnded(int amount)
        {
            goodQuestionsPlaythrough += amount;
            goodQuestionsLevel = 0;
        }

        /// <summary>
        /// This method is used to increase the amount of bad playthrough questions after the level has ended.
        /// </summary>
        /// <param name="amount">The amount of questions you want to increase.</param>
        public void IncreaseBadQuestionsAfterLevelEnded(int amount)
        {
            badQuestionsPlaythrough += amount;
            badQuestionsLevel = 0;
        }
    }
}
