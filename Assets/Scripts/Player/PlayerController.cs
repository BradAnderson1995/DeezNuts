using UnityEngine;
using System.Collections;
using Assets.Scripts.Level;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof (InputController))]
    public class PlayerController : AbstractMovable
    {
        private bool canAct = false;

        // Use this for initialization
        public void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        public void Update()
        {

        }

        public void EnableAction()
        {
            canAct = true;
        }

        public override void Move()
        {
            
        }

        public void Move(int x, int y)
        {
            // Only execute if move ready and control has been input
            if (!(Mathf.Abs(x) > 0 || Mathf.Abs(y) > 0) || !canAct)
            {
                return;
            }

            // Execute movement
            if (Mathf.Abs(x) > 0)
            {
                TryMove(new Vector2(transform.position.x + LevelManager.UnitSize*x, transform.position.y), new Vector2(x, 0));
                // Start the LevelManager counting down to the next move
                levelManager.EnableCount();
                canAct = false;
            }
            else if (Mathf.Abs(y) > 0)
            {
                TryMove(new Vector2(transform.position.x, transform.position.y + LevelManager.UnitSize*y), new Vector2(0, y));
                // Start the LevelManager counting down to the next move
                levelManager.EnableCount();
                canAct = false;
            }
        }

        protected override void AttackEnemy(AbstractMovable enemy)
        {
            print("Attacking enemy");
        }
    }
}