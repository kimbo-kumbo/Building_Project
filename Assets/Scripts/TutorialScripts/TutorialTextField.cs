using UnityEngine;
using UnityEngine.UI;

public class TutorialTextField : MonoBehaviour
{
    [SerializeField] Text text;

    public void SetText(string text)
    {
        this.text.text = text;
    }
}
