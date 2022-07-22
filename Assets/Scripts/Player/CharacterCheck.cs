using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCheck : MonoBehaviour
{
    public GameObject GuyCharacter;
    public GameObject GirlCharacter;

    GameObject instantiatedCharacter;
    GameObject gameCamera;

    Vector3 camOffset = new Vector3(0, 3, -10);

    void Awake()
    {
        CheckGender();
        CheckPet();
    }

    private void Start()
    {
        gameCamera = GameObject.FindGameObjectWithTag("MainCamera");
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
    // Update is called once per frame
    void Update()
    {
        gameCamera.transform.position = instantiatedCharacter.transform.position + camOffset;
    }
}
