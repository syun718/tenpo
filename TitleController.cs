using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour {
    //サウンド
    public AudioClip tap;
    //GameObjectの格納
    private GameObject start;
    //音の大きさのフェードアウト
    private bool volumeFade;
    //音の大きさ
    private float volume = 1;
    //ロゴの点滅のための計測時間
    private float time = 0;
    private bool go = true;
    void Start()
    {
        //フェードイン
        CameraFade.StartAlphaFade(Color.black, true, 1.0f, 0.5f);
        //ゲームオブジェクトGameStartを探す
        this.start = GameObject.Find("GameStart");
    }

    private void goSelect()
    {
        this.volumeFade = true;
    }

    void Update () {
        this.time += 1;
        if (this.time <= 30)
        {
            this.start.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        else if(this.time > 30 && this.time <= 60)        {
            this.start.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0);
        }
        else if(this.time > 60)
        {
            this.time = 0;
        }
        //音の大きさをフェードアウト
        this.GetComponent<AudioSource>().volume *= this.volume;
        if (this.volumeFade)
        {
            this.volume *= 0.995f;
        }
        if(this.volume < 0.01)
        {
            this.volume = 0;
        }
        if (this.go)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //効果音再生
                this.GetComponent<AudioSource>().PlayOneShot(this.tap);
                //SelectSceneをロード   
                CameraFade.StartAlphaFade(Color.black, false, 1.5f, 1.5f, () => { Application.LoadLevel("SelectScene"); });
                Invoke("goSelect", 1.5f);
                this.go = false;
            }
        }
    }
}
