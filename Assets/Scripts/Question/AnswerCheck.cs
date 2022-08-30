using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerCheck : MonoBehaviour
{
    public GameObject coin;
    public List<GameObject> Slots;

    public TextMeshProUGUI questionText;
    public TextMeshProUGUI wrongAnswerText;
    public TextMeshProUGUI correctText;
    public TextMeshProUGUI solutionAnswerText;
    private TextMeshProUGUI triesLeftText;

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

    public TextMeshProUGUI TriesLeftText;

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

        wrongAnswerText.enabled = false;
        movementButtons = GameObject.FindGameObjectWithTag("MovementButtons").GetComponentInChildren<Canvas>();
        TriesLeftText.text = "Tries left: " + tries;
        //hintImage.GetComponent<Image>().sprite = question.hint;
        solutionAnswerText.SetText(question.solution); //Changes the solution text to the solution value from the CSV file
                                                       //Get values from answer list
        for (var i = 0; i < question.finalAnswers.Count; i++)
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
    }

    public void CheckAnswer()
    {
        if (!correct)
        {
            if (!excludedSubjects.Contains(question.subject))
            {
                //Ensure all 5 slots have a value
                for (var i = 0; i < 5; i++)
                {
                    if (!sortedValues.ContainsKey("Slot (" + i + ")"))
                    {
                        sortedValues.Add("Slot (" + i + ")", "");
                    }
                }
                //Sort dictionary
                SortValues();
                SortAnswerValues();
                //Then check values against answer array
                //if all values align, then set boolean to true
                //If there is only one final answer, only check value no matter the key
                if (sortedValues.ContainsValue(question.finalAnswers[0]) && filledAnswers <= 2)
                {
                    correct = true;
                    questionFinishedCanvas.SetActive(true);
                    wrongAnswerText.enabled = false;
                }

                if (sortedValues["Slot (0)"].Equals(sortedAnswers["Slot (0)"]) && sortedValues["Slot (1)"].Equals(sortedAnswers["Slot (1)"]) && sortedValues["Slot (2)"].Equals(sortedAnswers["Slot (2)"]) &&
                    sortedValues["Slot (3)"].Equals(sortedAnswers["Slot (3)"]) && sortedValues["Slot (4)"].Equals(sortedAnswers["Slot (4)"]))
                {
                    Debug.Log($"D {sortedValues.SequenceEqual(sortedAnswers)}");
                    correct = true;
                    questionFinishedCanvas.SetActive(true);
                    wrongAnswerText.enabled = false;
                }

                if (sortedValues.Equals(sortedAnswers))
                {
                    Debug.Log($"D {sortedValues.SequenceEqual(sortedAnswers)}");
                    correct = true;
                    questionFinishedCanvas.SetActive(true);
                    wrongAnswerText.enabled = false;
                }
            }
            else if (sortedValues.Values.Sum(int.Parse) == question.finalAnswers.Sum(int.Parse) && sortedValues.Values.Count > 1)
            {
                correct = true;
                questionFinishedCanvas.SetActive(true);
                wrongAnswerText.enabled = false;
            }
            //If correct is false when button is clicked, subtract 1 from tries left and displays wrong answer text and disables it after 2 seconds
            // while playing a losing sound
            if (!correct)
            {
                tries -= 1;
                sortedValues.Clear();
                wrongAnswerText.enabled = true;
                SFX.Instance.LoseSound();
                Invoke(nameof(TryAgain), 2f);
            }
            //Opens up another panel that shows a hint where the hint is an image url link retrieved from the CSV file
            if (tries == 2 && !correct)
            {
                link = question.hint;
                hintCanvas.SetActive(true);
                StartCoroutine(GetComponent<HintScript>().LoadImageCoroutine(link));
                //questionCanvas.SetActive(false);
            }
            //Opens the solution canvas which auto completes the question and shows what the answer was
            if (tries == 0 && !correct)
            {
                solutionCanvas.SetActive(true);
                questionCanvas.SetActive(false);
            }
        }
        if (correct)
        {
            hintCanvas.SetActive(false);                //Close hint panel
            SFX.Instance.WinSound();                    //Play win sound
            questionCanvas.SetActive(false);            //Close question panel
            coin.SetActive(false);                      //Disables coin
            questionCounter.answerToFinish--;           //Decrease the number of questions to finish left in the level by 1
            SpawnPlatform(questionLoader.question);     //Spawns platform to unlock more areas of the map
            SpawnArrow();                               //Spawn arrow pointing to next coin
            CheckWin(questionCounter.answerToFinish);   //Check if all questions has been completed in the level
        }
        TriesLeftText.text = "Tries left: " + tries;
    }

    //Disables the text
    void TryAgain()
    {
        wrongAnswerText.enabled = false;
    }

    //Function to disable question canvas upon completion of the question and to re-enable movement
    void CloseFinishedCanvas()
    {
        questionFinishedCanvas.SetActive(false);
        movementButtons.enabled = true;
    }
    //Close the hint panel
    void CloseHintCanvas()
    {
        hintCanvas.SetActive(false);
    }
    //Close the solution panel, code inside does the same as the if(correct) statement above
    void CloseSolutionCanvas()
    {
        coin.SetActive(false);
        questionCounter.answerToFinish--;
        SpawnPlatform(questionLoader.question);
        SpawnArrow();
        CheckWin(questionCounter.answerToFinish);
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

    public void SpawnPlatform(int questionNum)
    {
        tileMapSpawns[questionNum - 1].SetActive(!tileMapSpawns[questionNum - 1].activeSelf);
    }

    public void SpawnArrow()
    {
        foreach (var t in arrows)
        {
            t.SetActive(false);
        }

        arrow.SetActive(true);
    }

    public void CheckWin(int answerToFinish)
    {
        if (answerToFinish == 0)
        {
            treasure.GetComponent<levelWin>().win = true;
        }
    }
}
