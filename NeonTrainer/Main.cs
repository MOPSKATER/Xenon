using HarmonyLib;
using MelonLoader;
using NeonTrainer.Mods;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NeonTrainer
{
    public class Main : MelonMod
    {
        public static Game Game { get; private set; }

        public static LevelRushStats RushStats { get; private set; }

        public override void OnApplicationLateStart()
        {
            AntiCheat.Anticheat.TriggerAnticheat();
            PatchGame();
            Game game = Singleton<Game>.Instance;

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
            HarmonyMethod patch = new (typeof(UIZipline).GetMethod("PostUpdate"));
            harmony.Patch(target, null, patch);

            target = typeof(LevelRush).GetMethod("UseMiracle");
            patch = new HarmonyMethod(typeof(Katana).GetMethod("PreUseMiracle"));
            harmony.Patch(target, patch);

            target = typeof(LevelRush).GetMethod("CanUseMiracle");
            patch = new HarmonyMethod(typeof(Katana).GetMethod("PreCanUseMiracle"));
            harmony.Patch(target, patch);
        }

        private void OnLevelLoadComplete()
        {
            RushStats = LevelRush.GetCurrentLevelRush();

            if (SceneManager.GetActiveScene().name.Equals("Heaven_Environment"))
                return;

            GameObject modObject = new("Mod Manager");
            modObject.AddComponent<ModManager>();
        }
    }
}
