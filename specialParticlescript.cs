using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class specialParticlescript : MonoBehaviour {
    GameObject gameDirector;
    GameDirector g;
    public bool spesialAttack_type = false; //falseで魔法陣
                                            //trueでライン
    // Use this for initialization
    void Start () { 
        //ゲームオブジェクトGameDirectorを探す
        this.gameDirector = GameObject.Find("GameDirector");
        this.g = this.gameDirector.GetComponent<GameDirector>();
    }

    void particleDestroy()
    {
        Destroy(this.gameObject);
    }

    void Update () {
        //パーティクルラインが必殺技不発で消える処理
        if ((this.spesialAttack_type == true) && (g.specialState_count != 111 && g.specialState_count >= 0 && g.specialState == 0)) {
            Destroy(this.gameObject);
        }
        if(this.g.note_count >= this.g.max_num / 3 - 1)
        {
            Destroy(this.gameObject);
        }
        if(this.g.gameState != 0)
        {
            Destroy(this.gameObject);
        }
        //魔法陣とパーティクルラインが必殺技発動で消える処理
        if (g.specialState_count == 111)
        {
            Invoke("particleDestroy",1);
        }
    }
}
