using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneLoading : MonoBehaviour
{

    private float fps = 10.0f;
    private float time;
    public GameObject loadingSider;
    public Text progressText;
    public string[] mapNames;
    public string[] songOriginators;
    public string[] songAdaptors;
    public Text mapName;
    public Text songOriginator;
    public Text songAdaptor;
    //异步对象
    AsyncOperation async;

    //读取场景的进度，它的取值范围在0 - 1 之间。
    int progress = 0;

    void Start()
    {
        //在这里开启一个异步任务，
        //进入loadScene方法。
        mapName.text = mapNames[GameCtr.nowMapIndex-1];
        songAdaptor.text=  songAdaptors[GameCtr.nowMapIndex - 1];
        songOriginator.text= songOriginators[GameCtr.nowMapIndex - 1];
        StartCoroutine(loadScene());
    }

    //注意这里返回值一定是 IEnumerator
    IEnumerator loadScene()
    {
        //异步读取场景。
        //Globe.loadName 就是A场景中需要读取的C场景名称。
        yield return new WaitForSeconds(3);
        async = SceneManager.LoadSceneAsync("1");
        //读取完毕后返回， 系统会自动进入C场景
        yield return async;

    }
    void Update()
    {

        //在这里计算读取的进度，
        //progress 的取值范围在0.1 - 1之间， 但是它不会等于1
        //也就是说progress可能是0.9的时候就直接进入新场景了
        //所以在写进度条的时候需要注意一下。
        //为了计算百分比 所以直接乘以100即可
      //  Debug.Log(UIManager.loadName);
        if (async != null)
        {
            progress = (int)(async.progress * 100);
            progressText.text = progress.ToString() + "%";  //更新加载进度文本
            loadingSider.GetComponent<Slider>().value = async.progress;
        }
    }


}