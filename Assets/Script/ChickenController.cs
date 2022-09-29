using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenController : MonoBehaviour
{
    private float moveSpeed = 1f;
    public Rigidbody2D rb;

    private Transform playerTransform;
    private float distanceToPlayer;
    private Vector2 directionToPlayer;
    private Vector2 roamDirection;
    private float roamTimer;

    // Start is called before the first frame update
    void Start()
    {
        roamTimer = Random.Range(2,8);
        playerTransform = GameObject.FindWithTag("Player").transform;
        SetNewRoamDirection();
    }
    void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        directionToPlayer = (transform.position - playerTransform.position).normalized;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (distanceToPlayer < 2f) // Get herded
        {
            rb.MovePosition(rb.position + directionToPlayer * moveSpeed * Time.fixedDeltaTime);
        } 
        else 
        {
            roamTimer -= Time.fixedDeltaTime;
            if (roamTimer > 0)
            {
                // Wander around
                rb.MovePosition(rb.position + roamDirection * moveSpeed/2 * Time.fixedDeltaTime);
            } else if (roamTimer < -4) {
                // Stand still 
                roamTimer = Random.Range(2,8);
                SetNewRoamDirection();
            }
        }
    }

    private void SetNewRoamDirection()
    {
        roamDirection.Set(Random.Range(-1f,1f), Random.Range(-1f,1f));
        roamDirection = roamDirection.normalized;
    }
}
