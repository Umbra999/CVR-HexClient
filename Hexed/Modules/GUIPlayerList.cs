using ABI_RC.Core.Player;
using ABI_RC.Core.Savior;
using Hexed.Wrappers;
using UnityEngine;

namespace Hexed.Modules
{
    internal class GUIPlayerList : MonoBehaviour
    {
        private static CVRPlayerEntity SelectedPlayer;
        public void OnGUI()
        {
            GUI.Box(new Rect(Screen.width - 170, 10, 150, 400), "PLAYERS");

            int index = 40;

            foreach (var player in CVRPlayerManager.Instance.GetAllNetworkedPlayers())
            {
                if (GUI.Button(new Rect(Screen.width - 155, index, 120, 20), player.GetUsername()))
                {
                    SelectedPlayer = player;
                }
                index += 25;
            }

            if (SelectedPlayer != null)
            {
                GUI.Box(new Rect(Screen.width - 380, Screen.height - 320, 350, 300), $"SELECTED: {SelectedPlayer.GetUsername()}");

                if (GUI.Button(new Rect(Screen.width - 370, Screen.height - 290, 120, 20), "Portaldrop"))
                {
                    PortalHandler.SpawnPortal("HEXED BY UMBRA", SelectedPlayer.GetPlayerObject());
                }

                if (GUI.Button(new Rect(Screen.width - 370, Screen.height - 265, 120, 20), "Copy UserID"))
                {
                    System.Windows.Forms.Clipboard.SetText(SelectedPlayer.GetUserID());
                }

                if (GUI.Button(new Rect(Screen.width - 370, Screen.height - 240, 120, 20), "Copy AvatarID"))
                {
                    System.Windows.Forms.Clipboard.SetText(SelectedPlayer.GetAvatarID());
                }

                if (GUI.Button(new Rect(Screen.width - 370, Screen.height - 215, 120, 20), "Forcekick"))
                {
                    GuardianExtendedControls.Instance.KickUser(SelectedPlayer.GetUserID());
                }

                if (GUI.Button(new Rect(Screen.width - 370, Screen.height - 190, 120, 20), "Forceclone"))
                {
                    GeneralWrappers.SwitchAvatar(SelectedPlayer.GetAvatarID());
                }

                if (GUI.Button(new Rect(Screen.width - 370, Screen.height - 165, 120, 20), "Teleport"))
                {
                    PlayerWrappers.GetLocalPlayer().transform.position = SelectedPlayer.GetPlayerObject().transform.position;
                }

                if (GUI.Button(new Rect(Screen.width - 370, Screen.height - 140, 120, 20), "Deselect"))
                {
                    SelectedPlayer = null;
                }
            }
            else
            {
                GUI.Box(new Rect(Screen.width - 380, Screen.height - 320, 350, 300), $"SELECTED: LOCALPLAYER");

                if (GUI.Button(new Rect(Screen.width - 370, Screen.height - 290, 120, 20), "Copy Token"))
                {
                    System.Windows.Forms.Clipboard.SetText(MetaPort.Instance.accessKey);
                }

                if (GUI.Button(new Rect(Screen.width - 370, Screen.height - 265, 120, 20), "Copy UserID"))
                {
                    System.Windows.Forms.Clipboard.SetText(MetaPort.Instance.ownerId);
                }

                if (GUI.Button(new Rect(Screen.width - 370, Screen.height - 240, 120, 20), "Copy AvatarID"))
                {
                    System.Windows.Forms.Clipboard.SetText(MetaPort.Instance.currentAvatarGuid);
                }
            }
        }
    }
}
