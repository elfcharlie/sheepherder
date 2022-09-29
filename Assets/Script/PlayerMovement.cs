using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb; 
    public Animator anim;
    public Transform movePoint;
    public LayerMask stopsMovement;
    
    private Vector2 movement;
    private Transform playerCenter;
    private bool isRunning = false;
    

    void Start() 
    {
        movePoint.parent = null;
        playerCenter = transform.parent.transform;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        playerCenter.position = Vector3.MoveTowards(playerCenter.position, movePoint.position, moveSpeed * Time.deltaTime);
        
        if(Vector3.Distance(playerCenter.position, movePoint.position) <= 0.05f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), 0.2f, stopsMovement))
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                }
                anim.SetFloat("Horizontal", movement.x);
                anim.SetFloat("Vertical", 0);
            } else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), 0.2f, stopsMovement))
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                }
                anim.SetFloat("Horizontal", 0);
                anim.SetFloat("Vertical", movement.y);
            } else if(movement.sqrMagnitude > 0) // Handling animations
            {
                anim.SetFloat("Horizontal", movement.x);
                anim.SetFloat("Vertical", movement.y);
            }
            anim.SetFloat("Speed", movement.sqrMagnitude);
        }

        // Running
        if (Input.GetKey(KeyCode.K) && isRunning == false)
        {
            isRunning = true;
            moveSpeed *= 2f;
            anim.SetBool("IsRunning", isRunning);
        } else if (Input.GetKeyUp(KeyCode.K) && isRunning == true)
        {
            isRunning = false;
            moveSpeed /= 2f;
            anim.SetBool("IsRunning", isRunning);
        }
        // Jumping
        if (Input.GetKeyDown(KeyCode.Space)){
            anim.SetTrigger("Jump");
        }
    }

    public void HasLanded()
    {
        
       // anim.SetTrigger("Jump");
        Debug.Log("HASLANDED");
    }
    void FixedUpdate(){
        //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
