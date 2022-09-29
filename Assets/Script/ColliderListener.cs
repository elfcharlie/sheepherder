using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class ColliderListener : MonoBehaviour
 {
    private GameObject[] levelObjects;
    private Collider2D[] levelColliders;

    void Awake()
    {
        levelObjects = GameObject.FindGameObjectsWithTag("Level");
        for(int i = 0; i<levelObjects.Length; i++){
            levelColliders[i] = levelObjects[i].GetComponent<BoxCollider2D>();
        }

         Collider2D collider = GetComponentInChildren<Collider2D>();
         if (collider.gameObject != gameObject)
         {
             ColliderBridge cb = collider.gameObject.AddComponent<ColliderBridge>();
             cb.Initialize(this);
         }
     }
     public void OnCollisionEnter2D(Collision2D collision)
     {
         // Do your stuff here
     }
     public void OnTriggerEnter2D(Collider2D other)
     {
         // Do your stuff here
     }
 }
