using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectHeroine : MonoBehaviour {
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
	
	void Update () {
        //アニメーションのタイマー
        this.animTimer += Time.deltaTime;
        if (this.animTimer >= this.animTime - 0.15f)
        {
            this.animTimer = 0;
            this.initialization();
            int randomNum = Random.Range(0, 9);
            switch (randomNum)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                    this.anim.Play("heroine_breath");
                    break;
                case 7:
                    this.anim.Play("heroine_look");
                    break;
                case 8:
                    this.anim.Play("heroine_smile");
                    break;
            }
        }
    }

    public void noMotion()
    {
        this.initialization();
        this.anim.Play("heroine_no");
    }

    private void initialization()
    {
        AnimatorStateInfo currentState = this.anim.GetCurrentAnimatorStateInfo(0);
        this.animTime = currentState.length;
        this.animTimer = 0;
    }
}
