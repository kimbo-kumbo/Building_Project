using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TutorialProgress : MonoBehaviour
{
    [SerializeField] Transform parent;
    private Queue<Text> _stepsProgress = new Queue<Text>();

    private void Start()
    {
        var steps = parent.GetComponentsInChildren<Text>().Skip(1).ToArray();
        foreach(var step in steps)
        {
            _stepsProgress.Enqueue(step);
            Debug.Log(step.text);
        }       
    }

    public void EndStepTutorial()
    {
        if (_stepsProgress.Count > 0)
        {
            var currentStep = _stepsProgress.Dequeue();
            currentStep.color = Color.green;
        }
    }
}