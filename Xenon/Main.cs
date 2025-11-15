using HarmonyLib;
using MelonLoader;
using Semver;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using Xenon.Mods;

namespace Xenon
{
    public class Main : MelonMod
    {
        private bool oldAntiCheat = false;
        public static Game Game
        {
            get; private set;
        }

        public static LevelRushStats RushStats
        {
            get; private set;
        }

        private readonly GUIStyle AntiCheatStyle = new()
        {
            fontSize = 20,
            fontStyle = FontStyle.Bold
        };
        public override void OnLateInitializeMelon()
        {
            bool foundNeonLite = false;
            foreach (var item in RegisteredMelons)
            {
                if (item.Info.Name == "NeonLite" && item.Info.SemanticVersion > SemVersion.Parse("3.0.0"))
                {
                    Debug.Log("[Xenon] Found NeonLite AntiCheat.");
                    NeonLite.Modules.Anticheat.Register(MelonAssembly);
                    foundNeonLite = true;
                    break;
                }
            }
            if (!foundNeonLite)
            {
                Debug.Log("[Xenon] Didn't find NeonLite AntiCheat.");
                AntiCheat.Anticheat.TriggerAnticheat();
                oldAntiCheat = true;
            }
            PatchGame();
            Game game = Singleton<Game>.Instance;
            Settings.Register();
            if (game == null)
            {
                return;
            }
            Game = game;
            Game.OnLevelLoadComplete += OnLevelLoadComplete;

            if (RM.drifter)
            {
                OnLevelLoadComplete();
            }
            Debug.Log("[Xenon] Completed setup.");
        }
        public override void OnGUI()
        {
            if (!(oldAntiCheat && RM.mechController.GetIsAlive()))
            {
                return;
            }
            AntiCheatStyle.normal.textColor = Color.white;
            GUI.Label(new Rect(10, 10, 150, 30),
                      "AntiCheat active",
                      AntiCheatStyle);
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
}
