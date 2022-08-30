using UnityEngine;

public class CharacterCheck : MonoBehaviour
{
    public GameObject GuyCharacter;
    public GameObject GirlCharacter;

    GameObject instantiatedCharacter;

    Vector3 camOffset = new Vector3(0, 3, -10);

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
            instantiatedCharacter.GetComponent<PlayerMovement>().pets[0].SetActive(true);
        }
        if (CharacterManager.Instance.pet == "Rabbit")
        {
            instantiatedCharacter.GetComponent<PlayerMovement>().pets[1].SetActive(true);
        }
        if (CharacterManager.Instance.pet == "Frog")
        {
            instantiatedCharacter.GetComponent<PlayerMovement>().pets[2].SetActive(true);
        }
    }
}
