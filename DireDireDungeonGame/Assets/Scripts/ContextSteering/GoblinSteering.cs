using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Friedforfun.ContextSteering.PlanarMovement;
using Friedforfun.ContextSteering.Utilities;

namespace Friedforfun.ContextSteering.Demo
{
    public class GoblinSteering : MonoBehaviour
    {
        [SerializeField] private PlanarSteeringController steer;
        [SerializeField] private CharacterController control;
        [SerializeField] public GameObject LookTarget;

        [Tooltip("Movement speed of the agent.")]
        [Range(0.1f, 20f)]
        [SerializeField] private float Speed = 1f;

        [Tooltip("Minimum sqrMagnitute of direction vector to allow movement, higher values can reduce jittery movement but may also stop the agent moving when you might want it to.")]
        [Range(0.001f, 0.5f)]
        [SerializeField] private float ConfidenceThreshold = 0.1f;

        private enum State
        {
            Idle,
            Chase
        }

        public float triggerDistance = 5;

        private State goblinState;
        private Rigidbody2D rb;
        private GameObject player;

        private void Start()
        {
            player = GameObject.Find("Player");
            rb = gameObject.GetComponent<Rigidbody2D>();
            goblinState = State.Idle;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
