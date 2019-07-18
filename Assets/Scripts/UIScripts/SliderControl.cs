using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;
public class SliderControl : MonoBehaviour
{

    public Scrollbar m_Scrollbar;
    public ScrollRect m_ScrollRect;
    public Scrollbar b_Scrollbar;
    public ScrollRect b_ScrollRect;
    public Transform[] mapCDs;
    public Transform[] mapButtons;
    public Sprite[] GS_CDCovers;
    public AudioClip[] music;
    public Image panelBG;
    public AudioSource audio;
    public Text songNameText;
    public Scrollbar musicProcess;
    public Font ChineseFont;
    public Font EnglishFont;
    private float mTargetValue;

    private bool mNeedMove = false;

    private const float MOVE_SPEED = 1F;

    private const float SMOOTH_TIME = 0.2F;

    private float mMoveSpeed = 0f;

    public int selectMapIndex;
    public void OnPointerDown()
    {
        mNeedMove = false;
    }

    public void OnMapEnter()
    {
     //   print("抬起！！！！");
        UIManager.loadName = "1";     //获取关卡名
        GameCtr.nowMapIndex = GetComponent<SliderControl>().selectMapIndex;
        Application.LoadLevel("loading");       //加载“加载等待界面”
    }

    public void OnPointerUp()
    {
        float t = 1.0f/ (mapCDs.Length - 1);
        int x = (int)((mapCDs.Length - 1)*m_Scrollbar.value+0.5f);
        mTargetValue = t * x;
        mapButtons[selectMapIndex - 1].GetComponent<UITweenCtr>().ScaleTween(1, 0.2f,false);
        int toMapIndex = (int)(mTargetValue * (mapCDs.Length - 1)) + 1;
        if (selectMapIndex!= toMapIndex)
        { 
            selectMapIndex = toMapIndex;
            audio.clip = music[toMapIndex-1];
            audio.Play();
            

        }
        mapButtons[selectMapIndex - 1].GetComponent<UITweenCtr>().ScaleTween(2, 0.2f, false);
        // 判断当前位于哪个区间，设置自动滑动至的位置

        mNeedMove = true;
        mMoveSpeed = 0;
    }

    public void OnButtonClick(int value)
    {
        mTargetValue = 1.0f / (mapCDs.Length - 1) * (value - 1);
        mapButtons[selectMapIndex - 1].GetComponent<UITweenCtr>().ScaleTween(1, 0.2f, false);
 
        if(selectMapIndex!=value)
        {
            selectMapIndex = value;
            audio.clip = music[selectMapIndex - 1];
            audio.Play();
        }
      
        mapButtons[selectMapIndex - 1].GetComponent<UITweenCtr>().ScaleTween(2, 0.2f, false);

        mNeedMove = true;
    }

    private void Awake()
    {
        selectMapIndex = 1;
        audio.clip = music[0];
        audio.Play();
        mapButtons[0].GetComponent<UITweenCtr>().ScaleTween(2, 0.1f, false);
    }
    void Update()
    {
        if (mNeedMove)
        {
            if (Mathf.Abs(m_Scrollbar.value - mTargetValue) < 0.01f)
            {
                m_Scrollbar.value = mTargetValue;
                b_Scrollbar.value = mTargetValue;
                int t = (int)(mTargetValue *(mapCDs.Length - 1));
             //   print(t);
                panelBG.sprite = GS_CDCovers[t];

                songNameText.text = audio.clip.name;
                Regex regChina = new Regex("^[^\x00-\xFF]");
                if (regChina.IsMatch(music[selectMapIndex - 1].name))
                {
                    songNameText.font = ChineseFont;
                }
                else
                {
                    songNameText.font = EnglishFont;
                }
                mNeedMove = false;
                return;
            }
            m_Scrollbar.value = Mathf.SmoothDamp(m_Scrollbar.value, mTargetValue, ref mMoveSpeed, SMOOTH_TIME);
            b_Scrollbar.value = Mathf.SmoothDamp(m_Scrollbar.value, mTargetValue, ref mMoveSpeed, SMOOTH_TIME);
        }

        musicProcess.value = audio.time / audio.clip.length;
    }

}