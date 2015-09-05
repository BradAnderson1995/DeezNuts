using UnityEngine;
using System.Collections;
using Assets.Scripts.Player;

namespace Assets.Scripts.Player
{
    public class InputController : MonoBehaviour
    {
        private int x = 0;
        private int y = 0;
        private PlayerController playerController;

        // Use this for initialization
        public void Start()
        {
            playerController = GetComponent<PlayerController>();
        }

        // Update is called once per frame
        public void Update()
        {
            // Get state of movement controls
            if (Input.GetAxis("Horizontal") > 0)
            {
                x = 1;
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                x = -1;
            }
            else
            {
                x = 0;
            }
            if (Input.GetAxis("Vertical") > 0)
            {
                y = 1;
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                y = -1;
            }
            else
            {
                y = 0;
            }
        }

        public void FixedUpdate()
        {
            playerController.Move(x, y);
        }
    }
}