using Assets.Scripts.Level;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class AbstractMovable : MonoBehaviour
    {
        protected LevelManager levelManager;
        protected Collider2D collider;

        public void Start()
        {
            levelManager = FindObjectOfType<LevelManager>();
            collider = GetComponent<Collider2D>();
        }

        public abstract void Move();

        protected virtual bool TryMove(Vector2 newPosition, Vector2 direction)
        {
            // TODO: Add enemy collision code
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
                AttackEnemy(raycast.collider.GetComponent<AbstractMovable>());
                return false;
            }
            return false;
        }

        protected abstract void AttackEnemy(AbstractMovable enemy);
    }
}