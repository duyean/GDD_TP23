using UnityEngine;
using UnityEngine.UI;

public class MainMenuBackgroundFade : MonoBehaviour
{
    [SerializeField] GameObject[] BGImages;

    public int BGNum = 0;
    public float elapsedTime = 8;

    private Color bgCol;
    private Color tempColor;
    public void Update()
    {
        elapsedTime -= Time.deltaTime;
        if (elapsedTime <= 0)
        {
            fade(BGNum);
        }
    }

    public void fade(int bg)
    {
        bgCol = BGImages[bg].GetComponent<Image>().color;
        if (bgCol.a >= 0)
        {
            tempColor = bgCol;
            tempColor.a -= Time.deltaTime * 0.3f;
            BGImages[bg].GetComponent<Image>().color = tempColor;
        }
        else if (BGImages[bg].GetComponent<Image>().color.a <= 0)
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
                    tempColor = bgCol;
                    tempColor.a = 1;
                    BGImages[i].GetComponent<Image>().color = tempColor;
                }
                BGNum = 0;
            }
        }
    }
}
