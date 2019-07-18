using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class PlayerCtr : MonoBehaviour {
    public Sprite[] textEffect;
    private int gateIndex = 0; //目前到达的关口序号
    public GameObject[] colorEffects;
    public Transform[] effectsSixMode;
    public Transform[] effectsFourMode;
    private GameObject theCube;
    private Transform nextNote;
    public  float speed;

    private bool isGo = false;   //倒计时GO
    public List<Transform> notes = new List<Transform>();
    public List<Transform> longNotes = new List<Transform>();
    UIshow uiShow;
    UIManager uiManager;
    

    float comboincrement = 0;  //连击增量
  //  public Transform[] trackButtonsSixMode;
    public Transform[] trackSpriteSixMode;
    public Transform[] trackSpriteFourMode;
    private bool[] isDown=new bool[6];     //当前是否摁下
    private bool[] isJustPressd =new bool[6]; //当前是否刚摁下

    public int score;     //当前分数
    public int combo;    //连击数
    public int maxCombo;
    public int perfectCount;
    public int greatCount;
    public int coolCount;
    public int missCount;

    float time = 0.0f;
    public Text showTime;
    // Use this for initialization
    void Start () {
        uiShow = GameObject.Find("Canvas").GetComponent<UIshow>();
        uiManager= GameObject.Find("Canvas").GetComponent<UIManager>();
        isGo = true;
        for(int i=0;i<6;i++)
        {
            isDown[i] = false;
            isJustPressd[i] = false;
        }
       
        score = 0;
        combo = 0;
        maxCombo = 0;
        perfectCount = 0;
        greatCount = 0;
        coolCount = 0;
        missCount = 0;
    }

    void Update()
    {
        Test(); //调试时使用
    }


    // Update is called once per frame
    void FixedUpdate () {
        if (isGo)
        {
            GameObject.Find("Main Camera").transform.Translate(new Vector3(0,0,1) * speed * Time.deltaTime, Space.World);
         
            NoteIsPassCtr();
            LongNoteCtr();
            time += Time.fixedDeltaTime;
            showTime.text = time.ToString() + " s";
        }
    }
    //
    /// <summary>
    /// 更改按键或添加按键信息时
    /// 更改对应操作的响应函数即可
    /// </summary>

    //------------------------对应操作的响应函数---------------------------
    //左
    //↑↑↑↑↑↑↑↑↑↑↑与门交互事件控制↑↑↑↑↑↑↑↑↑↑↑
    //测试时使用
    public void Test()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            {
               speed = 0;
            }
       /* if(Input.GetKeyDown(KeyCode.S))
        {
            speed = 10;
        }*/
 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 500, 0));
        }

        if (Input.GetKeyDown(KeyCode.M))
        { 
            OnNoteButtonClick(1);
            OnTrackButtonDown(1);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            OnTrackButtonDown(2);
            OnNoteButtonClick(2);
            
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            OnNoteButtonClick(3);
            OnTrackButtonDown(3);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            OnTrackButtonDown(4);
            OnNoteButtonClick(4);
            
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            OnTrackButtonDown(5);
            OnNoteButtonClick(5);
            
        }
        if (Input.GetKeyDown(KeyCode.Z))
        { 
            OnTrackButtonDown(6);
             OnNoteButtonClick(6);
                
        }

        if (Input.GetKeyUp(KeyCode.M))
            OnTrackButtonUp(1);
        if (Input.GetKeyUp(KeyCode.K))
            OnTrackButtonUp(2);
        if (Input.GetKeyUp(KeyCode.O))
            OnTrackButtonUp(3);
        if (Input.GetKeyUp(KeyCode.Q))
            OnTrackButtonUp(4);
        if (Input.GetKeyUp(KeyCode.A))
            OnTrackButtonUp(5);
        if (Input.GetKeyUp(KeyCode.Z))
            OnTrackButtonUp(6);
    }

    //========================与跳跃板/浮空色块 交互事件============================

    //========================与跳跃板交互事件============================


    public void IsJustPressdCtr(int index)
    {
        if(isJustPressd[index])
        {
            StartCoroutine("WaitTime",index);
        }
    }

    public IEnumerator WaitTime( int index)
    {
        float time = 0.05f;
        while (time > 0)
        {
            yield return new WaitForSeconds(0.05f);
            time -= 0.05f;
        }
        isJustPressd[index] = false;
    }

    public void OnNoteButtonClick(int trackIndex)
    {
      
        if(GameCtr.gameMode ==6)
            effectsSixMode[trackIndex - 1].GetComponent<ParticleSystem>().Play();
        else if(GameCtr.gameMode ==4)
            effectsFourMode[trackIndex - 1].GetComponent<ParticleSystem>().Play();
        if (!isJustPressd[trackIndex-1])
        {
            for (int i=0;i<notes.Count;i++)
            {
                NoteCtr noteCtr = notes[i].gameObject.GetComponent<NoteCtr>();
                if (!noteCtr.isPass)
                {
                    if (!noteCtr.isRight)
                    {
                        if (Mathf.Abs(notes[i].position.z - gameObject.transform.position.z) < (3.0f*speed/10.0f)&&trackIndex ==noteCtr.track)
                        {
                            //点击正确
                            noteCtr.isRight = true; //该色块点击正确
                            noteCtr.isPass = true; //该色块判定结束
                            int distance = (int)((Math.Abs(transform.position.z - notes[i].transform.position.z))/ (speed / 10.0f)); //计算点击正确时距离门的距离，以触发对应的效果

                            switch (distance)
                            {
                                case 0:
                                    perfectCount++;
                                    score += 36;
                                    break;
                                case 1:
                                    greatCount++;
                                    score += 24;
                                    break;
                                case 2:
                                    coolCount++;
                                    score += 12;
                                    break;
                                default:
                                    break;
                            }
                            combo++;
                            if (combo < 10)
                                score += 20;
                            else if (combo < 100)
                                score += 35;
                            else if (combo >= 100)
                                score += 50;
                            TweenComboImage();
                            TweenDetermineImage(uiShow.determineImage, distance);
                            uiShow.comboText.text = combo.ToString();
                            uiShow.scoreText.text = "Score "+score.ToString();
                            //播放特效
                            //TODO
                            effectsSixMode[6+distance].GetComponent<ParticleSystem>().Play();
                            //notes[i].gameObject.SetActive(false);
                            GameObject note = notes[i].gameObject;
                            notes.Remove(notes[i]);
                            note.SetActive(false);
                            break;
                        }
                    }
                }
            }
        } 
        isJustPressd[trackIndex - 1] = true;
        IsJustPressdCtr(trackIndex - 1);
    }
    //长按色块的判定
    private void LongNoteCtr()
    {
        foreach (Transform longNote in longNotes)
        {
            NoteCtr noteCtr = longNote.gameObject.GetComponent<NoteCtr>();
            if(!noteCtr.isPass)
            {
                if ((gameObject.transform.position.z+ (0.5f * longNote.localScale.y*10) - longNote.position.z ) > 0)
                {
                    //进入判定范围
                  //  print("进入长条判定范围");
                    if(isDown[noteCtr.track-1] ==true)
                    {
                        //正确操作
                        noteCtr.isRight = true;
                        comboincrement += 5 * Time.deltaTime;
                        if(comboincrement>=1)
                        {
                            effectsSixMode[6].GetComponent<ParticleSystem>().Play();
                            combo++;
                            if (combo < 10)
                                score += 20;
                            else if (combo < 100)
                                score += 35;
                            else if (combo >= 100)
                                score += 50;
                            uiShow.comboText.text = combo.ToString();
                            uiShow.scoreText.text = "Score " + score.ToString();
                            TweenComboImage();
                            TweenDetermineImage(uiShow.determineImage, 0);
                            comboincrement = 0;
                            perfectCount++;
                        }
                        
                        //播放特效
                        //TODO
                    }
                }
            }
        }
    }
    public void NoteIsPassCtr()
    {
        if(notes.Count>0)
        { 
            for(int i=0;i<notes.Count;i++)
          {
            NoteCtr noteCtr = notes[i].GetComponent<NoteCtr>();
            if(noteCtr.isPass==false)
           { 
            if (gameObject.transform.position.z- notes[i].position.z>3.0*speed/10)
            {
                if(noteCtr.isRight==false)
                {
                    //错过操作
                //    print("miss");
                        missCount++;
                        DOTween.KillAll();
                        TweenDetermineImage(uiShow.determineImage, 3);
                        uiShow.determineImage.sprite = uiShow.prefabDetermineImages[3];
                        if (combo > maxCombo)
                            maxCombo = combo;
                        combo = 0;
                        uiShow.comboImage.gameObject.SetActive(false);
                        uiShow.comboText.text = "";
              //          noteCtr.isPass = true;
                }
                        GameObject note = notes[i].gameObject;
                        notes.Remove(notes[i]);
                        note.SetActive(false);
                        //  noteCtr.isPass = true;
                    }
           }
        }
        }

        for(int i=0;i<longNotes.Count;i++)
        {
            NoteCtr noteCtr = longNotes[i].GetComponent<NoteCtr>();
            if (noteCtr.isPass == false)
            {
                if (gameObject.transform.position.z-0.5f* longNotes[i].localScale.y*10- longNotes[i].position.z > 0)
                {
                    if (noteCtr.isRight == false)
                    {
                        //错过操作
                      
                        missCount++;
                        DOTween.KillAll();   //清楚其他的tween
                        TweenDetermineImage(uiShow.determineImage, 3);
                        uiShow.determineImage.sprite = uiShow.prefabDetermineImages[3];
                        if (combo > maxCombo)
                            maxCombo = combo;
                        combo = 0;
                        uiShow.comboImage.gameObject.SetActive(false);
                        uiShow.comboText.text = "";
                        //     noteCtr.isPass = true;

                    }
                    GameObject note = longNotes[i].gameObject;
                    longNotes.Remove(longNotes[i]);
                    note.SetActive(false);
                    //   noteCtr.isPass = true;
                }
            }
        }
    }
    //按键按下事件
    public void OnTrackButtonDown(int index)
    {
        isDown[index - 1] = true;
       // trackButtons[index - 1].GetComponent<CanvasGroup>().alpha = 0.5f;
       if(GameCtr.gameMode ==6)
        trackSpriteSixMode[index - 1].GetComponent<SpriteGlow>().GlowColor =new Color(0,1.3475f,0.6784f,1.0f);
       else if(GameCtr.gameMode ==4)
        trackSpriteFourMode[index - 1].GetComponent<SpriteGlow>().GlowColor = new Color(0, 1.3475f, 0.6784f, 1.0f);

    }
    //按键抬起事件
    public void OnTrackButtonUp(int index)
    {
        isDown[index - 1] = false;
        // trackButtons[index - 1].GetComponent<CanvasGroup>().alpha = 0f;
        if (GameCtr.gameMode == 6)
            trackSpriteSixMode[index - 1].GetComponent<SpriteGlow>().GlowColor = new Color(0, 1.068734f, 1.347535f, 0.3f);
        else if (GameCtr.gameMode == 4)
            trackSpriteFourMode[index - 1].GetComponent<SpriteGlow>().GlowColor = new Color(0, 1.068734f, 1.347535f, 0.3f);

    }

    public void TweenDetermineImage(Image it,int imageIndex)
    {
       // DOTween.Kill(it);
        it.sprite = uiShow.prefabDetermineImages[imageIndex];
        it.gameObject.SetActive(true);
        it.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        it.GetComponent<CanvasGroup>().alpha = 1;
        it.GetComponent<UITweenCtr>().ScaleTween(1.0f, 0.2f,true);
      
    }
    public void TweenComboImage()
    {
        DOTween.KillAll();
        UITweenCtr tweenCtr = uiShow.comboImage.GetComponent<UITweenCtr>();
        uiShow.comboImage.gameObject.SetActive(true);
        Vector3 toPos = new Vector3(tweenCtr.firstPos.x, tweenCtr.firstPos.y + 50,0);
        uiShow.comboImage.transform.position = tweenCtr.firstPos;
        uiShow.comboImage.GetComponent<CanvasGroup>().alpha = 1;
        tweenCtr.PosTween(toPos, 0.2f, true);
    }


}
