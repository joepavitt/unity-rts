using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaceableBuilding : MonoBehaviour {

	public Material placementNotAllowedMaterial;
	public GameObject buildingPrefab;

	[HideInInspector]
	public Material defaultMaterial;
	[HideInInspector]
	public List<Collider> colliders = new List<Collider> ();

	// Use this for initialization
	void Start () {
		defaultMaterial = GetComponentInChildren<Renderer> ().material;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider c) {
		if (c.tag == "Building" || c.tag == "Unit") {
			colliders.Add (c);
		}
	}

	void OnTriggerExit(Collider c) {
		if (c.tag == "Building" || c.tag == "Unit") {
			colliders.Remove (c);
		}
	}
}
