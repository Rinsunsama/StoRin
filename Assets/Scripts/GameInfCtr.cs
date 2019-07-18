using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class GameInfCtr : MonoBehaviour {

    public static int maxMapIndex;   //当前最大关卡（通关进度）
    // Use this for initialization
    void Start () {
         maxMapIndex= PlayerPrefs.GetInt("maxMapIndex");
        if(maxMapIndex==0)
        {
            PlayerPrefs.SetInt("maxMapIndex", 1);
            maxMapIndex = PlayerPrefs.GetInt("maxMapIndex");
        }
    //    Debug.Log("最大关卡是" + maxMapIndex);
    }
	
}
