using Assets.Core;
using System.Linq;
using UnityEngine;
public class Treasure : MonoBehaviour
{
    public Player player;
    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag(Constants.TAG_PLAYER))
        {
            if(player.Items.Any(c => c.tag == Constants.TAG_KEY))
                player.GameCore.Finish();
            else
                player.GameCore.FindTheKeyMessage(true);

        }
    }
}
