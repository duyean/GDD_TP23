using System.Collections.Generic;

public class QuestionsObject {
    public enum QuestionSubject { none, English, Math, Science, Chinese, Malay }
    public QuestionSubject subject;
    public List<string> answerSlots = new List<string>();
    public List<string> answers = new List<string>();
    public List<string> finalAnswer = new List<string>();
    public string hint;
    public string question;
    public int questionId;
    public QuestionsObject(QuestionSubject _subject, List<string> _answerSlots, List<string> _answers, List<string> _finalAnswer, string _hint, string _question, int _questionId) {
        this.subject = _subject;
        this.answerSlots = _answerSlots;
        this.answers = _answers;
        this.finalAnswer = _finalAnswer;
        this.hint = _hint;
        this.question = _question;
        this.questionId = _questionId;
    }
}
