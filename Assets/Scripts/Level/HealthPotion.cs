using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Level
{
    public class HealthPotion : MonoBehaviour, IItem {

        // Use this for initialization
        void Start () {
	
        }
	
        // Update is called once per frame
        void Update () {
	
        }

        public void ReceiveItem(PlayerController player)
        {
            player.GetPotion();
            Destroy(gameObject);
        }
    }
}
