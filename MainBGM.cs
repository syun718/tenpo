using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBGM : MonoBehaviour {
    //BGM
    public AudioClip mainBGM;
    //SE
    public AudioClip perfectTap;
    public AudioClip greatTap;
    public AudioClip badTap;
    public AudioClip clear;
    public AudioClip fail;
    public AudioClip next;
    public AudioClip assist;
    public AudioClip cutin;
    public AudioClip specialSword;
    public AudioClip specialBom;
    public AudioClip specialBigBom;
    //AudioSource
    private AudioSource[] mainAudio;
    //BGMを再生するまでの時間
    public float delayTime;
    //結果再生を1度だけに
    private bool result = true;
    void Start () {
        //AudioSourceをコンポネント
        this.mainAudio = GetComponents<AudioSource>();
        //再生する曲の決定
        this.mainAudio[0].clip = this.mainBGM;
        this.mainAudio[0].PlayDelayed(this.delayTime);
    }
    public void perfectTapSound()
    {
        this.mainAudio[1].clip = this.perfectTap;
        this.mainAudio[1].Play();
    }
    public void greatTapSound()
    {
        this.mainAudio[1].clip = this.greatTap;
        this.mainAudio[1].Play();
    }
    public void badTapSound()
    {
        this.mainAudio[1].clip = this.badTap;
        this.mainAudio[1].Play();
    }
    public void assistFinished()
    {
        this.mainAudio[1].PlayOneShot(this.assist);
    }
    public void cutIn()
    {
        this.mainAudio[0].PlayOneShot(this.cutin);
    }
    public void spesialAttack1()
    {
        this.mainAudio[0].PlayOneShot(this.specialSword);
    }
    public void spesialAttack2()
    {
        this.mainAudio[0].PlayOneShot(this.specialBom);
    }
    public void spesialAttack3()
    {
        this.mainAudio[0].PlayOneShot(this.specialBigBom);
    }
    public void clearSound()
    {
        if (this.result)
        {
            this.mainAudio[1].PlayOneShot(this.clear);
        }
        this.result = false;
    }
    public void failSound()
    {
        if (this.result)
        {
            this.mainAudio[1].PlayOneShot(this.fail);
        }
        this.result = false;
    }
    public void nextSound()
    {
        this.mainAudio[1].PlayOneShot(this.next);
    }

    public void pause()
    {
        this.mainAudio[0].Pause();
    }
}
