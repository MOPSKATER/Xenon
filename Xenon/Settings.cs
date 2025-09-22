using MelonLoader;
using MelonLoader.Preferences;
using UnityEngine;

namespace Xenon
{
    public static class Settings
    {
        public static MelonPreferences_Category keyBindings;

        public static MelonPreferences_Entry<KeyCode> noClip;
        public static MelonPreferences_Entry<KeyCode> miracle;

        public static MelonPreferences_Entry<KeyCode> radiusZipline;
        public static MelonPreferences_Entry<KeyCode> radiusBoof;
        public static MelonPreferences_Entry<KeyCode> radiusClear;

        public static MelonPreferences_Entry<KeyCode> saveModifier;
        public static MelonPreferences_Entry<KeyCode> loadModifier;

        public static MelonPreferences_Entry<KeyCode> speedUp;
        public static MelonPreferences_Entry<KeyCode> speedDown;
        public static MelonPreferences_Entry<KeyCode> speedReset;
        public static MelonPreferences_Entry<KeyCode> matrixKey1;
        public static MelonPreferences_Entry<KeyCode> matrixKey2;
        public static MelonPreferences_Entry<float> matrixSpeed1;
        public static MelonPreferences_Entry<float> matrixSpeed2;

        public static List<MelonPreferences_Entry<KeyCode>> teleport = [];

        public static MelonPreferences_Category xenonSettings;

        public static MelonPreferences_Entry<bool> speedShow;
        public static MelonPreferences_Entry<bool> useBulletSize;
        public static MelonPreferences_Entry<float> bulletSizeModifier;


        public static void Register()
        {
            keyBindings = MelonPreferences.CreateCategory("Xenon Binds");

            noClip = keyBindings.CreateEntry("Noclip", KeyCode.N);
            miracle = keyBindings.CreateEntry("Miracle katana", KeyCode.K);

            radiusZipline = keyBindings.CreateEntry("Zipline sphere", KeyCode.V, description: "Spawn a sphere where you are looking with radius of zipline range");
            radiusBoof = keyBindings.CreateEntry("Boof sphere", KeyCode.B, description: "Spawn a sphere where you are looking with radius of boof range");
            radiusClear = keyBindings.CreateEntry("Clear spheres", KeyCode.C);

            saveModifier = keyBindings.CreateEntry("Save modifier", KeyCode.LeftControl);
            loadModifier = keyBindings.CreateEntry("Load modifier", KeyCode.LeftShift);


            speedUp = keyBindings.CreateEntry("Speed up", KeyCode.UpArrow, description: "Increase the rate of time by 10%");
            speedDown = keyBindings.CreateEntry("Speed down", KeyCode.DownArrow, description: "Decrease the rate of time by 10%");
            speedReset = keyBindings.CreateEntry("Speed reset", KeyCode.LeftArrow, description: "Reset the rate of time");
            matrixKey1 = keyBindings.CreateEntry("Matrix button 1", KeyCode.F1, description: "Enter Matrix™ Mode 1 while holding this key");
            matrixKey2 = keyBindings.CreateEntry("Matrix button 2", KeyCode.F2, description: "Enter Matrix™ Mode 2 while holding this key");

            for (int slot = 0; slot < 10; slot++)
            {
                teleport.Add(keyBindings.CreateEntry($"Savestate slot {slot}", KeyCode.Alpha0 + slot));
            }
            xenonSettings = MelonPreferences.CreateCategory("Xenon Settings");

            speedShow = xenonSettings.CreateEntry("Show time rate?", true);
            matrixSpeed1 = xenonSettings.CreateEntry("Matrix speed 1", 0.5f, description: "What speed to go at when in Matrix™ Mode 1", validator: new ValueRange<float>(0.15f, 2f));
            matrixSpeed2 = xenonSettings.CreateEntry("Matrix speed 2", 2f, description: "What speed to go at when in Matrix™ Mode 2", validator: new ValueRange<float>(0.15f, 2f));


            useBulletSize = xenonSettings.CreateEntry("Activate BulletScaler?", true);

            bulletSizeModifier = xenonSettings.CreateEntry("Bullet Hitbox", 0.01f, null, "WARNING: Setting above 1000 may freeze your game");

        }
    }
}
