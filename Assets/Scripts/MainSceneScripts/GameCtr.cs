using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameCtr : MonoBehaviour {

    /* static GameCtr instance;

     public static GameCtr Instance
     {
         get
         {
             if (instance == null)
             {
                 instance = FindObjectOfType(typeof(GameCtr)) as GameCtr;
                 if (instance == null)
                 {
                     GameObject obj = new GameObject();
                     instance = obj.AddComponent(typeof(GameCtr)) as GameCtr;
                 }
             }
             return instance;
         }
     }
     */
    public Material[] colorMaterials;
    GameObject me;                     //主角
    GameObject audioCtr;
    AudioSource audio;
    public AudioClip[] music;
    public Text timeText;                //倒计时文本
    public GameObject timeReciprocalPanel;    //倒计时面板
    public GameObject[] notePrefabsSixKeyMode;
    public GameObject[] notePrefabsFourKeyMode;
    public List<string> noteStrings = new List<string>();
    public static int nowMapIndex = 0;
    public static int gameMode = 6;
    public static int speedMode = 1;
    public Transform sixModeUI;
    public Transform fourModeUI;
    public Transform sixModeSprite;
    public Transform fourModeSprite;
    public Color[] timeTextColors;
    float scoreShow = 0;
    bool isOver = false;
    UIshow uishow;
    // Use this for initialization
    private void Awake()
    {
        //instance = this;

        uishow = GameObject.Find("Canvas").GetComponent<UIshow>();
        audioCtr = GameObject.Find("audioCtr");
        audio = audioCtr.GetComponent<AudioSource>();
        audio.clip = music[nowMapIndex - 1];
        me = GameObject.Find("me");
        me.GetComponent<PlayerCtr>().enabled = false;
        me.GetComponent<PlayerCtr>().speed = 10 * (speedMode + 1);
        if(gameMode==6)
        {
            sixModeUI.gameObject.SetActive(true);
            sixModeSprite.gameObject.SetActive(true);
            fourModeUI.gameObject.SetActive(false);
            fourModeSprite.gameObject.SetActive(false);
        }
        else if(gameMode==4)
        {
            sixModeUI.gameObject.SetActive(false);
            sixModeSprite.gameObject.SetActive(false);
            fourModeUI.gameObject.SetActive(true);
            fourModeSprite.gameObject.SetActive(true);
        }
        ClipTransform(noteStrings[nowMapIndex - 1]);
        StartCoroutine("TimeReciprocal", 3);
    }
    void Start() {
        Screen.SetResolution(1080, 1920, true);
    }

    // Update is called once per frame
    void Update() {
        IsOver();
        if (isOver)
            {
                Invoke("GameOver", 2.0f);
                SocreAnimation();
            }
      //  print(audio.time);
            
    }
    //倒计时
    public IEnumerator TimeReciprocal(int time)
    {
        timeReciprocalPanel.SetActive(true);
        me.GetComponent<PlayerCtr>().enabled = false;
        timeText.text = time.ToString();
        timeText.color = timeTextColors[time-1];
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;
            if(time>0)
            timeText.color = timeTextColors[time - 1];
            timeText.text = time.ToString();
            
        }
        audio.Play();
        me.GetComponent<PlayerCtr>().enabled = true;
        timeReciprocalPanel.SetActive(false);
    }

    //继续按钮
    public void OnGoOnButtonClick()
    {
        //暂定3秒倒数后开始
        //TODO
        GetComponent<UIManager>().pausePanel.GetComponent<UITweenCtr>().Hide(1);
        StartCoroutine("TimeReciprocal", 3);
        Time.timeScale = 1;

    }

    //将鼓点字符串转换为障碍物信息，且实例化障碍物
    private void ClipTransform(string noteString)
    {
        string[] Clips = noteString.Split(new char[] { '_' }, 500);  //返回每个障碍物信息，每一行string为一个障碍物信息。
        int error = 0;
       //------------------------------------------------------六键模式------------------------------------------------
        if(gameMode ==6)
        { 
        foreach (string clip in Clips)
        {
            error += 1;
            Debug.Log("error: " + error);
            int obstacleClassIndex = int.Parse(new string(clip.ToCharArray(0, 1)));  //障碍物种类
            int trackIndex = int.Parse(new string(clip.ToCharArray(1, 1)));
            // int color = int.Parse(new string(clip.ToCharArray(2, 1)));
            // print(color);
            char[] time = clip.ToCharArray(2, 5);
            char[] duration = clip.ToCharArray(7, 2);
            float timeC = int.Parse(new string(time)) / 100.0f;
            float durationC = int.Parse(new string(duration)) / 10.0f;
            PlayerCtr playCtr = me.GetComponent<PlayerCtr>();
            //处理普通色块
            if (obstacleClassIndex == 0)
            {
                Vector3 pos = new Vector3(notePrefabsSixKeyMode[trackIndex - 1].transform.position.x, notePrefabsSixKeyMode[trackIndex - 1].transform.position.y, timeC * playCtr.speed);
                GameObject obs = GameObject.Instantiate(notePrefabsSixKeyMode[trackIndex-1],pos, notePrefabsSixKeyMode[trackIndex - 1].transform.rotation);
                //obs.GetComponent<Renderer>().material = colorMaterials[color];
                obs.GetComponent<NoteCtr>().track = trackIndex;
                playCtr.notes.Add(obs.transform);
               // playCtr.notes.Add(obs);
            }
             //处理长按色块
            else if(obstacleClassIndex ==1)
            {
                Vector3 pos = new Vector3(notePrefabsSixKeyMode[trackIndex +5 ].transform.position.x, notePrefabsSixKeyMode[trackIndex + 5].transform.position.y, (timeC+0.5f*durationC)* playCtr.speed);
                GameObject obs = GameObject.Instantiate(notePrefabsSixKeyMode[trackIndex + 5], pos, notePrefabsSixKeyMode[trackIndex + 5].transform.rotation);
              //  obs.GetComponent<Renderer>().material = colorMaterials[color];
                obs.GetComponent<NoteCtr>().track = trackIndex;
                obs.transform.localScale = new Vector3(0.19f, durationC*playCtr.speed/10.0f, 0.1f);
                playCtr.longNotes.Add(obs.transform);
            }
            if (obstacleClassIndex == 2)
            {
                Vector3 pos = new Vector3(notePrefabsSixKeyMode[trackIndex +11].transform.position.x, notePrefabsSixKeyMode[trackIndex + 11].transform.position.y, timeC * playCtr.speed);
                GameObject obs = GameObject.Instantiate(notePrefabsSixKeyMode[trackIndex + 11], pos, notePrefabsSixKeyMode[trackIndex + 11].transform.rotation);
                //obs.GetComponent<Renderer>().material = colorMaterials[color];
                obs.GetComponent<NoteCtr>().track = trackIndex;
                playCtr.notes.Add(obs.transform);
                // playCtr.notes.Add(obs);
            }
            
            if(obstacleClassIndex ==3)
                {
                    Vector3 pos = new Vector3(notePrefabsSixKeyMode[trackIndex + 17].transform.position.x, notePrefabsSixKeyMode[trackIndex + 17].transform.position.y, timeC * playCtr.speed);
                    GameObject obs = GameObject.Instantiate(notePrefabsSixKeyMode[trackIndex + 17], pos, notePrefabsSixKeyMode[trackIndex + 17].transform.rotation);
                    //obs.GetComponent<Renderer>().material = colorMaterials[color];
                    obs.GetComponent<NoteCtr>().track = trackIndex;
                    playCtr.notes.Add(obs.transform);
                }
         }
        }


  //----------------------------------------------四键模式-------------------------------------------------------------------
        else if(gameMode==4)
        {
            foreach (string clip in Clips)
            {
                error += 1;
                Debug.Log("error: " + error);
                int obstacleClassIndex = int.Parse(new string(clip.ToCharArray(0, 1)));  //障碍物种类
                int trackIndex = int.Parse(new string(clip.ToCharArray(1, 1)));
                if (trackIndex == 3)
                    trackIndex = 1;
                else if (trackIndex == 4|| trackIndex == 6)
                    trackIndex = 3;
                else if (trackIndex == 5)
                    trackIndex = 4;
                // int color = int.Parse(new string(clip.ToCharArray(2, 1)));
                // print(color);
                char[] time = clip.ToCharArray(2, 5);
                char[] duration = clip.ToCharArray(7, 2);
                float timeC = int.Parse(new string(time)) / 100.0f;
                float durationC = int.Parse(new string(duration)) / 10.0f;
                PlayerCtr playCtr = me.GetComponent<PlayerCtr>();
                //处理普通色块
                if (obstacleClassIndex == 0)
                {
                    Vector3 pos = new Vector3(notePrefabsFourKeyMode[trackIndex - 1].transform.position.x, notePrefabsFourKeyMode[trackIndex - 1].transform.position.y, timeC * playCtr.speed);
                    GameObject obs = GameObject.Instantiate(notePrefabsFourKeyMode[trackIndex - 1], pos, notePrefabsFourKeyMode[trackIndex - 1].transform.rotation);
                    //obs.GetComponent<Renderer>().material = colorMaterials[color];
                    obs.GetComponent<NoteCtr>().track = trackIndex;
                    playCtr.notes.Add(obs.transform);
                    // playCtr.notes.Add(obs);
                }
                //处理长按色块
                else if (obstacleClassIndex == 1)
                {                    Vector3 pos = new Vector3(notePrefabsFourKeyMode[trackIndex + 3].transform.position.x, notePrefabsFourKeyMode[trackIndex + 3].transform.position.y, (timeC + 0.5f * durationC) * playCtr.speed);
                    GameObject obs = GameObject.Instantiate(notePrefabsFourKeyMode[trackIndex + 3], pos, notePrefabsFourKeyMode[trackIndex + 3].transform.rotation);
                    //  obs.GetComponent<Renderer>().material = colorMaterials[color];
                    obs.GetComponent<NoteCtr>().track = trackIndex;
                    obs.transform.localScale = new Vector3(0.19f, durationC * playCtr.speed / 10.0f, 0.1f);
                    playCtr.longNotes.Add(obs.transform);
                }
                if (obstacleClassIndex == 2)
                {
                    Vector3 pos = new Vector3(notePrefabsFourKeyMode[trackIndex + 7].transform.position.x, notePrefabsFourKeyMode[trackIndex + 7].transform.position.y, timeC * playCtr.speed);
                    GameObject obs = GameObject.Instantiate(notePrefabsFourKeyMode[trackIndex + 7], pos, notePrefabsFourKeyMode[trackIndex + 7].transform.rotation);
                    //obs.GetComponent<Renderer>().material = colorMaterials[color];
                    obs.GetComponent<NoteCtr>().track = trackIndex;
                    playCtr.notes.Add(obs.transform);
                    // playCtr.notes.Add(obs);
                }
                if (obstacleClassIndex == 3)
                {
                    Vector3 pos = new Vector3(notePrefabsFourKeyMode[trackIndex + 11].transform.position.x, notePrefabsFourKeyMode[trackIndex + 11].transform.position.y, timeC * playCtr.speed);
                    GameObject obs = GameObject.Instantiate(notePrefabsFourKeyMode[trackIndex + 11], pos, notePrefabsFourKeyMode[trackIndex + 11].transform.rotation);
                    //obs.GetComponent<Renderer>().material = colorMaterials[color];
                    obs.GetComponent<NoteCtr>().track = trackIndex;
                    playCtr.notes.Add(obs.transform);
                }
            }

        }
    }


    //判断游戏进程是否结束
    public void IsOver()
    {
        PlayerCtr cp = me.GetComponent<PlayerCtr>();
        if(audio.clip.length-audio.time<=0.1f)
        {
            isOver = true ;
        }
    }


    //游戏结束后结算界面
    public void GameOver()
    {
        PlayerCtr cp = me.GetComponent<PlayerCtr>();
        
        if (cp.combo > cp.maxCombo)
        cp.maxCombo = cp.combo;
        uishow.maxCombo.text = cp.maxCombo.ToString();
        
        uishow.perfectCount.text = cp.perfectCount.ToString();
        uishow.greatCount.text = cp.greatCount.ToString();
        uishow.coolCount.text = cp.coolCount.ToString();
        uishow.missCount.text = cp.missCount.ToString();
        string it = "map" + nowMapIndex.ToString();
        int highestScore = PlayerPrefs.GetInt(it);
        uishow.highScore.text = highestScore.ToString();
        GameObject gameOverPanel = GameObject.Find("Canvas").GetComponent<UIManager>().gameOverPanel;
        gameOverPanel.SetActive(true);
        gameOverPanel.GetComponent<UITweenCtr>().Show(0.5f);
        //动画特效      
        uishow.maxCombo.GetComponent<UITweenCtr>().PosTween(uishow.maxCombo.GetComponent<UITweenCtr>().toPos,1.0f, false);
        uishow.perfectCount.GetComponent<UITweenCtr>().PosTween(uishow.perfectCount.GetComponent<UITweenCtr>().toPos, 1.2f, false);
        uishow.greatCount.GetComponent<UITweenCtr>().PosTween(uishow.greatCount.GetComponent<UITweenCtr>().toPos, 1.4f, false);
        uishow.coolCount.GetComponent<UITweenCtr>().PosTween(uishow.coolCount.GetComponent<UITweenCtr>().toPos, 1.6f, false);
        uishow.missCount.GetComponent<UITweenCtr>().PosTween(uishow.missCount.GetComponent<UITweenCtr>().toPos, 1.8f, false);
        uishow.score.GetComponent<UITweenCtr>().PosTween(new Vector3(200.0f,0f,0f), 1.8f, false);
        uishow.highScore.GetComponent<UITweenCtr>().PosTween(uishow.highScore.GetComponent<UITweenCtr>().toPos, 2.0f, false);
        uishow.reStartButtonOver.GetComponent<UITweenCtr>().Show(4.0f);
        uishow.toMainButtonOver.GetComponent<UITweenCtr>().Show(4.0f);

        me.GetComponent<PlayerCtr>().enabled = false;
        GameObject.Find("songText").GetComponent<Text>().text = audio.clip.name;
        GameObject.Find("gameModeText").GetComponent<Text>().text = gameMode.ToString()+"KEY";
        //highestScore = PlayerPrefs.GetInt(it);
        return;
    }


    public void SocreAnimation()
    {
        PlayerCtr playCtr = me.GetComponent<PlayerCtr>();
        int score = playCtr.score;
        score += playCtr.maxCombo * 15 + (playCtr.perfectCount + playCtr.greatCount + playCtr.coolCount - playCtr.missCount) * 5;
        print("score增加了"+score);
        scoreShow += (score * Time.deltaTime/5);
        if (scoreShow >=score)
        {
            string it = "map" + nowMapIndex.ToString();
            int highestScore = PlayerPrefs.GetInt(it);
            if (score> highestScore)
            {
                print("超记录了啊！");
                PlayerPrefs.SetInt(it, score);
                uishow.recordBreakText.gameObject.SetActive(true);  
            }
            scoreShow = score;
            this.enabled = false;
            print("结束了");
        }
        Debug.Log((int)scoreShow);
        uishow.score.text = ((int)scoreShow).ToString();
    }
}
