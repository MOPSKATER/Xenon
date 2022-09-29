using UnityEngine;
using UnityEngine.InputSystem;

namespace NeonTrainer.Mods
{
    internal class Sphere : Mod
    {
        private readonly List<GameObject> _spheres = new();
        private const float RADIUS_DOMINION = 120f, RADIUS_BOOK = 1000f;

        private void Update()
        {
            if (Keyboard.current.cKey.wasPressedThisFrame)
            {
                foreach (GameObject sphere in _spheres)
                    Destroy(sphere);
                _spheres.Clear();
                return;
            }

            if (!Keyboard.current.bKey.wasPressedThisFrame && !Keyboard.current.vKey.wasPressedThisFrame) return;


            Vector3? target = GetZiplinePoint(RM.mechController);
            if (target == null) return;

            GameObject newSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            _spheres.Add(newSphere);
            newSphere.transform.position = target.Value;
            newSphere.transform.localScale = Keyboard.current.bKey.wasPressedThisFrame ?
                new(RADIUS_BOOK, RADIUS_BOOK, RADIUS_BOOK) : new(RADIUS_DOMINION, RADIUS_DOMINION, RADIUS_DOMINION);
            newSphere.GetComponent<Collider>().enabled = false;

            Material material = newSphere.GetComponent<Renderer>().material;
            material.shader = Shader.Find("Sprites/Default");
            material.color = new(0f, 1f, 0f, 0.1f);
        }

        private Vector3? GetZiplinePoint(MechController controller)
        {
            Vector3 pos = new(0.5f, 0.5f, 0.5f);
            Ray ray = controller.playerCamera.cam.ViewportPointToRay(pos);

            if (Physics.Raycast(ray, out RaycastHit raycastHit, 600f, controller.ziplineLayerMask, QueryTriggerInteraction.Ignore)) return raycastHit.point;
            return null;
        }
    }
}
