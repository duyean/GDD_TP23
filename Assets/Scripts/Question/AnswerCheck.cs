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

	private Dictionary<string, string> slotValues = new Dictionary<string, string>();
	private Dictionary<string, string> sortedValues = new Dictionary<string, string>();
	private Dictionary<string, string> answers = new Dictionary<string, string>();
	//To do:
	//Fix Slot values not resetting

	// Start is called before the first frame update
	void Start()
	{
		correct = false;
	}

	public void SortValues()
    {
		//From https://www.tutorialsteacher.com/linq/linq-sorting-operators-orderby-orderbydescending#orderby
		var orderByKey = from s in slotValues
						 orderby s.Key
						 select s;
		foreach (var item in orderByKey)
        {
			sortedValues.TryAdd(item.Key, item.Value);
        }
	}

	public void AddValues(string key, string value)
    {
		//Get values from slot list
		slotValues.Add(key, value);
		sortedValues.Add(key, value);
	}

	public void RemoveValues(string key)
	{
		//Get values from slot list
		slotValues.Remove(key);
	}

	// Update is called once per frame
	void Update()
	{
        if (!correct)
		{
			//Sort dictionary
			SortValues();
			//Get values from answer list
			for (int i = 0; i < question.finalAnswers.Count; i++)
			{
				if (!answers.ContainsValue(question.finalAnswers[i]))
				{
					answers.Add(new string("Slot (" + i + ")"), question.finalAnswers[i]);
				}
			}
			//Then check values against answer array
			//if all values align, then set boolean to true
			if (sortedValues.SequenceEqual(answers))
			{
				correct = true;
			}
			//else set to false;
			else
			{
				correct = false;
				sortedValues.Clear();
			}
		}

        else
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
