using Assets.Scripts.Level;
using UnityEngine;
using Assets.Scripts.Player;

namespace Assets.Scripts
{
    public class EnemyFollow : AbstractMovable
    {
        [SerializeField]   private int enemyHealth = 3;
        [SerializeField]   private int damageDealt = 1;

        private Transform playerPos;
        private int moveX;
        private int moveY;

        void Start()
        {
            base.Start();
            health = enemyHealth;
            damage = damageDealt;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void Move()
        {
            moveX = 0;
            moveY = 0;

            playerPos = FindObjectOfType<PlayerController>().transform;
            if (Mathf.Sqrt(Mathf.Pow((playerPos.position.x -transform.position.x),2) + Mathf.Pow((playerPos.position.y - transform.position.y),2)) < 80)
            {
                if (Mathf.Abs(playerPos.position.x - transform.position.x) > Mathf.Abs(playerPos.position.y - transform.position.y))
                {
                    moveX = playerPos.position.x > transform.position.x ? 1 : -1;
                }
                else
                    moveY = playerPos.position.y > transform.position.y ? 1 : -1;
                TryMove(new Vector2(transform.position.x + moveX*(LevelManager.UnitSize), transform.position.y + moveY*(LevelManager.UnitSize)), new Vector2(moveX,moveY));
                print(moveX);
                print(moveY);
            }
        }
    }
}
