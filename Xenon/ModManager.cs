using UnityEngine;
using Xenon.Mods;

namespace Xenon
{
    internal class ModManager : MonoBehaviour
    {
        private readonly Type[] mods =
        {
            typeof(Noclip),
            typeof(Teleport),
            typeof(TimeController),
            typeof(Sphere),
            typeof(Katana),
            typeof(Help),
            typeof(BulletScaler)
        };

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
