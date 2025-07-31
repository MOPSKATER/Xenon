using System;
using System.Linq;
using MelonLoader;
using UnityEngine;

namespace Xenon.Mods
{
    internal class BulletScaler : Mod
    {
        public static void PostCreateProjectile(ref string path, ref ProjectileBase __result)
        {
            if (BulletScaler.excludePaths.Contains(path))
            {
                return;
            }
            __result._collisionRadiusDamageable = Settings.bulletSizeModifier.Value;
            var smth = __result.gameObject.GetComponent<Transform>();
            smth.localScale = new Vector3(Settings.bulletSizeModifier.Value, Settings.bulletSizeModifier.Value, Settings.bulletSizeModifier.Value);
        }

        private static readonly string[] excludePaths = new string[]
        {
            "Projectiles/ProjectileBomb",
            "Projectiles/ProjectileMine",
            "Projectiles/ProjectileRocketFast"
        };
    }
}
