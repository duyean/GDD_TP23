using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FilledSlot : MonoBehaviour
{
    public TextMeshProUGUI filledText;
    public string filledValue;
    public Data question;

    // Start is called before the first frame update
    void Start()
    {
        filledText = GetComponentInChildren<TextMeshProUGUI>();
    }
}
