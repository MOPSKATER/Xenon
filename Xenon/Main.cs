using HarmonyLib;
using MelonLoader;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Xenon.Mods;

namespace Xenon
{
    public class Main : MelonMod
    {
        public static Game Game { get; private set; }

        public static LevelRushStats RushStats { get; private set; }

        public override void OnLateInitializeMelon()
        {
            AntiCheat.Anticheat.TriggerAnticheat();
            PatchGame();
            Game game = Singleton<Game>.Instance;
            Settings.Register();
            if (game == null)
                return;
            Game = game;
            Game.OnLevelLoadComplete += OnLevelLoadComplete;

            if (RM.drifter)
                OnLevelLoadComplete();
        }

        private void PatchGame()
        {
            HarmonyLib.Harmony harmony = new("de.MOPSKATER.NeonTrainer");

            MethodInfo target = typeof(MechController).GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance);
            HarmonyMethod patch = new(typeof(UIZipline).GetMethod("PostUpdate"));
            harmony.Patch(target, null, patch);

            target = typeof(LevelRush).GetMethod("UseMiracle");
            patch = new HarmonyMethod(typeof(Katana).GetMethod("PreUseMiracle"));
            harmony.Patch(target, patch);

            target = typeof(LevelRush).GetMethod("CanUseMiracle");
            patch = new HarmonyMethod(typeof(Katana).GetMethod("PreCanUseMiracle"));
            harmony.Patch(target, patch);

            target = typeof(Exploder).GetMethod("BulletImpact");
            patch = new HarmonyMethod(typeof(PhantomVisualiser).GetMethod("PreBulletImpact"));
            harmony.Patch(target, patch);

            target = typeof(ProjectileBase).GetMethod("UpdateTime");
            patch = new HarmonyMethod(typeof(PhantomVisualiser).GetMethod("PostUpdateTime"));
            harmony.Patch(target, null, patch);

            target = typeof(ProjectileBase).GetMethod("CreateProjectile", new Type[]
            {
                typeof(string),
                typeof(Vector3),
                typeof(Vector3),
                typeof(ProjectileWeapon)
            });
			patch = new HarmonyMethod(typeof(BulletScaler).GetMethod("PostCreateProjectile"));
            harmony.Patch(target, null, patch);

        }

        private void OnLevelLoadComplete()
        {
            RushStats = LevelRush.GetCurrentLevelRush();

            if (SceneManager.GetActiveScene().name.Equals("Heaven_Environment"))
            {
                TimeController.Reset();
                return;
            }

            GameObject modObject = new("Mod Manager");
            modObject.AddComponent<ModManager>();
        }
    }
    public static class Settings
    {
        public const string name = "Xenon";
        public static MelonPreferences_Category keyBindings;

        public static MelonPreferences_Entry<KeyCode> noClip;
        public static MelonPreferences_Entry<KeyCode> miracle;

        public static MelonPreferences_Entry<KeyCode> radiusZipline;
        public static MelonPreferences_Entry<KeyCode> radiusBoof;
        public static MelonPreferences_Entry<KeyCode> radiusClear;

        public static MelonPreferences_Entry<KeyCode> saveModifier;
        public static MelonPreferences_Entry<KeyCode> loadModifier;
        public static MelonPreferences_Entry<KeyCode> fullLoadModifier;

        public static MelonPreferences_Entry<KeyCode> speedUp;
        public static MelonPreferences_Entry<KeyCode> speedDown;
        public static MelonPreferences_Entry<KeyCode> speedReset;

        public static List<MelonPreferences_Entry<KeyCode>> teleport = [];

        public static MelonPreferences_Category xenonSettings;
        public static MelonPreferences_Entry<bool> speedShow;
        public static MelonPreferences_Entry<float> bulletSizeModifier;

        //public static MelonPreferences_Entry<bool> teleportUseModifier;

        //public static MelonPreferences_Entry<int> numberTpSlots;


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
            //fullLoadModifier = keyBindings.CreateEntry("Full load modifier", KeyCode.None);


            speedUp = keyBindings.CreateEntry("Speed up", KeyCode.UpArrow, description: "Increase the rate of time by 10%");
            speedDown = keyBindings.CreateEntry("Speed down", KeyCode.DownArrow, description: "Decrease the rate of time by 10%");
            speedReset = keyBindings.CreateEntry("Speed reset", KeyCode.LeftArrow, description: "Reset the rate of time");

            for (int slot = 0; slot < 10; slot++)
            {
                teleport.Add(keyBindings.CreateEntry($"Savestate slot {slot}", KeyCode.Alpha0 + slot));
            }
            xenonSettings = MelonPreferences.CreateCategory("Xenon Settings");

            speedShow = xenonSettings.CreateEntry("Show time rate?", true);

            bulletSizeModifier = xenonSettings.CreateEntry("Bullet Hitbox", 0.01f, null, "WARNING: Setting to super high numbers WILL freeze your game");


            //numberTpSlots = tpSettings.CreateEntry("Savestate slots", 10);
        }
    }
}
