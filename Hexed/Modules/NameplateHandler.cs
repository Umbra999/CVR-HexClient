using ABI_RC.Core.Networking.IO.Social;
using ABI_RC.Core.Player;
using Hexed.Wrappers;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hexed.Modules
{
    internal class NameplateHandler : MonoBehaviour
    {
        public Image BackgroundImageComp;
        public Image BackgroundMask;
        public TextMeshProUGUI Nametag;
        public Color BackgroundColor;
        public Color FontColor;


        public void Start()
        {       
            var Player = CVRPlayerManager.Instance.GetPlayer(transform.parent.gameObject.name);
            if (Player == null) return;

            //Color Pink = new Color(255, 0, 90, 170);

            bool IsFriend = Friends.List.Where(x => x.UserId == Player.GetUserID()).FirstOrDefault() != null;

            BackgroundColor = Player.GetRank().GetRankColor();
            FontColor = IsFriend ? Color.yellow : BackgroundColor;

            GameObject SpecialTag = transform.Find("Canvas/Content/Image/Image").gameObject;
            SpecialTag.SetActive(false);

            GameObject NametagObject = transform.Find("Canvas/Content/TMP:Username").gameObject;
            Nametag = NametagObject.GetComponent<TextMeshProUGUI>();
            Nametag.color = new Color(FontColor.r, FontColor.g, FontColor.b, 0.6f);

            GameObject PlateBackground = transform.Find("Canvas/Content/Image").gameObject;
            DestroyImmediate(PlateBackground.GetComponent<Image>());

            BackgroundImageComp = PlateBackground.AddComponent<Image>();
            BackgroundImageComp.type = Image.Type.Sliced;
            BackgroundImageComp.pixelsPerUnitMultiplier = 500;
            BackgroundImageComp.color = new Color(BackgroundColor.r, BackgroundColor.g, BackgroundColor.b, 0.32f);
            BackgroundImageComp.LoadImageFromResource("PlateBG", 200, new Vector4(255, 0, 255, 0));

            GameObject UserMask = transform.Find("Canvas/Content/Image/ObjectMaskSlave/UserImageMask").gameObject;
            DestroyImmediate(UserMask.GetComponent<Image>());

            Image UserMaskImage = UserMask.AddComponent<Image>();
            UserMaskImage.LoadImageFromResource("PlateIconBG");
            UserMask.transform.localScale = new Vector3(1.25f, 1.1f, 1);
            DestroyImmediate(UserMask.GetComponent<Mask>());
            UserMask.AddComponent<Mask>();
            GameObject BackgroundObject = transform.Find("Canvas/Content/Image/ObjectMaskSlave/UserImageMask (1)").gameObject;
            DestroyImmediate(BackgroundObject.GetComponent<Image>());

            BackgroundMask = BackgroundObject.AddComponent<Image>();
            BackgroundMask.color = new Color(BackgroundColor.r, BackgroundColor.g, BackgroundColor.b, 0.32f);
            BackgroundMask.LoadImageFromResource("PlateIconBG");
            BackgroundMask.transform.SetSiblingIndex(0);
            BackgroundMask.transform.localScale = new Vector3(1.45f, 1.25f, 1);
            UserMask.transform.Find("UserImage").transform.localScale = new Vector3(1.05f, 1.05f, 1);

            GameObject FriendIconObject = PlateBackground.transform.Find("FriendsIndicator").gameObject;
            Image FriendIcon = FriendIconObject.GetComponent<Image>();
            FriendIcon.enabled = false;
            FriendIconObject.SetActive(false);
        }
    }
}
