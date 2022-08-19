using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Hexed.Wrappers;
using ABI_RC.Core.Player;

namespace Hexed.Modules
{
    internal class Playerlist : MonoBehaviour
    {
        private static float Delay = 0;

        private TMPro.TextMeshProUGUI PlayerText;

        public void Start()
        {
            GameObject TextObject = Instantiate(Resources.FindObjectsOfTypeAll<TMPro.TextMeshProUGUI>().FirstOrDefault(x => x.name == "DisplayName").gameObject, GameObject.Find("/Cohtml/QuickMenu").transform);
            TextObject.name = "Playerlist";
            PlayerText = TextObject.GetComponent<TMPro.TextMeshProUGUI>();
            PlayerText.enableWordWrapping = false;
            PlayerText.fontSize = 3.3f;
            PlayerText.color = Color.white;
            PlayerText.richText = true;
            PlayerText.alignment = TMPro.TextAlignmentOptions.TopLeft;
            PlayerText.text = "";
            PlayerText.outlineWidth = 0f;
            PlayerText.outlineColor = new Color(0, 0, 0, 0);
        }
          

        public void Update()
        {
            Delay += Time.deltaTime;
            if (Delay < 2) return;
            Delay = 0;

            foreach (var Player in CVRPlayerManager.Instance.GetAllNetworkedPlayers())
            {
                PlayerText.text += Player.GetUsername() + "\n";
            }
        }
    }
}
