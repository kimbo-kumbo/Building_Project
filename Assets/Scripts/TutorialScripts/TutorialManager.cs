using System;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public TutorialScript[] tutorialScripts;
    private TutorialScript _currentScript;
    private int _currentStep;

    public bool IsActive => _currentScript != null;
    private TutorialStep CurrentStep => _currentScript.steps[_currentStep];
    private TutorialStep NextStep => _currentScript.steps[_currentStep + 1];
    private bool HasNextStep => _currentScript.steps.Length > _currentStep + 1;

    private void StartTutorial(TutorialEvent @event)
    {
        foreach(var script in tutorialScripts)
        {
            if(script.startTriger == @event)
            {
                _currentScript = script;
                _currentStep = 0;
                ProcessCurrentStep();
                break;
            }
        }
    }
    
    private void FinishTutorial()
    {
        _currentScript = null;
        _currentStep = 0;
    }
    
    private void ProcessEvent(TutorialEvent @event)
    {
        if (NextStep.startTriger == @event)
        {
            PlayNextStep();
        }
        if (!HasNextStep)
        {
            FinishTutorial();
        }
    }

    private void PlayNextStep()
    {
        _currentStep++;
        ProcessCurrentStep();
    }

    private void ProcessCurrentStep()
    {
        switch (CurrentStep.action)
        {
            case TutorialAction.ShowText:
                ShowText(CurrentStep.data);
                break;
        }
    }

    private void ShowText(string data)
    {
        Debug.Log(data);
    }

    public void OnEvent(TutorialEvent @event)
    {
        if(IsActive)
        {
            ProcessEvent(@event);
        }
        else
        {
            StartTutorial(@event);
        }
        
    }
}
