using UnityEngine;
using System.Collections;

public class RTSCamera : MonoBehaviour {

	public float ScrollEdge = 0.1f;

	public float PanSpeed = 10;

	public float zoomRangeMin = 5.0f;
	public float zoomRangeMax = 30.0f;

	public float ZoomSpeed = 5;

	// public float rotateSpeed = 10;

	private Vector3 initialPosition;

	void Start () {
		initialPosition = transform.position;
	}


	void Update () {

		if ( Input.GetKey("d") ) { // || Input.mousePosition.x >= Screen.width * (1 - ScrollEdge)) {             
			transform.Translate(Vector3.right * Time.deltaTime * PanSpeed, Space.Self );   
		}
		else if ( Input.GetKey("a") ) { // || Input.mousePosition.x <= Screen.width * ScrollEdge ) {            
			transform.Translate(Vector3.right * Time.deltaTime * -PanSpeed, Space.Self );              
		}

		if ( Input.GetKey("w") ) { // || Input.mousePosition.y >= Screen.height * (1 - ScrollEdge) ) {            
			transform.Translate(Vector3.forward * Time.deltaTime * PanSpeed, Space.Self );             
		}
		else if ( Input.GetKey("s") ) { // || Input.mousePosition.y <= Screen.height * ScrollEdge ) {         
			transform.Translate(Vector3.forward * Time.deltaTime * -PanSpeed, Space.Self );            
		}  

		/*if ( Input.GetKey("q")) {
			transform.Rotate(Vector3.up * Time.deltaTime * -rotateSpeed, Space.World);
		}
		else if ( Input.GetKey("e")) {
			transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed, Space.World);
		}*/

		// zoom in/out

		float CurrentSize = Camera.main.orthographicSize - (Input.GetAxis ("Mouse ScrollWheel") * ZoomSpeed);
		CurrentSize = Mathf.Clamp (CurrentSize, zoomRangeMin, zoomRangeMax);
		Camera.main.orthographicSize = Mathf.Clamp(CurrentSize, zoomRangeMin, zoomRangeMax);

		transform.position = new Vector3( transform.position.x, transform.position.y - (transform.position.y - (initialPosition.y)) * 0.1f, transform.position.z );
	}
}