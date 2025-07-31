using UniverseLib.Input;

namespace Xenon.Mods
{
    internal class Noclip : Mod
    {
        private bool _active;
        void Update()
        {
            if (InputManager.GetKeyDown(Settings.noClip.Value))
            {
                _active = !_active;
                RM.drifter.SetNoclip(_active);
            }
        }
    }
}
