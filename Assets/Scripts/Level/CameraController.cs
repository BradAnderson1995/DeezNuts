using UnityEngine;
using System.Collections;
using Assets.Scripts.Player;

public class CameraController : MonoBehaviour
{
    private Transform player;

	// Use this for initialization
	void Start ()
	{
	    player = FindObjectOfType<PlayerController>().transform;
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }
}
