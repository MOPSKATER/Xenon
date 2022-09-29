using UnityEngine.InputSystem;

namespace Xenon.Mods
{
    internal class Noclip : Mod
    {
        private bool _active;
        void Update()
        {
            if (Keyboard.current.nKey.wasPressedThisFrame)
            {
                _active = !_active;
                RM.drifter.SetNoclip(_active);
            }
        }
    }
}
