using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FilledSlot : MonoBehaviour
{
    public TextMeshProUGUI filledText;
    public string filledValue;
    public int slotNumber;
    public Data question;
    // Start is called before the first frame update
    void Start()
    {
        filledValue = question.filledValues[slotNumber];
        filledText = GetComponentInChildren<TextMeshProUGUI>();
        filledText.SetText(filledValue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
