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
	public TextMeshProUGUI solutionAnswerText;
	public TextMeshProUGUI triesLeftText;

	public Image hintImage;

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
	public Button closeSolutionButton;
	public Canvas movementButtons;

	public GameObject questionCanvas;
	public GameObject hintCanvas;
	public GameObject solutionCanvas;
	public GameObject questionFinishedCanvas;


	string link;

	[SerializeField]
	GameObject[] tileMapSpawns;

	[SerializeField]
	GameObject treasure;

	[SerializeField]
	GameObject arrow;

	[SerializeField]
	GameObject[] arrows;

	private Dictionary<string, string> slotValues = new Dictionary<string, string>();
	private Dictionary<string, string> sortedValues = new Dictionary<string, string>();
	private Dictionary<string, string> answers = new Dictionary<string, string>();
	private Dictionary<string, string> sortedAnswers = new Dictionary<string, string>();
	private int filledAnswers;

	// Start is called before the first frame update
	void Start()
	{
		tries = 5;
		correct = false;
		Button checkButton = answerButton.GetComponent<Button>();
		Button canvasCloseFinishedButton = closeFinishedButton.GetComponent<Button>();
		Button canvasCloseHintButton = closeHintButton.GetComponent<Button>();
		Button canvasCloseSolutionButton = closeSolutionButton.GetComponent<Button>();
		checkButton.onClick.AddListener(CheckAnswer); //When button is clicked, calls the function in brackets
        canvasCloseFinishedButton.onClick.AddListener(CloseFinishedCanvas);
		canvasCloseHintButton.onClick.AddListener(CloseHintCanvas);
		canvasCloseSolutionButton.onClick.AddListener(CloseSolutionCanvas);
		//correctText.color = Color.green;
		//wrongAnswerText.color = Color.red;
		wrongAnswerText.enabled = false;
		movementButtons = GameObject.FindGameObjectWithTag("MovementButtons").GetComponentInChildren<Canvas>();
		triesLeftText.text = "Tries left: " + tries;
		//hintImage.GetComponent<Image>().sprite = question.hint;
		solutionAnswerText.SetText(question.solution);
	}

    public void CheckAnswer()
	{
		if (!correct)
		{
			//Get values from answer list
			for (int i = 0; i < question.finalAnswers.Count; i++)
			{
				if (!answers.ContainsValue(question.finalAnswers[i]) && question.finalAnswers[i] != "")
				{
					answers.Add(new string("Slot (" + i + ")"), question.finalAnswers[i]);
					filledAnswers++;
				}
				if (question.finalAnswers[i] == "")
				{
					answers.Add(new string("Slot (" + i + ")"), "");
				}
			}
			//Ensure all 5 slots have a value
			for (int i = 0; i < 5; i++)
			{
				if (sortedValues.ContainsKey("Slot (" + i + ")"))
				{
				}
				else
				{
					sortedValues.Add("Slot (" + i + ")", "");
				}
			}
			//Sort dictionary
			SortValues();
			SortAnswerValues();
			if (!excludedSubjects.Contains(question.subject))
			{
				//Then check values against answer array
				//if all values align, then set boolean to true
				//If there is only one final answer, only check value no matter the key
				if (sortedValues.ContainsValue(question.finalAnswers[0]) && filledAnswers <= 2)
				{
					correct = true;
                    questionFinishedCanvas.SetActive(true);
                    wrongAnswerText.enabled = false;
                }

                if (sortedValues["Slot (0)"].Equals(sortedAnswers["Slot (0)"]) && sortedValues["Slot (1)"].Equals(sortedAnswers["Slot (1)"]) && sortedValues["Slot (2)"].Equals(sortedAnswers["Slot (2)"]) && sortedValues["Slot (3)"].Equals(sortedAnswers["Slot (3)"]) && sortedValues["Slot (4)"].Equals(sortedAnswers["Slot (4)"]))
                {
					Debug.Log($"D {sortedValues.SequenceEqual(sortedAnswers)}");
					correct = true;
					questionFinishedCanvas.SetActive(true);
					wrongAnswerText.enabled = false;
				}

				if (sortedValues.SequenceEqual(sortedAnswers))
				{
					Debug.Log($"D {sortedValues.SequenceEqual(sortedAnswers)}");
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
			if (tries == 2 && !correct)
			{
				link = question.hint;
				hintCanvas.SetActive(true);
				StartCoroutine(GetComponent<HintScript>().LoadImageCoroutine(link));
				//questionCanvas.SetActive(false);
            }
			if (tries == 0 && !correct)
			{
				solutionCanvas.SetActive(true);
				questionCanvas.SetActive(false);
			}
		}
		if(correct)
        {
			hintCanvas.SetActive(false);
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
		hintCanvas.SetActive(false);
		//questionCanvas.SetActive(true);
	}
	void CloseSolutionCanvas()
    {
        coin.SetActive(false);
        questionCounter.answerToFinish--;
        spawnPlatform(questionLoader.question);
        spawnArrow();
        checkWin(questionCounter.answerToFinish);
		solutionCanvas.SetActive(false);
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
		var orderByKey2 = from s in sortedValues
						  orderby s.Key
						 select s;
		foreach (var item in orderByKey2)
		{
			sortedValues.TryAdd(item.Key, item.Value);
		}
	}

	public void SortAnswerValues()
    {
		var orderByKey = from s in answers
						 orderby s.Key
						 select s;
		foreach (var item in orderByKey)
		{
			sortedAnswers.TryAdd(item.Key, item.Value);
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
		for (int i = 0; i < arrows.Length; i++)
		{
			arrows[i].SetActive(false);
		}

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
