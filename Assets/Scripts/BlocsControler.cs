using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BlocsControler : MonoBehaviour
{
    [SerializeField] Text textProgres; //сериализованное поле типа Text , выводит на Canvas информацию о пройденных блоках
    [SerializeField] Transform[] blocks; //сериализованное поле типа Transform , содержит ссылку на массив компонентов Transform
    public int _progres = 0; //публичное поле типа int, накапличает число пройденных блоков
    private float _step = 20f; //приватное поле типа float, содержит значение шага платформы
    private float _lastZ = 180f; //приватное поле типа float, содержит значение длинны всего пути
    private int _currentBlock = 0; //приватное поле типа , содержит index текущего блока
    public GameObject _prefabs; //публичное поле типа GameObject, содержит ссылку на префаб


    private void OnTriggerEnter(Collider other) //проверка столкновений
    {
        if (other.gameObject.tag == "TrigerLoadLevel") //если пересекли зону обновлени€ уровн€
        {
            UpdateLevel(); //вызываем метод обновлени€ уровн€
        }
    }

    private void UpdateLevel()
    {
        _progres++; //увеличиваем счЄтчик прогресса
        textProgres.text = "Blocks passed: " + _progres.ToString(); //переносим значение счЄтчика на Canvas
        _lastZ += _step; //увеличиваем шаг смещени€ по оси Z дл€ последней платформы
        var position = blocks[_currentBlock].position; // получаем позицию текущего блока 
        position.z = _lastZ; //обновл€ем позицию 
        blocks[_currentBlock].position = position; //переносим платформу в новую позицию
        _currentBlock++; //увеличиваем индекс
        if (_currentBlock >= blocks.Length) //условие сброса индекса
        {
            _currentBlock = 0;
        }

        CreatePrefabs(); //создаЄм экземпл€р префаба
    }

    private void CreatePrefabs() //метод создани€ объекта на основе префаба
    {
        var prefabs = GameObject.Instantiate(_prefabs,new Vector3(Random.Range(-5f, 5f), 1, _lastZ), Quaternion.identity);        
    }
}
