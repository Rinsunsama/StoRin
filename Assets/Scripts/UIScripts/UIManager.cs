using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public static string loadName;
    public GameObject gameStartPanel;
    public GameObject mapChoicePanel;
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    private GameObject gameNameText;
    private GameObject me;
    public Button[] mapButtons;
    public Image VolumeButtonSprite;
    private bool VolumeIsOpen = true;
    public Transform[] effects;
    public Text[] speedModeTexts;
    // Use this for initialization
    void Awake () {
        
        gameNameText = GameObject.Find("GameName_text");
        if(gameStartPanel)
        gameStartPanel.GetComponent<UITweenCtr>().Show(2.5f);
        if(gameNameText)
        gameNameText.GetComponent<UITweenCtr>().Show(5.0f);
        if(GameObject.Find("me"))
        {
            me = GameObject.Find("me");
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
   //点击开始按钮->切换到选择关卡面板
    public void OnGameStartButtonClick()
    {
        gameStartPanel.GetComponent<UITweenCtr>().Hide(2.0f);
        mapChoicePanel.gameObject.SetActive(true);
        mapChoicePanel.GetComponent<UITweenCtr>().Show(4.0f);
        OnSpeedButtonClick(GameCtr.speedMode);
    }

    //暂停按钮
    public void OnPauseButtonClick()
    {
        pausePanel.SetActive(true);
        pausePanel.GetComponent<UITweenCtr>().Show(1);
        Time.timeScale = 0;  //暂停游戏 
        GameObject.Find("audioCtr").GetComponent<AudioSource>().Pause();
    }

    //返回关卡选择按钮
    public void OnGoToMapChoiceButtonClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("0");
    }

    public void RestartButtonClick()
    {
        Time.timeScale = 1;
        Application.LoadLevel("loading");
    }
    //声量按钮
    public void OnVoluemButtonClick()
    {
        //关闭声音或者开启声音
        if(VolumeIsOpen)
        {
            //关闭声音
            //TODO
            //更改声音图标贴图
            VolumeButtonSprite.sprite = Resources.Load("volumeClose", typeof(Sprite)) as Sprite;
            VolumeIsOpen = false;
        }
        else
        {
            //打开声音
            //TODO
            //更换贴图
            VolumeButtonSprite.sprite = Resources.Load("volumeOpen", typeof(Sprite)) as Sprite;
            VolumeIsOpen = true;
        }
    }

    public void OnTrackButtonClick(int index)
    {
       
        me.SendMessage("OnNoteButtonClick", index);
    }

    public void OnTrackButtonDown(int index)
    {
        
        me.GetComponent<PlayerCtr>().OnTrackButtonDown(index);
       
    }

    public void OnTrackButtonUp(int index)
    {
        
        me.GetComponent<PlayerCtr>().OnTrackButtonUp(index);
        
    }

    public void OnGameModeButtonClick()
    {
        if (GameCtr.gameMode == 6)
        { 
            GameCtr.gameMode = 4;
            GameObject.Find("gameModeButton").GetComponentInChildren<Text>().text = "4";
        }
        else if (GameCtr.gameMode == 4)
        {
            GameCtr.gameMode = 6;
            GameObject.Find("gameModeButton").GetComponentInChildren<Text>().text = "6";
        }
    }
    
    public void OnSpeedButtonClick(int speedMode)
    {
        speedModeTexts[GameCtr.speedMode-1].color = Color.white;
        switch(speedMode)
        {
            case 1:
                speedModeTexts[speedMode - 1].color = new Color(0, 1, 118/255f, 1);
                break;
            case 2:
                speedModeTexts[speedMode - 1].color = new Color(0, 192/255f,1, 1);
                break;
            case 3:
                speedModeTexts[speedMode - 1].color = new Color(241/255f, 0, 1, 1);
                break;
            case 4:
                speedModeTexts[speedMode - 1].color = Color.red;
                break;
            default:
                break;

        }
        
        GameCtr.speedMode = speedMode;
    }

}
