using UnityEngine;

namespace Assets.Scripts
{
    public abstract class AbstractMovable : MonoBehaviour
    {
        public abstract void Move();

        protected virtual bool TryMove(Vector2 newPosition, Vector2 direction, LayerMask wallLayer, LayerMask enemyLayer)
        {
            // TODO: Add enemy collision code
            RaycastHit2D raycast = Physics2D.Raycast(transform.position, direction, 16f, wallLayer);
            if (raycast.transform == null)
            {
                raycast = Physics2D.Raycast(transform.position, direction, 16f, enemyLayer);
                if (raycast.transform == null)
                {
                    transform.position = newPosition;
                    return true;
                }
                AttackEnemy(newPosition);
                return false;
            }
            return false;
        }

        protected abstract void AttackEnemy(Vector2 attackPosition);
    }
}