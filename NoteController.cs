using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour {
    //流れていく向き
    Vector3 flowDirection;
    //流れていく速度
    float flowSpeed;
    //ゲームオブジェクトの格納
    private GameObject gameDirector;
    private GameObject generator;
    private GameDirector g;
    private GameObject player;
    private GameObject heroine;
    //フレームカウント
    private int frame_count = 0;
    //ノーツの出る位置の番号(0が真ん中、1が左、2が右になる)
    private int note_way;
    //ノーツの種類判定
    public int note_type = 0;
    //子分ノーツの不規則移動値
    private float randomX = 0;
    private float randomY = -1.0f;
    //bad範囲
    private int badRange1 = 138;
    private int badRange2 = 178;
    //great範囲
    private int greatRange1 = 144;
    private int greatRange2 = 172;
    //perfect範囲
    private int perfectRange1 = 152;
    private int perfectRange2 = 164;
    void Start () {
        //ゲームオブジェクトGameDirectorを探す
        this.gameDirector = GameObject.Find("GameDirector");
        //ゲームオブジェクトGeneratorを探す
        this.generator = GameObject.Find("Generator");
        //ゲームオブジェクトのPlayerを探す
        this.player = GameObject.Find("Player");
        //ゲームオブジェクトのHeroineを探す
        this.heroine = GameObject.Find("Heroine");
        //変数pにGameDirectorスクリプトをコンポーネント
        this.g = this.gameDirector.GetComponent<GameDirector>();
        //flowDirectionのx座標pxを初期化
        float px = 0;
        //位置番号の決定
        this.note_way = 0;
        //pxの値を最初に生成された場所から決定
        if (transform.position.x >= 0) {
			if (transform.position.x >= 0.2f) {
				px = 0.4f;
				//ノーツの種類判別
				note_type = g.note_box [g.note_count + (this.g.max_num / 3 * 2)];
                //notePrefabを子ノードに
                //this.generator.GetComponent<Generator>().noteParticleRight.transform.parent = this.transform;
                this.note_way = 2;
			}
            else 
			{
				px = 0;
				//ノーツの種類判別
				note_type = g.note_box [g.note_count];
                //notePrefabを子ノードに
                //this.generator.GetComponent<Generator>().noteParticleCenter.transform.parent = this.transform;
                this.note_way = 0;
            }
		}else 
		{
			px = -0.4f;
			//ノーツの種類判別
			note_type = g.note_box [g.note_count + (this.g.max_num / 3)];
            //notePrefabを子ノードに
            this.note_way = 1;
            //this.generator.GetComponent<Generator>().noteParticleLeft.transform.parent = this.transform;
        }
        //流れる向きの決定
        this.flowDirection = new Vector3(px, -1.0f, 0);
        if(this.g.framerate == 5)
        {
            //bad範囲
            this.badRange1 = 80;
            this.badRange2 = 130;
            //great範囲
            this.greatRange1 = 92;
            this.greatRange2 = 120;
            //perfect範囲
            this.perfectRange1 = 100;
            this.perfectRange2 = 112;
            //流れる速度の決定
            this.flowSpeed = 2.5f;
        }
        else
        {
            //bad範囲
            this.badRange1 = 138;
            this.badRange2 = 178;
            //great範囲
            this.greatRange1 = 144;
            this.greatRange2 = 172;
            //perfect範囲
            this.perfectRange1 = 152;
            this.perfectRange2 = 164;
            //流れる速度の決定
            this.flowSpeed = 1.6f;
        }
	}

	void Update () {
        //フレームカウントの加算
        this.frame_count += 1;
        if (this.note_type != 5)
        {
            //フレームごとに大きくなる 
            //this.transform.localScale += new Vector3(0.005f, 0.005f, 0);
            //決められた向きに流れる
            this.transform.position += this.flowDirection * this.flowSpeed * Time.deltaTime;
        }else if(this.note_type == 5)
        {
            Vector3 randomDirection = new Vector3(this.randomX, this.randomY, 0);
            if (this.frame_count % 30 == 0)
            {
                this.randomX = Random.Range(-1.3f, 1.3f);
                this.randomY = Random.Range(-0.5f, -1.5f);
            }
            this.transform.position += randomDirection * this.flowSpeed * Time.deltaTime;
        }
        //ラインより下に行ったら破壊
        if (this.transform.position.y < -3.3f)
        {
            //miss判定
            this.g.miss();
            //ノートの破壊
            Destroy(gameObject);
        }else if (this.transform.position.y > 10.0f || this.transform.position.x < -6.0f || this.transform.position.x > 6.0f)
        {
            this.g.followerNote_Slash();
            //ノートの破壊
            Destroy(gameObject);
        }
        if(this.g.gameState == 4)
        {
            Destroy(this.gameObject);
        }
    }

    //タップ判定
    public void note_tap()
    {
        Heroine heroineAbility = this.heroine.GetComponent<Heroine>();
        if (this.transform.position.y < -1.0f)
        {
            //bad判定以上
            if (this.frame_count > (this.badRange1 - heroineAbility.rangeUpPoint) && this.frame_count < (this.badRange2 + heroineAbility.rangeUpPoint))
            {
                //great判定以上
                if (this.frame_count > (this.greatRange1 - heroineAbility.rangeUpPoint) && this.frame_count < (this.greatRange2 + heroineAbility.rangeUpPoint))
                {
                    //perfect判定
                    if (this.frame_count > (this.perfectRange1 - heroineAbility.rangeUpPoint) && this.frame_count < (this.perfectRange2 + heroineAbility.rangeUpPoint))
                    {
                        this.g.perfect();
                    }
                    else
                    {
                        this.g.great();
                    }
                    if (this.g.gameState == 0)
                    {
                        //プレイヤーの魔力量が5以上になったら
                        if (this.player.GetComponent<Player>().magicPoint >= 4)
                        {
                            //魔力消費
                            this.player.GetComponent<Player>().magicPoint -= 4;
                        }
                    }
                }
                else
                {
                    this.g.bad();
                }
            }
            else
            {
                this.g.bad();
            }
            //ノートの破壊
            Destroy(gameObject);
        }
    }
    public void note_down()
    {
        //スコアアップ
        this.g.score += 50;
        //オブジェクトの破壊
        Destroy(this.gameObject);
    }
}