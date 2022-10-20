using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DogController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Joystick joyStick;
    public Animator anim;
    private Vector2 movement;
    private HighScoreManager highScoreManager;
    private float idleTimer = 0;


    // Start is called before the first frame update
    void Awake()
    {
        if(SceneManager.GetActiveScene().buildIndex != 0){
            highScoreManager = GameObject.FindWithTag("HighScoreManager").GetComponent<HighScoreManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {

        /*movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        */
        movement.x = joyStick.Horizontal;
        movement.y = joyStick.Vertical;
        if(movement.x != 0)
        {
            anim.SetBool("isIdle", false);
            anim.SetBool("isLying", false);
            anim.SetFloat("X", movement.x);
            anim.SetBool("isRunning", true);
            idleTimer = 4f;
        }
        else if (idleTimer > 0)
        {
            idleTimer -= Time.deltaTime;
            anim.SetBool("isRunning", false);
            anim.SetBool("isIdle", true);
        }
        else if (idleTimer <= 0)
        {
            anim.SetBool("isIdle", false);
            anim.SetBool("isLying", true);
        }
        
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    public void StopMovement()
    {
        moveSpeed = 0f;
        highScoreManager.StopTimer();

    }
    public void StartMovement()
    {
        moveSpeed = 5f;
        highScoreManager.StartTimer();
    }
    public Vector2 GetMovement()
    {
        return movement;
    }
    void OnTriggerStay2D (Collider2D collider)
    {
        Debug.Log("INSIDE!!!!!!!");
    }
}
