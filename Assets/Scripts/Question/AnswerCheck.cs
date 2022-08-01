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
	public TextMeshProUGUI questionText;
	public TextMeshProUGUI wrongAnswerText;
	public TextMeshProUGUI correctText;
	public QuestionCounter questionCounter;
	public QuestionLoader questionLoader;
	public Data question;
	public List<string> excludedSubjects;
	public bool correct;
	public bool sort;
	public Button answerButton;
	public Button closeButton;
	public Canvas questionCanvas;
	public GameObject questionFinishedCanvas;
	public GameObject controlButtons;

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
		Button checkButton = answerButton.GetComponent<Button>();
		Button canvasCloseButton = closeButton.GetComponent<Button>();
		checkButton.onClick.AddListener(CheckAnswer); //When button is clicked, calls the CheckAnswer function
		canvasCloseButton.onClick.AddListener(CloseCanvas); //When button is clicked, calls the CloseCanvas function
		correctText.color = Color.green;
		wrongAnswerText.color = Color.red;
		wrongAnswerText.enabled = false;
	}

	void CheckAnswer()
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
			Debug.Log(!excludedSubjects.Contains(question.subject));
			if (!excludedSubjects.Contains(question.subject))
			{
				//Then check values against answer array
				//if all values align, then set boolean to true
				if (sortedValues.SequenceEqual(answers))
				{
					correct = true;
					questionFinishedCanvas.SetActive(true);
					wrongAnswerText.enabled = false;

				}
			}
			else if (sortedValues.Values.Sum(x => int.Parse(x)) == question.finalAnswers.Sum(x => int.Parse(x)) && sortedValues.Values.Count > 1)
			{
				correct = true;
				questionFinishedCanvas.SetActive(true);
				wrongAnswerText.enabled = false;
			}

			if (!correct)
			{
				sortedValues.Clear();
				wrongAnswerText.enabled = true;
			}
		}
		if(correct)
        {
			coin.SetActive(false);
			questionCounter.answerToFinish--;
			spawnPlatform(questionLoader.question);
			checkWin(questionCounter.answerToFinish);
		}
	}

	

	void CloseCanvas()
    {
		questionFinishedCanvas.SetActive(false);
		controlButtons.SetActive(true); //Enables movement buttons when second canvas is closed

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
		if(questionCanvas.isActiveAndEnabled)
        {
			controlButtons.SetActive(false); //Disables movement buttons when question canvas opened
        }
        

		//Obsolete??

        //      if (!correct)
        //{
        //	//Sort dictionary
        //	SortValues();
        //	//Get values from answer list
        //	for (int i = 0; i < question.finalAnswers.Count; i++)
        //	{
        //		if (!answers.ContainsValue(question.finalAnswers[i]))
        //		{
        //			answers.Add(new string("Slot (" + i + ")"), question.finalAnswers[i]);
        //		}
        //	}
        //	Debug.Log(!excludedSubjects.Contains(question.subject));
        //	if (!excludedSubjects.Contains(question.subject))
        //	{
        //		//Then check values against answer array
        //		//if all values align, then set boolean to true
        //		if (sortedValues.SequenceEqual(answers))
        //		{
        //			correct = true;
        //		}
        //	}
        //	else if (sortedValues.Values.Sum(x => int.Parse(x)) == question.finalAnswers.Sum(x => int.Parse(x)) && sortedValues.Values.Count > 1)
        //	{
        //		correct = true;
        //	}

        //	if (!correct)
        //	{
        //		sortedValues.Clear();
        //	}
        //}

        //      else
        //{
        //	UnFreeze();
        //	coin.SetActive(false);
        //	questionCounter.answerToFinish--;
        //	spawnPlatform(questionLoader.question);
        //	checkWin(questionCounter.answerToFinish);
        //}
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

	//Obsolete

	//	void UnFreeze()
	//{
	//	//Enable joypad
	//}

}
