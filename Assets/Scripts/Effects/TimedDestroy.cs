using UnityEngine;

namespace Assets.Scripts.Effects
{
    public class TimedDestroy : MonoBehaviour
    {
        [SerializeField] private int countDown;

        private void Update()
        {
            countDown -= 1;
            if (countDown <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
