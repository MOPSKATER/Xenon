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
            __result._collisionRadiusDamageable = Settings.bulletSizeModifier.Value;
            __result.gameObject.GetComponent<SphereCollider>().radius = Settings.bulletSizeModifier.Value;
            var smth = __result.gameObject.GetComponent<Transform>();
            var fudgedV = Settings.bulletSizeModifier.Value / 2;
            smth.localScale = new Vector3(fudgedV, fudgedV, fudgedV);
        }

        private static readonly string[] excludePaths = new string[]
        {
            "Projectiles/ProjectileBomb",
            "Projectiles/ProjectileMine",
            "Projectiles/ProjectileRocketFast"
        };
    }
}
