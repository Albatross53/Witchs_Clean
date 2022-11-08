using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenManager : MonoBehaviour
{
    public GameObject m_canvas = null;

    //로딩창
    public GameObject m_loadingPanel = null;
    public Slider m_slider = null;
    public Text m_progressText = null;

    /// <summary>
    /// static
    /// </summary>
    static ScenManager g_scenManager = null;

    /// <summary>
    /// awake
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            g_scenManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        Debug.Log("endable");
        FindObj();

        SceneManager.sceneLoaded += LoadSceneEvent;
    }

    /// <summary>
    /// 오브젝트 찾기
    /// </summary>
    void FindObj()
    {
        m_canvas = GameObject.Find("Canvas");
        m_loadingPanel = m_canvas.transform.Find("LoadingPanel").gameObject;
        m_slider = m_loadingPanel.transform.Find("LoadingBar").GetComponent<Slider>();
        m_progressText = m_slider.transform.Find("ProgressText").GetComponent<Text>();
    }

    /// <summary>
    /// 씬이 로딩됬을 때
    /// </summary>
    /// <param name="scene">씬</param>
    /// <param name="mode">모드</param>
    void LoadSceneEvent(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("LoadSceneEvent");
        FindObj();
        if(SceneManager.GetActiveScene().name == "Main")
        {
            Debug.Log("main");
            if (GameDataManager.Instance.m_toAddExp <= 0 && GameDataManager.Instance.m_toAddMoney <= 0)
            {
                return;
            }
            PlayerValueManager.Instance.GameEndSetting();
        }
    }

    /// <summary>
    /// 메인이면 true 아니면 false
    /// </summary>
    /// <returns></returns>
    public bool CheckMain()
    {
        if (SceneManager.GetActiveScene().name == "Main")
        {
            return true;
        }
        return false;
    }

    //씬 이동 함수
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadAsynchrounously(sceneName));
    }

    IEnumerator LoadAsynchrounously(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        m_loadingPanel.SetActive(true);

        while (!operation.isDone)
        {

            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            m_slider.value = progress;
            m_progressText.text = progress * 100f + "%";

            yield return null;
        }
    }

    public static ScenManager Instance
    {
        get
        {
            return g_scenManager;
        }
    }
}
