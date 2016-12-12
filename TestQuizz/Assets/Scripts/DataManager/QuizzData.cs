using System;
using System.Collections.Generic;


[Serializable]
public class QuizzData
{
    public List<QuizzLevelElt> allQuestion;
}

[Serializable]
public class QuizzLevelElt
{
    public List<QuizElt> question;
}

[Serializable]
public class QuizElt
{
    public string question;
    public string option1;
    public string option2;
    public string response;

}

public class LoadedQuestion{

    public bool Isanswered;
    public QuizElt eltquizz;
    public bool IsTimeOver;
    public float timeRemaining = -2;

    public LoadedQuestion(bool _Isanswered, QuizElt _eltquizz, bool _isTimeOver = false, float _timeRemaining =-2)
    {
        Isanswered = _Isanswered;
        eltquizz = _eltquizz;
        IsTimeOver = _isTimeOver;
        timeRemaining = _timeRemaining;

    }

 
}
