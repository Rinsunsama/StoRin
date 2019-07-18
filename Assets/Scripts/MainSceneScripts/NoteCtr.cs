using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteCtr : MonoBehaviour
{
    public int track;
    public string noteColor;
    public bool isPass;
    public bool isRight;
    // Update is called once per frame
    private void Start()
    {
        isPass = false;
        isRight = false;
    }
    void Update()
    {
        DestroyEffect();
    }

    //初始化关口信息
    public void Initializat()
    {
      
    }
    //销毁关口特效
    public void DestroyEffect()
    {
        foreach (ParticleSystem ps in GetComponentsInChildren<ParticleSystem>())
        {
            if (!ps.isStopped)
            {
                GameObject.Destroy(ps.gameObject, 1.0f);
            }
        }

    }


}
