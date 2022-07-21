using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionLoader : MonoBehaviour
{
    public AnswerCheck answerCheck;
    public string subject;
    public int level;
    public int question;
    public GameObject item;
    public GameObject canvasGameObject;
    public Canvas canvas;
    public FilledSlot[] filledSlots;
    public List<GameObject> itemList;
    private Coin coin;
    public List<Vector2> positions;

    void Awake()
    {
        //Get coins for the level
        coin = gameObject.GetComponentInChildren<Coin>();
        //assign level and question number for question loader
        level = coin.level;
        question = coin.questionNumber;
        foreach (FilledSlot filledSlot in filledSlots)
        {
            filledSlot.question = (Data)Resources.Load("Questions/" + subject + "_" + level + "_" + question);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //load questions based on parameters set by coin
        answerCheck.question = (Data)Resources.Load("Questions/" + subject + "_" + level + "_" + question);
        SpawnAnswers(question);
    }

    // Update is called once per frame
    void Update()
    {
        //disable canvas and destroy item clones if answer is correct
        if (answerCheck.correct)
        {
            canvasGameObject.SetActive(false);
        }
    }

    //Instantiate item prefab with answer values from specified scriptable object
    void SpawnAnswers(int level)
    {
        if (answerCheck.question.questionID == level)
        {
            foreach (string s in answerCheck.question.answers)
            {
                item.GetComponent<DragDrop>().itemValue = s;
                //RaycastHit2D hit = Physics2D.BoxCast(item.transform.position, new Vector2(1,1),0,Vector2.one);
                //if()
                Instantiate(item, positions[Random.Range(0, positions.Count)], Quaternion.identity).transform.parent = canvas.transform;
                item.GetComponent<DragDrop>().canvas = canvas;
                itemList.Add(item);
            }
        }
        canvasGameObject.SetActive(false);
    }
}
