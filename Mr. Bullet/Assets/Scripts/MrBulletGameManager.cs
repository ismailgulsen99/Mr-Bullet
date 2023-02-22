using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrBulletGameManager : MonoBehaviour
{
    public int enemyCount = 1;
    public int blackBullets = 3;
    public int goldenBullets = 1;
    
    [HideInInspector]
    public bool isGameOver = false;

    public GameObject blackBulletGO;
    public GameObject goldenBulletGO;

    void Awake()
    {
        FindObjectOfType<PlayerController>().ammoCount = blackBullets + goldenBullets;

        for (int i = 0; i < blackBullets; i++)
        {
            GameObject temp = Instantiate(blackBulletGO);
            temp.transform.SetParent(GameObject.Find("AllBullets").transform);
        }

        for (int j = 0; j < goldenBullets; j++)
        {
            GameObject temp2 = Instantiate(goldenBulletGO);
            temp2.transform.SetParent(GameObject.Find("AllBullets").transform);
        }
    }

    void Update()
    {
        if (!isGameOver && FindObjectOfType<PlayerController>().ammoCount == 0 && enemyCount > 0 && GameObject.FindGameObjectsWithTag("Bullet").Length <= 0)
        {
            isGameOver = true;
            MrBulletSceneController.mrBulletSC.GameOver();
        }
    }

    public void CheckBulletCount()
    {
        if(goldenBullets >= 1)
        {
            goldenBullets--;
            GameObject.FindGameObjectWithTag("GoldenBullet").SetActive(false);
        }

        else if(blackBullets > 0)
        {
            blackBullets--;
            GameObject.FindGameObjectWithTag("BlackBullet").SetActive(false);
        }
    }

    public void CheckEnemyCount()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemyCount == 0)
        {
            MrBulletSceneController.mrBulletSC.WinGame();
            MrBulletSceneController.mrBulletSC.PassLevel();
        }
    }
}
