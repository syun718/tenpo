using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {
    //ゲームオブジェクトの格納
	GameObject gameDirector;
    //GameDirectorに参照
	GameDirector g;
    //ゲームオブジェクトにPrefabを格納
    public GameObject notePrefab;
	public GameObject longNotePrefab;
    public GameObject followerPrefab;
    public GameObject note_bonusPrefab;
    public GameObject longNote_bonusPrefab;
    public GameObject perfectParticlePrefab;
    public GameObject greatParticlePrefab;
    public GameObject badParticlePrefab;
    public GameObject missParticlePrefab;
    public GameObject bonusTapParticlePrefab;
    public GameObject EnemyBreakParticlePrefab;
    public GameObject GuardPrefab;
    public GameObject GuardParticle;
    public GameObject MagicSquareParticle;
    public GameObject SPline1Particle;
    public GameObject SPline2Particle;
    public GameObject SPline3Particle;
    public GameObject bomParticle;
    public GameObject BurstParticle;
    public GameObject fairy;
    public GameObject fairyPrefab;
    internal GameObject EnemyBreakParticle;
    public GameObject powerUpParticlePrefab;
    public GameObject rangeUpParticlePrefab;
    public GameObject scorePointUpParticlePrefab;
    public GameObject cureParticlePrefab;
    public GameObject resultPaper;
    public GameObject specialAttack;
    public void SpawnNote()
    {
        //ゲームオブジェクトGameDirectorを探す
        this.gameDirector = GameObject.Find("GameDirector");
        //変数pにGameDirectorスクリプトをコンポーネント
        this.g = this.gameDirector.GetComponent<GameDirector>();
        //ノーツの種類を取得して、prefabを変更する
        int note_type = this.g.note_type;
        //GameDirectorのノーツの数を増やす
        if (note_type != 3)
        {
            this.g.noteNum += 1;
        }
        //初期ｘ座標位置
        float px = 0;
        //ゲームオブジェクトnote
        GameObject note = null;
        //ノートの種類によって生成するPrefabを変える
        switch (note_type){
            case 1:
                //ボーナスゲーム中ならnote_bonusPrefabを生成
                if (g.gameState >= 2)
                {
                    note = Instantiate(this.note_bonusPrefab) as GameObject;
                }
                else
                {
                    note = Instantiate(this.notePrefab) as GameObject;
                }
                break;
            case 2:
            case 3:
            case 4:
                if (g.gameState >= 2)
                {
                    note = Instantiate(this.longNote_bonusPrefab) as GameObject;
                }
                else
                {
                    note = Instantiate(this.longNotePrefab) as GameObject;
                }
                break;
            case 5:
                if (g.gameState >= 2)
                {
                    note = Instantiate(this.fairyPrefab) as GameObject;
                }
                else
                {
                    note = Instantiate(this.followerPrefab) as GameObject;
                }
                break;

            default:
                break;
		}
		//ノーツとParticleが生成される場所の指定
		switch(this.g.note_way){
			case 0:
				px = 0;
				break;
			case 1:
				px = -0.2f;
				break;

			case 2:
				px = 0.2f;
				break;
		}
        //生成される場所の指定
        note.transform.position = new Vector3(px, 2.2f, 0);
    }

    public void spawnPerfectParticle()
    {
        
        Vector3 a = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //tapParticleの生成
        if (this.g.gameState < 2)
        {
            GameObject tapParticle = Instantiate(perfectParticlePrefab, new Vector3(a.x, a.y, 0), Quaternion.identity) as GameObject;
        }else
        {
            GameObject tapParticle = Instantiate(bonusTapParticlePrefab, new Vector3(a.x, a.y, 0), Quaternion.identity) as GameObject;
        }
        
        /*touch
        foreach (Touch t in Input.touches)
        {
            Vector3 a = Camera.main.ScreenToWorldPoint(t.position);
            if (this.g.gameState < 2)
            {
                GameObject tapParticle = Instantiate(perfectParticlePrefab, new Vector3(a.x, a.y, 0), Quaternion.identity) as GameObject;
            }
            else
            {
                GameObject tapParticle = Instantiate(bonusTapParticlePrefab, new Vector3(a.x, a.y, 0), Quaternion.identity) as GameObject;
            }
        }
        */
    }

    public void spawnGreatParticle()
    {
        
        Vector3 a = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //tapParticleの生成
        if (this.g.gameState < 2)
        {
            GameObject tapParticle = Instantiate(greatParticlePrefab, new Vector3(a.x, a.y, 0), Quaternion.identity) as GameObject;
        }
        else
        {
            GameObject tapParticle = Instantiate(bonusTapParticlePrefab, new Vector3(a.x, a.y, 0), Quaternion.identity) as GameObject;
        }
        
        /*touch
        foreach (Touch t in Input.touches)
        {
            Vector3 a = Camera.main.ScreenToWorldPoint(t.position);
            if (this.g.gameState < 2)
            {
                GameObject tapParticle = Instantiate(greatParticlePrefab, new Vector3(a.x, a.y, 0), Quaternion.identity) as GameObject;
            }
            else
            {
                GameObject tapParticle = Instantiate(bonusTapParticlePrefab, new Vector3(a.x, a.y, 0), Quaternion.identity) as GameObject;
            }
        }
        */
    }

    public void spawnBadParticle()
    {
        //tapParticleの生成
        GameObject tapParticle = Instantiate(badParticlePrefab) as GameObject;
        Vector3 a = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        /*touch
        foreach (Touch t in Input.touches)
        {
            Vector3 a = Camera.main.ScreenToWorldPoint(t.position);
            tapParticle.transform.position = new Vector3(a.x, a.y, 0);
        }
        */
        tapParticle.transform.position = new Vector3(a.x, a.y, 0);
    }
    public void spawnGuardParticle()
    {
        //tapParticleの生成
        GameObject tapParticle = Instantiate(GuardParticle) as GameObject;
        Vector3 a = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        /*touch
        foreach (Touch t in Input.touches)
        {
            Vector3 a = Camera.main.ScreenToWorldPoint(t.position);
            tapParticle.transform.position = new Vector3(a.x, a.y, 0);
        }
        */
        tapParticle.transform.position = new Vector3(a.x, a.y, 0);
    }

    public void spawnGuardPrefab()
    {
        //tapParticleの生成
        GameObject tapParticle = Instantiate(GuardPrefab) as GameObject;
        Vector3 a = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        /*touch
        foreach (Touch t in Input.touches)
        {
            Vector3 a = Camera.main.ScreenToWorldPoint(t.position);
            tapParticle.transform.position = new Vector3(a.x, a.y, 0);
        }
        */
        tapParticle.transform.position = new Vector3(a.x, a.y, 0);
    }

    public void spawnMagicSquareParticle()
    {
        GameObject tapParticle = Instantiate(MagicSquareParticle) as GameObject;
        tapParticle.transform.position = new Vector3(0, 3, 0);
    }
    public void spawnSPline1Particle()
    {
        GameObject tapParticle = Instantiate(SPline1Particle) as GameObject;
        tapParticle.transform.position = new Vector3(-0.8f, 3, 0);
    }
    public void spawnSPline2Particle()
    {
        GameObject tapParticle = Instantiate(SPline2Particle) as GameObject;
        tapParticle.transform.position = new Vector3(0, 1.8f, 0);
    }
    public void spawnSPline3Particle()
    {
        GameObject tapParticle = Instantiate(SPline3Particle) as GameObject;
        tapParticle.transform.position = new Vector3(0.8f, 3, 0);
    }
    public void spawnBomParticle()
    {
        GameObject tapParticle = Instantiate(bomParticle) as GameObject;
        float randX = Random.Range(-0.5f, 0.5f);
        float randY = Random.Range(2.5f, 3.5f);
        tapParticle.transform.position = new Vector3(randX, randY, 0);
    }
    public void spawnBurstParticle()
    {
        GameObject tapParticle = Instantiate(BurstParticle) as GameObject;
        tapParticle.transform.position = new Vector3(0, 3, 0);
    }

    public void spawnEnemyBreakParticle()
    {
        //EnemyBreakParticleの生成
        this.EnemyBreakParticle = Instantiate(EnemyBreakParticlePrefab) as GameObject;
        //生成される場所の指定
        this.EnemyBreakParticle.transform.position = new Vector3(0, 2.9f, 0);
    }

    public void spawnFairy()
    {
        //fairyPrefabの生成
        GameObject fairy = Instantiate(this.fairy) as GameObject;
        //生成される場所の指定
        fairy.transform.position = new Vector3(0, 2.9f, 0);
    }

    public void spawnFairyFailed()
    {
        //fairyPrefabの生成
        GameObject fairy = Instantiate(this.fairy) as GameObject;
        //生成される場所の指定
        fairy.transform.position = new Vector3(5.0f, 8.0f, 0);
    }

    public void spawnPowerUpParticle()
    {
        //fairyPrefabの生成
        GameObject powerUpParticle = Instantiate(this.powerUpParticlePrefab) as GameObject;
        //生成される場所の指定
        powerUpParticle.transform.position = new Vector3(0, -5.0f, 0);
    }

    public void spawnRangeUpParticle()
    {
        //fairyPrefabの生成
        GameObject rangeUpParticle = Instantiate(this.rangeUpParticlePrefab) as GameObject;
        //生成される場所の指定
        rangeUpParticle.transform.position = new Vector3(0, -4.0f, 0);
    }
    public void spawnScorePointUpParticle()
    {
        //fairyPrefabの生成
        GameObject scorePointUpParticle = Instantiate(this.scorePointUpParticlePrefab) as GameObject;
        //生成される場所の指定
        scorePointUpParticle.transform.position = new Vector3(0, -5.0f, 0);
    }

    public void spawnCureParticle()
    {
        //fairyPrefabの生成
        GameObject cureParticle = Instantiate(this.cureParticlePrefab) as GameObject;
        //生成される場所の指定
        cureParticle.transform.position = new Vector3(0, -3.0f, 0);
    }

    public void spawnSpecialAttack()
    {
        //resultPaperの生成
        GameObject attack = Instantiate(this.specialAttack) as GameObject;
    }

    public void spawnResult()
    {
        //resultPaperの生成
        GameObject paper = Instantiate(this.resultPaper) as GameObject;
    }
}
