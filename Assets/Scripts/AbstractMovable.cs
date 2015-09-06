using Assets.Scripts.Level;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class AbstractMovable : MonoBehaviour
    {
        [SerializeField] private GameObject hitEffect;
        protected Animator animator;
        protected LevelManager levelManager;
        protected Collider2D collider;
        protected int health;
        protected int damage;
        protected bool facingRight = true;

        public void Start()
        {
            animator = GetComponent<Animator>();
            levelManager = FindObjectOfType<LevelManager>();
            collider = GetComponent<Collider2D>();
        }

        public void FixedUpdate()
        {
            animator.ResetTrigger("Up");
            animator.ResetTrigger("Down");
            animator.ResetTrigger("Side");
        }

        public abstract void Move();

        protected virtual void Animate(Vector2 direction)
        {
            // Flip the player
            if ((direction.x < 0 && facingRight) || (direction.x > 0 && !facingRight))
            {
                Flip();
            }
            if (Mathf.Abs(direction.x) > 0)
            {
                animator.SetTrigger("Side");              
            }
            else if (direction.y > 0)
            {
                animator.SetTrigger("Up");
            }
            else if (direction.y < 0)
            {
                animator.SetTrigger("Down");
            }
        }

        protected virtual bool TryMove(Vector2 newPosition, Vector2 direction)
        {
            Animate(direction);
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
            Instantiate(hitEffect, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            health -= damageReceived;
            print("At end " + health);
            if (health <= 0)
            {
                Die();
            }
        }

        protected virtual void Die()
        {
            levelManager.DestroyObject(this);
        }

        protected void Flip()
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            facingRight = !facingRight;
        }
    }
}