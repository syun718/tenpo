using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    //アニメーション
    private Animator anim;
    private float animTimer;
    private float animTime;
    private bool animstate;
    public int state = 0;
    //魔力量
    public int magicPoint;
    //スコアアップ上昇値
    public float scoreUpPoint;
    //プレイヤー体力
    public float playerHp;
    //マックスHP量
    public float maxHp;
    //攻撃力
    public int attackPower;
    //攻撃力アップ時間
    private float powerUpTime = 0;
    //スコアアップ上昇値増加時間
    private float scoreUpTime = 0;
    //パワーアップスイッチ
    public bool powerUpSwitch = false;
    //スコアアップ上昇スイッチ
    public bool scoreUpSwitch = false;
    void Start () {
        //魔力量の初期化
        this.magicPoint = 0;
        //攻撃力の初期化
        this.attackPower = 1;
        //マックスHP量の設定
        this.maxHp = this.playerHp;
        this.anim = GetComponent<Animator>();
    }
	
	void Update () {
        if (this.state == 0)
        {
            //パワーアップスイッチがtrueの時
            if (this.powerUpSwitch)
            {
                //攻撃力アップ
                this.attackPower = 3;
                //攻撃力アップ時間の更新
                this.powerUpTime += Time.deltaTime;
            }
            //パワーアップスイッチがfalseの時
            else
            {
                //攻撃力を元に
                this.attackPower = 1;
            }
            //攻撃力アップ時間が5秒を超えたら
            if (this.powerUpTime > 5)
            {
                //攻撃力アップ時間初期化
                this.powerUpTime = 0;
                //パワーアップスイッチをfalseに
                this.powerUpSwitch = false;
            }
            //スコアアップ上昇スイッチがtrueの時
            if (this.scoreUpSwitch)
            {
                //スコアアップ上昇
                this.scoreUpPoint = 1.5f;
                //スコア力アップ上昇時間の更新
                this.scoreUpTime += Time.deltaTime;
            }
            //スコアアップ上昇スイッチがfalseの時
            else
            {
                //スコアアップ上昇を元に
                this.scoreUpPoint = 1.0f;
            }
            //スコアアップ上昇増加時間が6秒を超えたら
            if (this.scoreUpTime > 6)
            {
                //スコアアップ上昇時間初期化
                this.scoreUpTime = 0;
                //スコアアップ上昇スイッチをfalseに
                this.scoreUpSwitch = false;
            }
            //アニメーションのタイマー
            this.animTimer += Time.deltaTime;
            if (this.animTimer >= this.animTime - 0.15f)
            {
                this.animstate = false;
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
                        this.anim.Play("player_breath");
                        break;
                    case 7:
                        this.anim.Play("player_look");
                        break;
                    case 8:
                        this.anim.Play("player_no");
                        break;
                }
            }
        }
        else if (this.state == 1)
        {
            this.anim.Play("player_smile");
        }
        else if (this.state == 2)
        {
            this.anim.Play("player_tire");
            this.state = 3;
        }
        else
        {
            this.anim.Play("player_tired");
        }
    }

    public void attackMotion()
    {
        this.initialization();
        this.anim.Play("player_attack");
    }

    public void damageMotion()
    {
        if (!this.animstate)
        {
            this.anim.Play("player_damage");
            this.initialization();
        }
        this.animstate = true;
    }

    private void initialization()
    {
        AnimatorStateInfo currentState = this.anim.GetCurrentAnimatorStateInfo(0);
        this.animTime = currentState.length;
        this.animTimer = 0;
    }
}
