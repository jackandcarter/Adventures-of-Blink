using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using AdventuresOfBlink.Combat;
using AdventuresOfBlink.Data;

namespace AdventuresOfBlink.Editor
{
    /// <summary>
    /// Basic editor window for inspecting abilities and combos.
    /// This is an early tool that can be extended for timing and fine-tuning.
    /// </summary>
    public class BattleEditorWindow : EditorWindow
    {
        private AbilityData selectedAbility;
        private ComboSequence selectedCombo;

        [MenuItem("Blink Tools/Battle Editor")]
        public static void ShowWindow()
        {
            GetWindow<BattleEditorWindow>("Battle Editor");
        }

        private void OnGUI()
        {
            GUILayout.Label("Ability", EditorStyles.boldLabel);
            selectedAbility = (AbilityData)EditorGUILayout.ObjectField("Selected Ability", selectedAbility, typeof(AbilityData), false);
            if (selectedAbility != null)
            {
                DrawAbility(selectedAbility);
            }

            GUILayout.Space(10);
            GUILayout.Label("Combo", EditorStyles.boldLabel);
            selectedCombo = (ComboSequence)EditorGUILayout.ObjectField("Selected Combo", selectedCombo, typeof(ComboSequence), false);
            if (selectedCombo != null)
            {
                DrawCombo(selectedCombo);
            }
        }

        private void DrawAbility(AbilityData ability)
        {
            EditorGUILayout.LabelField("Name", ability.abilityName);
            EditorGUILayout.FloatField("Base Damage", ability.baseDamage);
            EditorGUILayout.FloatField("Cooldown", ability.cooldown);
            EditorGUILayout.ObjectField("Animation", ability.animationClip, typeof(AnimationClip), false);
        }

        private void DrawCombo(ComboSequence combo)
        {
            EditorGUILayout.FloatField("Input Window", combo.inputWindow);
            for (int i = 0; i < combo.steps.Count; i++)
            {
                combo.steps[i] = (AbilityData)EditorGUILayout.ObjectField($"Step {i + 1}", combo.steps[i], typeof(AbilityData), false);
            }
        }
    }
}
