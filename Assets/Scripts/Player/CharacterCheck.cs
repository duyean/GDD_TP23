using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCheck : MonoBehaviour
{
    public GameObject GuyCharacter;
    public GameObject GirlCharacter;
    public GameObject Cat;
    public GameObject Frog;

    void Awake()
    {
        CheckGender();
        CheckPet();
    }

    public void CheckGender()
    {
        if (CharacterManager.Instance.isMale == true)
        {
            Instantiate(GuyCharacter);
        }
        else
        {
            Instantiate(GirlCharacter);
        }

    }
    public void CheckPet()
    {
        if(CharacterManager.Instance.pet == "Cat")
        {
            Instantiate(Cat);
            
        }
       // CharacterManager.Instance.pet = "Rabbit";
       // {
      //      Instantiate(Rabbit);
      //  }
        if(CharacterManager.Instance.pet == "Frog")
        {
            Instantiate(Frog);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
