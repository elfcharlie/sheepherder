using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawner : MonoBehaviour
{
    public GameObject sheep;
    public int sheepAmount;
    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;
    private int sheepNumber = 1;

    // Start is called before the first frame update
    void Awake()
    {
        xMin = transform.position.x - gameObject.GetComponent<RectTransform>().rect.width / 2;
        xMax = xMin + gameObject.GetComponent<RectTransform>().rect.width;
        yMin = transform.position.y - gameObject.GetComponent<RectTransform>().rect.height / 2;
        yMax = yMin + gameObject.GetComponent<RectTransform>().rect.height;

        InstantiateSheep(sheepAmount);
    }
    public void InstantiateSheep(int sheepAmount){
        for (int i = 0; i < sheepAmount; i++) 
        {
            Transform transform = GameObject.Find("Sheep").transform;
            float xPos = Random.Range(xMin, xMax);
            float yPos = Random.Range(yMin, yMax);
            Vector3 position = new Vector3(xPos, yPos, 0f);
            var currentInstance = Instantiate(sheep, position, Quaternion.identity, transform);
            currentInstance.name = ("Sheep" + sheepNumber);
            sheepNumber += 1;
        }
    }
}
