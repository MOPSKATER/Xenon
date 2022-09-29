using UnityEngine;

namespace Xenon.Mods
{
    internal abstract class Mod : MonoBehaviour
    {
        public string Name { get; private set; }
        public string[] DisplayInfo { get; private set; }
    }
}
