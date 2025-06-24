using System.Collections.Generic;
using AdventuresOfBlink.Data;
using UnityEngine;

namespace AdventuresOfBlink.Companion
{
    /// <summary>
    /// Controls Duke, the robotic companion that follows the player
    /// and executes special abilities.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class DukeController : MonoBehaviour
    {
        [Header("Follow Settings")]
        [Tooltip("Transform that Duke should follow.")]
        public Transform target;

        [Tooltip("Speed at which Duke moves toward the target.")]
        public float followSpeed = 4f;

        [Tooltip("Minimum distance to maintain from the target.")]
        public float followDistance = 1.5f;

        [Header("Abilities")]
        public List<DukeAbilitySlot> abilities = new();

        private CharacterController controller;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            FollowTarget();
        }

        private void FollowTarget()
        {
            if (target == null)
                return;

            Vector3 offset = target.position - transform.position;
            offset.y = 0f;
            if (offset.sqrMagnitude <= followDistance * followDistance)
                return;

            Vector3 move = offset.normalized * followSpeed * Time.deltaTime;
            controller.Move(move);
            if (move.sqrMagnitude > 0.001f)
                transform.rotation = Quaternion.LookRotation(offset);
        }

        /// <summary>
        /// Executes one of Duke's abilities by index.
        /// </summary>
        public void ExecuteAbility(int index)
        {
            if (index < 0 || index >= abilities.Count)
                return;

            DukeAbilitySlot slot = abilities[index];
            if (slot.data == null)
                return;

            Debug.Log($"Duke executes {slot.data.abilityName} at level {slot.level}");
            // Animation or effect hook would go here.
        }

        /// <summary>
        /// Upgrades the specified ability if possible.
        /// </summary>
        public void UpgradeAbility(int index)
        {
            if (index < 0 || index >= abilities.Count)
                return;

            DukeAbilitySlot slot = abilities[index];
            if (slot.data == null || slot.level >= slot.data.maxLevel)
                return;

            slot.level++;
            Debug.Log($"Upgraded {slot.data.abilityName} to level {slot.level}");
        }

        [System.Serializable]
        public class DukeAbilitySlot
        {
            public DukeAbilityData data;
            [Tooltip("Current upgrade level.")]
            public int level = 1;
        }
    }
}
