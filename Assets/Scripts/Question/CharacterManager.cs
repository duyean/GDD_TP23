using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{
    public bool isMale = true;
    public string pet = "Cat";

    [SerializeField]
    GameObject menu;

    // change pet 
    public void ChangePet(string petName)
    {
        pet = petName;
    }

    // change gender for character
    public void ChangeGender(bool maleOrNo)
    {
         isMale = maleOrNo;
    }

    public void OpenCustomizeMenu()
    {
        menu.SetActive(true);
    }

    public void CloseCustomizeMenu()
    {
        menu.SetActive(false);
    }
}
