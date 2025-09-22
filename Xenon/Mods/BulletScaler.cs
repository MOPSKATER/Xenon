using System;
using System.Linq;
using MelonLoader;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UniverseLib.Input;

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
            Transform bulletTransform = __result.gameObject.GetComponent<Transform>();

            if (Settings.useBulletSize.Value)
            {
                __result._collisionRadiusDamageable = Settings.bulletSizeModifier.Value;
                __result.gameObject.GetComponent<SphereCollider>().radius = Settings.bulletSizeModifier.Value;
                float halvedValue = Settings.bulletSizeModifier.Value / 2;
                bulletTransform.localScale = new Vector3(halvedValue, halvedValue, halvedValue);
            }
        }

        private static readonly string[] excludePaths = new string[]
        {
            "Projectiles/ProjectileBomb",
            "Projectiles/ProjectileMine",
            "Projectiles/ProjectileRocketFast"
        };
    }
}
