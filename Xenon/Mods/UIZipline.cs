using System.Reflection;
using UnityEngine;

namespace Xenon.Mods
{
    internal class UIZipline
    {
        private static readonly FieldInfo deckInfo = typeof(MechController).GetField("deck", BindingFlags.NonPublic | BindingFlags.Instance);

        public static void PostUpdate(MechController __instance)
        {
            PlayerCardDeck playerCardDeck = (PlayerCardDeck)deckInfo.GetValue(__instance);
            PlayerCard playerCard = playerCardDeck?.GetCardInHand(0);

            if (playerCardDeck == null || playerCard == null || playerCard.data.discardAbility != PlayerCardData.DiscardAbility.Telefrag) return;

            MechController.ZiplinePoint ziplinePoint = GetZiplinePoint(__instance);
            RM.ui.SetZiplineUI(true, ziplinePoint.hasValidPoint, 0f, Mathf.InverseLerp(0f, 500f, ziplinePoint.distance), ziplinePoint.distance, ziplinePoint.point);
            UIAbilityIndicator_Zipline indicator = RM.ui._ziplineIndicator;

            indicator.rangeFinder.color = ziplinePoint.hasValidPoint ? Color.green : Color.red;
            if (ziplinePoint.distance <= 500)
                indicator.rangeFinder.text = ziplinePoint.distance > 0f ? ziplinePoint.distance.ToString("F1") : "-INF";
            else
                indicator.rangeFinder.text = (500 - ziplinePoint.distance).ToString("F1");
        }

        private static MechController.ZiplinePoint GetZiplinePoint(MechController controller)
        {
            MechController.ZiplinePoint ziplinePoint = default;
            Vector3 pos = new(0.5f, 0.5f, 0.5f);
            Ray ray = controller.playerCamera.cam.ViewportPointToRay(pos);

            if (Physics.Raycast(ray, out RaycastHit raycastHit, 800f, controller.ziplineLayerMask, QueryTriggerInteraction.Ignore))
            {
                ziplinePoint.damageable = raycastHit.collider.gameObject.GetComponent<BaseDamageable>();
                ziplinePoint.point = raycastHit.point;
                ziplinePoint.hasValidPoint = raycastHit.distance <= 500f;
                ziplinePoint.distance = raycastHit.distance;
            }
            else
            {
                ziplinePoint.hasValidPoint = false;
                ziplinePoint.point = Vector3.zero;
                ziplinePoint.distance = -1f;
            }
            return ziplinePoint;
        }
    }
}
