using UnityEngine;

public class TrigerLoadLevel : MonoBehaviour
{
    private Coin _coin;

    private void Start()
    {
        _coin = transform.parent.GetComponentInChildren<Coin>();
    }

    public void ActivateCoin()
    {
        _coin.gameObject.SetActive(true);
    }
}