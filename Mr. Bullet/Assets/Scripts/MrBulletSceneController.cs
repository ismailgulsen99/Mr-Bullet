using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MrBulletSceneController : MonoBehaviour
{
    public static MrBulletSceneController mrBulletSC;
    
    [Header("WinScreen")]
    public Text CongratsText;
    public GameObject BackgroundPanelGO;
    public Image oneStar, twoStar, threeStar;
    public Sprite goldenStar, darkStar;

    [Header("GameOver")]
    public GameObject RestartBackgroundGO;

    private MrBulletGameManager _mrBulletGM;

    private int _startBullets;
    private int _levelNo;       //for which level are we at

    void Awake()
    {
        _levelNo = PlayerPrefs.GetInt("WHICH_LEVEL", 1);

        mrBulletSC = this;

        _mrBulletGM = FindObjectOfType<MrBulletGameManager>();
    }

    void Start()
    {
        _startBullets = _mrBulletGM.blackBullets;
    }

    public void GameOver()
    {
        RestartBackgroundGO.SetActive(true);
    }

    public void WinGame()
    {
        BackgroundPanelGO.SetActive(true);

        if(_mrBulletGM.blackBullets >= _startBullets)
        {
            CongratsText.text = "FANTASTIC!";
            StartCoroutine(StarsCollected(3));
        }

        else if(_mrBulletGM.blackBullets >= _startBullets - (_mrBulletGM.blackBullets/2))
        {
            CongratsText.text = "AWESOME!";
            StartCoroutine(StarsCollected(2));
        }

        else if(_mrBulletGM.blackBullets > 0)
        {
            CongratsText.text = "WELL DONE!";
            StartCoroutine(StarsCollected(1));
        }

        else
        {
            CongratsText.text = "GOOD!";
            StartCoroutine(StarsCollected(0));
        }

    }

    public void PassLevel()
    {
        if (_levelNo >= SceneManager.GetActiveScene().buildIndex)    ///this if is for increase the level
        {
            PlayerPrefs.SetInt("WHICH_LEVEL", _levelNo + 1);
        }
    }

    private IEnumerator StarsCollected(int starsNumber)
    {
        yield return new WaitForSeconds(0.5f);

        switch(starsNumber)
        {
            case 3:
                yield return new WaitForSeconds(0.15f); //I used WaitForSeconds for stars come one by one when we pass a level.
                oneStar.sprite = goldenStar;
                yield return new WaitForSeconds(0.15f);
                twoStar.sprite = goldenStar;
                yield return new WaitForSeconds(0.15f);
                threeStar.sprite = goldenStar;
                break;
            case 2:
                yield return new WaitForSeconds(0.15f);
                oneStar.sprite = goldenStar;
                yield return new WaitForSeconds(0.15f);
                twoStar.sprite = goldenStar;
                break;
            case 1:
                yield return new WaitForSeconds(0.15f);
                oneStar.sprite = goldenStar;
                break;
            case 0:
                yield return new WaitForSeconds(0.15f);
                break;
        }
    }

    public void OnRestartClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnNextLevelClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnQuitClick()
    {
        SceneManager.LoadScene(0);  //0 is equal to main menu in build settings
    }
}
