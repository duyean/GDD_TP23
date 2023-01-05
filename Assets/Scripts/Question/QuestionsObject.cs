using System.Collections.Generic;

public class QuestionsObject {
    public enum QuestionSubject { English, Math, Chinese /*, Science, Malay, none*/ }
    public QuestionSubject subject;
    public List<string> answerSlots = new List<string>();
    public List<string> answers = new List<string>();
    public List<string> finalAnswer = new List<string>();
    public string hint;
    public string question;
    public string postFix;
    public string preFix;
    public int questionType;
    public int questionId;
    public string qImage;
    public QuestionsObject(QuestionSubject _subject, List<string> _answerSlots, List<string> _answers, List<string> _finalAnswer, string _hint, string _question, int _questionId, string _preFix, string _postFix, int _questionType, string _qImage) {
        this.subject = _subject;
        this.answerSlots = _answerSlots;
        this.answers = _answers;
        this.finalAnswer = _finalAnswer;
        this.hint = _hint;
        this.question = _question;
        this.questionId = _questionId;
        this.preFix = _preFix;
        this.postFix = _postFix;
        this.questionType = _questionType;
        this.qImage = _qImage;
    }
}
