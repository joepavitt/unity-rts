using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UnityRTS {
	public class DragSelectionHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler {
		
		[SerializeField]
		Image selectionBoxImage;

		Vector2 startPosition;
		Rect selectionRect;

		public void OnBeginDrag (PointerEventData eventData)
		{
			if (!Input.GetKey (KeyCode.LeftControl) && !Input.GetKey (KeyCode.RightControl)) {
				RTS_Selectable.DeselectAll (new BaseEventData (EventSystem.current));
			}
			selectionBoxImage.gameObject.SetActive (true);
			startPosition = eventData.position;
			selectionRect = new Rect ();
		}

		public void OnDrag (PointerEventData eventData)
		{
			if (eventData.position.x < startPosition.x) {
				// if dragging left
				selectionRect.xMin = eventData.position.x;
				selectionRect.xMax = startPosition.x;
			} else {
				// if dragging right
				selectionRect.xMin = startPosition.x;
				selectionRect.xMax = eventData.position.x;
			}

			if (eventData.position.y < startPosition.y) {
				// if dragging up
				selectionRect.yMin = eventData.position.y;
				selectionRect.yMax = startPosition.y;
			} else {
				// if dragging down
				selectionRect.yMin = startPosition.y;
				selectionRect.yMax = eventData.position.y;
			}

			// transform the image
			selectionBoxImage.rectTransform.offsetMin = selectionRect.min;
			selectionBoxImage.rectTransform.offsetMax = selectionRect.max;
		}

		public void OnEndDrag (PointerEventData eventData)
		{
			selectionBoxImage.gameObject.SetActive (false);
			foreach (RTS_Selectable selectable in RTS_Selectable.allSelectables) {
				if (selectionRect.Contains (Camera.main.WorldToScreenPoint (selectable.transform.position))) {
					selectable.OnSelect (eventData);
				}
			}
		}

		// bubbles/propagates the pointer click
		public void OnPointerClick (PointerEventData eventData)
		{
			List<RaycastResult> results = new List<RaycastResult> ();
			EventSystem.current.RaycastAll (eventData, results);

			Debug.Log (results.Count);

			if (results.Count < 4) {
				RTS_Selectable.DeselectAll (eventData);
			} else {
				float myDistance = 0;

				foreach (RaycastResult result in results) {
					// get the drag selection box distance
					if (result.gameObject == gameObject) {
						myDistance = result.distance;
						break;
					}
				}

				GameObject nextObject = null;
				float maxDistance = Mathf.Infinity;

				foreach (RaycastResult result in results) {
					// if there are any objects further away than the drag selector - this is the nextObject to pass the event to
					if (result.distance > myDistance && result.distance < maxDistance) {
						nextObject = result.gameObject;
						maxDistance = result.distance;
					}
				}

				if (nextObject) {
					// grab on click event and bubble it through to the object behind the UI
					ExecuteEvents.Execute<IPointerClickHandler> (nextObject, eventData, (x, y) => {x.OnPointerClick((PointerEventData)y); });
				}
			}
		}

		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}
	}
}