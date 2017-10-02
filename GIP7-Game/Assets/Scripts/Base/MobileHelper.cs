using UnityEngine;

namespace Assets.Scripts.Base
{
    public abstract class MobileHelper
    {
        public static bool OnEditor
        {
            get
            {
#if UNITY_EDITOR
                return true;
#else
                return false;
#endif
            }
        }
        public static bool OnMobileDevice
        {
            get
            {
#if UNITY_STANDALONE
                return false;
#else
                return !OnEditor;
#endif
            }
        }

        public static bool OnTouchDevice
        {
            get
            {
                return OnMobileDevice || Input.touchSupported || Input.multiTouchEnabled;
            }
        }
    }
}
