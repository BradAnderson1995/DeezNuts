using Assets.Scripts.Level;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class EnemyRandom : AbstractMovable
    {
        [SerializeField]   private int enemyHealth = 3;
        [SerializeField]   private int damageDealt = 1;

        public int moves = 2;

        private System.Random rnd = new System.Random();
        private int direction;
        private int lastDir;
        private bool trying;

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
            direction = rnd.Next(0, 4);
            while (moves > 0)
            {
                // 0: North 1: West 2: South    3: East
                trying = true;
                lastDir = 5;

                while (trying)
                {
                    if (direction == lastDir)
                    {
                        while (direction == lastDir)
                        {
                            direction = rnd.Next(0,4);
                        }
                    }
                    else if (direction == 0)
                    {
                        if (TryMove(new Vector2(transform.position.x, transform.position.y + LevelManager.UnitSize), new Vector2(0, 1f)))
                            trying = false;
                        direction = rnd.Next(1, 4);
                    }
                    else if (direction == 1)
                    {
                        if (TryMove(new Vector2(transform.position.x + LevelManager.UnitSize, transform.position.y), new Vector2(1f, 0)))
                            trying = false;
                        direction = rnd.Next(0, 4);
                        while (direction == 1)
                        {
                            direction = rnd.Next(0, 4);
                        }
                    }
                    else if (direction == 2)
                    {
                        if (TryMove(new Vector2(transform.position.x, transform.position.y - LevelManager.UnitSize), new Vector2(0, -1f)))
                            trying = false;
                        direction = rnd.Next(0, 4);
                        while (direction == 2)
                        {
                            direction = rnd.Next(0, 4);
                        }
                    }
                    else if (direction == 3)
                    {
                        if (TryMove(new Vector2(transform.position.x - LevelManager.UnitSize, transform.position.y), new Vector2(-1f, 0)))
                            trying = false;
                        direction = rnd.Next(0, 3);
                    }
                }
                lastDir = direction;
                moves--;
            }
        }
    }
}
