using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchedDoor : MonoBehaviour
{
    public bool canTransport;
    public bool isOpen;
    public SwitchedDoor linkedDoor;
    public Sprite openSprite;
    public Sprite closeSprite;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = closeSprite;
        canTransport = true;
        isOpen = false;
        linkedDoor.isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (isOpen && coll.gameObject.tag == "Player" && canTransport)
        {
            OpenSesame(coll);
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (isOpen && coll.gameObject.tag == "Player")
        {
            canTransport = true;
        }
    }

    private void OpenSesame(Collision2D target)
    {
        linkedDoor.canTransport = false;
        target.transform.position = linkedDoor.transform.position;
    }
}
