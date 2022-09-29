using UnityEngine.InputSystem;

namespace NeonTrainer.Mods
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
