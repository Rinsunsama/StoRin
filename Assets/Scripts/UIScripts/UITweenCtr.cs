using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class UITweenCtr : MonoBehaviour
{
    private RectTransform rectTransform;
    private CanvasGroup cg;            //面板Canvas Group组件，用来改变透明度
    public Vector3 toPos;              //动画结束的位置
    public Vector3 firstPos;           //

    private void Awake()
    {
        firstPos = transform.position;
        cg = this.transform.GetComponent<CanvasGroup>();
        rectTransform = this.transform.GetComponent<RectTransform>();
    }
    //显示动画
    public void Show(float time)
    {
        this.gameObject.SetActive(true);
        Tweener tween = DOTween.To(() => cg.alpha, x => cg.alpha = x, 1.0f, time);//用1秒的时间从0变成1.
        tween.SetUpdate(true);
        if(cg)
        cg.blocksRaycasts = true;//可以和该UI对象交互  
    }
    //隐藏动画
    public void Hide(float time)
    {
        Tweener tween = DOTween.To(() => cg.alpha, x => cg.alpha = x, 0.0f, time);//用1秒的时间从1变成0.
        tween.SetUpdate(true);
        cg.blocksRaycasts = false;//不可以和该UI对象交互  
    }
    //位移动画
    public void PosTween(Vector3 toPos,float time,bool isHide)
    {
        if (!isHide)
        {
            Tweener tweener = transform.DOLocalMove(toPos, time);
        }
        else
        {
            Tweener tweener = transform.DOMove(toPos, time);
            tweener.OnComplete(HideImage);
            
        }
        
    }
    public void ScaleTween(float scaleTo,float time,bool isHide)
    {
        Tweener tween = transform.DOScale(new Vector3(scaleTo, scaleTo, scaleTo), time);//用1秒的时间从1变成0.
        if (isHide)
        {
            tween.OnComplete(HideImage);
            
        }
    }

    public void HideImage()
    {
        Tweener tween = DOTween.To(() => cg.alpha, x => cg.alpha = x, 0.0f, 3.0f);//用1秒的时间从1变成0.
        tween.SetUpdate(true);
    }
}