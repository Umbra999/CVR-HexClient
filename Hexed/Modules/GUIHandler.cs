using ABI_RC.Core.InteractionSystem;
using UnityEngine;
using Hexed.Wrappers;
using System.Windows.Forms;
using Hexed.Core;
using Hexed.Configs;
using ABI_RC.Core.Savior;
using ABI_RC.Core.Player;
using System.Linq;
using ABI_RC.Core.Networking;
using System;

namespace Hexed.Modules
{
    internal class GUIHandler : MonoBehaviour
    {
        public void OnGUI()
        {
            GUI.Box(new Rect(10, 10, 150, 400), "H E X E D");

            if (GUI.Button(new Rect(30, 40, 110, 20), "Inf Portal"))
            {
                foreach (CVRPortalManager Item in FindObjectsOfType<CVRPortalManager>())
                {
                    Item.portalTime = float.PositiveInfinity;
                }
            }

            else if (GUI.Button(new Rect(30, 65, 110, 20), "Join ID"))
            {
                
            }

            else if (GUI.Button(new Rect(30, 90, 110, 20), "Anti Portal"))
            {
                if (Variables.AntiPortal)
                {
                    Wrappers.Logger.Log("Anti Portal stopped", Wrappers.Logger.LogsType.Info);
                    Variables.AntiPortal = false;
                }
                else
                {
                    Wrappers.Logger.Log("Anti Portal started", Wrappers.Logger.LogsType.Info);
                    Variables.AntiPortal = true;
                }
            }

            else if (GUI.Button(new Rect(30, 115, 110, 20), "Kill Portals"))
            {
                PortalHandler.KillAllPortals();
            }

            else if (GUI.Button(new Rect(30, 140, 110, 20), "EMPTY"))
            {

			}

            else if (GUI.Button(new Rect(30, 165, 110, 20), "Change AviID"))
            {
                GeneralWrappers.SwitchAvatar(Clipboard.GetText());
            }

            else if (GUI.Button(new Rect(30, 190, 110, 20), "Copy WorldID"))
            {
                Clipboard.SetText(MetaPort.Instance.CurrentWorldId);
            }

            else if (GUI.Button(new Rect(30, 215, 110, 20), "Log Received"))
            {
                if (Variables.LogReceiveEvents)
                {
                    Wrappers.Logger.Log("Receive Log stopped", Wrappers.Logger.LogsType.Info);
                    Variables.LogReceiveEvents = false;
                }
                else
                {
                    Wrappers.Logger.Log("Receive Log started", Wrappers.Logger.LogsType.Info);
                    Variables.LogReceiveEvents = true;
                }
            }

            else if (GUI.Button(new Rect(30, 240, 110, 20), "Log Raise"))
            {
                if (Variables.LogRaiseEvents)
                {
                    Wrappers.Logger.Log("RaiseLog stopped", Wrappers.Logger.LogsType.Info);
                    Variables.LogRaiseEvents = false;
                }
                else
                {
                    Wrappers.Logger.Log("Raise Log started", Wrappers.Logger.LogsType.Info);
                    Variables.LogRaiseEvents = true;
                }
            }

            else if (GUI.Button(new Rect(30, 265, 110, 20), "No Serialize"))
            {
                FakeSerialize.ToggleSerialize(!FakeSerialize.NoSerialize);
            }

            else if (GUI.Button(new Rect(30, 290, 110, 20), "Copy InstanceID"))
            {
                Clipboard.SetText(MetaPort.Instance.CurrentInstanceId);
            }

            else if (GUI.Button(new Rect(30, 315, 110, 20), "Jump Boost"))
            {
                if (PlayerWrappers.GetLocalPlayerSetup()._movementSystem.jumpHeight == 1) PlayerWrappers.GetLocalPlayerSetup()._movementSystem.jumpHeight = 5;
                else PlayerWrappers.GetLocalPlayerSetup()._movementSystem.jumpHeight = 1;

            }

            else if (GUI.Button(new Rect(30, 340, 110, 20), "Speed Boost"))
            {
                if (PlayerWrappers.GetLocalPlayerSetup()._movementSystem.sprintMultiplier == 2) PlayerWrappers.GetLocalPlayerSetup()._movementSystem.sprintMultiplier = 5;
                else PlayerWrappers.GetLocalPlayerSetup()._movementSystem.sprintMultiplier = 2;
            }

            else if (GUI.Button(new Rect(30, 375, 110, 20), "Exit"))
            {
                HexedServer.ForceExit();
            }
        }
    }
}
