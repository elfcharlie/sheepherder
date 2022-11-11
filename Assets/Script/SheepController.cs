using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepController : MonoBehaviour
{
    private float moveSpeed = 2f;
    public Rigidbody2D rb;
    public LayerMask stopMovementLayerMask;
    private Transform playerTransform;
    private float distanceToPlayer;
    private Vector2 directionToPlayer;
    private Vector2 playerDirection;
    private Vector2 transposedPlayerDirection;
    private Vector2 perpendicularToPlayerDirection;
    private Vector2 moveDirection;
    private Vector2 roamDirection;
    private Vector2 otherSheepDirection;
    private GameObject[] otherSheep;
    private float otherSheepDistance = 10;
    private List<GameObject> moveTowardsSheep = new List<GameObject>();
    private float roamTimer;

    void Start()
    {
        otherSheep = GameObject.FindGameObjectsWithTag("Sheep");
        roamTimer = Random.Range(2,8);
        playerTransform = GameObject.FindWithTag("Player").transform;
        SetNewRoamDirection();
    }
    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        directionToPlayer = (transform.position - playerTransform.position).normalized;
        playerDirection = playerTransform.gameObject.GetComponent<DogController>().GetMovement().normalized;
        moveDirection = (directionToPlayer + playerDirection);
        foreach(GameObject sheep in otherSheep){
            if (sheep.GetInstanceID() != gameObject.GetInstanceID() && moveTowardsSheep.Count <= 2 
                && Vector2.Distance(transform.position, sheep.transform.position) < 10f
                && Vector2.Distance(transform.position, sheep.transform.position) > 2f){
                moveTowardsSheep.Add(sheep);
            }
        }
    }

    void FixedUpdate()
    {
        if (distanceToPlayer < 4f) // Get herded
        {
            //MoveToghether();
            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
        } 
        else 
        {
            roamTimer -= Time.fixedDeltaTime;
            if (roamTimer > 0)
            {
                // Wander around
                rb.MovePosition(rb.position + roamDirection * moveSpeed/5 * Time.fixedDeltaTime);
                StayTogether(10);
            } else if (roamTimer < -4) {
                // Stand still 
                roamTimer = Random.Range(2,8);
                SetNewRoamDirection();
            }
        }
    }

    private void MoveToghether()
    {
        perpendicularToPlayerDirection = Vector2.Perpendicular(playerDirection);
        if (Vector2.SignedAngle(playerDirection, directionToPlayer) > 0)
        {
            perpendicularToPlayerDirection *= -1;
        }
        moveDirection += 0.2f * perpendicularToPlayerDirection;
        transposedPlayerDirection = new Vector2(playerDirection.y, playerDirection.x);
    }

    private void SetNewRoamDirection()
    {
        moveTowardsSheep.Clear();
        roamDirection.Set(Random.Range(-1f,1f), Random.Range(-1f,1f));
        roamDirection = roamDirection.normalized;
        // SHOULD BELOW BE DONE EACH FRAME?
        /*foreach (GameObject sheep in moveTowardsSheep){
            Vector2 otherSheepDirection = (transform.position - sheep.transform.position).normalized;
            otherSheepDistance = (transform.position - sheep.transform.position).magnitude;
            moveDirection += otherSheepDirection;
            roamDirection = roamDirection - otherSheepDirection;
        }*/
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, 3 * ( new Vector3(moveDirection.x, moveDirection.y, 0f)));
    }

    private void StayTogether(float speedCoeff)
    {
        foreach(GameObject sheep in moveTowardsSheep)
        {
            otherSheepDirection = transform.position - sheep.transform.position;
            otherSheepDistance = otherSheepDirection.magnitude;
            otherSheepDirection = otherSheepDirection.normalized;
            rb.MovePosition(rb.position - otherSheepDirection * otherSheepDistance / speedCoeff * Time.fixedDeltaTime);
        }
    }
}
