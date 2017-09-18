using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fairy : MonoBehaviour {
	//animation
	private Animator anim;
	private float animTimer;
	private float animTime;
    public int state = 0;
    private GameObject gameDirector;
    private bool end = false;
	void Start () {
        this.gameDirector = GameObject.Find("GameDirector");
		this.anim = GetComponent<Animator> ();
        if (this.transform.position.y < 5.0f)
        {
            this.anim.Play("fairy_onstage");
        }
        else
        {
            this.anim.Play("fairy_in");
            this.end = true;
        }
        this.initialization();
	}

	void Update () {
        if (this.state == 0 && !this.end)
        {
            //アニメーションのタイマー
            this.animTimer += Time.deltaTime;
            if (this.animTimer >= this.animTime + 0.15f)
            {
                this.animTimer = 0;
                int randomNum = Random.Range(0, 5);
                switch (randomNum)
                {
                    case 0:
                    case 1:
                        this.anim.Play("fairy_happy");
                        break;
                    case 2:
                    case 3:
                        this.anim.Play("fairy_ukiuki");
                        break;
                    case 4:
                        this.anim.Play("fairy_smile");
                        break;
                }
                this.initialization();
            }
        }
        else if (this.state == 1)
        {
            this.anim.Play("fairy_win");
        }
        else if (this.state == 2)
        {
            this.anim.Play("fairy_fail");
        }
    }

	private void initialization(){
		AnimatorStateInfo currentState = this.anim.GetCurrentAnimatorStateInfo (0);
		this.animTime = currentState.length;
		this.animTimer = 0;
	}

    public void endMotion()
    {
        if (!this.end)
        {
            this.end = true;
            this.animTimer = 0;
            this.anim.Play("fairy_end");
        }
    }

    public void inMotion()
    {
        this.anim.Play("fairy_in");
    }
}
