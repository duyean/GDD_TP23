using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
public class AnswerCheck : MonoBehaviour
{
	public GameObject coin;
	public List<GameObject> Slots;
	public TextMeshProUGUI correctWrongText;
	public QuestionCounter questionCounter;
	public QuestionLoader questionLoader;
	public Data question;
	public bool correct;
	public bool sort;
	
	[SerializeField]
	GameObject[] tileMapSpawns;

	[SerializeField]
	GameObject treasure;

	private List<string> slotValues = new List<string>();
	private List<string> answers = new List<string>();
	//To do:
	//Fix Slot values not resetting

	// Start is called before the first frame update
	void Start()
	{
		correct = false;
	}

	public void GetValues(string value)
    {
		//Get values from slot list
		slotValues.Add(value);
        if (sort)
        {
			slotValues.Sort();
		}
	}

	public void RemoveValues(string value)
	{
		//Get values from slot list
		slotValues.Remove(value);
		if (sort)
		{
			slotValues.Sort();
		}
	}

	// Update is called once per frame
	void Update()
	{
		//Get values from answer list
        foreach (string answer in question.finalAnswers)
        {
            if (answer != null)
            {
                if (!answers.Contains(answer))
				{
					answers.Add(answer);
				}
            }
        }
		//Then check values against answer array
		//if all values align, then set boolean to true
		if (slotValues.SequenceEqual(answers))
		{
			correct = true;
		}
        //else set to false;
        else
        {
            correct = false;
        }

        if (correct)
		{
			UnFreeze();
			coin.SetActive(false);
			questionCounter.answerToFinish--;
			spawnPlatform(questionLoader.question);
			checkWin(questionCounter.answerToFinish);
		}
	}

	//functions to add
	public void spawnPlatform(int questionNum)
	{
		tileMapSpawns[questionNum - 1].SetActive(true);
	}

	public void checkWin(int answerToFinish)
	{
		if (answerToFinish == 0)
		{
			treasure.GetComponent<levelWin>().win = true;
		}
	}

		void UnFreeze()
	{
		//Enable joypad
	}

}
