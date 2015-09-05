using Assets.Scripts.Level;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class Enemy1 : AbstractMovable
    {
        private bool up = false;
        private int damage = 5;

        // Update is called once per frame
        void Update () {
	
        }

        public override void Move()
        {
            print("Enemy trying to move");
            if (up)
            {
                TryMove(new Vector2(transform.position.x, transform.position.y - LevelManager.UnitSize), new Vector2(0, -1f));
                up = false;
            }
            else
            {
                TryMove(new Vector2(transform.position.x, transform.position.y + LevelManager.UnitSize), new Vector2(0, 1f));
                up = true;
            }
        }

        protected override void AttackEnemy(AbstractMovable enemy)
        {
            
        }
    }
}
