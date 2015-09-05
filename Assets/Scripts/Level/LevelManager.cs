using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Player;

namespace Assets.Scripts.Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private int WaitFrames = 30;
        [SerializeField] public LayerMask WallLayer;
        [SerializeField] public LayerMask EnemyLayer;
        private int frameCounter = 0;
        private bool count = true;
        private PlayerController playerController;
        private List<AbstractMovable> enemyList; 

        public const float UnitSize = 16f;

        // Use this for initialization
        private void Start()
        {
            playerController = FindObjectOfType<PlayerController>();
            enemyList = FindObjectsOfType<AbstractMovable>().ToList();
            print(enemyList.Count);
        }

        // Update is called once per frame
        private void Update()
        {

        }

        private void FixedUpdate()
        {
            if (count)
            {
                if (frameCounter < WaitFrames)
                {
                    frameCounter += 1;
                }
                else
                {
                    frameCounter = 0;
                    count = false;
                    // Trigger player canAct
                    playerController.EnableAction();
                }
            }
        }

        public void EnableCount()
        {
            foreach (AbstractMovable movable in enemyList)
            {
                movable.Move();
            }
            count = true;
        }
    }
}