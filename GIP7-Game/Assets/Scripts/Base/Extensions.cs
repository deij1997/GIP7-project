using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Assets.Scripts.Base
{
    public static class Extensions
    {
        public const char DIFFICULTY = 'd';

        /// <summary>
        /// Gets all numbers from a string
        /// </summary>
        /// <returns></returns>
        public static string[] GetNumbers(this string input)
        {
            List<string> strings = new List<string>();
            string ret = "";
            foreach (char c in input)
            {
                if (c.IsNumber())
                {
                    ret += c;
                }
                else
                {
                    if (ret.Length != 0)
                    {
                        strings.Add(ret);
                        ret = "";
                    }
                }
            }
            return strings.ToArray();
        }

        /// <summary>
        /// Gets the difficulty from a string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetDifficulty(this string input)
        {
            bool count = false;
            string ret = "";
            foreach (char c in input)
            {
                if (!count)
                {
                    if (c == DIFFICULTY)
                    {
                        count = true;
                    }
                }
                else
                {
                    if (c.IsNumber())
                    {
                        ret += c;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (ret.Length == 0)
            {
                ret = "0";
            }
            return ret;
        }

        /// <summary>
        /// Checks whether a character is a number. The safe way
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNumber(this char input)
        {
            switch (input)
            {
                case '0':
                    break;
                case '1':
                    break;
                case '2':
                    break;
                case '3':
                    break;
                case '4':
                    break;
                case '5':
                    break;
                case '6':
                    break;
                case '7':
                    break;
                case '8':
                    break;
                case '9':
                    break;
                default: 
                    return false;
            }
            return true;
        }
    }
}
