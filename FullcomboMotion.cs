using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullcomboMotion : MonoBehaviour {

    public float time;

	void Start () {
        this.gameObject.SetActive(false);
        Invoke("anim", this.time);
    }
	
    private void anim()
    {
        this.gameObject.SetActive(true);
    }
}
