using System;

namespace Assets.Scripts.Base
{
    [Serializable]
    public class Stats
    {
        public string categoryName;
        public int handle;
        public int fight;
        public int hide;
        public int levelDifficulty;
        public int coins;
        public int amountOfPlaythroughs;
        public DateTime lastFinishedPlaythrough;

        public int Total
        {
            get
            {
                return (fight + handle + hide);
            }
        }
    }
}
