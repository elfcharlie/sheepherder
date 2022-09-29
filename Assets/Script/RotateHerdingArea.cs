using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHerdingArea : MonoBehaviour
{   
    private Vector2 dogMovement;
    private Vector3 targetVector;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dogMovement = transform.parent.gameObject.GetComponent<DogController>().GetMovement().normalized;
        targetVector = new Vector3(dogMovement.x, dogMovement.y, 0f);
        transform.rotation = Quaternion.LookRotation(targetVector, Vector3.forward);
    }
}
