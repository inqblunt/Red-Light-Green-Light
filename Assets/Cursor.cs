using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cursor : MonoBehaviour
{
    public Player player;
    private Image _imago;
    private Camera _cam;

    private void Awake()
    {
        _imago = gameObject.GetComponent<Image>();
        _cam = Camera.main;
    }

    void Update()
    {
        var tempposition = _cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(tempposition.x, tempposition.y, 0);
        _imago.enabled = player.isGrounded;
    }
}
