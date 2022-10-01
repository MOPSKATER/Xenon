using System.Reflection;
using UnityEngine;

namespace Xenon.Mods
{
    internal class PhantomVisualiser : Mod
    {
        private static readonly FieldInfo _isInHitDecayMode = typeof(ProjectileBase).GetField("_isInHitDecayMode", BindingFlags.NonPublic | BindingFlags.Instance);

        public static bool PreBulletImpact() => false;

        public static void PostUpdateTime(ProjectileBase __instance)
        {
            if ((bool)_isInHitDecayMode.GetValue(__instance))
                __instance.SetMaterialPropertyFloat(BaseDamageable.shaderIDDissolve, 0.5f);
        }
    }
}
