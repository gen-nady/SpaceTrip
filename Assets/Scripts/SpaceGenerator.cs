using UnityEngine;

public class SpaceGenerator : MonoBehaviour
{
    [Header("Звезды")]
    public GameObject[] Stars;
    [Header("Цвет звезд")]
    public Color[] Colors;
    static readonly float MinY = -5.5f;
    static readonly float MaxY = 5.5f;
    static readonly float MinSpeed = 2f;
    static readonly float MaxSpeed = 6f;
    static readonly float MinScale = 3f;
    static readonly float MaxScale = 7f;
    static readonly float Interval = 0.05f; 
    void Start()
    {
        InvokeRepeating("Spawn", 0, Interval);
    }
    //создание звезд
    void Spawn()
    {
        GameObject star = Stars[Random.Range(0, Stars.Length)]; //префаб звезды рандомно
        Vector2 pos = new Vector2(transform.position.x, Random.Range(MinY, MaxY)); //задаем крайниех позиции
        float scl = Random.Range(MinScale, MaxScale); //задаем случайный размер
        Vector3 scale = new Vector3(scl, scl, scl);
        float speed = Random.Range(MinSpeed, MaxSpeed); //задаем случайную скорость
        Color color = Colors[Random.Range(0, Colors.Length)]; //задаем случайный цвет из нашего массива
        GameObject starInstantiate = Instantiate(star, pos, Quaternion.identity) as GameObject; //создаем сами свезды 
        //задаем им параметры, выбранные выше
        starInstantiate.GetComponent<MoveObject>().speed = speed; 
        starInstantiate.transform.localScale = scale;
        starInstantiate.GetComponent<SpriteRenderer>().color = color;
    }
}
