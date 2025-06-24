using UnityEngine;
using UnityEngine.AI;

namespace AdventuresOfBlink.World
{
    /// <summary>
    /// Boundary that optionally pushes the player back or stops movement when triggered.
    /// </summary>
    public class BoundaryTrigger : TriggerVolume
    {
        [Tooltip("If true the player will be pushed back on enter.")]
        public bool pushPlayer = true;

        [Tooltip("Distance to push the player when triggered.")]
        public float pushDistance = 1f;

        [Tooltip("If true the player's NavMeshAgent is stopped on enter.")]
        public bool blockMovement;

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            if (!other.CompareTag("Player"))
                return;

            if (blockMovement)
            {
                NavMeshAgent agent = other.GetComponent<NavMeshAgent>();
                if (agent != null)
                    agent.ResetPath();
            }

            if (pushPlayer)
            {
                Vector3 dir = other.transform.position - transform.position;
                dir.y = 0f;
                other.transform.position += dir.normalized * pushDistance;
            }
        }
    }
}
