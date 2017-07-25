using UnityEngine;
using System.Collections;

public class SelectionManager : MonoBehaviour {

	// store the currently selected object
	public GameObject selected;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update ()
	{
		if ( Input.GetMouseButtonDown(0) )
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, 100))
			{
				Debug.Log(hit.collider.gameObject.name);
				if (hit.collider.CompareTag ("Selectable")) {
					selected = hit.collider.gameObject;
				} else {
					selected = null;
				}
			}
		}
	}
}
