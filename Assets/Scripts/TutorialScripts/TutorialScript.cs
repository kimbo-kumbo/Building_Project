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
    public bool endStepTutorial;
}

[System.Serializable]
public enum TutorialEvent    
{
    Update,
    GameStart,
    PlayerMoveLeft,
    PlayerMoveRight,
    PlayerJamp,
    ItemsTakeFinish
}

[System.Serializable]
public enum TutorialAction
{
    ShowText,
    HintOnUI,
    HintOnGameObject,    
    Clear,
    Wait,
    CollectItems
}