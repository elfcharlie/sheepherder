using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatBubbleSize : MonoBehaviour
{  
    private SpriteRenderer spriteRenderer;
    private GameObject textObject;
    private float oldTextObjectSizeY;
    private float newTextObjectSizeY;
    private float scalingFactor;
    private float spriteHeight;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        textObject = transform.GetChild(0).gameObject;
        oldTextObjectSizeY = textObject.GetComponent<TextMeshProUGUI>().preferredHeight;
        newTextObjectSizeY = oldTextObjectSizeY;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSize();
    }
    public void UpdateSize()
    {
        
        newTextObjectSizeY = textObject.GetComponent<TextMeshProUGUI>().preferredHeight;
        scalingFactor = newTextObjectSizeY / oldTextObjectSizeY;
        if(newTextObjectSizeY < 7){
            spriteHeight = 1.45f;    
        } else if (newTextObjectSizeY > 7f && newTextObjectSizeY < 12f)
        {
            spriteHeight = 2f;
        } else if (newTextObjectSizeY > 13f && newTextObjectSizeY < 18f)
        {
            spriteHeight = 2.6f;
        } else if (newTextObjectSizeY > 20f && newTextObjectSizeY < 25f)
        {
            spriteHeight = 3.17f;
        }
        spriteRenderer.size = new Vector2(4f, spriteHeight);
        // FIX SPRITE RENDERER SIZE
        oldTextObjectSizeY = newTextObjectSizeY;
    }
}
