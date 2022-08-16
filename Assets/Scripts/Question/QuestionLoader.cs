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
	public GameObject slot;
	public Canvas canvas;
	public FilledSlot[] filledSlots;
	public List<GameObject> itemList;
	public List<GameObject> filledSlotList;
	public List<Vector2> itemPositions;
	public List<Vector2> filledSlotPositions;
	public TMPro.TMP_FontAsset chineseFont;

	private Coin coin;
	//private int integer;

	void Awake()
	{
		//Get coin for the level
		coin = gameObject.GetComponentInChildren<Coin>();
		//assign level and question number for question loader
		subject = coin.subject;
		level = coin.level;
		question = coin.questionNumber;
		foreach (FilledSlot filledSlot in filledSlots)
		{
			filledSlot.question = (Data)Resources.Load("Questions/" + subject + "_" + level + "_" + question);
		}
		//load questions based on parameters set by coin
		answerCheck.question = (Data)Resources.Load("Questions/" + subject + "_" + level + "_" + question);
		SpawnAnswers(question);
	}

	
	//Instantiate item prefab with answer values from specified scriptable object
	void SpawnAnswers(int level)
	{
		if (answerCheck.question.questionID == level)
		{
			answerCheck.questionText.text = answerCheck.question.question;

			if (answerCheck.question.subject == "Chinese")
			{
				for (int i = 0; i < answerCheck.question.filledValues.Count; i++)
				{
					if (answerCheck.question.filledValues[i] != "")
					{
						filledSlot.GetComponent<FilledSlot>().filledValue = answerCheck.question.filledValues[i];
						Instantiate(filledSlot, filledSlotPositions[i], Quaternion.identity).transform.SetParent(canvas.transform, false);       //old code in case need: Instantiate(filledSlot, filledSlotPositions[i], Quaternion.identity).transform.parent = canvas.transform;
						filledSlotList.Add(filledSlot);
					}
					//Spawn slot
					else
					{
						slot.GetComponent<Slot>().slotName = "Slot (" + i + ")";
						Instantiate(slot, filledSlotPositions[i], Quaternion.identity).transform.SetParent(canvas.transform, false);       //old code in case need: Instantiate(item, itemPositions[i], Quaternion.identity).transform.parent = canvas.transform;
					}
				}
					for (int i = 0; i < answerCheck.question.answers.Count; i++)
					{
						if (answerCheck.question.answers[i] != "")
						{
							var font = item.GetComponentInChildren<TMPro.TextMeshProUGUI>();
							font.font = chineseFont;
							item.GetComponent<DragDrop>().itemValue = answerCheck.question.answers[i];
							Instantiate(item, itemPositions[i], Quaternion.identity).transform.SetParent(canvas.transform, false);       //old code in case need: Instantiate(item, itemPositions[i], Quaternion.identity).transform.parent = canvas.transform;
							item.GetComponent<DragDrop>().canvas = canvas;
							itemList.Add(item);
						}

					}
			}
            else
            {
				for (int i = 0; i < answerCheck.question.filledValues.Count; i++)
				{
					if (answerCheck.question.filledValues[i] != "")
					{
						filledSlot.GetComponent<FilledSlot>().filledValue = answerCheck.question.filledValues[i];
						Instantiate(filledSlot, filledSlotPositions[i], Quaternion.identity).transform.SetParent(canvas.transform, false);       //old code in case need: Instantiate(filledSlot, filledSlotPositions[i], Quaternion.identity).transform.parent = canvas.transform;
						filledSlotList.Add(filledSlot);
					}
					//Spawn slot
					else
					{
						slot.GetComponent<Slot>().slotName = "Slot (" + i + ")";
						Instantiate(slot, filledSlotPositions[i], Quaternion.identity).transform.SetParent(canvas.transform, false);       //old code in case need: Instantiate(item, itemPositions[i], Quaternion.identity).transform.parent = canvas.transform;
					}
				}
				for (int i = 0; i < answerCheck.question.answers.Count; i++)
				{
					if (answerCheck.question.answers[i] != "")
					{
						item.GetComponent<DragDrop>().itemValue = answerCheck.question.answers[i];
						Instantiate(item, itemPositions[i], Quaternion.identity).transform.SetParent(canvas.transform, false);       //old code in case need: Instantiate(item, itemPositions[i], Quaternion.identity).transform.parent = canvas.transform;
						item.GetComponent<DragDrop>().canvas = canvas;
						itemList.Add(item);
					}
				}
			}
			
		}
		canvasGameObject.SetActive(false);
	}
}
