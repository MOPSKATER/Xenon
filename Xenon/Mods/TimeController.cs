using UnityEngine;
using UniverseLib.Input;

namespace Xenon.Mods
{
    internal class TimeController : Mod
    {
        private static float currentScale = 1f;
        private const float scaleStep = 0.05f;

        private GUIStyle _style;


        void Awake()
        {
            Debug.Log("Scale " + currentScale);
            RM.time.SetTargetTimescale(currentScale);
            _style = new()
            {
                fontSize = 36,
                fontStyle = FontStyle.Bold
            };
            _style.normal.textColor = Color.gray;

        }

        void Update()
        {
            if (InputManager.GetKeyDown(Settings.speedReset.Value))
            {
                Reset();
            }
            else if (InputManager.GetKeyDown(Settings.speedUp.Value))
            {
                float newTime = RM.time.GetCurrentTimeScale() + scaleStep;
                if (newTime > 2f) return;

                RM.time.SetTargetTimescale(newTime);
                currentScale = newTime;
            }
            else if (InputManager.GetKeyDown(Settings.speedDown.Value))
            {
                float newTime = RM.time.GetCurrentTimeScale() - scaleStep;
                if (newTime < 0.10f) return;

                RM.time.SetTargetTimescale(newTime);
                currentScale = newTime;
            }
        }

        public static void Reset() => currentScale = 1f;

        private void OnGUI()
        {
            if(!(RM.mechController.GetIsAlive() && currentScale != 1f && Settings.speedShow.Value)) return;

            GUI.Label(new Rect(Camera.main.pixelWidth - 600, 10, 100, 50), $"{currentScale:P0}", _style);

        }
    }
}
