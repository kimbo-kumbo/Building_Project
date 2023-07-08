using UnityEngine;
using UnityEngine.UI;

public class BlocsControler : MonoBehaviour
{
    [SerializeField] Text textProgres; 
    [SerializeField] Transform[] blocks; 
    public int _progres = 0; 
    private float _step = 20f; 
    private float _lastZ = 180f; 
    private int _currentBlock = 0; 
    public GameObject _prefabs;

#if UNITY_EDITOR
    private void Start()
    {
        if (textProgres == null || _prefabs == null || blocks == null)
            Debug.LogError($"Есть незаполненные ссылки в {name}");
        else Debug.Log("Все ссылки заполнены");
    }
#endif
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "TrigerLoadLevel") 
        {
            UpdateLevel(); 
        }
    }
    private void UpdateLevel()
    {
        _progres++; 
        textProgres.text = "Blocks passed: " + _progres.ToString();
        _lastZ += _step; 
        var position = blocks[_currentBlock].position; 
        position.z = _lastZ; 
        blocks[_currentBlock].position = position; 
        _currentBlock++; 
        if (_currentBlock >= blocks.Length) 
        {
            _currentBlock = 0;
        }
        CreatePrefabs(); 
    }
    private void CreatePrefabs()
    {
        var prefabs = GameObject.Instantiate(_prefabs,new Vector3(Random.Range(-5f, 5f), 1, _lastZ), Quaternion.identity);        
    }
}