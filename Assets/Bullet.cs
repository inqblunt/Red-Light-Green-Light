using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Kill());
    }

    // Update is called once per frame
    void Update()
    {
        
    } 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            Damage(collision.gameObject);
            Destroy(gameObject);
        }       
    }

    void Damage(GameObject target)
    {
        //do something
    }

    private IEnumerator Kill()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
