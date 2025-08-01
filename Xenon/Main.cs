using HarmonyLib;
using MelonLoader;
using System.Reflection;
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
}
