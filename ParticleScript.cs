using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour {
    public float time;
    void Start () {
        //this.GetComponent<ParticleSystem>().Play();
        //3秒後パーティクルを破壊
        Invoke("particleDestroy", this.time);
	}
	
    void particleDestroy()
    {
        //破壊
        Destroy(this.gameObject);
    }

	void Update () {
		
	}
}
