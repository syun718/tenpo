using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heroine : MonoBehaviour {
    //ゲームオブジェクトの格納
    GameObject gameDirector;
    GameObject generator;
    GameObject player;
    private GameObject soundPlayer;
    Player playerAbility;
    GameDirector g;
    [SerializeField]
    private GameObject powerIcon;
    [SerializeField]
    private GameObject rangeIcon;
    [SerializeField]
    private GameObject scoreIcon;
    [SerializeField]
    private GameObject cureIcon;
    //animation
    private Animator anim;
	private float animTimer;
	private float animTime;
	private bool animstate;
    //判定強化数
    public int rangeUpPoint;
    //判定強化時間
    private float rangeUpTime = 0;
    //判定強化スイッチ
    private bool rangeUpSwitch = false;
    //判定強化魔法発動確率
    private int rangeUprate = 68;
    //攻撃力アップ魔法発動確率
    private int powerUprate = 60;
    //攻撃魔法を1度だけ発動
    private bool powerUpOnce = true;
    //スコアアップ上昇魔法発動確率
    private int scorePointUprate = 70;
    //スコアアップ上昇魔法を1度だけ発動
    private bool scorePointUpOnce = true;
    //回復魔法発動確率
    private int cureRate = 64;
    //回復魔法を1度だけ発動
    private bool cureOnce = true;
    //経過時間1
    private float time1 = 0;
    //経過時間2
    private float time2 = 0;
    //経過時間3
    private float time3 = 0;
    //発動された魔法の番号
    public int magicType;  //1.治癒 2.攻撃力上昇 3.判定強化 4.スコア上昇率アップ
    //ゲームが終わっているかどうか
    public int state = 0;

    void Start () {
        //ゲームオブジェクトGameDirectorを探す
        this.gameDirector = GameObject.Find("GameDirector");
        //ゲームオブジェクトのPlayerを探す
        this.player = GameObject.Find("Player");
        //ゲームオブジェクトGeneratorを探す
        this.generator = GameObject.Find("Generator");
        //ゲームオブジェクトSoundPlayerを探す
        this.soundPlayer = GameObject.Find("SoundPlayer");
        //Playerに参照
        this.playerAbility = this.player.GetComponent<Player>();
        //GameDirectorに参照
        this.g = this.gameDirector.GetComponent<GameDirector>();
		//Animationをコンポネント
		this.anim = GetComponent<Animator>();
        this.cureIcon.SetActive(false);
        this.powerIcon.SetActive(false);
        this.rangeIcon.SetActive(false);
        this.scoreIcon.SetActive(false);
    }
    void Update()
    {
        if (this.state == 0)
        {
            //魔法の種類を初期化
            this.magicType = 0;
            //コンボ数が22コンボ達成ごとに一定の確率で攻撃力アップ
            if (this.g.combo % 20 == 0 && this.g.combo != 0)
            {
                if (this.powerUpOnce)
                {
                    //発動確立を超えたら魔法発動
                    int randomNum = Random.Range(0, 101);
                    if (randomNum > this.powerUprate)
                    {
                        if (this.g.gameState == 0)
                        {
                            this.magicMotion();
                            this.powerUp();
                            this.powerIcon.SetActive(true);
                        }
                        else
                        {
                            this.time1 = 0;
                            this.time2 = 0;
                            this.time3 = 0;
                            this.magicMotion();
                            this.scorePointUp();
                            this.scoreIcon.SetActive(true);
                        }
                    }
                }
                this.powerUpOnce = false;
            }
            if (!this.powerUpOnce)
            {
                this.time1 += Time.deltaTime;
                if (this.time1 > 5.0f)
                {
                    this.powerUpOnce = true;
                    this.time1 = 0;
                    if (this.g.gameState == 0)
                    {
                        this.powerIcon.SetActive(false);
                    }else
                    {
                        this.scoreIcon.SetActive(false);
                    }
                }
            }
            //GameDirectorのPerfectCountが15を達成するごとに一定の確率でスコア上昇値アップ
            if (this.g.perfectCount % 15 == 0 && this.g.perfectCount != 0)
            {
                if (this.scorePointUpOnce)
                {
                    //発動確立を超えたら魔法発動
                    int randomNum = Random.Range(0, 101);
                    if (randomNum > this.scorePointUprate)
                    {
                        this.magicMotion();
                        this.scorePointUp();
                        this.scoreIcon.SetActive(true);
                        if (this.g.gameState != 0)
                        {
                            this.time1 = 0;
                            this.time2 = 0;
                            this.time3 = 0;
                        }
                    }
                }
                this.scorePointUpOnce = false;
            }
            if (!this.scorePointUpOnce)
            {
                this.time2 += Time.deltaTime;
                if (this.time2 > 5.0f)
                {
                    this.scorePointUpOnce = true;
                    this.time2 = 0;
                    this.scoreIcon.SetActive(false);
                }
            }
            //ノーツ数が18個ごとに一定の確率で回復
            if (this.g.noteNum % 18 == 0 && this.g.noteNum != 0 && this.playerAbility.playerHp < this.playerAbility.maxHp)
            {
                if (this.cureOnce)
                {
                    //発動確立を超えたら魔法発動
                    int randomNum = Random.Range(0, 101);
                    if (randomNum > this.cureRate)
                    {
                        if (this.g.gameState == 0)
                        {
                            this.magicMotion();
                            this.cure();
                            this.cureIcon.SetActive(true);
                        }
                        else
                        {
                            this.time1 = 0;
                            this.time2 = 0;
                            this.time3 = 0;
                            this.magicMotion();
                            this.scorePointUp();
                            this.scoreIcon.SetActive(true);
                        }
                    }
                }
                this.cureOnce = false;
            }
            if (!this.cureOnce)
            {
                this.time3 += Time.deltaTime;
                if (this.time3 > 5.0f)
                {
                    this.cureOnce = true;
                    this.time3 = 0;
                    if (this.g.gameState != 0)
                    {
                        this.scoreIcon.SetActive(false);
                    }
                }
            }
            if(this.time3 > 2.0f)
            {
                this.cureIcon.SetActive(false);
            }
            //GameDirectorのフレームカウント780(13秒)ごとに一定の確率で判定強化
            if (this.g.frame_count % 780 == 0 && this.g.frame_count != 0)
            {
                //発動確立を超えたら魔法発動
                int randomNum = Random.Range(0, 101);
                if (randomNum > this.rangeUprate)
                {
                    this.magicMotion();
                    this.rangeUp();
                    this.rangeIcon.SetActive(true);
                }
            }
            //判定強化スイッチがtrueの時
            if (this.rangeUpSwitch)
            {
                //判定強化
                this.rangeUpPoint = 3;
                //判定強化時間の更新
                this.rangeUpTime += Time.deltaTime;
            }
            //判定強化スイッチがfalseの時
            else
            {
                //判定強化をなし
                this.rangeUpPoint = 0;
            }
            //判定強化時間が6秒を超えたら
            if (this.rangeUpTime > 6)
            {
                //判定強化時間初期化
                this.rangeUpTime = 0;
                //判定強化スイッチをfalseに
                this.rangeUpSwitch = false;
                this.rangeIcon.SetActive(false);
            }

            //アニメーションのタイマー
            this.animTimer += Time.deltaTime;
            if (this.animTimer >= this.animTime - 0.2f)
            {
                this.animstate = false;
                this.animTimer = 0;
                int randomNum = Random.Range(0, 8);
                this.initialization();
                switch (randomNum)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        this.anim.Play("heroine_breath");
                        break;
                    case 6:
                        this.animstate = true;
                        this.anim.Play("heroine_look");
                        break;
                    case 7:
                        this.animstate = true;
                        this.anim.Play("heroine_smile");
                        break;
                }
            }
        }
        else if (this.state == 1)
        {
            this.anim.Play("heroine_wink");
        }
        else
        {
            this.anim.Play("heroine_no");
        }
    }

    private void cure()
    {
        //プレイヤーの体力回復(回復量3)
        for(int i = 0;i < 3;i++)
        {
            this.playerAbility.playerHp++;
            this.g.playerHpGage.GetComponent<Image>().fillAmount += 1.0f / this.playerAbility.maxHp;
            //途中で体力がマックスになったらそこまで
            if (this.playerAbility.playerHp == this.playerAbility.maxHp)
            {
                break;
            }
        }
        //発動魔法の種類決定
        this.magicType = 1;
        //パーティクルの発動
        this.generator.GetComponent<Generator>().spawnCureParticle();
        this.soundPlayer.GetComponent<SoundPlayer>().cureSound();
    }

    private void powerUp()
    {
        //プレイヤーのパワーアップスイッチをtrueに
        this.playerAbility.powerUpSwitch = true;
        this.magicType = 2;
        this.generator.GetComponent<Generator>().spawnPowerUpParticle();
        this.soundPlayer.GetComponent<SoundPlayer>().powerUpSound();
    }

    private void rangeUp()
    {
        //判定強化スイッチをtrueに
        this.rangeUpSwitch = true;
        this.magicType = 3;
        this.generator.GetComponent<Generator>().spawnRangeUpParticle();
        this.soundPlayer.GetComponent<SoundPlayer>().rangeUpSound();
    }

    private void scorePointUp()
    {
        //スコアアップ上昇スイッチをtrueに
        this.playerAbility.scoreUpSwitch = true;
        this.magicType = 4;
        this.generator.GetComponent<Generator>().spawnScorePointUpParticle();
        this.soundPlayer.GetComponent<SoundPlayer>().scorePointUpSound();
    }
    /*
    private void scoreUp()
    {
        this.g.score += (int)(this.g.score * 0.05f);
    }
    */

	public void noMotion(){
		if (!this.animstate) {
            this.initialization();
            this.anim.Play ("heroine_no");
		}
        this.animstate = true;
	}
		
	public void happyMotion(){
		if (!this.animstate) {
            this.initialization();
            this.anim.Play ("heroine_happy");
		}
        this.animstate = true;
	}

	public void winkMotion(){
		this.anim.Play ("heroine_wink");
	}

    private void magicMotion()
    {
        this.animTimer = 0;
        this.animTime = 1.3f;
        this.anim.Play("heroine_magic");
        this.animstate = true;
    }

    private void initialization(){
		AnimatorStateInfo currentState = this.anim.GetCurrentAnimatorStateInfo (0);
		this.animTime = currentState.length;
		this.animTimer = 0;
	}
}
