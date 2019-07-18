using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MapEditor : MonoBehaviour
{

    // Use this for initialization
    float time;
    string mapString;
    string allString;
    string newMapString;
    bool[] isDown = new bool[6];
    float[] pressTimes = new float[6];
    float goToTime=0;
    public Text showTime;
    public AudioSource au;
    public Scrollbar sc;
    public GameObject[] notePrefabsSixKeyMode;
    List<GameObject> notesAdd = new List<GameObject>();
    void Start()
    {
        mapString = string.Empty;
        allString = string.Empty;
        newMapString = string.Empty;
        time = 0;
        for (int i = 0; i < 6; i++)
        {
            isDown[i] = false;
            pressTimes[i] = 0;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //   Debug.Log(au.time);
        /*   if (Input.GetKeyDown(KeyCode.RightArrow))
               Time.timeScale += 1;
           if (Input.GetKeyDown(KeyCode.LeftArrow))
               Time.timeScale -= 1;*/
        sc.value = au.time / au.clip.length;
        showTime.text = au.time.ToString("0.00");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (mapString != string.Empty)
            {
                ClipTransform(mapString);
                au.time = goToTime;
                transform.position = new Vector3(0, 0, 20 * goToTime - 9.1f);
            }
           
        }
        string timeC = ((int)(au.time * 100)).ToString();
          if (Input.GetKeyDown(KeyCode.Escape))
          {
              foreach (GameObject go in notesAdd)
              {
                  Destroy(go);
              }
              au.time = goToTime;
              transform.position = new Vector3(0, 0, 20 * goToTime - 9.1f);
              mapString = string.Empty;
              newMapString = string.Empty;
          }
          if (Input.GetKeyDown(KeyCode.S))
          {
              allString += newMapString;
              mapString = string.Empty;
              newMapString = string.Empty;
              notesAdd.Clear();
          }
          //---------------------------------------------------
          /*if (Input.GetKeyDown(KeyCode.M))
          {
              isDown[0] = true;
          }
          if (Input.GetKeyDown(KeyCode.K))
          {
              isDown[1] = true;
          }
          if (Input.GetKeyDown(KeyCode.O))
          {
              isDown[2] = true;
          }
          if (Input.GetKeyDown(KeyCode.Q))
          {
              isDown[3] = true;
          }*/
          if (Input.GetKeyDown(KeyCode.A))
          {
              isDown[4] = true;
          }
          /*
          if (Input.GetKeyDown(KeyCode.L))
          {
              isDown[5] = true;
          }*/
        //---------------------------------------------
        /* if (Input.GetKeyUp(KeyCode.M))
         {
             isDown[0] = false;
             string timeC1 = ((int)(au.time * 100 - pressTimes[0] * 100)).ToString();
             if (pressTimes[0] > 0.2f)
             {

                 string timeC2 = ((int)(pressTimes[0] * 10)).ToString();
                 if (timeC2.Length == 1)
                     timeC2 = "0" + timeC2;
                 string it = "1100" + timeC1 + timeC2 + "_";
                 Debug.Log(timeC2);
                 if (it.Length == 11)
                     it = it.Remove(2, 1);
                 else if (it.Length == 12)
                     it = it.Remove(2, 2);
                 mapString += it;
             }
             else
             {
                 string it = "0100" + timeC1 + "00_";
                 if (it.Length == 11)
                     it = it.Remove(2, 1);
                 else if (it.Length == 12)
                     it = it.Remove(2, 2);
                 mapString += it;
             }
             pressTimes[0] = 0;
         }
         if (Input.GetKeyUp(KeyCode.K))
         {
             isDown[1] = false;
             string timeC1 = ((int)(au.time * 100 - pressTimes[1] * 100)).ToString();
             if (pressTimes[1] > 0.2f)
             {

                 string timeC2 = ((int)(pressTimes[1] * 10)).ToString();
                 if (timeC2.Length == 1)
                     timeC2 = "0" + timeC2;
                 Debug.Log(timeC2);
                 string it = "1200" + timeC1 + timeC2 + "_";
                 if (it.Length == 11)
                     it = it.Remove(2, 1);
                 else if (it.Length == 12)
                     it = it.Remove(2, 2);
                 mapString += it;
             }
             else
             {
                 string it = "0200" + timeC1 + "00_";
                 if (it.Length == 11)
                     it = it.Remove(2, 1);
                 else if (it.Length == 12)
                     it = it.Remove(2, 2);
                 mapString += it;
             }
             pressTimes[1] = 0;
         }
         if (Input.GetKeyUp(KeyCode.O))
         {
             isDown[2] = false;
             string timeC1 = ((int)(au.time * 100 - pressTimes[2] * 100)).ToString();
             if (pressTimes[2] > 0.2f)
             {

                 string timeC2 = ((int)(pressTimes[2] * 10)).ToString();
                 if (timeC2.Length == 1)
                     timeC2 = "0" + timeC2;
                 Debug.Log(timeC2);
                 string it = "1300" + timeC1 + timeC2 + "_";
                 if (it.Length == 11)
                     it = it.Remove(2, 1);
                 else if (it.Length == 12)
                     it = it.Remove(2, 2);
                 mapString += it;
             }
             else
             {
                 string it = "0300" + timeC1 + "00_";
                 if (it.Length == 11)
                     it = it.Remove(2, 1);
                 else if (it.Length == 12)
                     it = it.Remove(2, 2);
                 mapString += it;
             }
             pressTimes[2] = 0;
         }
         if (Input.GetKeyUp(KeyCode.Q))
         {
             isDown[3] = false;
             string timeC1 = ((int)(au.time * 100 - pressTimes[3] * 100)).ToString();
             if (pressTimes[3] > 0.2f)
             {

                 string timeC2 = ((int)(pressTimes[3] * 10)).ToString();
                 if (timeC2.Length == 1)
                     timeC2 = "0" + timeC2;
                 Debug.Log(timeC2);
                 string it = "1400" + timeC1 + timeC2 + "_";
                 if (it.Length == 11)
                     it = it.Remove(2, 1);
                 else if (it.Length == 12)
                     it = it.Remove(2, 2);
                 mapString += it;
             }
             else
             {
                 string it = "0400" + timeC1 + "00_";
                 if (it.Length == 11)
                     it = it.Remove(2, 1);
                 else if (it.Length == 12)
                     it = it.Remove(2, 2);
                 mapString += it;
             }
             pressTimes[3] = 0;
         }
         */
        if (Input.GetKeyUp(KeyCode.A))
        {
            isDown[4] = false;
            string timeC1 = ((int)(au.time * 100 - pressTimes[4] * 100)).ToString();
            string newTime = (int.Parse(timeC1) / 100.0).ToString();
            newMapString += newTime + "_";
            if (pressTimes[4] > 0.2f)
            {

                string timeC2 = ((int)(pressTimes[4] * 10)).ToString();
                if (timeC2.Length == 1)
                    timeC2 = "0" + timeC2;
                Debug.Log(timeC2);
                string it = "1500" + timeC1 + timeC2 + "_";
                if (it.Length == 11)
                    it = it.Remove(2, 1);
                else if (it.Length == 12)
                    it = it.Remove(2, 2);
                mapString += it;
            }
            else
            {
                string it = "0500" + timeC1 + "00_";
                if (it.Length == 11)
                    it = it.Remove(2, 1);
                else if (it.Length == 12)
                    it = it.Remove(2, 2);
                mapString += it;
            }
            pressTimes[4] = 0;
        }
        /*if (Input.GetKeyUp(KeyCode.Z))
        {
            isDown[5] = false;
            string timeC1 = ((int)(au.time * 100 - pressTimes[5] * 100)).ToString();
            if (pressTimes[5] > 0.2f)
            {

                string timeC2 = ((int)(pressTimes[5] * 10)).ToString();
                if (timeC2.Length == 1)
                    timeC2 = "0" + timeC2;
                Debug.Log(timeC2);
                string it = "1600" + timeC1 + timeC2 + "_";
                if (it.Length == 11)
                    it = it.Remove(2, 1);
                else if (it.Length == 12)
                    it = it.Remove(2, 2);
                mapString += it;
            }
            else
            {
                string it = "0600" + timeC1 + "00_";
                if (it.Length == 11)
                    it = it.Remove(2, 1);
                else if (it.Length == 12)
                    it = it.Remove(2, 2);
                mapString += it;
            }
            pressTimes[5] = 0;
        }*/
        PressTimeCtr();
    }
    private void FixedUpdate()
    {
        gameObject.transform.Translate(Vector3.forward * 20 * Time.fixedDeltaTime);
        //  sc.value = au.time/au.clip.length;
    }

    void PressTimeCtr()
    {
        for (int i = 0; i < 6; i++)
        {
            if (isDown[i])
            {
                pressTimes[i] += Time.deltaTime;
                // Debug.Log(pressTimes[i]);
            }
        }

    }


    public void OnValueChange()
    {
        au.time = sc.value * au.clip.length;
        transform.position = new Vector3(0, 0, 20 * au.time - 9.1f);
    }



    private void ClipTransform(string noteString)
    {
        if(noteString == null || noteString.Length <=0)
        {
            return;
        }
        string[] Clips = noteString.Split(new char[] { '_' }, 500);  //返回每个障碍物信息，每一行string为一个障碍物信息。
        int error = 0;
        char[] firstTime = noteString.ToCharArray(2, 5);
        goToTime = (int.Parse(new string(firstTime)) / 100.0f)-2.0f;
        //------------------------------------------------------六键模式------------------------------------------------
        foreach (string clip in Clips)
        {
            if (clip.Length > 5)
            {
                error += 1;
               // Debug.Log("error: " + error);
                int obstacleClassIndex = int.Parse(new string(clip.ToCharArray(0, 1)));  //障碍物种类
                int trackIndex = int.Parse(new string(clip.ToCharArray(1, 1)));
                // int color = int.Parse(new string(clip.ToCharArray(2, 1)));
                // print(color);
                char[] time = clip.ToCharArray(2, 5);
                float timeC = int.Parse(new string(time)) / 100.0f;
                char[] duration = clip.ToCharArray(7, 2);
                
                float durationC = int.Parse(new string(duration)) / 10.0f;
                //处理普通色块
                if (obstacleClassIndex == 0)
                {
                    Vector3 pos = new Vector3(notePrefabsSixKeyMode[trackIndex - 1].transform.position.x, notePrefabsSixKeyMode[trackIndex - 1].transform.position.y, timeC * 20);
                    GameObject obs = GameObject.Instantiate(notePrefabsSixKeyMode[trackIndex - 1], pos, notePrefabsSixKeyMode[trackIndex - 1].transform.rotation);
                    notesAdd.Add(obs);
                }
                //处理长按色块
                else if (obstacleClassIndex == 1)
                {
                    Vector3 pos = new Vector3(notePrefabsSixKeyMode[trackIndex + 5].transform.position.x, notePrefabsSixKeyMode[trackIndex + 5].transform.position.y, (timeC + 0.5f * durationC) * 20);
                    GameObject obs = GameObject.Instantiate(notePrefabsSixKeyMode[trackIndex + 5], pos, notePrefabsSixKeyMode[trackIndex + 5].transform.rotation);
                    obs.transform.localScale = new Vector3(0.19f, durationC * 20 / 10.0f, 0.1f);
                    notesAdd.Add(obs);
                }
                if (obstacleClassIndex == 2)
                {
                    Vector3 pos = new Vector3(notePrefabsSixKeyMode[trackIndex + 11].transform.position.x, notePrefabsSixKeyMode[trackIndex + 11].transform.position.y, timeC * 20);
                    GameObject obs = GameObject.Instantiate(notePrefabsSixKeyMode[trackIndex + 11], pos, notePrefabsSixKeyMode[trackIndex + 11].transform.rotation);
                }

            }
        }
    }

    public void OnInputStringButtonDown()
    {
        Debug.Log(allString);
    }

}

