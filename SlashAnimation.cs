using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAnimation : MonoBehaviour {
    private Animator anim;
    public bool state;
    SpriteRenderer sprender;

    void Start () {
        this.anim = GetComponent<Animator>();
        this.sprender = this.GetComponent<SpriteRenderer>();
    }
	
	void Update () {
        if (this.state)
        {
            this.sprender.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            this.state = false;
            int num = Random.Range(0, 8);
            switch (num)
            {
                case 0:
                    this.anim.Play("slash");
                    break;
                case 1:
                    this.anim.Play("slash2");
                    break;
                case 2:
                    this.anim.Play("slash3");
                    break;
                case 3:
                    this.anim.Play("slash4");
                    break;
                case 4:
                    this.anim.Play("slash5");
                    break;
                case 5:
                    this.anim.Play("slash6");
                    break;
                case 6:
                    this.anim.Play("slash7");
                    break;
                case 7:
                    this.anim.Play("slash8");
                    break;
            }
        }
	}
}
