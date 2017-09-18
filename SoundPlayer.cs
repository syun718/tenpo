using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour {
    //SE
    public AudioClip guard; //player
    public AudioClip sword; //player
    public AudioClip playerDamaged; //player
    public AudioClip enemyDamaged; //enemy
    public AudioClip enemyDamaged2; //enemy
    public AudioClip enemyDamaged3; //enemy
    public AudioClip enemyDestroy; //enemy
    public AudioClip enemyEscapeSound; //enemy
    public AudioClip cure; //heroine
    public AudioClip powerUp; //heroine
    public AudioClip rangeUp; //heroine
    public AudioClip scorePointUp; //heroine
    private AudioSource player;
    private AudioSource heroine;
    private AudioSource enemy;
	void Start () {
        //AudioSourceをコンポネント
        this.player = GetComponent<AudioSource>();
        this.heroine = GetComponent<AudioSource>();
        this.enemy = GetComponent<AudioSource>();
	}
    public void guardSound()
    {
        this.player.PlayOneShot(this.guard);
    }
    public void playerDamagedSound()
    {
        this.player.PlayOneShot(this.playerDamaged);
    }
    public void swordSound()
    {
        this.player.PlayOneShot(this.sword);
    }
    public void enemyDamagedSound()
    {
        int num = Random.Range(0, 3);
        if(num == 0)

        {
            this.enemy.PlayOneShot(this.enemyDamaged);
        }
        else if(num == 1)
        {
            this.enemy.PlayOneShot(this.enemyDamaged2);
        }
        else
        {
            this.enemy.PlayOneShot(this.enemyDamaged3);
        }
    }
    public void enemyDestroySound()
    {
        this.enemy.PlayOneShot(this.enemyDestroy);
    }
    public void enemyEscape()
    {
        this.enemy.PlayOneShot(this.enemyEscapeSound);
    }
    public void cureSound()
    {
        this.heroine.PlayOneShot(this.cure);
    }
    public void powerUpSound()
    {
        this.heroine.PlayOneShot(this.powerUp);
    }
    public void rangeUpSound()
    {
        this.heroine.PlayOneShot(this.rangeUp);
    }
    public void scorePointUpSound()
    {
        this.heroine.PlayOneShot(this.scorePointUp);
    }
}
