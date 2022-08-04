using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuBackgroundFade : MonoBehaviour
{
    [SerializeField]
    GameObject[] BGImages;

    int BGNum = 0;

    float a;
    float elapsedTime = 8;
    private void Start()
    {

    } 

    public void Update()
    {

        elapsedTime -= Time.deltaTime;
        if (elapsedTime<=0)
        {
            fade(BGNum);
        }

        
    }

    public void fade(int bg)
    {
        Color bgCol = BGImages[bg].GetComponent<Image>().color;
        if (bgCol.a >= 0 )
        {
            Color tempColor = bgCol;
            tempColor.a -= Time.deltaTime * 0.3f;
            BGImages[bg].GetComponent<Image>().color = tempColor;
        }
        else if (BGImages[bg].GetComponent<Image>().color.a<=0)
        {
            if (BGNum <= 2)
            {
                BGNum += 1;
                elapsedTime = 5f;
            }
            if (BGNum == 3)
            {
                BGNum = 0;
                elapsedTime = 5f;

                for (int i = 0; i <= 2; i++)
                {
                    Color tempColor = bgCol;
                    tempColor.a = 1;
                    BGImages[i].GetComponent<Image>().color = tempColor;
                }
                BGNum = 0;

            }

        }
    }
    


}
