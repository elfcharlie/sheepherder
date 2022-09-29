using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float runSpeed;
    public float jumpForce;
    private float horizontalDirection;
    public Animator anim;
    private bool facingRight = false;
    private bool isJumping = false;
    private float jumpingTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update() {
        horizontalDirection = Input.GetAxisRaw("Horizontal");
        if (horizontalDirection == 1 && !facingRight){
            Flip();
            facingRight = true;
            anim.SetBool("IsIdle", false);
        } else if (horizontalDirection == -1 && facingRight) {
            Flip();
            facingRight = false;
            anim.SetBool("IsIdle", false);
        } else {
            anim.SetBool("IsIdle", true);
        }
        if (Input.GetKeyDown("space") && IsGrounded()){
            isJumping = true;
            anim.SetBool("IsJumping", isJumping);
        } else if (IsGrounded() && jumpingTime > 0.5f){
            isJumping = false;
            anim.SetBool("IsJumping", isJumping);
        }
        
        
        anim.SetFloat("Speed", Mathf.Abs(horizontalDirection));
        
    }

    void FixedUpdate() {
        rb.velocity = new Vector2(horizontalDirection * runSpeed, rb.velocity.y);
        if (isJumping && IsGrounded()){
            rb.AddForce(transform.up * jumpForce);
            jumpingTime += Time.fixedDeltaTime;
        }
    }
    private void Flip(){
        Vector3 theScale = transform.localScale;
	    theScale.x *= -1;
		transform.localScale = theScale;
    }
    
    private bool IsGrounded(){
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    
    /*void OnMouseDown(){
        if (IsGrounded()){
            rb.AddForce(transform.up * jumpForce);
            anim.SetBool("IsJumping", true);
        }
    }*/


}
