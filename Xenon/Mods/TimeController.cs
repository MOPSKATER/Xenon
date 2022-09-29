using UnityEngine.InputSystem;

namespace Xenon.Mods
{
    internal class TimeController : Mod
    {
        public static float currentScale = 1f;
        private const float scaleStep = 0.05f;

        void Awake()
        {
            RM.time.SetTargetTimescale(currentScale);
        }

        void Update()
        {
            if (Keyboard.current.upArrowKey.wasPressedThisFrame)
            {
                float newTime = RM.time.GetCurrentTimeScale() + scaleStep;
                if (newTime <= 2f)
                    RM.time.SetTargetTimescale(newTime);
            }
            else if (Keyboard.current.downArrowKey.wasPressedThisFrame)
            {
                float newTime = RM.time.GetCurrentTimeScale() - scaleStep;
                if (newTime > 0.15f)
                    RM.time.SetTargetTimescale(newTime);
            }
        }
    }
}
