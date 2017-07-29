using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class MySelectable : MonoBehaviour, ISelectHandler, IPointerClickHandler, IDeselectHandler {

	// statis ensure just one copy across this across ALL instances of this class.
	public static HashSet<MySelectable> allSelectables = new HashSet<MySelectable> ();
	public static HashSet<MySelectable> currentlySelected = new HashSet<MySelectable> ();

	public bool isSelected;

	Renderer myRenderer;

	// SerializeField -> Expose to Inspector
	[SerializeField]
	Material unselectedMaterial;
	[SerializeField]
	Material selectedMaterial;

	void Awake () {
		allSelectables.Add (this);
		myRenderer = GetComponent<Renderer> ();
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
		foreach (MySelectable selectable in currentlySelected) {
			selectable.OnDeselect (eventData);
		}
		currentlySelected.Clear ();
	}
}
