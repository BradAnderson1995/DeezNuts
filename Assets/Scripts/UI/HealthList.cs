using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HealthList : MonoBehaviour {
    public List<HealthContainer> containers;
    // Use this for initialization
	void Start () {
        containers = GetComponentsInChildren<HealthContainer>().OrderByDescending(item => item.heartIndex).ToList();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
