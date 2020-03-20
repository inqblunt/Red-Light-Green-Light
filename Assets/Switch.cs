using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public SwitchedDoor linkedDoor;
    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.GetComponent<Bullet>())
        {
            linkedDoor.isOpen = true;
            linkedDoor.linkedDoor.isOpen = true;
            linkedDoor.GetComponent<SpriteRenderer>().sprite = linkedDoor.openSprite;
            linkedDoor.linkedDoor.GetComponent<SpriteRenderer>().sprite = linkedDoor.openSprite;
            Destroy(gameObject);
        }
    }
}
