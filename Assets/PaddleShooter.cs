using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PaddleMover))]
public class PaddleShooter : MonoBehaviour {

	private PaddleMover paddleMover;
	// Use this for initialization
	void Start () {
		paddleMover = GetComponent<PaddleMover>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
