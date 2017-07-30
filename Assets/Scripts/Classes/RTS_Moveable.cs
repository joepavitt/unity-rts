using UnityEngine;
using System.Collections;

namespace UnityRTS
{
	public class RTS_Moveable : RTS_Selectable
	{
		private NavMeshAgent navMeshAgent;
		private RTS_Selectable mySelectable;

		// Use this for initialization
		protected override void Awake () {
			base.Awake (); // call -parent awake function
			Debug.Log ("Moveable AWAKE");
			navMeshAgent = GetComponent<NavMeshAgent> ();
			mySelectable = GetComponent<RTS_Selectable> ();
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
}


