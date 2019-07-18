using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCubeCtr : MonoBehaviour {

    public string color;
	// Use this for initialization
	void Start () {
        color = gameObject.GetComponent<Renderer>().material.name.Replace(" (Instance)", "");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
