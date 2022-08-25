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

    [HideInInspector] public GameObject instantiatedAnswerSlotObj;
    [HideInInspector] public GameObject instantiatedSlotObj;
    [HideInInspector] public GameObject instantiatedImageObj;
    [HideInInspector] public GameObject instantiatedFilledObj;
	[HideInInspector] public GameObject instantiatedFilledImgObj;

	public Canvas canvas;
	public FilledSlot[] filledSlots;
	public List<GameObject> itemList;
	public List<GameObject> filledSlotList;
	public List<Vector2> itemPositions;
	public List<Vector2> filledSlotPositions;
	public TMPro.TMP_FontAsset chineseFont;

	[SerializeField] private Sprite sprite;
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
		//double check that question loaded is correct
		if (answerCheck.question.questionID == level)
		{
			//set question display text
			answerCheck.questionText.text = answerCheck.question.question;

			//check for Chinese, then change font
			if (answerCheck.question.subject == "Chinese")
			{
				//for every filled value, conduct the following checks
				for (int i = 0; i < answerCheck.question.filledValues.Count; i++)
				{
					//check if filled value is not null/empty
					if (!answerCheck.question.filledValues[i].Equals(""))
                    {
                        //check if filled slot contains "~", if it does, skip slot
                        if (answerCheck.question.filledValues[i].Equals("~"))
                        {
                            //skip slot
                        }
                        //then check if filled value contains "http", if it does, load image
                        else if (answerCheck.question.filledValues[i].Contains("http"))
                        {
							instantiatedFilledImgObj = Instantiate(filledSlot, filledSlotPositions[i], Quaternion.identity);
							var font = instantiatedFilledImgObj.GetComponentInChildren<TMPro.TextMeshProUGUI>();
							font.font = chineseFont;
							StartCoroutine("LoadFilledSlotCoroutine");
                            setFilledSlot(instantiatedFilledObj, i);
							filledSlotList.Add(instantiatedFilledImgObj);
						}
						//if not an image or needs to be skipped, spawn as per normal
                        else
                        {
							instantiatedFilledObj = Instantiate(filledSlot, filledSlotPositions[i], Quaternion.identity);
							var font = instantiatedFilledObj.GetComponentInChildren<TMPro.TextMeshProUGUI>();
							font.font = chineseFont;
							setFilledSlot(instantiatedFilledObj, i);
							filledSlotList.Add(instantiatedFilledObj);
						}
                    }
                    //If filled slot is empty, spawn a slot to drag in answer instead
                    else if (answerCheck.question.filledValues[i].Equals(""))
					{
						instantiatedAnswerSlotObj = Instantiate(slot, filledSlotPositions[i], Quaternion.identity);
						setSlot(instantiatedAnswerSlotObj, i);
					}
                }
				//spawn answers for Chinese question
				for (int i = 0; i < answerCheck.question.answers.Count; i++)
				{
					if (!answerCheck.question.answers[i].Equals(""))
					{
						instantiatedSlotObj = Instantiate(item, itemPositions[i], Quaternion.identity);
						var font = instantiatedSlotObj.GetComponentInChildren<TMPro.TextMeshProUGUI>();
						font.font = chineseFont;
						setDragDrop(instantiatedSlotObj, i);
						itemList.Add(instantiatedSlotObj);
					}
				}
			}
			//if not Chinese question, do the following
            else
            {
				for (int i = 0; i < answerCheck.question.filledValues.Count; i++)
				{
					//if filled values is not null/empty, do the following
					if (!answerCheck.question.filledValues[i].Equals(""))
                    {
						//check if filled value is special. if it is, skip spawning
						if (answerCheck.question.filledValues[i].Equals("~"))
                        {
							//skip
                        }
                        //if filled value contains "http", load an image
                        else if (answerCheck.question.filledValues[i].Contains("http"))
                        {
                            List<string> splitString = new List<string>();
							splitString.Add(answerCheck.question.filledValues[i].Split("|")[0]);
							splitString.Add(answerCheck.question.filledValues[i].Split("|")[1]);
							instantiatedFilledImgObj = Instantiate(filledSlot, filledSlotPositions[i], Quaternion.identity);

							//set filledslot values
							StartCoroutine(LoadImageCoroutine(splitString[1],instantiatedFilledImgObj));
							setFilledSlot(instantiatedFilledImgObj, i);

                            filledSlotList.Add(instantiatedFilledImgObj);
						}
                        //if filled value is not an image or a skipped slot, load a regular filled slot
                        else if(!answerCheck.question.filledValues[i].Equals(""))
                        {
                            instantiatedFilledObj = Instantiate(filledSlot, filledSlotPositions[i], Quaternion.identity);
							setFilledSlot(instantiatedFilledObj, i);
							filledSlotList.Add(instantiatedFilledObj);
                        }
                    }
                    //If filled slot is empty, spawn a slot to drag in answer instead
                    if (answerCheck.question.filledValues[i].Equals(""))
                    {
                        instantiatedAnswerSlotObj = Instantiate(slot, filledSlotPositions[i], Quaternion.identity);
                        setSlot(instantiatedAnswerSlotObj, i);
                    }
                }
				//spawn answers for other questions
				for (int i = 0; i < answerCheck.question.answers.Count; i++)
                {
					//if answers is not null/empty, do the following
                    if (!answerCheck.question.answers[i].Equals(""))
					{
						//if answers contain "http", load an image
						if (answerCheck.question.answers[i].Contains("http"))
						{
							List<string> splitString = new List<string>();
							splitString.Add(answerCheck.question.answers[i].Split("|")[0]);
							splitString.Add(answerCheck.question.answers[i].Split("|")[1]);
							instantiatedImageObj = Instantiate(item, itemPositions[i], Quaternion.identity);

							//setting values for the instantiatedObj
							StartCoroutine(LoadImageCoroutine(splitString[1],instantiatedImageObj));
                            setDragDrop(instantiatedImageObj, i);
                            instantiatedImageObj.GetComponent<DragDrop>().itemValue = splitString[0];
                            instantiatedImageObj.GetComponent<DragDrop>().valueText.SetText("");
                            itemList.Add(instantiatedImageObj);
						}
						//if not, load answers normally
                        else
                        {
							instantiatedSlotObj = Instantiate(item, itemPositions[i], Quaternion.identity);
							setDragDrop(instantiatedSlotObj, i);
							itemList.Add(instantiatedSlotObj);
						}
					}
				}
			}
        }
		canvasGameObject.SetActive(false);
	}

	void setFilledSlot(GameObject gameObject, int i)
	{
        gameObject.GetComponent<FilledSlot>().filledValue = answerCheck.question.filledValues[i];
        gameObject.GetComponent<FilledSlot>().filledText.SetText(answerCheck.question.filledValues[i]);
        gameObject.transform.SetParent(canvas.transform, false);
    }

    void setSlot(GameObject gameObject, int i)
	{
        gameObject.GetComponent<Slot>().slotName = "Slot (" + i + ")";
        gameObject.transform.SetParent(canvas.transform, false);
    }

    void setDragDrop(GameObject gameObject, int i)
	{
        gameObject.transform.SetParent(canvas.transform, false);
		gameObject.GetComponent<DragDrop>().itemValue = answerCheck.question.answers[i];
        gameObject.GetComponent<DragDrop>().canvas = canvas;
        gameObject.GetComponent<DragDrop>().valueText.SetText(answerCheck.question.answers[i]);

    }

    public IEnumerator LoadImageCoroutine(string url, GameObject gameObject)
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
		gameObject.GetComponent<Image>().sprite = sprite;
	}
}
