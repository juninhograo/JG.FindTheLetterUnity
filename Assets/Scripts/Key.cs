using Assets.Core;
using UnityEngine;
public class Key : MonoBehaviour
{
    public Player player;
    void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.CompareTag(Constants.TAG_PLAYER))
        {
            gameObject.SetActive(false);
            player.Items.Add(gameObject);
            player.GameCore.ShowKeyUI();
        }
    }
}