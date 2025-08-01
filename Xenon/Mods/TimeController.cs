using UnityEngine;
using UniverseLib.Input;

namespace Xenon.Mods
{
    internal class TimeController : Mod
    {
        private static float currentScale = 1f;
        private const float scaleStep = 0.05f;

        void Awake()
        {
            Debug.Log("Scale " + currentScale);
            RM.time.SetTargetTimescale(currentScale);
        }

        void Update()
        {
            if (InputManager.GetKeyDown(Settings.speedUp.Value))
            {
                float newTime = RM.time.GetCurrentTimeScale() + scaleStep;
                if (newTime > 2f) return;

                RM.time.SetTargetTimescale(newTime);
                currentScale = newTime;
            }
            else if (InputManager.GetKeyDown(Settings.speedDown.Value))
            {
                float newTime = RM.time.GetCurrentTimeScale() - scaleStep;
                if (newTime <= 0.15f) return;

                RM.time.SetTargetTimescale(newTime);
                currentScale = newTime;
            }
        }

        public static void Reset() => currentScale = 1f;
    }
}
