using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class AutoTweenCtr : MonoBehaviour {

    public CanvasGroup touchToStartCg;
    public Image startPanelBG;
    public CanvasGroup gameNameDownCg;
	// Use this for initialization
	void Start () {
        Screen.SetResolution(600, 1080, true);
        Tweener tweener = DOTween.To(() => touchToStartCg.alpha, x => touchToStartCg.alpha = x, 0,3.0f).SetLoops(-1, LoopType.Yoyo);
        Tweener tweener2 =DOTween.To(() => gameNameDownCg.alpha, x => gameNameDownCg.alpha= x, 0.1f, 4.0f).SetLoops(-1, LoopType.Yoyo);
    //    Debug.Log(startPanelBG.color.ToString());
        startPanelBG.DOColor(new Color(0 , 18.0f/255, 30.0f/255 , 1.0f), 4.0f).SetLoops(-1, LoopType.Yoyo);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
