using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public AudioClip boxHitAC;
    public AudioClip plankHitAC;
    public AudioClip groundHitAC;
    public AudioClip explotionHitAC;

    void OnCollisionEnter2D(Collision2D target)
    {
        if(target.gameObject.tag == "Box")
        {
            MrBulletAudioManager.mrBulletAM.PlaySoundFX(boxHitAC, 1.0f);

            Destroy(target.gameObject);
        }

        if (target.gameObject.tag == "Plank")
        {
            MrBulletAudioManager.mrBulletAM.PlaySoundFX(plankHitAC, 1.0f);
        }

        if (target.gameObject.tag == "Ground")
        {
            MrBulletAudioManager.mrBulletAM.PlaySoundFX(groundHitAC, 1.0f);
        }

        if (target.gameObject.tag == "TNT")
        {
            MrBulletAudioManager.mrBulletAM.PlaySoundFX(explotionHitAC, 1.0f);
        }
    }
}
