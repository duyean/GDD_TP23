using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionSpawner : MonoBehaviour {
    public LoadQuestion questionScript;
    public QuestionsObject.QuestionSubject subject;
    public int level, tries;
    public GameObject solvedObj;
    private GameObject movementButtons;
    private MovementScript movementScript;
    private void Start() {
        GlobalVariables.instance.questionsInMap++;
        questionScript.level = level;
        questionScript.tries = tries;
        questionScript.subject = subject;
        questionScript.winObj = solvedObj;
        movementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementScript>();
        movementButtons = GameObject.FindGameObjectWithTag("MovementButtons");
        questionScript.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.tag == "Player") {
            GetComponent<CircleCollider2D>().enabled = false;
            questionScript.gameObject.SetActive(true);
            questionScript.loadQuestion();
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
}
