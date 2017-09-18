using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEffect : MonoBehaviour {
    public bool touch = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (touch)
        {
            //getmouse
            if (Input.GetMouseButton(0))
            {
                Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 1.0f);

                gameObject.transform.position = Camera.main.ScreenToWorldPoint(screenPosition);
            }
            //不具合がでたらPrefabの生成に切り替え
            //touch
            foreach (Touch t in Input.touches)
            {
                //タップした座標取得
                Vector3 screenPosition = new Vector3(t.position.x, t.position.x, Camera.main.nearClipPlane + 1.0f);
                //このオブジェクトを動かす
                gameObject.transform.position = Camera.main.ScreenToWorldPoint(screenPosition);
            }
            
        }
        
    }
}
