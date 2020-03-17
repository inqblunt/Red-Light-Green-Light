using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickSurface : MonoBehaviour
{
    public Player player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            player.isGrounded = true;
        }
    }
}
