using ABI_RC.Core.InteractionSystem;
using ABI_RC.Core.Networking;
using ABI_RC.Core.Util;
using DarkRift;
using Hexed.Wrappers;
using System.Collections;
using UnityEngine;

namespace Hexed.Modules
{
    internal class PortalHandler
    {
        public static bool ColliderKiller = false;
        public static IEnumerator ColliderRemover()
        {
            if (ColliderKiller) yield break;

            ColliderKiller = true;
            while (ColliderKiller)
            {
                SpawnPortal("HEXED BY UMBRA", Vector3.positiveInfinity);
                yield return new WaitForSeconds(2);
                SpawnPortal("HEXED BY UMBRA", Vector3.negativeInfinity);
                yield return new WaitForSeconds(2);
            }
            ColliderKiller = false;
        }

        public static void KillAllPortals()
        {
            foreach (CVRPortalManager Item in Object.FindObjectsOfType<CVRPortalManager>())
            {
                Item.Despawn();
            }
        }

        public static void PortalToRaycast(string InstanceID)
        {
            if (Physics.Raycast(Camera.current.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                CVRSyncHelper.SpawnPortal(InstanceID, hit.point.x, hit.point.y, hit.point.z);
            }
        }

        public static void SpawnPortal(string InstanceID, GameObject ObjectToSpawnAt)
        {
            SpawnPortal(InstanceID, ObjectToSpawnAt.transform.position);
        }

        public static void SpawnPortal(string InstanceID, Vector3 Position)
        {
            using (DarkRiftWriter darkRiftWriter = DarkRiftWriter.Create())
            {
                darkRiftWriter.Write(InstanceID);
                darkRiftWriter.Write(Position.x);
                darkRiftWriter.Write(Position.y);
                darkRiftWriter.Write(Position.z);
                using (Message message = Message.Create(10000, darkRiftWriter))
                {
                    NetworkManager.Instance.GameNetwork.SendMessage(message, SendMode.Reliable);
                }
            }
        }
    }
}
