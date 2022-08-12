using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerCheck : MonoBehaviour
{
	public GameObject coin;
	public List<GameObject> Slots;

	public TextMeshProUGUI questionText;
	public TextMeshProUGUI wrongAnswerText;
	public TextMeshProUGUI correctText;
	public TextMeshProUGUI hintAnswerText;
	public TextMeshProUGUI triesLeftText;

	public QuestionCounter questionCounter;
	public QuestionLoader questionLoader;
	public Data question;
	public List<string> excludedSubjects;

	public bool correct;
	public bool sort;
	public int tries;

	public Button answerButton;
	public Button closeFinishedButton;
	public Button closeHintButton;
	public Canvas movementButtons;

	public GameObject questionCanvas;
	public GameObject hintCanvas;
	public GameObject questionFinishedCanvas;



	[SerializeField]
	GameObject[] tileMapSpawns;

	[SerializeField]
	GameObject treasure;

	[SerializeField]
	GameObject arrow;

	private Dictionary<string, string> slotValues = new Dictionary<string, string>();
	private Dictionary<string, string> sortedValues = new Dictionary<string, string>();
	private Dictionary<string, string> answers = new Dictionary<string, string>();

	// Start is called before the first frame update
	void Start()
	{
		tries = 3;
		correct = false;
		Button checkButton = answerButton.GetComponent<Button>();
		Button canvasCloseFinishedButton = closeFinishedButton.GetComponent<Button>();
		Button canvasCloseHintButton = closeHintButton.GetComponent<Button>();
		checkButton.onClick.AddListener(CheckAnswer); //When button is clicked, calls the function in brackets
        canvasCloseFinishedButton.onClick.AddListener(CloseFinishedCanvas);
		canvasCloseHintButton.onClick.AddListener(CloseHintCanvas);
		//correctText.color = Color.green;
		//wrongAnswerText.color = Color.red;
		wrongAnswerText.enabled = false;
		movementButtons = GameObject.FindGameObjectWithTag("MovementButtons").GetComponentInChildren<Canvas>();
		triesLeftText.text = "Tries left: " + tries;
		hintAnswerText.SetText(question.solution);
	}

	public void CheckAnswer()
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
				tries -= 1;
				sortedValues.Clear();
				wrongAnswerText.enabled = true;
				SFX.Instance.LoseSound();
				Invoke(nameof(TryAgain), 2f);
			}
			if (tries == 0)
			{
				hintCanvas.SetActive(true);
				questionCanvas.SetActive(false);
				sortedValues.Clear();
            }
		}
		if(correct)
        {
			SFX.Instance.WinSound();
			questionCanvas.SetActive(false);
			coin.SetActive(false);
			questionCounter.answerToFinish--;
			spawnPlatform(questionLoader.question);
			spawnArrow();
			checkWin(questionCounter.answerToFinish);
		}
		triesLeftText.text = "Tries left: " + tries;
	}

	void TryAgain()
    {
		wrongAnswerText.enabled = false;
    }

	void CloseFinishedCanvas()
    {
		questionFinishedCanvas.SetActive(false);
		movementButtons.enabled = true;
	}
	void CloseHintCanvas()
    {
		coin.SetActive(false);
		questionCounter.answerToFinish--;
		spawnPlatform(questionLoader.question);
		spawnArrow();
		checkWin(questionCounter.answerToFinish);
		hintCanvas.SetActive(false);
		movementButtons.enabled = true;
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
		//Remove values from slot list
		sortedValues.Remove(key);
		slotValues.Remove(key);
	}

	public void spawnPlatform(int questionNum)
	{
		tileMapSpawns[questionNum - 1].SetActive(!tileMapSpawns[questionNum - 1].activeSelf);
	}

	public void spawnArrow()
    {
		arrow.SetActive(true);
    }

	public void checkWin(int answerToFinish)
	{
		if (answerToFinish == 0)
		{
			treasure.GetComponent<levelWin>().win = true;
		}
	}
}
