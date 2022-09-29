using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarScore : MonoBehaviour
{
    private List<Image> stars = new List<Image>();
    //public GameObject star1;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++){
            stars.Add(transform.GetChild(i).gameObject.GetComponent<Image>());
            Color starColor = stars[i].color;
            starColor.a = 0.2f;
            stars[i].color = starColor;
        }
        Debug.Log("START");
    }

    public IEnumerator SetScore(int score){

        WaitForSeconds wait1 = new WaitForSeconds(1f);
        for (int i = 0; i < score; i++){
            Color starColor = transform.GetChild(i).gameObject.GetComponent<Image>().color;
            starColor.a = 0.8f;
            stars[i].color = starColor;
            yield return wait1;
        }
    }
}
