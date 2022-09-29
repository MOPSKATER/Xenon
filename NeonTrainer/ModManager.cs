using NeonTrainer.Mods;
using UnityEngine;

namespace NeonTrainer
{
    internal class ModManager : MonoBehaviour
    {
        private readonly Type[] mods = { typeof(CharakterInfo), typeof(Noclip), typeof(Teleport), typeof(TimeController), typeof(Sphere), typeof(Katana), typeof(Help) };

        void Awake()
        {
            foreach (Type type in mods)
                gameObject.AddComponent(type);
        }

        void Update()
        {
            // TODO
        }
    }
}
