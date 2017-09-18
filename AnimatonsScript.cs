using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimatonsScript : MonoBehaviour {
	//アニメの格納庫
	private Animator anim;
    public int caunt;

	// Use this for initialization
	void Start () {
		//Animatorの取得
		this.anim = GetComponent<Animator>();
        this.anim.enabled = false;
        caunt = 0;
	}

	public void anima()
	{
        this.anim.enabled = true;
	}

	public void animGameOver()
	{
        this.anim.enabled = true;
        this.caunt = 1;
    }

	// Update is called once per frame
	void Update () {
		if (this.caunt == 1) 
		{
            this.anim.SetBool ("Bool", true);
		}
        else
        {
            this.anim.SetBool("Bool", false);
        }
	}
}
