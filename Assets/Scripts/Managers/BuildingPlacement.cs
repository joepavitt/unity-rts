using UnityEngine;
using System.Collections;

public class BuildingPlacement : MonoBehaviour {

	private PlaceableBuilding placeableBuilding;
	private Renderer placeableBuildingRenderer;
	private Transform currentBuilding;
	private bool isPlaced;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{	
		if (currentBuilding != null && !isPlaced) {
			// Vector3 m = Input.mousePosition; // this is enough for orthographic camera
			// Vector3 p = Camera.main.ScreenToWorldPoint(m);
			// currentBuilding.position = new Vector3 (p.x, 0, p.z);

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100, 9)) {
				currentBuilding.position = new Vector3(hit.point.x, hit.point.y, hit.point.z); 
				// isLegalPosition ();
				if (IsLegalPosition ()) {
					placeableBuildingRenderer.material = placeableBuilding.defaultMaterial;
					if (Input.GetMouseButtonDown (0)) {
						isPlaced = true;
					}
				} else {
					placeableBuildingRenderer.material = placeableBuilding.placementNotAllowedMaterial;
				}
			}
		}
	}

	bool IsLegalPosition() 
	{
		if (placeableBuilding.colliders.Count > 0){
			return false;
		}
		return true;
	}

	public void SetItem (GameObject b) 
	{
		isPlaced = false;
		currentBuilding = ((GameObject)Instantiate (b)).transform;
		placeableBuilding = currentBuilding.GetComponent<PlaceableBuilding> ();
		placeableBuildingRenderer = placeableBuilding.GetComponentInChildren<Renderer> ();
	}
}