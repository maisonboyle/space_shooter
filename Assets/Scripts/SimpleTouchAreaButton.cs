using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleTouchAreaButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler  {

	private bool touched;
	private int pointerID;
	private bool canFire;

	void Awake(){
		touched = false;
		canFire = false;
	}


	public void OnPointerDown (PointerEventData data){
		// set start point
		if (!touched) {
			touched = true;
			canFire = true;
			pointerID = data.pointerId;
		}
	}

	public void OnPointerUp (PointerEventData data){
		// reset everything
		if (data.pointerId == pointerID) {
			touched = false;
			canFire = false;
		}
	}

	public bool CanFire(){
		return canFire;
	}

}
