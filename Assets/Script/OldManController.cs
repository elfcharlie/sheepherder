using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class OldManController : MonoBehaviour
{
    public LayerMask playerLayerMask;
    private GameObject oldManSpeechBubble;
    private GameObject oldManTextObject;
    private TextMeshProUGUI oldManText;
    private DogController dogController;
    private GamePlayManager gamePlayManager;
    private Transform playerTransform;
    public float moveSpeed = 0.5f;
    private bool isMoving = false;
    private bool isTalking = false;
    private bool isLevelFinished = false;
    private string introText = "Milo! Please herd the sheep into the sheepfold!";
    private string runMiloText = "Run faster Milo!";
    private string finishText = "Great job Milo!";
    private List<Node> path;
    private Node currentNode = null;
    private Node targetNode = null;
    // Start is called before the first frame update

    void Awake()
    {
        transform.position = GameObject.FindWithTag("OldManStartPosition").transform.position; 
    }
    void Start()
    {
        isLevelFinished = false;
        oldManSpeechBubble = gameObject.transform.GetChild(0).gameObject;
        oldManTextObject = gameObject.transform.GetChild(0).GetChild(0).gameObject;
        oldManText = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        oldManText.text = introText;
        dogController = GameObject.FindWithTag("Player").GetComponent<DogController>();
        gamePlayManager = GameObject.FindWithTag("GamePlayManager").GetComponent<GamePlayManager>();
        playerTransform = GameObject.FindWithTag("Player").transform;
        HideSpeechBubble();
        StopAllCoroutines();
        SayIntroText(introText);
        isMoving = false;
        
    }
    void FixedUpdate()
    {
        if (path != null && targetNode == null)
        {
            targetNode = path[0];
        }
        if(targetNode != null && Mathf.Abs(transform.position.x - targetNode.worldPosition.x) < 0.02 && Mathf.Abs(transform.position.y - targetNode.worldPosition.y) < 0.02)
        {
            targetNode = path[0];

        }

        if (!isTalking && !isLevelFinished && Physics2D.OverlapCircle(transform.position, 0.2f, playerLayerMask))
        {
            StartCoroutine(TypeSentence(runMiloText));
        }
        if (currentNode.walkable && isMoving && !isTalking && !isLevelFinished && path != null && targetNode != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetNode.worldPosition, moveSpeed * Time.fixedDeltaTime);
        }
        else if (isMoving && !isLevelFinished && path == null){
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.fixedDeltaTime);
        }
        else if(!currentNode.walkable)
        {

            float[] directions = new float[2];
            directions[0] = Mathf.Abs(currentNode.worldPosition.x - transform.position.x);
            directions[1] = Mathf.Abs(currentNode.worldPosition.y - transform.position.y);
            float horizontalSign = Mathf.Sign(currentNode.worldPosition.x - transform.position.x);
            float verticalSign = Mathf.Sign(currentNode.worldPosition.y - transform.position.y);
            int maxIndex = directions.ToList().IndexOf(directions.Max());
            
            if (directions[0] == directions[1])
            {
                //transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, path[0].worldPosition.y, 0), -verticalSign * moveSpeed * Time.fixedDeltaTime);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetNode.worldPosition.x, transform.position.y, 0), moveSpeed * Time.fixedDeltaTime);
                Debug.Log("Equal");
            }
            else if (maxIndex == 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetNode.worldPosition.x, transform.position.y, 0), moveSpeed * Time.fixedDeltaTime);
                Debug.Log("SIDE");
            }
            else if(maxIndex == 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, targetNode.worldPosition.y, 0), moveSpeed * Time.fixedDeltaTime);
                Debug.Log("UP");

            }
        }
    }

    IEnumerator TypeSentence (string sentence)
    {
        WaitForSeconds wait01 = new WaitForSeconds(0.01f);
        WaitForSeconds wait2 = new WaitForSeconds(0.1f);
        WaitForSeconds wait4 = new WaitForSeconds(0.1f);
        if(sentence == introText){
            yield return wait2;
        }
        ShowSpeechBubble();
        oldManText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            oldManText.text += letter;
            yield return wait01;
        }
        yield return wait4;
        HideSpeechBubble();
        if(sentence == introText)
        {
            dogController.StartMovement();
        } else if (sentence == finishText){
            gamePlayManager.SetLevelFinished();
        }
    }
    
    private void ShowSpeechBubble()
    {
        oldManSpeechBubble.SetActive(true);
        isMoving = false;
        isTalking = true;
    }

    private void HideSpeechBubble()
    {
        oldManSpeechBubble.SetActive(false);
        isMoving = true;
        isTalking = false;
    }

    private void SayIntroText(string introText)
    {
        dogController.StopMovement();
        StartCoroutine(TypeSentence(introText));
        
    }

    public void Finish()
    {
        // Run towards Player
        moveSpeed = 3f;
        if (Vector3.Distance(transform.position, playerTransform.position) < 2f &&
            !isTalking && !isLevelFinished && Vector3.Distance(transform.position, playerTransform.position) <= 3f)
        {
            StartCoroutine(TypeSentence(finishText));
            isLevelFinished = true;
        }
    }

    public bool GetIsTalking(){
        return isTalking;
    }
    public void SetPath(List<Node> _path)
    {
        path =  _path;
    }
    public void SetNode(Node _currentNode)
    {
        currentNode = _currentNode;
    }
}
