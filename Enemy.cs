using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    SpriteRenderer sprender;
    //アニメーション
    private Animator anim;
    private float animTimer;
    private float animTime;
    //敵の体力
    public float enemyHp;
	//マックス体力
	public float enemyMaxHp;
    //透明度
    private float alpha = 1.0f;
    //逃走状態
    public bool escape;
    void Start () {
		this.enemyMaxHp = this.enemyHp;
        this.sprender = this.GetComponent<SpriteRenderer>();
        this.anim = GetComponent<Animator>();
    }

    void Update()
    {
        this.sprender.color = new Color(1.0f, 1.0f, 1.0f, this.alpha);
        if (this.escape)
        {
            this.alpha *= 0.98f;
        }
        if (this.enemyHp > 0)
        {
            //アニメーションのタイマー
            this.animTimer += Time.deltaTime;
            if (this.animTimer >= this.animTime - 0.2f)
            {
                this.animTimer = 0;
                this.initialization();
                int randomNum = Random.Range(0, 8);
                if (this.enemyMaxHp == 30)
                {
                    switch (randomNum)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                            this.anim.Play("enemy1_breath");
                            break;
                        case 4:
                        case 5:
                            this.anim.Play("enemy1_wink");
                            break;
                        case 6:
                        case 7:
                            this.anim.Play("enemy1_attack");
                            break;
                    }
                }
                else if (this.enemyMaxHp == 50)
                {
                    switch (randomNum)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                            this.anim.Play("enemy2_breath");
                            break;
                        case 4:
                            this.anim.Play("enemy2_breath2");
                            break;
                        case 5:
                            this.anim.Play("enemy2_cane");
                            break;
                        case 6:
                            this.anim.Play("enemy2_cane2");
                            break;
                        case 7:
                            this.anim.Play("enemy2_scaling");
                            break;
                    }
                }
                else
                {
                    switch (randomNum)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            this.anim.Play("enemy3_breath");
                            break;
                        case 6:
                        case 7:
                            this.anim.Play("enemy3_kick");
                            break;
                    }
                }
            }
        }
        else
        {
            if (this.enemyMaxHp == 30)
            {
                this.anim.Play("enemy1_died");
            }
            else if (this.enemyMaxHp == 50)
            {
                this.anim.Play("enemy2_died");
            }else
            {
                this.anim.Play("enemy3_died");
            }
        }
    }

    public void damageMotion()
    {
        
        if (this.enemyHp > 0)
        {
            if (this.enemyMaxHp == 30)
            {
                this.anim.Play("enemy1_damage");
            }
            else if (this.enemyMaxHp == 50)
            {
                this.anim.Play("enemy2_damage");
            }
            else
            {
                this.anim.Play("enemy3_damage");
            }
            this.initialization();
        }
    }

    private void initialization()
    {
        AnimatorStateInfo currentState = this.anim.GetCurrentAnimatorStateInfo(0);
        this.animTime = currentState.length;
        this.animTimer = 0;
    }

}

    
