using System.Collections;
using System.Collections.Generic;
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
	public Dictionary<string, string> answerDictionary = new Dictionary<string, string>();

	[SerializeField]
	GameObject[] tileMapSpawns;

	[SerializeField]
	GameObject treasure;

	private List<string> slotValues;
	private List<string> answers;
	//To do:
	//Write logic to get and check slot values

	// Start is called before the first frame update
	void Start()
	{
		correct = false;
		for (int i = 0; i < question.answers.Count; i++)
		{
			answerDictionary.Add(new string("Answer" + i), question.answers[i]);
		}
	}

	// Update is called once per frame
	void Update()
	{
		//If statements to check if the item values add up to the final answer
		//int.Parse is to convert the parameter value in the brackets into int
		if (question.subject == "Math")
		{
			//Get values from slot list
			foreach(GameObject slot in Slots)
            {
                slotValues.Add(slot.GetComponent<Slot>().value);
            }
			//Get values from answer array
			//Then check values against answer array
			foreach (string answer in question.answers)
            {
				answers.Add(answer);
            }
			//if all values align, then set boolean to true
			//if (int.Parse(slotValues[]) == int.Parse(answers[]))
			//{
			//	correct = true;
            //}
            //else set to false;
		    //else
			//{
				//correct = false;
			//}
		}

		if (question.subject == "English")
		{
			//if (firstSlot.GetComponent<Slot>().value + secondSlot.GetComponent<Slot>().value == question.finalAnswer)
			//{
			//    questionText.SetActive(true);
			//    questionLoader.question++;
			//    correct = true;
			//}
			//else if (firstSlot.GetComponent<Slot>().value + secondSlot.GetComponent<Slot>().value != question.finalAnswer)
			//{
			//    correct = false;
			//}
			//if (firstSlot.GetComponent<Slot>().value == question.finalAnswers[0] && secondSlot.GetComponent<Slot>().value == question.finalAnswers[1] && thirdSlot.GetComponent<Slot>().value == question.finalAnswers[2])
			//{
			//	spawnPlatform(questionLoader.question);
			//	questionLoader.question++;
			//	correct = true;

			//}
			//else
			//{
			//	correct = false;
			//}
		}

		if (correct)
		{
			UnFreeze();
			coin.SetActive(false);
			questionCounter.answerToFinish--;
		}
	}

	//functions to add
	public void spawnPlatform(int questionNum)
	{
		tileMapSpawns[questionNum - 1].SetActive(true);
	}

	public void checkWin(int level, int question)
	{
		if (question - level == -1)
		{
			treasure.GetComponent<levelWin>().win = true;
		}
	}

	void UnFreeze()
	{
		//Enable joypad
	}

}
