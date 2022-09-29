using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    private List<Collider2D> colliders = new List<Collider2D>();
    private int sheepInGoal;
    // Start is called before the first frame update
    void Start()
    {
        sheepInGoal = 0;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if(!colliders.Contains(other) && other.gameObject.tag == "Sheep")
        {
            colliders.Add(other); 
            sheepInGoal += 1;     
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(colliders.Contains(other) && other.gameObject.tag == "Sheep")
        {
            colliders.Remove(other);
            sheepInGoal -= 1;
        }
    }
    public int GetSheepInGoal()
    {
        return sheepInGoal;
    }
}
