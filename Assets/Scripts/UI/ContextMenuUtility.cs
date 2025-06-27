using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

namespace AdventuresOfBlink.UI
{
    /// <summary>
    /// Utility for spawning a very simple one-button context menu
    /// at the specified screen position.
    /// </summary>
    public static class ContextMenuUtility
    {
        public static void Show(string optionLabel, UnityAction callback, Vector2 position)
        {
            Canvas canvas = Object.FindObjectOfType<Canvas>();
            if (canvas == null)
                return;

            GameObject panel = new GameObject("ContextMenu", typeof(RectTransform), typeof(Image));
            panel.transform.SetParent(canvas.transform, false);
            RectTransform rect = panel.GetComponent<RectTransform>();
            rect.pivot = new Vector2(0, 1);
            rect.sizeDelta = new Vector2(120, 30);
            rect.position = position;

            GameObject buttonObj = new GameObject("Option", typeof(RectTransform), typeof(Button), typeof(Image));
            buttonObj.transform.SetParent(panel.transform, false);
            RectTransform br = buttonObj.GetComponent<RectTransform>();
            br.anchorMin = Vector2.zero;
            br.anchorMax = Vector2.one;
            br.offsetMin = Vector2.zero;
            br.offsetMax = Vector2.zero;

            GameObject textObj = new GameObject("Text", typeof(RectTransform), typeof(TMP_Text));
            textObj.transform.SetParent(buttonObj.transform, false);
            RectTransform tr = textObj.GetComponent<RectTransform>();
            tr.anchorMin = Vector2.zero;
            tr.anchorMax = Vector2.one;
            tr.offsetMin = Vector2.zero;
            tr.offsetMax = Vector2.zero;
            TMP_Text text = textObj.GetComponent<TMP_Text>();
            text.text = optionLabel;
            text.alignment = TextAlignmentOptions.Center;
            text.raycastTarget = false;

            Button btn = buttonObj.GetComponent<Button>();
            btn.onClick.AddListener(callback);
            btn.onClick.AddListener(() => Object.Destroy(panel));
        }
    }
}
