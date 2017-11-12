using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BackgroundScrollController : MonoBehaviour {
	public float speed = 1.0f;
	
	void Update () {
		Vector2 offset = new Vector2(Time.time * speed, 0);
		float newH = GetComponent<RawImage> ().uvRect.height;
		float newW = GetComponent<RawImage> ().uvRect.width;
		float newY = GetComponent<RawImage> ().uvRect.y;
		GetComponent<RawImage> ().uvRect = new Rect(offset.x, newY, newW, newH);

	}﻿
}
