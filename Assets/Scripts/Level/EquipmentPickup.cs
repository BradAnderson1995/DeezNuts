using UnityEngine;
using System.Collections;
using Assets.Scripts.Level;
using Assets.Scripts.Player;

public class EquipmentPickup : MonoBehaviour, IItem
{
    [SerializeField] private string itemName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ReceiveItem(PlayerController player)
    {
        player.Equip(itemName);
        Destroy(gameObject);
    }
}
