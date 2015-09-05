using Assets.Scripts.Level;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class Enemy1 : AbstractMovable
    {
        [SerializeField] private int enemyHealth = 3;
        [SerializeField] private int damageDealt = 1;
        private bool up = false;
        private bool wait = true;

        void Start()
        {
            base.Start();
            health = enemyHealth;
            damage = damageDealt;
        }

        // Update is called once per frame
        void Update () {
	
        }

        public override void Move()
        {
            if (wait)
            {
                wait = false;
                return;
            }
            if (up)
            {
                TryMove(new Vector2(transform.position.x, transform.position.y - LevelManager.UnitSize), new Vector2(0, -1f));
                up = false;
                wait = true;
            }
            else
            {
                TryMove(new Vector2(transform.position.x, transform.position.y + LevelManager.UnitSize), new Vector2(0, 1f));
                up = true;
                wait = true;
            }
        }
    }
}
