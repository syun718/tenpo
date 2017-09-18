using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {
    //サウンド
    public AudioClip buttonTap1;
    public AudioClip buttonTap2;
    public AudioClip changePaper;
    public AudioClip backToTitle;
    public AudioClip goBattle;
    //ゲームオブジェクトの格納
    private Button button1;
    private Button button2;
    private Button button3;
    private Button button4;
    [SerializeField]
    private GameObject highText;
    [SerializeField]
    private GameObject comboText;
    private GameObject enemyInformation;
    private GameObject enemy;
    [SerializeField]
    private GameObject note;
    [SerializeField]
    private GameObject longnote;
    [SerializeField]
    private GameObject follower;
    [SerializeField]
    private GameObject note1;
    [SerializeField]
    private GameObject longnote1;
    [SerializeField]
    private GameObject follower1;
    [SerializeField]
    private GameObject note2;
    [SerializeField]
    private GameObject longnote2;
    [SerializeField]
    private GameObject follower2;
    [SerializeField]
    private GameObject note3;
    [SerializeField]
    private GameObject longnote3;
    [SerializeField]
    private GameObject follower3;
    public GameObject finalAlert;
    public GameObject panel;
    private GameObject alertCanvas;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject heroine;
    [SerializeField]
    private GameObject fairy;
    //ボタン箱
    private Button[] button = new Button[4];
    private Button[] buttonCopy = new Button[4];
    //ボタンの定位置箱
    private Vector3[] buttonPos = new Vector3[4];
    //タッチ開始Y座標
    private float startPosY;
    //押したボタンの番号取得
    private int pushButtonNum;
    //最終警告が出ているかどうか
    private bool alertSwitch;
    //音の大きさのフェードアウト
    private bool volumeFade;
    //音の大きさ
    private float volume = 1;
    private bool once = true;
    private int highScore; //ハイスコア計算用変数 
    //スコアの保存先キー
    private string keyscore1 = "SCORE1";
    private string keyscore2 = "SCORE2";
    private string keyscore3 = "SCORE3";
    private int combo; //最大コンボ計算用変数 
    //最大コンボの保存先キー
    private string keycombo1 = "Maxcombo1";
    private string keycombo2 = "Maxcombo2";
    private string keycombo3 = "Maxcombo3";
    //敵のイラスト
    public Sprite enemy1;
    public Sprite enemy2;
    public Sprite enemy3;
    void Start() {
        CameraFade.StartAlphaFade(Color.black, true, 1.5f, 0);
        //ゲームオブジェクトStage1を探す
        this.button1 = GameObject.Find("Stage1").GetComponent<Button>();
        //ゲームオブジェクトStage2を探す
        this.button2 = GameObject.Find("Stage2").GetComponent<Button>();
        //ゲームオブジェクトStage3を探す
        this.button3 = GameObject.Find("Stage3").GetComponent<Button>();
        //ゲームオブジェクトStage4を探す
        this.button4 = GameObject.Find("Stage4").GetComponent<Button>();
        //ゲームオブジェクトEnemyNameを探す
        this.enemy = GameObject.Find("Enemy");
        //ゲームオブジェクトEnemyを探す
        this.enemyInformation = GameObject.Find("EnemyInformation");
        //ボタンの格納
        this.button[0] = this.button1;
        this.button[1] = this.button2;
        this.button[2] = this.button3;
        this.button[3] = this.button4;
        this.buttonCopy[0] = this.button1;
        this.buttonCopy[1] = this.button2;
        this.buttonCopy[2] = this.button3;
        this.buttonCopy[3] = this.button4;
        //定位置保存
        for (int i = 0;i < this.button.Length; i++)
        {
            this.buttonPos[i] = this.button[i].transform.position;
        }
    }
	
	void Update () {
        //タッチ開始座標取得
        if (Input.GetMouseButtonDown(0))
        {
            this.startPosY = Input.mousePosition.y;
        }
        //タッチ終了座標取得
        if (Input.GetMouseButtonUp(0))
        {
            float endPosY = Input.mousePosition.y;
            //下フリック
            if (endPosY - this.startPosY < -50)
            {
                this.GetComponent<AudioSource>().PlayOneShot(this.changePaper);
                this.changeDown();
            }
            //上フリック
            else if (endPosY - this.startPosY > 50)
            {
                this.GetComponent<AudioSource>().PlayOneShot(this.changePaper);
                this.changeUp();
            }
        }
        //ボタンの大きさ変更、ボタンの移動、敵情報の更新
        for (int i = 0; i < this.button.Length; i++)
        {
            if (i != 1)
            {
                this.button[i].transform.localScale = new Vector2(2.5f, 2.5f);
                this.button[i].interactable = false;
            }
            else
            {
                if (this.once)
                {
                    this.button[i].transform.localScale = new Vector2(5.0f, 5.0f);
                    if (this.button[1] == this.button1)
                    {
                        this.enemy.transform.localScale = new Vector3(0.3f, 0.3f, 0);
                        this.enemy.GetComponent<SpriteRenderer>().sprite = this.enemy1;
                        this.note = Instantiate(this.note1) as GameObject;
                        this.longnote = Instantiate(this.longnote1) as GameObject;
                        this.follower = Instantiate(this.follower1) as GameObject;
                        this.enemyInformation.GetComponent<Text>().fontSize = 45;
                        this.enemyInformation.GetComponent<Text>().text = "ノーツ        ロング          チビ";
                        //ハイスコアを表示
                        this.highScore = PlayerPrefs.GetInt(keyscore1, 0);
                        this.highText.GetComponent<Text>().text = "HighScore: " + highScore.ToString();
                        //最大コンボ数を表示
                        this.combo = PlayerPrefs.GetInt(keycombo1, 0);
                        this.comboText.GetComponent<Text>().text = "MaxCombo: " + this.combo.ToString();
                    }
                    else if (this.button[1] == this.button2)
                    {
                        this.enemy.transform.localScale = new Vector3(0.15f, 0.15f, 0);
                        this.note = Instantiate(this.note2) as GameObject;
                        this.longnote = Instantiate(this.longnote2) as GameObject;
                        this.follower = Instantiate(this.follower2) as GameObject;
                        this.enemy.GetComponent<SpriteRenderer>().sprite = this.enemy2;
                        this.enemyInformation.GetComponent<Text>().fontSize = 45;
                        this.enemyInformation.GetComponent<Text>().text = "ノーツ        ロング          チビ";
                        //ハイスコアを表示
                        this.highScore = PlayerPrefs.GetInt(keyscore2, 0);
                        this.highText.GetComponent<Text>().text = "HighScore: " + highScore.ToString();
                        //最大コンボ数を表示
                        this.combo = PlayerPrefs.GetInt(keycombo2, 0);
                        this.comboText.GetComponent<Text>().text = "MaxCombo: " + this.combo.ToString();
                    }
                    else if (this.button[1] == this.button3)
                    {
                        this.enemy.transform.localScale = new Vector3(0.15f, 0.15f, 0);
                        this.note = Instantiate(this.note3) as GameObject;
                        this.longnote = Instantiate(this.longnote3) as GameObject;
                        this.follower = Instantiate(this.follower3) as GameObject;
                        this.enemy.GetComponent<SpriteRenderer>().sprite = this.enemy3;
                        this.enemyInformation.GetComponent<Text>().fontSize = 45;
                        this.enemyInformation.GetComponent<Text>().text = "ノーツ        ロング          チビ";
                        //ハイスコアを表示
                        this.highScore = PlayerPrefs.GetInt(keyscore3, 0);
                        this.highText.GetComponent<Text>().text = "HighScore: " + this.highScore.ToString();
                        //最大コンボ数を表示
                        this.combo = PlayerPrefs.GetInt(keycombo3, 0);
                        this.comboText.GetComponent<Text>().text = "MaxCombo: " + this.combo.ToString();
                    }
                    else
                    {
                        this.enemy.GetComponent<SpriteRenderer>().sprite = null;
                        this.enemyInformation.GetComponent<Text>().fontSize = 60;
                        this.enemyInformation.GetComponent<Text>().text = "タイトルへ戻る";
                        this.highText.GetComponent<Text>().text = "";
                        this.comboText.GetComponent<Text>().text = "";
                    }
                    this.note.transform.position = new Vector3(-1.8f, -0.7f, 0);
                    this.longnote.transform.position = new Vector3(0.2f, -0.6f, 0);
                    this.follower.transform.position = new Vector3(2.2f, -0.7f, 0);
                    this.once = false;
                }
                this.button[i].interactable = true;
            }
            this.button[i].transform.position = Vector3.Lerp(this.button[i].transform.position, this.buttonPos[i], Time.deltaTime * 5);
        }
        //音の大きさをフェードアウト
        this.GetComponent<AudioSource>().volume *= this.volume;
        if (this.volumeFade)
        {
            this.volume *= 0.995f;
        }
    }

    void changeDown()
    {
        Destroy(this.note.gameObject);
        Destroy(this.longnote.gameObject);
        Destroy(this.follower.gameObject);
        if (this.alertSwitch == false)
        {
            for (int i = 0; i < this.button.Length; i++)
            {
                if (i != 0)
                {
                    this.buttonCopy[i] = this.button[i - 1];
                }
                else
                {
                    this.buttonCopy[0] = this.button[this.button.Length - 1];
                }
            }
            this.buttonCopy.CopyTo(this.button, 0);
            this.once = true;
        }
    }

    void changeUp()
    {
        Destroy(this.note.gameObject);
        Destroy(this.longnote.gameObject);
        Destroy(this.follower.gameObject);
        if (this.alertSwitch == false)
        {
            for (int i = 0; i < this.button.Length; i++)
            {
                if (i != this.button.Length - 1)
                {
                    this.buttonCopy[i] = this.button[i + 1];
                }
                else
                {
                    this.buttonCopy[this.button.Length - 1] = this.button[0];
                }
            }
            this.buttonCopy.CopyTo(this.button, 0);
            this.once = true;
        }
    }

    public void alertOn()
    {
        Debug.Log(1);
        if(button[1] == this.button1)
        {
            this.pushButtonNum = 1;
        }
        else if(button[1] == this.button2)
        {
            this.pushButtonNum = 2;
        }
        else if (button[1] == this.button3)
        {
            this.pushButtonNum = 3;
        }
        this.GetComponent<AudioSource>().PlayOneShot(this.buttonTap1);
        //最終警告生成
        if (this.alertSwitch == false)
        {
            this.alertCanvas = Instantiate(this.finalAlert) as GameObject;
            Button yesButton = GameObject.Find("YesButton").GetComponent<Button>();
            Button noButton = GameObject.Find("NoButton").GetComponent<Button>();
            yesButton.onClick.AddListener(LoadGameScene);
            noButton.onClick.AddListener(destroyAlert);
            this.alertSwitch = true;
        }
    }

    public void destroyAlert()
    {
        this.player.GetComponent<SelectPlayer>().noMotion();
        this.heroine.GetComponent<SelectHeroine>().noMotion();
        this.fairy.GetComponent<SelectFairy>().noMotion();
        //効果音の再生
        this.GetComponent<AudioSource>().PlayOneShot(this.buttonTap2);
        //最終警告の破棄
        this.alertSwitch = false;
        Destroy(this.alertCanvas);
    }

    public void LoadGameScene()
    {
        //効果音の再生
        this.GetComponent<AudioSource>().PlayOneShot(this.buttonTap1);
        Invoke("fadeOn", 2.0f);
        Invoke("goSound", 0.5f);
        //パネルを表示
        GameObject panelCanvas = Instantiate(this.panel) as GameObject;
        //押したボタンによってシーンが変わる
        /*
        switch (this.pushButtonNum) {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
        }
        */
        //GameSceneをロード
        switch (this.pushButtonNum) {
            case 1:
                CameraFade.StartAlphaFade(Color.white, false, 2.0f, 2.0f, () => { Application.LoadLevel("GameScene"); });
                break;
            case 2:
                CameraFade.StartAlphaFade(Color.white, false, 2.0f, 2.0f, () => { Application.LoadLevel("GameScene2"); });
                break;
            case 3:
                CameraFade.StartAlphaFade(Color.white, false, 2.0f, 2.0f, () => { Application.LoadLevel("GameScene3"); });
                break;
        }
        
    }

    public void TitleGameScene()
    {
        //効果音の再生
        this.GetComponent<AudioSource>().PlayOneShot(this.backToTitle);
        Invoke("fadeOn", 0.1f);
        //パネルを表示
        GameObject panelCanvas = Instantiate(this.panel) as GameObject;
        //TitleSceneをロード
        CameraFade.StartAlphaFade(Color.black, false, 0.5f, 0.5f, () => { Application.LoadLevel("TitleScene"); });
    }

    private void fadeOn()
    {
        this.volumeFade = true;
    }
    private void goSound()
    {
        //効果音の再生
        this.GetComponent<AudioSource>().PlayOneShot(this.goBattle);
    }
}
