using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleTouchPad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {

	public float smoothing;
	private Vector2 smoothdirection;
	private Vector2 origin;
	private Vector2 direction;
	private bool touched;
	private int pointerID;

	void Awake(){
		direction = Vector2.zero;
		touched = false;
	}

	public void OnPointerDown (PointerEventData data){
		if (!touched){
			origin = data.position;
			pointerID = data.pointerId;
			touched = true;
		}
	}

	public void OnDrag (PointerEventData data){
		if (data.pointerId == pointerID) {
			Vector2 currentPosition = data.position;
			Vector2 directionRaw = currentPosition - origin;
			direction = directionRaw.normalized;
		}
	}

	public void OnPointerUp (PointerEventData data){
		if (data.pointerId == pointerID) {
			direction = Vector2.zero;
			touched = false;
		}
	}

	public Vector2 GetDirection(){
		smoothdirection = Vector2.MoveTowards (smoothdirection, direction, smoothing);
		return smoothdirection;
	}
}
