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
	//private int integer;

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
			Debug.Log(filledSlot.question);
		}
		//load questions based on parameters set by coin
		answerCheck.question = (Data)Resources.Load("Questions/" + subject + "_" + level + "_" + question);
		SpawnAnswers(question);
	}

	// Start is called before the first frame update
	void Start()
	{
		
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
			answerCheck.questionText.text = answerCheck.question.question;
			for (int i = 0; i < answerCheck.question.answers.Count; i++)
			{
				item.GetComponent<DragDrop>().itemValue = answerCheck.question.answers[i];
				Instantiate(item, itemPositions[i], Quaternion.identity).transform.SetParent(canvas.transform, false);       //old code in case need: Instantiate(item, itemPositions[i], Quaternion.identity).transform.parent = canvas.transform;
				item.GetComponent<DragDrop>().canvas = canvas;
				itemList.Add(item);
			}
			for (int i = 0; i < answerCheck.question.filledValues.Count; i++)
			{
				filledSlot.GetComponent<FilledSlot>().filledValue = answerCheck.question.filledValues[i];
				Instantiate(filledSlot, filledSlotPositions[i], Quaternion.identity).transform.SetParent(canvas.transform, false);       //old code in case need: Instantiate(filledSlot, filledSlotPositions[i], Quaternion.identity).transform.parent = canvas.transform;
				filledSlotList.Add(filledSlot);
			}

		}
		canvasGameObject.SetActive(false);
	}
}
