using UnityEngine.InputSystem;

namespace Xenon.Mods
{
    internal class Katana : Mod
    {
        void Update()
        {
            if (Keyboard.current.kKey.wasPressedThisFrame)
                GS.AddCard("KATANA_MIRACLE");
        }

        public static bool PreUseMiracle()
        {
            if (!AntiCheat.Anticheat.IsAnticheatTriggered()) return true;
            return false;
        }

        public static bool PreCanUseMiracle(ref bool __result)
        {
            if (!AntiCheat.Anticheat.IsAnticheatTriggered()) return true;
            __result = true;
            return false;
        }
    }
}
