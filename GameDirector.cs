using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour {
    //Sceneの中のゲームオブジェクトの格納
    [SerializeField]
    private GameObject generator;
    [SerializeField]
    private GameObject backGround;
    [SerializeField]
    private GameObject soundPlayer;
    [SerializeField]
    private GameObject scoreLabel;
    [SerializeField]
    private GameObject comboLabel;
    [SerializeField]
    private GameObject highscoreLabel;
    [SerializeField]
    private GameObject fullcomboLabel;
    [SerializeField]
    private GameObject tapDecisionUI;
    [SerializeField]
    private GameObject magicUI;
    [SerializeField]
    public GameObject playerHpGage;
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject enemyHpGage;
    [SerializeField]
    private GameObject enemyHpBar;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject playerHpFace;
    [SerializeField]
    private GameObject cutin;
    [SerializeField]
    private GameObject heroine;
    private GameObject fairy;
    [SerializeField]
    private GameObject slash;
    //[SerializeField]
    //private GameObject swordEffecter;
    [SerializeField]
    private GameObject gameOver;
    [SerializeField]
    private GameObject fullCombo;
    private Player playerAbility;
    private Heroine heroineAbility;
    private Enemy enemyAbility;
    private GameObject specialAssist1;
    private GameObject specialAssist2;
    private GameObject specialAssist3;
    private GameObject note;
    private GameObject button;
    private GameObject ReturnButton;
    private GameObject Paper;
    //オブジェクトをしまう箱
    public GameObject[] Particle;
    bool result = false;
    float span = 0.3f;
    float delta = 0;
    float sped = 1.0f;
    float t = 1.0f;
    public int framerate;
    private TextAsset music_notes_in;
    //ゲームがを終わったかをカウント(０はゲーム中、1はゲーム終了、 2は討伐成功)
    public int caunt;
    //フレームカウント
    public int frame_count = 0;
    //ノーツの出る位置の番号(0が真ん中、1が左、2が右になる)
    public int note_way = 0;
    //ノーツカウント
    public int note_count = 0;
    //ノーツ生成のフラグ
    private bool note_go = false;
    //ノーツの保管箱
    public int[] note_box;
    //保管数
    public int max_num;
    //ノーツの種類
    public int note_type = 0;
    //現在のノーツ数
    public int noteNum = 0;
    //スコア
    public int score = 0;
    //スコアの保存先キー
    private string keyscore1 = "SCORE1";
    private string keyscore2 = "SCORE2";
    private string keyscore3 = "SCORE3";
    //コンボ数
    public int combo;
    private int gamecombo;
    private int gamemaxcombo;
    //Perfect数
    public int perfectCount;
    //Great数
    private int greatCount;
    //bad数
    private int badCount;
    //Great数
    private int missCount;
    //Guard数
    private int guardCount;
    //Slash数
    private int slashCount;
    //最大コンボの保存先キー
    private string keycombo1 = "Maxcombo1";
    private string keycombo2 = "Maxcombo2";
    private string keycombo3 = "Maxcombo3";
    //ゲーム中のコンボ数の保存キー
    public string keygamecombo = "GemeCombo";
    //ゲームの状況
    public int gameState = 0;   //0で通常プレイ中
                                //1で通常プレイが終わる瞬間
                                //2でボーナスゲームに入り、ロングノーツを出していない瞬間
                                //3でボーナスゲーム中
                                //4でゲームオ－バー
    //必殺技の状態
    public int specialState;
    public int specialState_count;
    public bool spesialAttack = false;
    //ガード中にParticleが出る間隔
    public int guardParticleSpan;
    //判定ポイント数
    public int decisionPoint;
    //guardSprite
    [SerializeField]
    private Sprite guardSprite;
    //PerfectSprite
    [SerializeField]
    private Sprite perfectSprite;
    //GreatSprite
    [SerializeField]
    private Sprite greatSprite;
    //BadSprite
    [SerializeField]
    private Sprite badSprite;
    //MissSprite
    [SerializeField]
    private Sprite missSprite;
    //slashSprite
    [SerializeField]
    private Sprite slashSprite;
    //cureSprite
    [SerializeField]
    private Sprite cureSprite;
    //powerUpSprite
    [SerializeField]
    private Sprite powerUpSprite;
    //rangeUpSprite
    [SerializeField]
    private Sprite rangeUpSprite;
    //magicUpSprite
    [SerializeField]
    private Sprite scoreUpSprite;
    [SerializeField]
    private Sprite playerFace1;
    [SerializeField]
    private Sprite playerFace2;
    //playerFaceの入れ替え時間
    private float damageTimer;
    //playerFaceの入れ替えスイッチ
    private bool damageSwitch;
    //ゲームオーバー時の背景の透明度
    private float gameOverAlpha = 1;
    //判定UIの透明度
    private float decisionAlpha = 0;
    //魔法UIの透明度
    private float magicUIAlpha = 0;
    //魔法UIの透明度上昇値
    private float magicUIAlphaValue = 0;
    //タッチした時のポジション
    private Vector2 startPos;
    //子分ノーツ
    private GameObject followerNote;
    //フリック制限時間カウント
    private int timeLimit = 0;
    //buttonの管理
    public int buttoncount;
    //アニメが開始するまでの時間
    private float timer;
    //妖精の生成は1回だけ
    private bool fairyAnimOnece = true;
    //フルコンボしたか
    private bool full = true;
    private bool full2 = false;
    //Textゲームオブジェクト
    public Text SubjugationFailureText;
    public Text ScoreText;
    public Text SubdueSuccessText;
	public Text ScoreText1;
	public Text ComboText;
	public Text ComboText1;
    void Start() {
        //VSyncをOFFにする
        QualitySettings.vSyncCount = 0;
        //ターゲットフレームレートを60に設定
        Application.targetFrameRate = 60;
        /*
        PlayerPrefs.DeleteKey(this.keyscore1);
        PlayerPrefs.DeleteKey(this.keyscore2);
        PlayerPrefs.DeleteKey(this.keyscore3);
        PlayerPrefs.DeleteKey(this.keycombo1);
        PlayerPrefs.DeleteKey(this.keycombo2);
        PlayerPrefs.DeleteKey(this.keycombo3);
        */
        if (framerate == 15)
        {
            //ノーツ保管箱にノートを入れる
            this.music_notes_in = Resources.Load("text/music-notes", typeof(TextAsset)) as TextAsset;
        }
        else if(framerate == 10)
        {
            //ノーツ保管箱にノートを入れる
            this.music_notes_in = Resources.Load("text/music-notes2", typeof(TextAsset)) as TextAsset;
        }
        else
        {
            //ノーツ保管箱にノートを入れる
            this.music_notes_in = Resources.Load("text/music-notes3", typeof(TextAsset)) as TextAsset;
        }
        string[] music_notes_out = this.music_notes_in.text.Split(","[0]);
        this.max_num = music_notes_out.Length - 1;
        this.note_box = new int[this.max_num];
        for (int i = 0; i < this.max_num; i++)
        {
            this.note_box[i] = int.Parse(music_notes_out[i]);
        }
        //ゲームオブジェクトのnoteを探す
        this.note = GameObject.Find("note");
        //スペシャルアシストを探す
        this.specialAssist1 = GameObject.Find("specialAssist1");
        this.specialAssist2 = GameObject.Find("specialAssist2");
        this.specialAssist3 = GameObject.Find("specialAssist3");
        //Playerに参照
        this.playerAbility = this.player.GetComponent<Player>();
        //Heroineに参照
        this.heroineAbility = this.heroine.GetComponent<Heroine>();
        //Enemyに参照
        this.enemyAbility = this.enemy.GetComponent<Enemy>();
        specialAssist1.SetActive(false);
        specialAssist2.SetActive(false);
        specialAssist3.SetActive(false);
        this.highscoreLabel.SetActive(false);
        this.fullcomboLabel.SetActive(false);
        //時間
        this.timer = 2.0f;
        //初期スコア
        this.score = 0;
        //初期コンボ数
        this.combo = 0;
        this.gamecombo = 0;
        this.gamemaxcombo = 0;
        //初期ミス数
        this.missCount = 0;
        //初期パーフェクト数
        this.perfectCount = 0;
        //初期グレート数
        this.greatCount = 0;
        //初期バッド数
        this.badCount = 0;
        this.guardCount = 0;
        this.slashCount = 0;
        this.caunt = 0;
        //Playerに参照
        this.playerAbility = this.player.GetComponent<Player>();
        //Heroineに参照
        this.heroineAbility = this.heroine.GetComponent<Heroine>();
        //Enemyに参照
        this.enemyAbility = this.enemy.GetComponent<Enemy>();
    }

	void Update() {	
        if (this.caunt == 0)
        {
            //フレームカウントの加算
            this.frame_count += 1;
            if (this.note_count < this.max_num / 3)
            {
                //Debug.Log (this.frame_count); //フレームカウントの確認
                if (this.frame_count % this.framerate == 0)
                {
                    //ノーツカウントの加算
                    this.note_count += 1;
                    //ゲーム状況が1か2でないとき
                    if ((this.gameState != 1) && (this.gameState != 2))
                    {
                        //ノーツの生成フラグをtrueに
                        this.note_go = true;
                    }
                    //ゲーム状況が2の時
                    if (this.gameState == 2)
                    {
                        for (this.note_way = 0; this.note_way < 3; this.note_way++)
                        {
                            //出てくるノーツがロングノーツの間と終端でない場合
                            if (this.note_box[this.note_count + (this.note_way * (this.max_num / 3))] == 1 || this.note_box[this.note_count + (this.note_way * (this.max_num / 3))] == 2)
                            {
                                //ゲーム状況を3に移行
                                this.gameState = 3;
                                //ノーツの生成フラグをtrueに
                                this.note_go = true;
                            }
                        }
                    }
                }
            }
            //ノーツ生成フラグがtrueの場合、ノーツ保管箱からノーツを生成
            if (this.note_go)
            {
                for (this.note_way = 0; this.note_way < 3; this.note_way++)
                {
                    if (this.note_box[this.note_count + (this.note_way * (this.max_num / 3))] > 0)
                    {
                        this.note_type = this.note_box[this.note_count + (this.note_way * (this.max_num / 3))];
                        this.generator.GetComponent<Generator>().SpawnNote();
                    }
                    //ノーツの生成フラグをfalseに
                    this.note_go = false;
                }
            }
        }
        if(this.gameState == 0) { 
            if (this.spesialAttack == false && this.note_count == (int)((this.max_num / 3) * 0.6f)) {
                this.generator.GetComponent<Generator>().spawnMagicSquareParticle();
                this.spesialAttack = true;
                this.backGround.GetComponent<SpriteRenderer>().color = new Color(0.4f, 0.4f, 0.4f, 1);
                specialAssist1.SetActive(true);
                specialAssist2.SetActive(true);
                specialAssist3.SetActive(true);
            }
        }
            //getmouse
            //画面タッチしたとき
            if (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0) || Input.GetMouseButtonDown(0))
            {
                //タッチした座標をワールド座標に変換
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //タッチをした位置にオブジェクトがあるかどうかを判定
                RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
                //ロングノーツが来るとき、盾を表示
                if (hit)
                {
                    //タッチしたオブジェクトのcolliderを四角く切り取る
                    Bounds rect = hit.collider.bounds;
                    //切り取った部分とタップした位置が重なっていたら
                    if (rect.Contains(worldPoint))
                    {
                        //そのオブジェクトを格納
                        GameObject tapObject = hit.collider.gameObject;
                        //タッチしたオブジェクトの名前がHeroine出ない場合
                        if (tapObject.name != "Heroine")
                        {
                            //NoteControllerをコンポネント
                            NoteController noteComponent = tapObject.GetComponent<NoteController>();
                            //note_typeを格納
                            int note_type = noteComponent.note_type;
                        //note_typeによってタップの判定のしかたが変わる
                        if (note_type == 1)
                            {
                                if (Input.GetMouseButtonDown(0))
                                {
                                    //通常ノーツ判定
                                    noteComponent.note_tap();
                                    this.guardCount++;
                                }
                            }
                            else if (note_type == 2 || note_type == 3 || note_type == 4)
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    if (this.note_count >= 11)
                                    {
                                        if ((this.note_box[(this.note_count - 6) + (360 * i)] == 3 ||
                                            this.note_box[(this.note_count - 11) + (360 * i)] == 3) &&
                                            this.note_box[(this.note_count - 6) + (360 * i)] != 1 &&
                                            this.note_box[(this.note_count - 6) + (360 * i)] != 5)
                                        {
                                            if (guardParticleSpan >= 15)
                                            {
                                                guardParticleSpan = 0;
                                        　  }
                                            else
                                            {
                                                guardParticleSpan++;
                                            }
                                        }
                                    }
                                }
                                if (Input.GetMouseButton(0))
                                    {
                                        //ガードの生成
                                        this.generator.GetComponent<Generator>().spawnGuardParticle();
                                        this.generator.GetComponent<Generator>().spawnGuardPrefab();
                                        //ロングノーツの判定
                                        noteComponent.note_down();
                                        //判定Spriteの決定
                                        this.tapDecisionUI.GetComponent<SpriteRenderer>().sprite = this.guardSprite;
                                        //判定UI表示
                                        this.decisionAlpha = 1;
                                        //効果音再生
                                        this.soundPlayer.GetComponent<SoundPlayer>().guardSound();
                                    }
                            }
                            //子分ノーツの判定1
                            else if (note_type == 5)
                            {
                                if (Input.GetMouseButtonDown(0))
                                {
                                    //タップ時の位置を取得
                                    this.startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                                    //子分ノーツオブジェクトに格納
                                    this.followerNote = tapObject;
                                    //斬撃Effect
                                    //this.swordEffecter.GetComponent<SwordEffect>().touch = true;
                                }
                            }
                            else
                            {

                            }
                        }
                        //タッチしたオブジェクトの名前がHeroineの場合
                        else
                        {

                        }
                    }
                }
                //必殺技発動コマンド
                if (this.spesialAttack)
                {
                    //必殺技発動コマンド
                    if (worldPoint.y > 1.5f)
                    {
                        if (worldPoint.y <= 1.9f && worldPoint.x >= -1.5f && worldPoint.x <= -1.1f)
                        {
                            if (this.specialState == 3 || this.specialState == 2)
                            {
                                this.specialState_count += 100;
                                if (this.specialState == 3)
                                {
                                    //効果音再生
                                    this.GetComponent<MainBGM>().assistFinished();
                                    this.generator.GetComponent<Generator>().spawnSPline1Particle();
                                }
                                else
                                {
                                    //効果音再生
                                    this.GetComponent<MainBGM>().assistFinished();
                                    this.generator.GetComponent<Generator>().spawnSPline2Particle();
                                }
                            }
                            this.specialState = 1;
                        }
                        if (worldPoint.y <= 1.9f && worldPoint.x <= 1.5f && worldPoint.x >= 1.1f)
                        {
                            if (this.specialState == 1 || this.specialState == 3)
                            {
                                this.specialState_count += 1;
                                if (this.specialState == 1)
                                {
                                    //効果音再生
                                    this.GetComponent<MainBGM>().assistFinished();
                                    this.generator.GetComponent<Generator>().spawnSPline2Particle();
                                }
                                else
                                {
                                    //効果音再生
                                    this.GetComponent<MainBGM>().assistFinished();
                                    this.generator.GetComponent<Generator>().spawnSPline3Particle();
                                }
                            }
                            this.specialState = 2;
                        }
                        if (worldPoint.y <= 4.4f && worldPoint.y >= 4.0f && worldPoint.x >= -0.2f && worldPoint.x <= 0.2f)
                        {
                            if (this.specialState == 1 || this.specialState == 2)
                            {
                                this.specialState_count += 10;
                                if (this.specialState == 2)
                                {
                                    //効果音再生
                                    this.GetComponent<MainBGM>().assistFinished();
                                    this.generator.GetComponent<Generator>().spawnSPline3Particle();
                                }
                                else
                                {
                                    //効果音再生
                                    this.GetComponent<MainBGM>().assistFinished();
                                    this.generator.GetComponent<Generator>().spawnSPline1Particle();
                                }
                            }
                            this.specialState = 3;
                        }
                        if (this.specialState_count == 111)
                        {
                            Destroy(specialAssist1);
                            Destroy(specialAssist2);
                            Destroy(specialAssist3);
                            this.cutin.SetActive(true);
                            //効果音再生
                            this.GetComponent<MainBGM>().cutIn();
                            Invoke("SpecialAttack_in", 1);
                            Invoke("SpecialAttack_span", 3.0f);
                            Invoke("SpecialAttack_span", 3.2f);
                            Invoke("SpecialAttack_span", 3.4f);
                            //Invoke("SpecialAttack_span", 3.6f);
                            //Invoke("SpecialAttack_span", 3.8f);
                            //Invoke("SpecialAttack_span", 4.0f);
                            Invoke("SpecialAttack_end", 4.0f);
                            this.spesialAttack = false;
                        }
                        if (Input.GetMouseButtonUp(0))
                        {
                            if (this.specialState_count != 111)
                            {
                                this.specialState = 0;
                                this.specialState_count = 0;
                            }
                        }
                    }
                    else
                    {
                        if (this.specialState_count != 111)
                        {
                            this.specialState = 0;
                            this.specialState_count = 0;
                        }
                    }
                }
            }
            //子分ノーツの判定2
            if (this.followerNote)
            {
                //制限時間カウントの加算
                this.timeLimit += 1;
                if (Input.GetMouseButtonUp(0))
                {
                    //制限時間カウントが1以上なら
                    if (this.timeLimit < 30)
                    {
                        //制限時間カウントを初期化
                        this.timeLimit = 0;
                        //タップ離した時の位置を取得
                        Vector2 endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        //startPosとendPosの距離を取得
                        Vector2 moveVec = endPos - this.startPos;
                        //格納した子分ノーツに力を加える
                        if (moveVec.x > 2.0f || moveVec.x < -2.0f)
                        {
                            //効果音再生
                            this.soundPlayer.GetComponent<SoundPlayer>().swordSound();
                            this.followerNote.GetComponent<Rigidbody2D>().AddForce(moveVec * 5, ForceMode2D.Impulse);
                        }
                    }
                    else
                    {
                        //制限時間カウントを初期化
                        this.timeLimit = 0;
                    }
                    //this.swordEffecter.GetComponent<SwordEffect>().touch = false;
                    //子分ノーツの中身を削除
                    this.followerNote = null;
                }
            }
        
        //touch
        foreach (Touch t in Input.touches) {
            //タッチした座標をワールド座標に変換
            Vector2 touchPoint = Camera.main.ScreenToWorldPoint(t.position);
            //タッチをした位置にオブジェクトがあるかどうかを判定
            RaycastHit2D hit = Physics2D.Raycast(touchPoint, Vector2.zero);
            //ロングノーツが来るとき、盾を表示
            for (int i = 0; i < 3; i++) {
                if (this.note_count >= 11) {
                    if ((this.note_box[(this.note_count - 6) + (360 * i)] == 3 ||
                                          this.note_box[(this.note_count - 11) + (360 * i)] == 3) &&
                                          this.note_box[(this.note_count - 6) + (360 * i)] != 1 &&
                                          this.note_box[(this.note_count - 6) + (360 * i)] != 5) {
                        if (guardParticleSpan >= 15) {
                            guardParticleSpan = 0;
                        } else {
                            guardParticleSpan++;
                        }
                        this.generator.GetComponent<Generator>().spawnGuardPrefab();
                    }
                }
            }
            if (hit) {
                //タッチしたオブジェクトのcolliderを四角く切り取る
                Bounds rect = hit.collider.bounds;
                //切り取った部分とタップした位置が重なっていたら
                if (rect.Contains(touchPoint)) {
                    //そのオブジェクトを格納
                    GameObject touchObject = hit.collider.gameObject;
                    if (touchObject.name != "Heroine") {
                        //NoteControllerをコンポネント
                        NoteController noteComponent = touchObject.GetComponent<NoteController>();
                        //note_typeを格納
                        int note_type = noteComponent.note_type;
                        //ノーツのy座標が一定値より下がっていたら
                        if (touchObject.transform.position.y < -0.5) {
                            //note_typeによってタップの判定のしかたが変わる
                            switch (t.phase) {
                                case TouchPhase.Began:
                                    //通常ノーツ、ロングノーツ(始)の判定
                                    if (note_type == 1) {
                                        noteComponent.note_tap();
                                    }
                                    //子分ノーツの判定1
                                    else if (note_type == 5) {
                                        //タップ時の位置を取得
                                        this.startPos = Camera.main.ScreenToWorldPoint(t.position);
                                        //子分ノーツオブジェクトに格納
                                        this.followerNote = touchObject;
                                        //斬撃Effect
                                        //this.swordEffecter.GetComponent<SwordEffect>().touch = true;
                                    }
                                    break;
                                case TouchPhase.Stationary:
                                    //ロングノーツ（間）の判定
                                    for (int i = 0; i < 3; i++)
                                    {
                                        if (this.note_count >= 11)
                                        {
                                            if ((this.note_box[(this.note_count - 6) + (360 * i)] == 3 ||
                                                this.note_box[(this.note_count - 11) + (360 * i)] == 3) &&
                                                this.note_box[(this.note_count - 6) + (360 * i)] != 1 &&
                                                this.note_box[(this.note_count - 6) + (360 * i)] != 5)
                                            {
                                                if (guardParticleSpan >= 15)
                                                {
                                                    guardParticleSpan = 0;
                                                }
                                                else
                                                {
                                                    guardParticleSpan++;
                                                }
                                            }
                                        }
                                    }
                                    if (note_type == 2 || note_type == 3 || note_type == 4) {
                                        this.guardCount++;
                                        //ガードの生成
                                        this.generator.GetComponent<Generator>().spawnGuardParticle();
                                        this.generator.GetComponent<Generator>().spawnGuardPrefab();
                                        //ロングノーツの判定
                                        noteComponent.note_down();
                                        //判定Spriteの決定
                                        this.tapDecisionUI.GetComponent<SpriteRenderer>().sprite = this.guardSprite;
                                        //判定UI表示
                                        this.decisionAlpha = 1;
                                        //効果音再生
                                        this.soundPlayer.GetComponent<SoundPlayer>().guardSound();
                                    }
                                    break;
                            }
                        }
                    } 
                }
            }
            if (this.spesialAttack) {
                //必殺技発動コマンド
                if (touchPoint.y > 1.5f) {
                    if (touchPoint.y <= 1.9f && touchPoint.x >= -1.5f && touchPoint.x <= -1.1f) {
                        if (this.specialState == 3 || this.specialState == 2) {
                            this.specialState_count += 100;
                            if (this.specialState == 3) {
                                //効果音再生
                                this.GetComponent<MainBGM>().assistFinished();
                                this.generator.GetComponent<Generator>().spawnSPline1Particle();
                            } else {
                                //効果音再生
                                this.GetComponent<MainBGM>().assistFinished();
                                this.generator.GetComponent<Generator>().spawnSPline2Particle();
                            }
                        }
                        this.specialState = 1;
                    }
                    if (touchPoint.y <= 1.9f && touchPoint.x <= 1.5f && touchPoint.x >= 1.1f) {
                        if (this.specialState == 1 || this.specialState == 3) {
                            this.specialState_count += 1;
                            if (this.specialState == 1) {
                                //効果音再生
                                this.GetComponent<MainBGM>().assistFinished();
                                this.generator.GetComponent<Generator>().spawnSPline2Particle();
                            } else {
                                //効果音再生
                                this.GetComponent<MainBGM>().assistFinished();
                                this.generator.GetComponent<Generator>().spawnSPline3Particle();
                            }
                        }
                        this.specialState = 2;
                    }
                    if (touchPoint.y <= 4.4f && touchPoint.y >= 4.0f && touchPoint.x >= -0.2f && touchPoint.x <= 0.2f) {
                        if (this.specialState == 1 || this.specialState == 2) {
                            this.specialState_count += 10;
                            if (this.specialState == 2) {
                                //効果音再生
                                this.GetComponent<MainBGM>().assistFinished();
                                this.generator.GetComponent<Generator>().spawnSPline3Particle();
                            } else {
                                //効果音再生
                                this.GetComponent<MainBGM>().assistFinished();
                                this.generator.GetComponent<Generator>().spawnSPline1Particle();
                            }
                        }
                        this.specialState = 3;
                    }

                    if (this.specialState_count == 111) {
                        specialAssist1.SetActive(false);
                        specialAssist2.SetActive(false);
                        specialAssist3.SetActive(false);
                        this.cutin.SetActive(true);
                        //効果音再生
                        this.GetComponent<MainBGM>().cutIn();
                        Invoke("SpecialAttack_in", 1);
                        Invoke("SpecialAttack_span", 3.0f);
                        Invoke("SpecialAttack_span", 3.2f);
                        Invoke("SpecialAttack_span", 3.4f);
                        //Invoke("SpecialAttack_span", 3.6f);
                        //Invoke("SpecialAttack_span", 3.8f);
                        //Invoke("SpecialAttack_span", 4.0f);
                        Invoke("SpecialAttack_end", 4.0f);
                        this.spesialAttack = false;
                    }
                    if (t.phase == TouchPhase.Ended) {
                        if (this.specialState_count != 111) {
                            this.specialState = 0;
                            this.specialState_count = 0;
                        }
                    }
                } else {
                    if (this.specialState_count != 111) {
                        this.specialState = 0;
                        this.specialState_count = 0;
                    }
                }

            }
        }
        //子分ノーツの判定2
        if (this.followerNote) {
            //制限時間カウントの加算
            timeLimit += 1;
            foreach (Touch t in Input.touches) {
                //タップ離した時の位置を取得
                if (t.phase == TouchPhase.Ended) {
                    //制限時間カウントが1以上なら
                    if (timeLimit < 30) {
                        //制限時間カウントを初期化
                        this.timeLimit = 0;
                        Vector2 endPos = Camera.main.ScreenToWorldPoint(t.position);
                        //startPosとendPosの距離を取得
                        Vector2 moveVec = endPos - this.startPos;
                        //格納した子分ノーツに力を加える
                        if (moveVec.x > 2.0f || moveVec.x < -2.0f) {
                            //効果音再生
                            this.soundPlayer.GetComponent<SoundPlayer>().swordSound();
                            this.followerNote.GetComponent<Rigidbody2D>().AddForce(moveVec * 5, ForceMode2D.Impulse);
                        }
                    } else {
                        //制限時間カウントを初期化
                        this.timeLimit = 0;
                    }
                    //this.swordEffecter.GetComponent<SwordEffect>().touch = false;
                    //子分ノーツの中身を削除
                    this.followerNote = null;
                }
            }
        }
        //スコアの更新
        this.scoreLabel.GetComponent<Text> ().text = this.score.ToString ();
		//コンボ数の更新
		this.comboLabel.GetComponent<Text> ().text = this.combo.ToString () + "combo";
		//判定テキストの色合い変更
		this.tapDecisionUI.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, this.decisionAlpha);
		//判定テキスト透明度の減少
		this.decisionAlpha *= 0.95f;
		//魔法テキストの表示
		if (this.heroineAbility.magicType > 0) {
			//魔法テキストの決定
			this.heroinMagicUI ();
			//魔法テキスト透明度の値変更
			this.magicUIAlphaValue = 0.017f;
		}
		if (this.magicUIAlpha >= 1.0f) {
			//魔法テキスト透明度の値変更
			this.magicUIAlphaValue = -0.017f;
		}
		//魔法テキスト透明度の更新
		this.magicUIAlpha += this.magicUIAlphaValue;
		if (this.magicUIAlpha <= 0) {
			//魔法テキスト透明度の値変更
			this.magicUIAlphaValue = 0;
		}
		//魔法テキストの色合い変更
		this.magicUI.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, this.magicUIAlpha);
        //enemyHpが0になったら
        if ((this.enemyAbility.enemyHp <= 0) && (this.gameState == 0))
        {
            //0.5秒後にBonusGame_inを読み込む
            Invoke("BonusGame_in", 2.0f);
            //5秒後にBonusGame_startを読み込む
            Invoke("BonusGame_start", 5.0f);
            //敵を撃破した瞬間のフラグ
            this.gameState = 1;
        }
        //playerHpが0になったら
        /*
        if ((this.playerAbility.playerHp <= 0) && (this.gameState == 0)) {
            this.gameState = 4;
            this.timer = 3;
            this.heroineAbility.state = 3;
            this.playerAbility.state = 2;
            this.GetComponent<MainBGM>().pause();
            this.GetComponent<MainBGM>().failSound();
            Instantiate(this.gameOver);
        }
        */
        if(this.gameState == 4)
        {
            this.timer -= Time.deltaTime;
            this.gameOverAlpha *= 0.9f;
            if(this.gameOverAlpha < 0.2f)
            {
                this.gameOverAlpha = 0.2f;
            }
            this.backGround.GetComponent<SpriteRenderer>().color = new Color(this.gameOverAlpha, this.gameOverAlpha, this.gameOverAlpha, 1);
            if(this.timer < 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    this.GetComponent<MainBGM>().nextSound();
                    this.timer = 10;
                    CameraFade.StartAlphaFade(Color.black, false, 1.5f, 1.0f, () => { Application.LoadLevel("SelectScene"); });
                }
            }
        }
        else
        {
            //ゲーム結果の処理
            if (this.frame_count >= ((this.max_num / 3) * this.framerate) + 120)
            {
                if (this.full)
                {
                    this.full2 = true;
                    this.full = false;
                    GameObject fullcombo = Instantiate(this.fullCombo) as GameObject;
                }
                if (this.spesialAttack)
                {
                    this.spesialAttack = false;
                    this.backGround.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                    Destroy(this.specialAssist1);
                    Destroy(this.specialAssist2);
                    Destroy(this.specialAssist3);
                }
                if (this.fairyAnimOnece)
                {
                    if (this.fairy != null)
                    {
                        //妖精退場
                        this.fairy.GetComponent<Fairy>().endMotion();
                    }
                    else
                    {
                        this.enemyAbility.escape = true;
                    }
                    this.fairyAnimOnece = false;
                }
            }
            if (this.frame_count >= ((note_box.Length / 3) * this.framerate + 420))
            {
                this.result = true;
                this.generator.GetComponent<Generator>().spawnResult();
                //kamiを探す
                this.Paper = GameObject.Find("kami(Clone)");
                //ボタンを探す
                this.button = GameObject.Find("Next");
                this.ReturnButton = GameObject.Find("ReturnButton");
                //ボタンを非表示
                this.button.SetActive(false);
                this.ReturnButton.SetActive(false);
                //オブジェクトを消去
                //Destroy (this.generator);
                Destroy(this.enemy);
                Destroy(this.enemyHpBar);
                this.comboLabel.SetActive(false);
                this.scoreLabel.SetActive(false);
                //Destroy(this.swordEffecter);
                //noteを非表示にする
                this.note.SetActive(false);
                this.note_count = 0;
                this.frame_count = 0;
                this.caunt = 1;
                this.buttoncount = 0;
                this.note_go = false;
                if (this.fairy == null)
                {
                    this.generator.GetComponent<Generator>().spawnFairyFailed();
                    this.fairy = GameObject.FindGameObjectWithTag("fairy");
                    Invoke("fairyStateChange", 2.0f);
                    Invoke("heroineStateChange", 2.0f);
                    Invoke("playerStateChange", 2.0f);
                }
                else
                {
                    this.fairy.GetComponent<Fairy>().inMotion();
                    Invoke("fairyStateChange", 2.0f);
                    Invoke("heroineStateChange", 2.0f);
                    Invoke("playerStateChange", 2.0f);
                }
            }
        }
        if (this.result)
        {
            //時間の計算
            this.timer -= Time.deltaTime;
        }
		if (this.timer < 0 && this.gameState != 4) {
			if (this.buttoncount == 0) {
				//ボタン表示
				this.button.SetActive (true);
                if (this.gameState >= 1) {
					//効果音再生
					this.GetComponent<MainBGM> ().clearSound ();
                    //ゲームクリアテキスト表示
                    SubdueSuccessText.enabled = true;
                    //GameCrearText中のAnimatonsスクリプトの中のメソッドにアクセス
                    this.SubdueSuccessText.GetComponent<AnimatonsScript> ().anima ();
			    	this.caunt = 2;
				} else if (this.gameState == 0) {
					//効果音再生
					this.GetComponent<MainBGM> ().failSound ();
                    //ゲームクリアテキスト表示
                    this.SubjugationFailureText.enabled = true;
                    //GameCrearText中のAnimatonsスクリプトの中のメソッドにアクセス
                    this.SubjugationFailureText.GetComponent<AnimatonsScript> ().animGameOver ();
                }
                timer = 0;
                result = false;
			}
		}

		if (this.caunt == 2) {
			this.delta += Time.deltaTime;
			if (this.delta > this.span) {
				//Random rand = new Random();
                this.delta = 0;
				GameObject go;
                //プレハブを出す位置
				//オブジェクトの座標
				float x = Random.Range (-4.15f, 4.15f);
				float y = Random.Range (0.0f, -5.4f);
				float z = Random.Range (0.0f, 2.0f);
				go = Instantiate (Particle [Random.Range (0, Particle.Length)], new Vector3 (x, y, 0), Quaternion.identity) as GameObject;
				//オブジェクトを消す
				GameObject.Destroy (go, 2.0f);
			}
		}
        //コンボスプライトが消える処理
        this.t -= Time.deltaTime;
        if (this.combo > gamecombo)
        {

            this.t = 0.3f;

            if (this.t >= 0.3f)
            {
                //更新
                gamecombo = combo;
                if (gamecombo > 0)
                {
                    this.comboLabel.GetComponent<Text>().text = this.gamecombo.ToString() + "combo";
                    this.comboLabel.SetActive(true);
                }
                else
                {
                    this.comboLabel.SetActive(false);
                }
            }
        }
        if (this.t <= 0)
        {
            if (this.combo == gamecombo)
            {
                this.comboLabel.SetActive(false);

            }
        }
        if (this.enemyHpBar != null)
        {
            //プレイヤー体力の減少
            this.playerHpGage.GetComponent<Image>().fillAmount = this.playerAbility.playerHp / this.playerAbility.maxHp;
            //敵体力を減少
            this.enemyHpGage.GetComponent<Image>().fillAmount = this.enemyAbility.enemyHp / this.enemyAbility.enemyMaxHp;
            if (!this.damageSwitch)
            {
                this.playerHpFace.GetComponent<SpriteRenderer>().sprite = this.playerFace1;
            }
            else
            {
                this.damageTimer += Time.deltaTime;
                this.playerHpFace.GetComponent<SpriteRenderer>().sprite = this.playerFace2;
            }
            if(this.damageTimer > 0.2f)
            {
                this.damageTimer = 0;
                this.damageSwitch = false;
            }
        }
    }

    public void BonusGame_in()
    {
        //敵オブジェクト破壊
        Destroy(this.enemy);
        Destroy(this.enemyHpBar);
        this.score += (int)(this.enemyAbility.enemyMaxHp * 100);
        //効果音再生
        this.soundPlayer.GetComponent<SoundPlayer>().enemyDestroySound();
        //敵が消えるときのパーティクル生成
        this.generator.GetComponent<Generator>().spawnEnemyBreakParticle();
        if (this.spesialAttack)
        {
            this.spesialAttack = false;
            this.backGround.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            this.specialAssist1.active = false;
            this.specialAssist2.active = false;
            this.specialAssist3.active = false;
        }
        if (this.frame_count <= ((this.max_num / 3) * this.framerate) - 180) {
            //2秒後にspawnfairyを読み込む
            Invoke("spawnfairy", 2.0f);
        }
    }

    public void spawnfairy()
    {
        //妖精の生成
        this.generator.GetComponent<Generator>().spawnFairy();
        this.fairy = GameObject.FindGameObjectWithTag("fairy");
    }

    public void fairyStateChange()
    {
        if (this.gameState >= 1)
        {
            this.fairy.GetComponent<Fairy>().state = 1;
        }
        else if(this.gameState == 0)
        {
            this.fairy.GetComponent<Fairy>().state = 2;
        }
    }

    public void heroineStateChange()
    {
        if (this.gameState >= 1)
        {
            this.heroineAbility.state = 1;
        }
        else
        {
            this.heroineAbility.state = 2;
        }
    }

    public void playerStateChange()
    {
        if (this.gameState >= 1)
        {
            this.playerAbility.state = 1;
        }
        else
        {
            this.playerAbility.state = 2;
        }
    }

    public void BonusGame_start()
    {
        //ゲームの状況を2に移行
        this.gameState = 2;
    }

    public void SpecialAttack_in()
    {
        this.cutin.SetActive(false);
        this.generator.GetComponent<Generator>().spawnSpecialAttack();
        //効果音再生
        Invoke("swordDelay", 0.3f);
        Invoke("swordDelay", 0.5f);
        Invoke("swordDelay", 0.8f);
        Invoke("swordDelay", 1.0f);
        Invoke("swordDelay", 1.2f);
        Invoke("swordDelay", 1.4f);
    }
    public void swordDelay()
    {
        //効果音再生
        this.GetComponent<MainBGM>().spesialAttack1();
    }
    public void SpecialAttack_span()
    {
        this.generator.GetComponent<Generator>().spawnBomParticle();
        //効果音再生
        this.GetComponent<MainBGM>().spesialAttack2();
    }
    public void SpecialAttack_end()
    {
        this.generator.GetComponent<Generator>().spawnBurstParticle();
        this.backGround.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        //効果音再生
        this.GetComponent<MainBGM>().spesialAttack3();
        this.score += 3000;
		this.enemyAbility.enemyHp -= this.enemyAbility.enemyMaxHp / 2;
    }

    public void decreasePlayerHp()
    {
        this.full = false;
        if (this.gameState < 2)
        {
            //プレイヤーにダメージ
            this.playerAbility.playerHp -= this.decisionPoint;
            this.damageSwitch = true;
        }
		//ヒロインが嫌がる
		this.heroineAbility.noMotion();
        //プレイヤー痛がる
        this.playerAbility.damageMotion();
    }

    public void decreaseEnemyHp()
    {
        //判定UI表示
        this.decisionAlpha = 1;
        //コンボ数アップ
        this.combo += 1;
        if (this.combo >= this.gamemaxcombo)
        {
            this.gamemaxcombo = this.combo;
        }
        //スコアの加算
        this.scoreUp();
        //プレイヤー魔力値増加
        this.playerAbility.magicPoint += this.decisionPoint;
        if (this.playerAbility.magicPoint >= 4)
        {
            if (this.enemyAbility.enemyHp > 0)
            {
                this.slash.GetComponent<SlashAnimation>().state = true;
                //敵にダメージ
                this.enemyAbility.enemyHp -= this.playerAbility.attackPower;
                //効果音再生
                this.soundPlayer.GetComponent<SoundPlayer>().enemyDamagedSound();
                //敵ダメージモーション
                this.enemyAbility.damageMotion();
            }
        }
        //ヒロイン喜ぶ
        this.heroineAbility.happyMotion();
        //プレイヤー攻撃モーション
        this.playerAbility.attackMotion();
    }

    public void miss()
    {
        //miss数の加算
        this.missCount++;
        //コンボが途切れる
        this.combo = 0;
        this.gamecombo = 0;
        //判定Spriteの決定
        this.tapDecisionUI.GetComponent<SpriteRenderer>().sprite = this.missSprite;
        //判定UI表示
        this.decisionAlpha = 1;
        //ダメージ数決定
        this.decisionPoint = 2;
        //PlayerHP画像ゲージの減少
        this.decreasePlayerHp();
        //効果音再生
        this.soundPlayer.GetComponent<SoundPlayer>().playerDamagedSound();
    }

    public void perfect()
    {
        //Perfectカウントの加算
        this.perfectCount++;
        //tapParticleを生成
        this.generator.GetComponent<Generator>().spawnPerfectParticle();
        //魔力量増加値決定
        this.decisionPoint = 2;
        //EnemyHP画像ゲージの減少
        this.decreaseEnemyHp();
        //判定Spriteの決定
        this.tapDecisionUI.GetComponent<SpriteRenderer>().sprite = this.perfectSprite;
        //効果音再生
        this.GetComponent<MainBGM>().perfectTapSound();
    }

    public void great()
    {
        //Great数の加算
        this.greatCount++;
        //tapParticleを生成
        this.generator.GetComponent<Generator>().spawnGreatParticle();
        //魔力量増加値決定
        this.decisionPoint = 1;
        //EnemyHP画像ゲージの減少
        this.decreaseEnemyHp();
        //判定Spriteの決定
        this.tapDecisionUI.GetComponent<SpriteRenderer>().sprite = this.greatSprite;
        //効果音再生
        this.GetComponent<MainBGM>().greatTapSound();
    }

    public void bad()
    {
        //bad数の加算
        this.badCount++;
        //tapParticleを生成
        this.generator.GetComponent<Generator>().spawnBadParticle();
        //コンボが途切れる
        this.combo = 0;
        this.gamecombo = 0;
        //ダメージ数決定
        this.decisionPoint = 1;
        //PlayerHP画像ゲージの減少
        this.decreasePlayerHp();
        //判定Spriteの決定
        this.tapDecisionUI.GetComponent<SpriteRenderer>().sprite = this.badSprite;
        //判定UI表示
        this.decisionAlpha = 1;
        //効果音再生
        this.GetComponent<MainBGM>().badTapSound();
    }
    public void followerNote_Slash()
    {
        //コンボ数アップ
        this.combo += 1;
        this.slashCount++;
        //判定Spriteの決定
        this.tapDecisionUI.GetComponent<SpriteRenderer>().sprite = this.slashSprite;
        //判定UI表示
        this.decisionAlpha = 1;
        if (this.gameState == 0)
        {
            //スコアの加算
            this.score += 1000;
        }
        else
        {
            //スコアの加算
            this.score += 2000;
        }
		//ヒロイン喜ぶ
		this.heroineAbility.happyMotion();
        this.playerAbility.attackMotion();
    }
    public void heroinMagicUI()
    {
        //MagicSpriteの決定
        switch (this.heroineAbility.magicType)
        {
            case 1:
                this.magicUI.GetComponent<SpriteRenderer>().sprite = this.cureSprite;
                break;
            case 2:
                this.magicUI.GetComponent<SpriteRenderer>().sprite = this.powerUpSprite;
                break;
            case 3:
                this.magicUI.GetComponent<SpriteRenderer>().sprite = this.rangeUpSprite;
                break;
            case 4:
                this.magicUI.GetComponent<SpriteRenderer>().sprite = this.scoreUpSprite;
                break;
        }
    }

    public void scoreUp()
    {
        //基本スコア上昇量
        int scorePoint;
        //通常ゲーム時
        if (this.gameState < 2)
        {
            scorePoint = (int)(100 * this.playerAbility.scoreUpPoint);
        }else
        //ボーナスゲーム時
        {
            scorePoint = (int)(300 * this.playerAbility.scoreUpPoint);
        }
        //スコアの加算
        for (int i = this.combo; i >= 0; i -= 25)
        {
            if (this.combo >= 100)
            {
                this.score += (int)((scorePoint + (scorePoint * (i / 100))) * this.decisionPoint);
            }
            else
            {
                this.score += scorePoint * this.decisionPoint;
            }
        }
    }

    public void Button()
    {
        if (this.framerate == 15) {
            int highScore = PlayerPrefs.GetInt(this.keyscore1, 0);
            int maxCombo = PlayerPrefs.GetInt(this.keycombo1, 0);
            if (this.score > highScore) {
                this.highscoreLabel.SetActive(true);
                PlayerPrefs.SetInt(this.keyscore1, this.score);
            }
            //最大コンボより現在コンボが高い時
            if (this.gamemaxcombo >= maxCombo)
            {
                //最大コンボを保存
                PlayerPrefs.SetInt(keycombo1, this.gamemaxcombo);
            }
        }
        else if(this.framerate == 10)
        {
            int highScore = PlayerPrefs.GetInt(this.keyscore2, 0);
            int maxCombo = PlayerPrefs.GetInt(this.keycombo2, 0);
            if (this.score > highScore)
            {
                this.highscoreLabel.SetActive(true);
                PlayerPrefs.SetInt(this.keyscore2, this.score);
            }
            //最大コンボより現在コンボが高い時
            if (this.gamemaxcombo >= maxCombo)
            {
                //最大コンボを保存
                PlayerPrefs.SetInt(keycombo2, this.gamemaxcombo);
            }
        }
        else
        {
            int highScore = PlayerPrefs.GetInt(this.keyscore3, 0);
            int maxCombo = PlayerPrefs.GetInt(this.keycombo3, 0);
            if (this.score > highScore)
            {
                this.highscoreLabel.SetActive(true);
                PlayerPrefs.SetInt(this.keyscore3, this.score);
            }
            //最大コンボより現在コンボが高い時
            if (this.gamemaxcombo >= maxCombo)
            {
                //最大コンボを保存
                PlayerPrefs.SetInt(keycombo3, gamemaxcombo);
            }
        }
        if (this.full2)
        {
            this.fullcomboLabel.SetActive(true);
        }
        //効果音再生
        this.GetComponent<MainBGM>().nextSound();
        //最大コンボを表示
		this.ScoreText1.GetComponent<Text> ().text = this.score.ToString ();
		//最大コンボを表示
		this.ComboText1.GetComponent<Text> ().text = this.gamemaxcombo.ToString ()
			+ "\n" + this.perfectCount.ToString ()
			+ "\n" + this.greatCount.ToString ()
			+ "\n" + this.badCount.ToString ()
			+ "\n" + this.missCount.ToString ()
			+ "\n" + this.guardCount.ToString ()
			+ "\n" + this.slashCount.ToString ();
        //テキスト表示
        this.ScoreText.enabled = true;
		this.ScoreText1.enabled = true;
		this.ComboText.enabled = true;
		this.ComboText1.enabled = true;
        Destroy(this.SubdueSuccessText);
        Destroy(this.SubjugationFailureText);
        Destroy(this.button);
        ReturnButton.SetActive(true);
        this.buttoncount = 1;
    }
}
