using UniverseLib.Input;

namespace Xenon.Mods
{
    internal class Katana : Mod
    {
        void Update()
        {
            if (InputManager.GetKeyDown(Settings.miracle.Value))
                GS.AddCard("KATANA_MIRACLE");
        }

        public static bool PreUseMiracle() => (!AntiCheat.Anticheat.IsAnticheatTriggered());

        public static bool PreCanUseMiracle(ref bool __result)
        {
            if (!AntiCheat.Anticheat.IsAnticheatTriggered()) return true;
            __result = true;
            return false;
        }
    }
}
