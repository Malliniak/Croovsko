using System;
using _Scripts.Helpers;
using UnityEngine;

namespace _Scripts.State
{
    public class PlayerDetector : MonoBehaviour
    {
        public bool playerInRange;
        public Vector3 playerPosition;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == 10)
                playerInRange = true;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.layer == 10)
                playerPosition = other.transform.position;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer == 10)
                playerInRange = false;
        }
    }
}
