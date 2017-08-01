using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace UnityRTS
{
	public class RTS_Selectable : RTS_Creatable, ISelectHandler, IPointerClickHandler, IDeselectHandler, IHasOptionsUI {

		// statis ensure just one copy across this across ALL instances of this class.
		public static HashSet<RTS_Selectable> allSelectables = new HashSet<RTS_Selectable> ();
		public static HashSet<RTS_Selectable> currentlySelected = new HashSet<RTS_Selectable> ();

		public bool isSelected;

		Renderer myRenderer;

		private GameObject optionsUI;

		// SerializeField -> Expose to Inspector
		[SerializeField]
		Material unselectedMaterial;
		[SerializeField]
		Material selectedMaterial;

		protected virtual void Awake () {
			allSelectables.Add (this);
			myRenderer = GetComponentInChildren<Renderer> ();
			optionsUI = GameObject.Find ("Options UI");
			optionsUI.SetActive (false);
			Debug.Log (optionsUI);
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
			ToggleUI (true);
		}

		public void OnDeselect (BaseEventData eventData)
		{
			myRenderer.material = unselectedMaterial;
			isSelected = false;
			ToggleUI (false);
		}

		public void ToggleUI (bool show) {
			optionsUI.SetActive (show);
		}

		public static void DeselectAll(BaseEventData eventData) {
			foreach (RTS_Selectable selectable in currentlySelected) {
				selectable.OnDeselect (eventData);
			}
			currentlySelected.Clear ();
		}

	}
}