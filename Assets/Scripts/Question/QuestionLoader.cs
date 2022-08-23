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

	public GameObject instantiatedAnswerSlotObj;
	public GameObject instantiatedSlotObj;
	public GameObject instantiatedImageObj;
	public GameObject instantiatedFilledObj;
	public GameObject instantiatedFilledImgObj;

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
						instantiatedFilledImgObj = Instantiate(filledSlot, filledSlotPositions[i], Quaternion.identity);
						var font = instantiatedFilledImgObj.GetComponentInChildren<TMPro.TextMeshProUGUI>();
						font.font = chineseFont;
						instantiatedFilledImgObj.GetComponent<FilledSlot>().filledValue = answerCheck.question.filledValues[i];
						instantiatedFilledImgObj.GetComponent<FilledSlot>().filledText.SetText(answerCheck.question.filledValues[i]);
						instantiatedFilledImgObj.transform.SetParent(canvas.transform, false);       //old code in case need: Instantiate(filledSlot, filledSlotPositions[i], Quaternion.identity).transform.parent = canvas.transform;
						filledSlotList.Add(instantiatedFilledImgObj);
					}
					//Spawn answer slot
					else
					{
						instantiatedAnswerSlotObj = Instantiate(slot, filledSlotPositions[i], Quaternion.identity);
						instantiatedAnswerSlotObj.GetComponent<Slot>().slotName = "Slot (" + i + ")";
						instantiatedAnswerSlotObj.transform.SetParent(canvas.transform, false);       //old code in case need: Instantiate(item, itemPositions[i], Quaternion.identity).transform.parent = canvas.transform;
					}
				}
				for (int i = 0; i < answerCheck.question.answers.Count; i++)
				{
					if (answerCheck.question.answers[i] != "")
					{
						instantiatedSlotObj = Instantiate(item, itemPositions[i], Quaternion.identity);
						var font = instantiatedSlotObj.GetComponentInChildren<TMPro.TextMeshProUGUI>();
						font.font = chineseFont;
						instantiatedSlotObj.GetComponent<DragDrop>().itemValue = answerCheck.question.answers[i];
						instantiatedSlotObj.GetComponent<DragDrop>().valueText.SetText(answerCheck.question.answers[i]);
						instantiatedSlotObj.transform.SetParent(canvas.transform, false);       //old code in case need: Instantiate(item, itemPositions[i], Quaternion.identity).transform.parent = canvas.transform;
						instantiatedSlotObj.GetComponent<DragDrop>().canvas = canvas;
						itemList.Add(instantiatedSlotObj);
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
							instantiatedFilledImgObj = Instantiate(filledSlot, filledSlotPositions[i], Quaternion.identity);

							//set filledslot values
							StartCoroutine(LoadFilledSlotCoroutine(splitString[1]));
							instantiatedFilledImgObj.GetComponent<FilledSlot>().filledValue = splitString[0];
							instantiatedFilledImgObj.GetComponent<FilledSlot>().filledText.SetText("");
							instantiatedFilledImgObj.transform.SetParent(canvas.transform, false);       //old code in case need: Instantiate(filledSlot, filledSlotPositions[i], Quaternion.identity).transform.parent = canvas.transform;
							filledSlotList.Add(instantiatedFilledImgObj);
						}
						else
                        {
							instantiatedFilledObj = Instantiate(filledSlot, filledSlotPositions[i], Quaternion.identity);
							instantiatedFilledObj.GetComponent<FilledSlot>().filledValue = answerCheck.question.filledValues[i];
							instantiatedFilledObj.GetComponent<FilledSlot>().filledText.SetText(answerCheck.question.filledValues[i]);
							instantiatedFilledObj.transform.SetParent(canvas.transform, false);       //old code in case need: Instantiate(filledSlot, filledSlotPositions[i], Quaternion.identity).transform.parent = canvas.transform;
							filledSlotList.Add(instantiatedFilledObj);
						}
					}
					else if (answerCheck.excludedSubjects.Contains(answerCheck.question.subject))
					{
						instantiatedSlotObj = Instantiate(slot, filledSlotPositions[i], Quaternion.identity);
						instantiatedSlotObj.GetComponent<Slot>().slotName = "Slot (" + i + ")";
						instantiatedSlotObj.transform.SetParent(canvas.transform, false);       //old code in case need: Instantiate(item, itemPositions[i], Quaternion.identity).transform.parent = canvas.transform;
						spawnedSlots = true;
					}
				}
				//Spawn slot
				if (answerCheck.question.answers.Count > answerCheck.question.finalAnswers.Count && spawnedSlots != true)
				{
					for (int j = 0; j < answerCheck.question.finalAnswers.Count; j++)
					{
						instantiatedAnswerSlotObj = Instantiate(slot, filledSlotPositions[j], Quaternion.identity);
						instantiatedAnswerSlotObj.GetComponent<Slot>().slotName = "Slot (" + j + ")";
						instantiatedAnswerSlotObj.transform.SetParent(canvas.transform, false);       //old code in case need: Instantiate(item, itemPositions[i], Quaternion.identity).transform.parent = canvas.transform;
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
							instantiatedImageObj = Instantiate(item, itemPositions[i], Quaternion.identity);

							//setting values for the instantiatedObj
							StartCoroutine(LoadSlotCoroutine(splitString[1]));
							instantiatedImageObj.transform.SetParent(canvas.transform, false);//old code in case need: Instantiate(item, itemPositions[i], Quaternion.identity).transform.parent = canvas.transform;
							instantiatedImageObj.GetComponent<DragDrop>().canvas = canvas;
							instantiatedImageObj.GetComponent<DragDrop>().itemValue = splitString[0];
							instantiatedImageObj.GetComponent<DragDrop>().valueText.SetText("");
							itemList.Add(instantiatedImageObj);
						}
                        else
                        {
							instantiatedSlotObj = Instantiate(item, itemPositions[i], Quaternion.identity);
							instantiatedSlotObj.transform.SetParent(canvas.transform, false);//old code in case need: Instantiate(item, itemPositions[i], Quaternion.identity).transform.parent = canvas.transform;
							instantiatedSlotObj.GetComponent<DragDrop>().itemValue = answerCheck.question.answers[i];
							instantiatedSlotObj.GetComponent<DragDrop>().canvas = canvas;
							instantiatedSlotObj.GetComponent<DragDrop>().valueText.SetText(answerCheck.question.answers[i]);
							itemList.Add(instantiatedSlotObj);
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
		instantiatedFilledImgObj.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));;
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
		instantiatedImageObj.GetComponent<Image>().sprite = sprite;
	}
}
