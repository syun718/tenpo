using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyFollwer : MonoBehaviour {
    private Animator anim;
    void Start () {
        this.anim = GetComponent<Animator>();
        this.anim.Play("fairy_follower");
    }
	
	void Update () {
		
	}
}
