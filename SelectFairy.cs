using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectFairy : MonoBehaviour {
    //ゲームオブジェクトの格納
    GameObject gameDirector;
    //アニメーション
    private Animator anim;
    private float animTimer;
    private float animTime;

    void Start () {
        //ゲームオブジェクトGameDirectorを探す
        this.gameDirector = GameObject.Find("GameDirector");
        this.anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        //アニメーションのタイマー
        this.animTimer += Time.deltaTime;
        if (this.animTimer >= this.animTime - 0.15f)
        {
            this.animTimer = 0;
            this.anim.Play("fairy_breath");
        }
    }

    public void noMotion()
    {
        this.anim.Play("fairy_no");
    }
}
