using UnityEngine;

public class MoveObj : MonoBehaviour
{
    public float Speed;
    [Header("Направление движения")]
    public Vector2 MoveDir;
    [Header("Спецэффект(партикл)")]
    public GameObject FX;
    public bool IsQuit;
    static ShipControl Ship;

    void Start()
    {
        Ship = GameObject.Find("shipPlayer").GetComponent<ShipControl>();
    }
    void Update()
    {
        transform.Translate(MoveDir * Speed * Time.deltaTime); //движение объекта
    }
    [System.Obsolete]
    void OnDestroy()
    {
        if (!IsQuit && Time.timeScale==1 && !Ship.IsOver)
        {
            GameObject p = Instantiate(FX, transform.position, Quaternion.identity) as GameObject;  //генерация анимации
            p.GetComponent<ParticleSystem>().Play(); //вопрсоизведение анимации
            Destroy(p, p.GetComponent<ParticleSystem>().duration); //уничтожение анимации
        }
    }
}
