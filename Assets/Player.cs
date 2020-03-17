using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isGrounded = true;
    Cursor cursor;
    public float Speed = 5f;
    public float JumpHeight = 2f;
    public float GroundDistance = 0.2f;
    public float DashDistance = 5f;
    public LayerMask Ground;

    private Rigidbody2D _body;
    public bool[] _isGroundedPre;
    public Transform[] _groundCheckers;
    private Vector2 direction = Vector2.zero;
    private float dashTime;
    public float startDashTime;
    public GameObject projectile;

    public int dashChargesMax = 3;
    public int dashCharges;

    public Animator camAnim;
    public ParticleSystem boost;
    public Transform barrel;
    public Transform dashUIContainer;
    public Transform[] dashUICharges;


    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
        dashCharges = dashChargesMax;

        dashUICharges = new Transform[3];
        for (var i=0; i<3; i++)
        {
            dashUICharges[i] = dashUIContainer.GetChild(i);
        }
    }

    void Update()
    {
        //_isGrounded = Physics2D.OverlapCircle(_groundChecker.position, GroundDistance, Ground);

       if(direction == Vector2.zero)
       {
            if (Input.GetKeyDown(KeyCode.A) && !_isGroundedPre[1] && !_isGroundedPre[3] && !_isGroundedPre[0] && dashCharges >= 1)
            {
                direction = Vector2.left;
                dashUICharges[dashCharges - 1].gameObject.SetActive(false);
                dashCharges--;
                camAnim.SetTrigger("New Trigger 0");
                boost.transform.eulerAngles = new Vector3(0, 0, 90);
                boost.Play();
            }
            else if (Input.GetKeyDown(KeyCode.D) && !_isGroundedPre[2] && !_isGroundedPre[3] && !_isGroundedPre[0] && dashCharges >= 1)
            {
                direction = Vector2.right;
                dashUICharges[dashCharges - 1].gameObject.SetActive(false);
                dashCharges--;
                camAnim.SetTrigger("New Trigger 0");
                boost.transform.eulerAngles = new Vector3(0, 0, -90);
                boost.Play();
            }
            else if (Input.GetKeyDown(KeyCode.W) && !_isGroundedPre[3] && !_isGroundedPre[1] && !_isGroundedPre[2] && dashCharges >= 1)
            {
                direction = Vector2.up;
                dashUICharges[dashCharges - 1].gameObject.SetActive(false);
                dashCharges--;
                camAnim.SetTrigger("New Trigger 0");
                boost.transform.eulerAngles = new Vector3(0, 0, 0);
                boost.Play();
            }
            else if (Input.GetKeyDown(KeyCode.S) && !_isGroundedPre[0] && !_isGroundedPre[1] && !_isGroundedPre[2] && dashCharges >= 1)
            {
                direction = Vector2.down;
                dashUICharges[dashCharges - 1].gameObject.SetActive(false);
                dashCharges--;
                camAnim.SetTrigger("New Trigger 0");
                boost.transform.eulerAngles = new Vector3(0, 0, 180);
                boost.Play();
            }
       }
       else
       {
            if (dashTime <= 0)
            {
                direction = Vector2.zero;
                dashTime = startDashTime; 
            }
            else
            {
                dashTime -= Time.deltaTime;
                _body.velocity = direction * DashDistance;             
            }
            
       }

       if (Input.GetMouseButtonDown(0) && isGrounded)
       {
           camAnim.SetTrigger("New Trigger 0");
           GameObject bullet = Instantiate(projectile, barrel.position, Quaternion.identity) as GameObject;
           Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
           bullet.GetComponent<Rigidbody2D>().AddForce(direction * 5);
       }
    }

    void FixedUpdate()
    {
        if (_body.gravityScale == 0 && direction == Vector2.zero) //write a different check here, this is only temp
        {
            _body.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        for (var i = 0; i < _isGroundedPre.Length; i++)
        {
            _isGroundedPre[i] = Physics2D.OverlapCircle(_groundCheckers[i].position, GroundDistance, Ground);
            if (_isGroundedPre[i])
                break;
        }
        isGrounded = true;
        StartCoroutine(RechargeDash());
        _body.gravityScale = 0;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        for (var i = 0; i < _isGroundedPre.Length; i++)
        {
            _isGroundedPre[i] = false;
        }
        isGrounded = false;
        _body.gravityScale = 3;
    }

    private IEnumerator RechargeDash()
    {
        yield return new WaitForSeconds(0.1f);
        if (dashCharges < dashChargesMax)
            dashCharges = dashChargesMax;
        for (var i = 0; i < 3; i++)
        {
            dashUICharges[i].gameObject.SetActive(true);
        }
    }
}
