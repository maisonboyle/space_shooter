using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleFireArea : MonoBehaviour,  IPointerDownHandler, IPointerUpHandler {


	private bool touched;
	private int pointerID;

	void Awake(){
		touched = false;
	}

	public void OnPointerDown (PointerEventData data){
		if (!touched){
			pointerID = data.pointerId;
			touched = true;
		}
	}

	public void OnPointerUp (PointerEventData data){
		if (data.pointerId == pointerID) {
			touched = false;
		}
	}

	public bool CanFire(){
		return touched;
	}
}