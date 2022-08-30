using TMPro;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{
    public bool isMale = true;
    public string pet = "Cat";
    public TextMeshProUGUI indicationText;

    [SerializeField] private GameObject menu;

    // change pet 
    public void ChangePet(string petName)
    {
        pet = petName;
        indicationText.text = ReturnGender(isMale) + " And " + petName;
    }

    // change gender for character
    public void ChangeGender(bool maleOrNo)
    {
        isMale = maleOrNo;
        indicationText.text = ReturnGender(maleOrNo) + " And " + pet;
    }

    public string ReturnGender(bool isMale)
    {
        if (isMale)
        {
            return "Boy";
        }
        return "Girl";
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
