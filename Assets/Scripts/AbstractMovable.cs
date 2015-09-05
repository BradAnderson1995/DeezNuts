using Assets.Scripts.Level;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class AbstractMovable : MonoBehaviour
    {
        protected LevelManager levelManager;
        protected Collider2D collider;
        protected int health;
        protected int damage;

        public void Start()
        {
            levelManager = FindObjectOfType<LevelManager>();
            collider = GetComponent<Collider2D>();
        }

        public abstract void Move();

        protected virtual bool TryMove(Vector2 newPosition, Vector2 direction)
        {
            RaycastHit2D raycast = Physics2D.Raycast(transform.position, direction, 16f, levelManager.WallLayer);
            if (raycast.transform == null)
            {
                collider.enabled = false;
                raycast = Physics2D.Raycast(transform.position, direction, 16f, levelManager.EnemyLayer);
                collider.enabled = true;
                if (raycast.transform == null)
                {
                    transform.position = newPosition;

                    return true;
                }
                if (raycast.collider.GetComponent<AbstractMovable>() != null)
                {
                    AttackEnemy(raycast.collider.GetComponent<AbstractMovable>());
                }
                return false;
            }
            return false;
        }

        protected virtual void AttackEnemy(AbstractMovable enemy)
        {
            enemy.TakeDamage(damage);
        }

        public virtual void TakeDamage(int damageReceived)
        {
//            print("At start " + health);
            health -= damageReceived;
//            print("At end " + health);
            if (health <= 0)
            {
                Die();
            }
        }

        protected virtual void Die()
        {
            levelManager.DestroyObject(this);
        }
    }
}