using UnityEngine.InputSystem;

namespace NeonTrainer.Mods
{
    internal class TimeController : Mod
    {
        private const float scaleStep = 0.05f;

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
