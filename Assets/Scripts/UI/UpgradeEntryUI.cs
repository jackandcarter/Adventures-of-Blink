using System.Text;
using UnityEngine;
using UnityEngine.UI;
using AdventuresOfBlink.Data;
using AdventuresOfBlink.Systems;

namespace AdventuresOfBlink.UI
{
    /// <summary>
    /// Displays a single upgrade option with purchase button.
    /// </summary>
    public class UpgradeEntryUI : MonoBehaviour
    {
        public Image icon;
        public Text nameText;
        public Text costText;
        public Button upgradeButton;

        private UpgradeData data;
        private UpgradeSystem system;

        /// <summary>
        /// Initializes the UI for the specified upgrade.
        /// </summary>
        public void Setup(UpgradeData upgrade, UpgradeSystem upgradeSystem)
        {
            data = upgrade;
            system = upgradeSystem;

            if (icon != null)
                icon.sprite = upgrade.icon;
            if (nameText != null)
                nameText.text = upgrade.upgradeName;
            if (costText != null)
                costText.text = BuildCostText();

            if (upgradeButton != null)
            {
                upgradeButton.onClick.RemoveAllListeners();
                upgradeButton.onClick.AddListener(TryUpgrade);
            }
        }

        private string BuildCostText()
        {
            if (data.requirements == null)
                return string.Empty;

            var sb = new StringBuilder();
            for (int i = 0; i < data.requirements.Length; i++)
            {
                var req = data.requirements[i];
                if (req.item != null)
                {
                    sb.Append(req.item.itemName);
                    sb.Append(" x");
                    sb.Append(req.amount);
                    if (i < data.requirements.Length - 1)
                        sb.Append(", ");
                }
            }
            return sb.ToString();
        }

        private void TryUpgrade()
        {
            if (system != null && data != null)
            {
                bool success = system.ApplyUpgrade(data);
                if (success && upgradeButton != null)
                    upgradeButton.interactable = false;
            }
        }
    }
}
