using UnityEngine;

public class Bonus : MonoBehaviour
{
    [Header("Бонусы")]
    public GameObject[] Bonuses;
    int LifePoints = 4;
    bool IsDead = false;
    static SoundManager SoundManag;
    void Start()
    {
        SoundManag = GameObject.Find("PlayZone").GetComponent<SoundManager>();
    }
    private void Update()
    {
        if (LifePoints == 0 && !IsDead)
        {
            Boom();           //если хп равны 0, то выполняем метод Boom 
        }
    }
    void Boom()
    {
        IsDead = true; 
        GameObject bonus = Bonuses[Random.Range(0, Bonuses.Length)];  //генерируем случайный бонус
        GameObject bonusIntantiate = Instantiate(bonus, transform.position, Quaternion.identity); //создаем его на сцене
        Destroy(gameObject);  //уничтожаем метеорит
        SoundManag.PlaySound(0); //звук взрыва
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("shipB"))  // при соприкосновении с пулей
        {
            LifePoints--;  //уменьшаем хп метеорита
            Destroy(coll.gameObject);   //уничтожаем пули
            SoundManag.PlaySound(1); 
        }
        if (coll.gameObject.CompareTag("shipR")) // при соприкосновении с ракетой
        {
            LifePoints = 0;  //уничтожаем метеорит
            Destroy(coll.gameObject);  //уничтожаем ракету
        }
    }
}
