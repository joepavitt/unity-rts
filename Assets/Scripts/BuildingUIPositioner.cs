using UnityEngine;
using System.Collections;

namespace UnityRTS {
	public class BuildingUIPositioner : MonoBehaviour {

		private Transform origin;

		// Use this for initialization
		void Start () {
			origin = transform.root;
		}
		
		// Update is called once per frame
		void Update () {
			transform.position = Camera.main.WorldToScreenPoint (origin.position);
		}
	}
}
