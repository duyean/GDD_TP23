using UnityEngine;
using UnityEngine.UI;

public class MainMenuBackgroundFade : MonoBehaviour
{
    [SerializeField] GameObject[] BGImages;

    private int BGNum = 0;
    private float elapsedTime = 8;
    private float refreshTime = 5;

    private Color bgCol;
    private Color tempColor;
    public void Update()
    {
        elapsedTime -= Time.deltaTime;
        // when elapsed time is over fade to the next BGNum which is set on the fade function
        if (elapsedTime <= 0)
        {
            fade(BGNum);
        }
    }

    // function turns the background image on top transparent and sets bgNum to the next image
    public void fade(int bg)
    {
        bgCol = BGImages[bg].GetComponent<Image>().color;
        // checking if alpha > 0 : 
        if (bgCol.a > 0)
        {
            tempColor = bgCol;
            tempColor.a -= Time.deltaTime * 0.3f;
            BGImages[bg].GetComponent<Image>().color = tempColor;
        }
        else if (BGImages[bg].GetComponent<Image>().color.a <= 0)
        {
            // sets the BGNum to the next index and resets the elapsed time
            if (BGNum <= 2)
            {
                BGNum += 1;
                elapsedTime = refreshTime;
            }
            if (BGNum == 3)
            {
                BGNum = 0;
                elapsedTime = refreshTime;

                for (int i = 0; i <= 2; i++)
                {
                    tempColor = bgCol;
                    tempColor.a = 1;
                    BGImages[i].GetComponent<Image>().color = tempColor;
                }
                BGNum = 0;
            }
        }
    }
}
