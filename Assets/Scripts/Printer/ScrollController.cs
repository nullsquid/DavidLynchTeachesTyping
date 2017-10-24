using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScrollController : MonoBehaviour {
    public Scrollbar scrollBar;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        //scrollRect.
        scrollBar.value = 0;
	}
}
