using UnityEngine;

[CreateAssetMenu(fileName = "Tutorial.asset",menuName = "Tutorial/Create Tutorial Script")]
public class TutorialScript : ScriptableObject
{
    public TutorialEvent startTriger;
    public TutorialStep[] steps;
    
}

[System.Serializable]
public class TutorialStep
{
    public TutorialEvent startTriger;
    public TutorialAction action;
    public string data;
}

[System.Serializable]
public enum TutorialEvent    
{
    GameStart
}

[System.Serializable]
public enum TutorialAction
{
    ShowText
}