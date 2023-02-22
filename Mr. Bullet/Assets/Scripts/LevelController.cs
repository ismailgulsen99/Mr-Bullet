using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private Button _levelButton;

    public int levelReq;

    void Start()
    {
        _levelButton = GetComponent<Button>();

        if (PlayerPrefs.GetInt("WHICH_LEVEL", 1) >= levelReq)
            _levelButton.onClick.AddListener(() => LoadLevel());

        else
            GetComponent<CanvasGroup>().alpha = 0.5f;
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(gameObject.name);
    }
}
