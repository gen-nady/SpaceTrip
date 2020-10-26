using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    static SoundManager SoundManag;
    [Header("Здоровье")]
    public int LifePoints;
    [Header("Префабы")]
    public GameObject Bullet, Coin;
    static readonly float ShootDelay = 1.15f;
    [Header("Точки выстрела")]
    public Transform ShootPoint;
    static ShipControl Ship;
    bool IsDead = false;
    void Shoot()
    {
        GameObject bulletInstantiate = Instantiate(Bullet, ShootPoint.position, Quaternion.identity) as GameObject;
        Destroy(bulletInstantiate, 4);
    }
    void Start()
    {
        SoundManag = GameObject.Find("PlayZone").GetComponent<SoundManager>();
        Ship = GameObject.Find("shipPlayer").GetComponent<ShipControl>();
        InvokeRepeating("Shoot", 1, ShootDelay); //стрельба коробля
    }
    void Update()
    {
        //проверка на жизни
        if (LifePoints == 0 && !IsDead) 
        {
            IsDead = true;
            Boom();
        }
    }
    void Boom()
    {
        //при уничтожении дает 150 очков и создает монетку
        Ship.AddScore(150);
        Destroy(gameObject);
        SoundManag.PlaySound(0);
        SpawnCoin();
    }
    //создание монетки
    void SpawnCoin()
    {
        GameObject coinInstantiate = Instantiate(Coin, transform.position, Quaternion.identity) as GameObject;
        Destroy(coinInstantiate, 8);
    }
    //полученный урон
    public void Damage(int dmg)
    {
        LifePoints -= dmg;
        if (LifePoints < 0)
        {
            LifePoints = 0;
        }
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("shipB")) //при соприкосновении с пулей врага, получает уровн равный пуле или ракете
        {
            Damage(Ship.BulletDmg);
            Destroy(coll.gameObject);
            SoundManag.PlaySound(1);
        }
        if (coll.gameObject.CompareTag("enemyB")) //при соприкосновении с пулей своих, получает 1 урон
        {
            Damage(1);
            Destroy(coll.gameObject);
            SoundManag.PlaySound(1);
        }
    }
}
