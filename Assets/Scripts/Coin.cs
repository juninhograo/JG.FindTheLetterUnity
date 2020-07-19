using Assets.Core;
using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision2D)
    {
        //Debug.Log($"CollisionEnter2D {collision2D.gameObject.tag}");
        //if (collision2D.gameObject.CompareTag(Constants.TAG_PLAYER))
        //{
        //    Destroy(this.gameObject);
        //    GetComponent<AudioSource>().Play();
        //}
    }
}
