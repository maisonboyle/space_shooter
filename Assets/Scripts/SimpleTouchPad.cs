using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleTouchPad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {

	public float smoothing;

	private Vector2 smoothDirection;
	private Vector2 origin;
	private Vector2 direction;
	private bool touched;
	private int pointerID;

	void Awake(){
		direction = Vector2.zero;
		touched = false;
	}


	public void OnPointerDown (PointerEventData data){
		// set start point
		if (!touched) {
			touched = true;
			pointerID = data.pointerId;
			origin = data.position;
		}
	}

	public void OnDrag (PointerEventData data){
		// Compare difference between start and current position
		if (data.pointerId == pointerID) {
			Vector2 currentPosition = data.position;
			Vector2 directionRaw = currentPosition - origin;
			direction = directionRaw.normalized;
		}
	}

	public void OnPointerUp (PointerEventData data){
		// reset everything
		if (data.pointerId == pointerID) {
			touched = false;
			direction = Vector2.zero;
		}
	}

	public Vector2 GetDirection(){
		smoothDirection = Vector2.MoveTowards (smoothDirection, direction, smoothing);
		return smoothDirection;
	}

}
