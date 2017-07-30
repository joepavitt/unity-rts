using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace UnityRTS
{
	public class RTS_Selectable : RTS_Creatable, ISelectHandler, IPointerClickHandler, IDeselectHandler {

		// statis ensure just one copy across this across ALL instances of this class.
		public static HashSet<RTS_Selectable> allSelectables = new HashSet<RTS_Selectable> ();
		public static HashSet<RTS_Selectable> currentlySelected = new HashSet<RTS_Selectable> ();

		public bool isSelected;

		Renderer myRenderer;

		// SerializeField -> Expose to Inspector
		[SerializeField]
		Material unselectedMaterial;
		[SerializeField]
		Material selectedMaterial;

		protected virtual void Awake () {
			Debug.Log ("Selectable AWAKE");
			allSelectables.Add (this);
			myRenderer = GetComponent<Renderer> ();
			Debug.Log (myRenderer);
		}


		public void OnPointerClick(PointerEventData eventData) {
			if (!Input.GetKey (KeyCode.LeftControl) && !Input.GetKey (KeyCode.RightControl)) {
				DeselectAll (eventData);
			}
			OnSelect (eventData);
		}

		public void OnSelect(BaseEventData eventData) {
			currentlySelected.Add (this);
			isSelected = true;
			myRenderer.material = selectedMaterial;
		}

		public void OnDeselect (BaseEventData eventData)
		{
			myRenderer.material = unselectedMaterial;
			isSelected = false;
		}

		public static void DeselectAll(BaseEventData eventData) {
			foreach (RTS_Selectable selectable in currentlySelected) {
				selectable.OnDeselect (eventData);
			}
			currentlySelected.Clear ();
		}
	}
}