using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterManager : Singleton<CharacterManager>
{
    public bool isMale = true;
    public string pet = "Cat";
    public TextMeshProUGUI indicationText;


    [SerializeField]
    GameObject menu;

    // change pet 
    public void ChangePet(string petName)
    {
        pet = petName;
        indicationText.text = returnGender(isMale) + " And " + petName;
        // add dingdong / correct sound effect here
    }

    // change gender for character
    public void ChangeGender(bool maleOrNo)
    {
        isMale = maleOrNo;
        indicationText.text = returnGender(maleOrNo) + " And " + pet;
        // add sound effect here
    }

    public string returnGender(bool isMale)
    {
        if (isMale)
        {
            return "Boy";
        }
        else
        {
            return "Girl";
        }
    }

    public void OpenCustomizeMenu()
    {
        menu.SetActive(true);
    }

    public void CloseCustomizeMenu()
    {
        menu.SetActive(false);
    }

    private void Update()
    {
        
    }
}
