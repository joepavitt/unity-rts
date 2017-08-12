using UnityEngine;
using System.Collections;

public class BuildingPlacement : MonoBehaviour {

	private PlaceableBuilding placeableBuilding;
	private Renderer placeableBuildingRenderer;
	private GameObject currentBuilding;
	private bool isPlaced;

	// Slope Detection
	private float maxDifference = 1f;
	private float tallestHeight;
	private float shortestHeight;
	private float greatestDifference;

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
				currentBuilding.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z); 
				// isLegalPosition ();
				if (IsLegalPosition ()) {
					placeableBuildingRenderer.material = placeableBuilding.defaultMaterial;
					if (Input.GetMouseButtonDown (0)) {
						isPlaced = true;
						Vector3 pos = currentBuilding.transform.position;
						Quaternion rot = currentBuilding.transform.rotation;
						Instantiate (placeableBuilding.buildingPrefab, pos, rot);
						Destroy (currentBuilding);
					}
				} else {
					placeableBuildingRenderer.material = placeableBuilding.placementNotAllowedMaterial;
				}
			}
		}
	}

	bool IsLegalPosition() 
	{
		// if colliding with another object
		if (placeableBuilding.colliders.Count > 0){
			return false;
		}

		// if trying to build on a slope
		if (currentBuilding.transform.FindChild("Graphic").FindChild("HeightPoints")) {
			tallestHeight = 0f;
			shortestHeight = 100f;

			GameObject HeightPointsObj = currentBuilding.transform.FindChild("Graphic").FindChild("HeightPoints").gameObject as GameObject;
			int heightPoints = HeightPointsObj.transform.childCount;

			float[] height = new float[heightPoints];

			for (int i = 0; i < heightPoints; i++) {
				GameObject point = HeightPointsObj.transform.GetChild (i).gameObject as GameObject;

				RaycastHit hit;
				if (Physics.Raycast(point.transform.position, Vector3.down, out hit, Mathf.Infinity, 9)) {
					height [i] = hit.point.y;
					if (height[i] > tallestHeight) {
						tallestHeight = height [i];
					}
					if (height[i] < shortestHeight) {
						shortestHeight = height [i];
					}
				}
			}

			// Debug.Log ((tallestHeight - shortestHeight).ToString() + " === " + maxDifference.ToString());

			if (tallestHeight - shortestHeight > maxDifference) {
				return false;
			}
		}

		// else
		return true;
	}

	public void SetItem (GameObject b) 
	{
		isPlaced = false;
		currentBuilding = ((GameObject)Instantiate (b));
		placeableBuilding = currentBuilding.GetComponent<PlaceableBuilding> ();
		placeableBuildingRenderer = placeableBuilding.GetComponentInChildren<Renderer> ();
	}
}