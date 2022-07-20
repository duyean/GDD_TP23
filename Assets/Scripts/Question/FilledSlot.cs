using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FilledSlot : MonoBehaviour
{
    public TextMeshProUGUI filledText;
    public string filledValue;
    // Start is called before the first frame update
    void Start()
    {
        filledText = GetComponentInChildren<TextMeshProUGUI>();
        filledText.SetText(filledValue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
