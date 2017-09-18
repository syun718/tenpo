using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class ButtonScript : MonoBehaviour {
    private bool once = true; 
	// Use this for initialization
	void Start () {
		
	}

    public void LoadGameScene()
    {
        //fadein
        //StartCoroutine("fadeIn");
        //GameSceneをロード
        SceneManager.LoadScene("GameScene");
    }

    public void TitleGameScene()
    {
        if (this.once)
        {
            GameObject.Find("GameDirector").GetComponent<MainBGM>().nextSound();
            //TitleSceneをロード
            CameraFade.StartAlphaFade(Color.black, false, 0.5f, 0.5f, () => { Application.LoadLevel("TitleScene"); });
            this.once = false;
        }
    }

    public void SelectScene()
    {
        //SelectSceneをロード
        CameraFade.StartAlphaFade(Color.black, false, 1.5f, 1.0f, () => { Application.LoadLevel("SelectScene"); });
    }
    public void touch()
    {
        //Canvas中のtestスクリプトの中のメソッドにアクセス
        GameObject.Find("GameDirector").GetComponent<GameDirector>().Button();
    }
}
