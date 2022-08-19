using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

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

	public GameObject instantiatedObj;

	public Canvas canvas;
	public FilledSlot[] filledSlots;
	public List<GameObject> itemList;
	public List<GameObject> filledSlotList;
	public List<Vector2> itemPositions;
	public List<Vector2> filledSlotPositions;
	public TMPro.TMP_FontAsset chineseFont;

	[SerializeField] private Sprite sprite;
	private Coin coin;
	private bool spawnedSlots;
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
						var font = filledSlot.GetComponentInChildren<TMPro.TextMeshProUGUI>();
						font.font = chineseFont;
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
                        if (answerCheck.question.filledValues[i].Contains("http"))
                        {
							List<string> splitString = new List<string>();
							splitString.Add(answerCheck.question.filledValues[i].Split("|")[0]);
							splitString.Add(answerCheck.question.filledValues[i].Split("|")[1]);
                            StartCoroutine(LoadFilledSlotCoroutine(splitString[1]));
							filledSlot.GetComponent<FilledSlot>().filledValue = splitString[0];
							Instantiate(filledSlot, filledSlotPositions[i], Quaternion.identity).transform.SetParent(canvas.transform, false);       //old code in case need: Instantiate(filledSlot, filledSlotPositions[i], Quaternion.identity).transform.parent = canvas.transform;
							filledSlotList.Add(filledSlot);
						}
						else
                        {
							filledSlot.GetComponent<FilledSlot>().filledValue = answerCheck.question.filledValues[i];
							Instantiate(filledSlot, filledSlotPositions[i], Quaternion.identity).transform.SetParent(canvas.transform, false);       //old code in case need: Instantiate(filledSlot, filledSlotPositions[i], Quaternion.identity).transform.parent = canvas.transform;
							filledSlotList.Add(filledSlot);
						}
					}
					else if (answerCheck.excludedSubjects.Contains(answerCheck.question.subject))
					{
						slot.GetComponent<Slot>().slotName = "Slot (" + i + ")";
						Instantiate(slot, filledSlotPositions[i], Quaternion.identity).transform.SetParent(canvas.transform, false);       //old code in case need: Instantiate(item, itemPositions[i], Quaternion.identity).transform.parent = canvas.transform;
						spawnedSlots = true;
					}
				}
				//Spawn slot
				if (answerCheck.question.answers.Count > answerCheck.question.finalAnswers.Count && spawnedSlots != true)
				{
					for (int j = 0; j < answerCheck.question.finalAnswers.Count; j++)
					{
						slot.GetComponent<Slot>().slotName = "Slot (" + j + ")";
						Instantiate(slot, filledSlotPositions[j], Quaternion.identity).transform.SetParent(canvas.transform, false);       //old code in case need: Instantiate(item, itemPositions[i], Quaternion.identity).transform.parent = canvas.transform;
					}
				}
				for (int i = 0; i < answerCheck.question.answers.Count; i++)
				{
					if (answerCheck.question.answers[i] != "")
					{
						if (answerCheck.question.answers[i].Contains("http"))
						{
							List<string> splitString = new List<string>();
							splitString.Add(answerCheck.question.answers[i].Split("|")[0]);
							splitString.Add(answerCheck.question.answers[i].Split("|")[1]);
							instantiatedObj = Instantiate(item, itemPositions[i], Quaternion.identity);
							StartCoroutine(LoadSlotCoroutine(splitString[1]));

							//setting values for the instantiatedObj
							instantiatedObj.GetComponent<DragDrop>().itemValue = splitString[0];
							itemList.Add(item);
						}
                        else
                        {
							item.GetComponent<DragDrop>().itemValue = answerCheck.question.answers[i];
							Instantiate(item, itemPositions[i], Quaternion.identity).transform.SetParent(canvas.transform, false);       //old code in case need: Instantiate(item, itemPositions[i], Quaternion.identity).transform.parent = canvas.transform;
							item.GetComponent<DragDrop>().canvas = canvas;
							itemList.Add(item);
						}
					}
				}
			}
		}
		canvasGameObject.SetActive(false);
	}

	public IEnumerator LoadFilledSlotCoroutine(string url)
	{
		UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
		var asyncOp = www.SendWebRequest();

		while (!www.isDone)
		{
			yield return null;
		}

		if (!string.IsNullOrEmpty(www.error))
		{
			Debug.Log(www.error);
			Debug.Log(url);
		}
		Debug.Log(www.isDone);
		var texture = DownloadHandlerTexture.GetContent(www);

		//sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0)); ;
		filledSlot.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));;
	}

	public IEnumerator LoadSlotCoroutine(string url)
	{
		UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
		var asyncOp = www.SendWebRequest();

		while (!www.isDone)
		{
			yield return null;
		}

		if (!string.IsNullOrEmpty(www.error))
		{
			Debug.Log(www.error);
			Debug.Log(url);
		}
		Debug.Log(www.isDone);
		var texture = DownloadHandlerTexture.GetContent(www);

		sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
		instantiatedObj.transform.SetParent(canvas.transform, false);//old code in case need: Instantiate(item, itemPositions[i], Quaternion.identity).transform.parent = canvas.transform;
		instantiatedObj.GetComponent<Image>().sprite = sprite;
		instantiatedObj.GetComponent<DragDrop>().canvas = canvas;
		Debug.Log($"BBBBBBBBBBB {sprite}");
	}
}
