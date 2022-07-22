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
    public GameObject filledSlot;
    public Canvas canvas;
    public FilledSlot[] filledSlots;
    public List<GameObject> itemList;
    public List<GameObject> filledSlotList;
    public List<Vector2> itemPositions;
    public List<Vector2> filledSlotPositions;

    private Coin coin;
    private int integer;

    void Awake()
    {
        //Get coins for the level
        coin = gameObject.GetComponentInChildren<Coin>();
        //assign level and question number for question loader
        subject = coin.subject;
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
            answerCheck.correctWrongText.text = answerCheck.question.question;
            foreach (string s in answerCheck.question.answers)
            {
                item.GetComponent<DragDrop>().itemValue = s;
                //RaycastHit2D hit = Physics2D.BoxCast(item.transform.position, new Vector2(1,1),0,Vector2.one);
                //if()
                Instantiate(item, itemPositions[Random.Range(0, itemPositions.Count)], Quaternion.identity).transform.parent = canvas.transform;
                item.GetComponent<DragDrop>().canvas = canvas;
                itemList.Add(item);
            }
            //foreach (string s in answerCheck.question.filledValues)
            //{
            //    filledSlot.GetComponent<FilledSlot>().filledValue = s;
            //    Instantiate(filledSlot, filledSlotPositions[forLoop(filledSlotPositions.Count)], Quaternion.identity).transform.parent = canvas.transform;
            //    filledSlotList.Add(filledSlot);
            //}
        }
        canvasGameObject.SetActive(false);
    }
    //int forLoop(int maxNumber)
    //{
    //    for (int i = 0; i < maxNumber; i++)
    //    {
    //        integer = i;
    //        return integer;
    //    }
    //    return 0;
    //}
}
