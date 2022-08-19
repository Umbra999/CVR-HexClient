using ABI_RC.Core;
using Hexed.Wrappers;
using UnityEngine;

namespace Hexed.Modules
{
    internal class KeybindHandler : MonoBehaviour
    {
        public void Update()
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha1))
            {
                GeneralWrappers.SwitchAvatar("17c267db-18c4-4900-bb73-ad323f082640");
            }

            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.X))
            {
                RootLogic.Instance.Respawn();
            }

            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (Physics.Raycast(Camera.current.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
                {
                    PlayerWrappers.GetLocalPlayer().transform.position = hit.point;
                }
            }
        }
    }
}
