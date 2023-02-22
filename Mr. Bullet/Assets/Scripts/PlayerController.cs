using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float rotateSpeed = 100f;
    public float bulletSpeed = 20f;

    public int ammoCount = 4;

    public GameObject bulletGO;

    public AudioClip gunShotAC;
    
    private Transform _handPosTF;
    private Transform _firePos1TF;
    private Transform _firePos2TF;

    private LineRenderer _laserLR;

    private GameObject _crosshairGO;

    void Awake()
    {
        _crosshairGO = GameObject.Find("Crosshair");
        _crosshairGO.SetActive(false);

        _handPosTF = GameObject.Find("/Player/LeftArm").transform;
        _firePos1TF = GameObject.Find("FirePosition1").transform;
        _firePos2TF = GameObject.Find("FirePosition2").transform;

        _laserLR = GameObject.Find("Gun").GetComponent<LineRenderer>();
        _laserLR.enabled = false;
    }

    void Update()
    {
        if(!IsMouseOverUI())
        {
            if (Input.GetMouseButton(0))
            {
                Aim();
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (ammoCount > 0)
                    Shoot();

                else
                {
                    _laserLR.enabled = false;
                }
            }
        }
        
    }

    void Aim()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _handPosTF.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        _handPosTF.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed);

        _laserLR.enabled = true;
        _laserLR.SetPosition(0, _firePos1TF.position);
        _laserLR.SetPosition(1, _firePos2TF.position);
        
        _crosshairGO.SetActive(true);

        _crosshairGO.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + (Vector3.forward * 10);
    }

    void Shoot()
    {
        _crosshairGO.SetActive(false);

        _laserLR.enabled = false;

        GameObject bullet = Instantiate(bulletGO, _firePos1TF.position, Quaternion.identity);

        if(transform.localScale.x > 0)
        {
            bullet.GetComponent<Rigidbody2D>().AddForce(_firePos1TF.right * bulletSpeed, ForceMode2D.Impulse);
        }

        else
        {
            bullet.GetComponent<Rigidbody2D>().AddForce(-_firePos1TF.right * bulletSpeed, ForceMode2D.Impulse);
        }

        ammoCount--;

        FindObjectOfType<MrBulletGameManager>().CheckBulletCount();

        MrBulletAudioManager.mrBulletAM.PlaySoundFX(gunShotAC, 0.5f);

        Destroy(bullet, 2f);
    }

    bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
