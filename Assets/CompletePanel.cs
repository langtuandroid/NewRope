
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

public class CompletePanel : MonoBehaviour
{

    public Image Background;
    public GameObject Others;

    private AudioSource _aSource;
    void Start()
    {
        _aSource = GetComponent<AudioSource>();
    }

    public void PanelActive()
    {
        _aSource.Play();
        Background.transform.parent.gameObject.SetActive(true);
        Background.DOFade(.75f, .2f).SetEase(Ease.Linear).OnComplete(OtherPanels);

    }

    public void OtherPanels()
    {
        Others.SetActive(true);
    }

    public void FailOnClick()
    {
        var a = PlayerPrefs.GetInt("Succes");
        SceneManager.LoadScene(a);

    }

    public void SuccesOnClick()
    {
        var a = PlayerPrefs.GetInt("Succes");
        a++;
        PlayerPrefs.SetInt("Succes",a);

        if (a >= 5)
        {
            a = 0;
            PlayerPrefs.SetInt("Succes",a);

        }

        SceneManager.LoadScene(a);
    }
}
