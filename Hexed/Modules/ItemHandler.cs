using ABI.CCK.Components;
using ABI_RC.Core.Player;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hexed.Wrappers;
using UnityEngine;

namespace Hexed.Modules
{
    internal class ItemHandler
    {
        public static bool PickupSpamToggle = false;
        public static IEnumerator PickupSpam(CVRPlayerEntity Target)
        {
            if (PickupSpamToggle) yield break;
            PickupSpamToggle = true;
            List<CVRPickupObject> AllPickups = Object.FindObjectsOfType<CVRPickupObject>().ToList();
            while (PickupSpamToggle && Target != null)
            {
                foreach (CVRPickupObject obj in AllPickups)
                {
                    obj.transform.position = Target.GetPlayerObject().transform.position;
                }
                yield return new WaitForEndOfFrame();
            }
            PickupSpamToggle = false;
        }

        public static void TeleportItemsToPosition(Vector3 Position)
        {
            List<CVRPickupObject> AllPickups = Object.FindObjectsOfType<CVRPickupObject>().ToList();
            foreach (var Item in AllPickups)
            {
                Item.transform.position = Position;
            }
        }
    }
}
