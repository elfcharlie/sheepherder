using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObject : MonoBehaviour
{
    public Transform movePoint;
    public LayerMask moveObject;
    public Transform playerCenter;
    private GameObject collidingGameObject;

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(playerCenter.position, movePoint.position) <= 0.05f)
        {
        if (Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), 0.2f, moveObject)){
            //Call object move method
            collidingGameObject = Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), 0.2f, moveObject).gameObject;
            collidingGameObject.GetComponent<ObjectMovement>().MoveObject();
        }
        if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), 0.2f, moveObject)){
            //Call object move method
            collidingGameObject = Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), 0.2f, moveObject).gameObject;
            collidingGameObject.GetComponent<ObjectMovement>().MoveObject();
        }
        }   
    }
}
