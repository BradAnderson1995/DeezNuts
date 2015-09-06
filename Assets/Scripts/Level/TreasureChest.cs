using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Level
{
    public class TreasureChest : MonoBehaviour, IItem
    {
        [SerializeField] private GameObject contains;
        private Animator animator;
        private bool open = false;

        // Use this for initialization
        void Start ()
        {
            animator = GetComponent<Animator>();
        }
	
        // Update is called once per frame
        void Update () {
	
        }

        public void ReceiveItem(PlayerController player)
        {
            if (open)
            {
                return;
            }
            open = true;
            animator.SetBool("Open", true);          
            Instantiate(contains, transform.position, Quaternion.identity);
        }
    }
}
