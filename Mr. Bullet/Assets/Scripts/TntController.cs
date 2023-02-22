using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TntController : MonoBehaviour
{
    public GameObject explotionPrefab;

    public float radius = 1.75f;
    public float power = 10.0f;

    void Explode()
    {
        Vector2 explotionPosition = transform.position;

        Collider2D[] targetsColl = Physics2D.OverlapCircleAll(explotionPosition, radius);

        foreach(Collider2D hit in targetsColl)
        {
            if(hit.GetComponent<Rigidbody2D>() != null)
            {
                var explodeDirection = hit.GetComponent<Rigidbody2D>().position - explotionPosition;

                hit.GetComponent<Rigidbody2D>().gravityScale = 1;
                hit.GetComponent<Rigidbody2D>().AddForce(power * explodeDirection, ForceMode2D.Impulse);
            }

            if (hit.tag == "Enemy")
                hit.tag = "Untagged";
        }
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if(target.gameObject.tag == "Bullet")
        {
            GameObject explotion = Instantiate(explotionPrefab);
            explotion.transform.position = transform.position;
            Explode();
            Destroy(explotion, 0.8f);
            Destroy(gameObject);
        }
    }
}
