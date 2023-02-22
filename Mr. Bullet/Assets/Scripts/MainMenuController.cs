using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [Header("Main Menu")]
    public GameObject playGO;
    public GameObject levelSelectionGO;

    public void OnPlayClick()
    {
        playGO.SetActive(false);
        levelSelectionGO.SetActive(true);
    }
}
