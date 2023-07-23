using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] Move _move;
    [SerializeField] TutorialTextField _tutorialTextFieldPrefab;
    //[SerializeField] GameObject _tutorialUIPrefab;
    [SerializeField] TutorialProgress _tutorialProgress;
    public TutorialScript[] tutorialScripts;
    private TutorialScript _currentScript;

    private int _currentStep;
    private float _lockTimer;
    private int _quantityItem;

    public bool IsActive => _currentScript != null;
    private TutorialStep CurrentStep => _currentScript.steps[_currentStep];
    private TutorialStep NextStep => _currentScript.steps[_currentStep + 1];
    private bool HasNextStep => _currentScript.steps.Length > _currentStep + 1;
    private TutorialTextField _tutorialTextField;
    
    public bool IsLocked => _lockTimer > 0;
    public bool IsEnoughCoins => _quantityItem == _move.countCoins ;

    private void StartTutorial(TutorialEvent _event)
    {
        foreach(var script in tutorialScripts)
        {
            if(script.startTriger == _event)
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
    
    private void ProcessEvent(TutorialEvent _event)
    {
        if (NextStep.startTriger == _event)
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
        if (CurrentStep != null && CurrentStep.endStepTutorial) _tutorialProgress.EndStepTutorial();
    }

    private void ProcessCurrentStep()
    {
        switch (CurrentStep.action)
        {
            case TutorialAction.ShowText:
                ShowText(CurrentStep.data);
                break;
            case TutorialAction.HintOnUI:
                ShowHintOnUI(CurrentStep.data);
                break;
            case TutorialAction.HintOnGameObject:
                ShowHintOnGO(CurrentStep.data);
                break;
            case TutorialAction.Clear:
                Clear();
                break;
            case TutorialAction.Wait:
                Wait(float.Parse(CurrentStep.data));
                break;
            case TutorialAction.CollectItems:
                CollectItems((int.Parse(CurrentStep.data)));
                break;
        }
    }
   
    private void Clear()
    {
        Destroy(_tutorialTextField.gameObject);
    }

    private void Wait(float time)
    {
        _lockTimer = time;
        
    }
    private void CollectItems(int count)
    {
        _quantityItem = count + _move.countCoins;
    }

    private void ShowHintOnGO(string data)
    {
        
    }

    private void ShowHintOnUI(string data)
    {
        //var go = GameObject.Find(data);
        //if (go == null)
        //{
        //    Debug.LogError("Game Object not found");
        //    return;
        //}
        //if (_tutorialUI == null)
        //{
        //    _tutorialUI = Instantiate(_tutorialUIPrefab);
        //}
        //_tutorialUI.transform.SetParent(go.transform, false);
    }

    private void ShowText(string data)
    {
        if(_tutorialTextField == null)
        {
            _tutorialTextField = Instantiate(_tutorialTextFieldPrefab);
            _tutorialTextField.SetText(data);
        }
    }

    private void Update()
    {
        if(IsLocked)
        {
            _lockTimer -= Time.unscaledDeltaTime;
        }
        OnEvent(TutorialEvent.Update);        
    }

    public void OnEvent(TutorialEvent _event)
    {
        if (IsLocked) return;

        if (IsActive)
        {
            ProcessEvent(_event);
        }
        else
        {
            StartTutorial(_event);
        }
    }
}
