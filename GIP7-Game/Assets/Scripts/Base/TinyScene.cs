using System;

namespace Assets.Scripts.Base
{
    [Serializable]
    public struct TinyScene
    {
        public string name;
        public int index;
        public int Difficulty
        {
            get
            {
                return Convert.ToInt32(name.GetDifficulty());
            }
        }

        public override string ToString()
        {
            return name + " " + Difficulty;
        }
    }
}
