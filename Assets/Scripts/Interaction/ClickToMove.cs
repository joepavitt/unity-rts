using UnityEngine;
using System.Collections;

public class ClickToMove : MonoBehaviour {

	private NavMeshAgent navMeshAgent;
	private MySelectable mySelectable;

	// Use this for initialization
	void Awake () {
		navMeshAgent = GetComponent<NavMeshAgent> ();
		mySelectable = GetComponent<MySelectable> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		if (Input.GetButtonDown ("Fire2") && mySelectable.isSelected) 
		{
			if (Physics.Raycast (ray, out hit, 100))
			{
				navMeshAgent.destination = hit.point;
				navMeshAgent.Resume();
			}
		}
	}
}
