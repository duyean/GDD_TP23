using UnityEngine;

public class CharacterCheck : MonoBehaviour
{
    public GameObject GuyCharacter;
    public GameObject GirlCharacter;

    GameObject instantiatedCharacter;

    void Awake()
    {
        CheckGender();
        CheckPet();
    }


    public void CheckGender()
    {
        if (CharacterManager.Instance.isMale == true)
        {
            instantiatedCharacter = Instantiate(GuyCharacter);
        }
        else
        {
            instantiatedCharacter = Instantiate(GirlCharacter);
        }

    }

    public void CheckPet()
    {
        if (CharacterManager.Instance.pet == "Cat")
        {
            instantiatedCharacter.GetComponent<Pets>().pets[0].SetActive(true);
        }
        if (CharacterManager.Instance.pet == "Rabbit")
        {
            instantiatedCharacter.GetComponent<Pets>().pets[1].SetActive(true);
        }
        if (CharacterManager.Instance.pet == "Frog")
        {
            instantiatedCharacter.GetComponent<Pets>().pets[2].SetActive(true);
        }
    }
}
