
using UnityEngine;
using DG.Tweening;
using ElephantSDK;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

public class CompletePanel : MonoBehaviour
{

    public Image Background;
    public GameObject Others;

    public GameObject FailPanel;
    public Image FailBackground;

    private AudioSource _aSource;
    public AudioSource FailASource;
    public GameObject FailOthers;
    private LevelTimer _lTimer;
    private RayCaster _rCaster;

    private GameManager _gm;
    void Start()
    {
        _aSource = GetComponent<AudioSource>();
        _lTimer = FindObjectOfType<LevelTimer>();

        var a = PlayerPrefs.GetInt("CurrentLevelIndex") - 1;
        _rCaster = FindObjectOfType<RayCaster>();
        Elephant.LevelStarted(a);
        _gm = FindObjectOfType<GameManager>();
        Debug.Log($"Level Started:{a}");
    }

    public void PanelActive()
    {
        if(FailPanel.activeSelf) return;
        _aSource.Play();
        Background.transform.parent.gameObject.SetActive(true);
        Background.DOFade(.75f, .2f).SetEase(Ease.Linear).OnComplete(OtherPanels);

    }

    public void FailPanelActive()
    {
        if(Others.activeSelf) return;
        
        FailPanel.SetActive(true);
        FailASource.Play();
        FailBackground.DOFade(.75f, .1f).SetEase(Ease.Linear).OnComplete(() =>
        {
            FailOthers.SetActive(true);
        });
    }

    public void OtherPanels()
    {
        Others.SetActive(true);
    }

    public void FailOnClick()
    {
        var a = PlayerPrefs.GetInt("CurrentLevelIndex")-1;
        //SceneManager.LoadScene(a);
        
        var p = Params.New()
            .Set("used_move_count", _rCaster.ClickCount)
            .Set("time", _lTimer.ClaculateTime());
        
        Elephant.LevelFailed(a,p);
        Debug.Log($"Level Failed:{a}");

        _gm.LevelFailed();

    }

    public void SuccesOnClick()
    {
    
        
        var a = PlayerPrefs.GetInt("CurrentLevelIndex")-1;
        Debug.Log(a);
        //SceneManager.LoadScene(a);
        
        var p = Params.New()
            .Set("used_move_count", _rCaster.ClickCount)
            .Set("time", _lTimer.ClaculateTime());
        
        Elephant.LevelCompleted(a,p);
        
        Debug.Log($"Level Completed:{a}");
        var b =PlayerPrefs.GetInt("FakeLevel");
        b++;
        PlayerPrefs.SetInt("FakeLevel",b);
        _gm.LevelSucces();
    }
}
