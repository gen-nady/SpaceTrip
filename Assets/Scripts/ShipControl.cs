using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class ShipControl : MonoBehaviour
{
    [Header("Здоровье")]
    public int Life_points = 10;
    public Image[] LifePoints;
    public Color[] lifeColors;
    [Header("Крайние точки корабля")]
    static readonly float MinY = -4.3f, MaxY = 4.3f, MinX = -8f, MaxX = 7f;
    [Header("Огневая мощь")]
    public int BulletDmg = 1;
    public GameObject Bullet, Rocket;
    public int RocketCount = 3;
    public Text RocketCountT;
    float ShootDelayShip;
    public Transform[] ShootPoints, ShootPoints1, ShootPoints2;
    public bool IsFire, IsReadyToShot;
    [Header("Очки и счет")]
    public int CoinsCount, ScoreCount;
    public Text CoinsCountT, ScoreCountT;
    [HideInInspector]
    public SoundManager SoundManag;
    [Header("Корабли")]
    public Sprite[] Ships;
    [Header("Прочее")]
    public bool IsOver;
    public GameObject GameOverPanel;
    public ParticleSystem DeadFX;
    public SimpleAds Ad;
    public void Move(Vector2 dir)
    {
        //характеристики кораблей(быстрота движения и задержка при выстреле)
        if (PlayerPrefs.GetInt("Play1") == 1)
        {
            transform.Translate(dir * Time.deltaTime * 4.5f);
            ShootDelayShip = 0.55f;
        }
        if (PlayerPrefs.GetInt("Play2") == 1)
        {
            transform.Translate(dir * Time.deltaTime * 4.75f);
            ShootDelayShip = 0.5f;
        }
        if (PlayerPrefs.GetInt("Play3") == 1)
        {
            transform.Translate(dir * Time.deltaTime * 5f);
            ShootDelayShip = 0.45f;
        }
    }
    void Start()
    {
        SoundManag = GameObject.Find("PlayZone").GetComponent<SoundManager>();
        int shipNum = PlayerPrefs.GetInt("ship");
        GetComponent<SpriteRenderer>().sprite = Ships[shipNum];
        IsReadyToShot = true;
        IsFire = false;
        CoinsCount = PlayerPrefs.GetInt("coins", 0); //количество монет
        Move(new Vector3(0, 0, 0)); //передаем значения кораблей
    }
    void Update()
    {
        //задаем основные параметры корабля, как кол-во ракет, счет, монет
        CoinsCountT.text = CoinsCount.ToString();
        RocketCountT.text = RocketCount.ToString();
        ScoreCountT.text = ScoreCount.ToString();
        //ограничение по полету нашего корабля
        Vector2 curPos = transform.localPosition;
        curPos.y = Mathf.Clamp(transform.localPosition.y, MinY, MaxY);
        curPos.x = Mathf.Clamp(transform.localPosition.x, MinX, MaxX);
        transform.localPosition = curPos;
        ChangeLife();
        //проверка на готовность стрельбы
        if (IsFire == true)
        {
            if (IsFire && IsReadyToShot)
            {
                Shoot();
            }
        }
        //при смерти вызвается метод GameOver и Save, открывается панель
        if (Life_points <= 0 && !IsOver)
        {
            GameOver();
            GameOverPanel.SetActive(true);
            Save();
        }
    }
    //добавление очков
    public void AddScore(int scoreToAdd)
    {
        ScoreCount += scoreToAdd;
    }
    //обновление полоски жизни
    public void ChangeLife()
    {
        for (int l = 0; l < LifePoints.Length; l++)
        {
            if (l < Life_points)
            {
                LifePoints[l].color = lifeColors[0];
            }
            else
            {
                LifePoints[l].color = lifeColors[1];
            }
        }
    }
    public void GameOver()
    {
        IsOver = true;
        DeadFX.Play();
        Hide();
        Save();
        GameOverPanel.SetActive(true); //активация панели конца игры
        if (ScoreCount > 1000) //показ рекламы
        {
            Ad.ShowAd();
        }
    }
    //скрытие корабля
    void Hide()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
    //сохранения монет и результата, если он лучший
    void Save()
    {
        PlayerPrefs.SetInt("coins", CoinsCount);
        int hs = PlayerPrefs.GetInt("HS");
        if (hs < ScoreCount)
        {
            PlayerPrefs.SetInt("HS", ScoreCount);
        }
    }
    //при выходе из приложения сохранение
    void OnApplicationQuit()
    {
        Save();
    }
    //стрельба
    void Shoot()
    {
        if (PlayerPrefs.GetInt("Play1") == 1)
        {
            ShootPoint(ShootPoints);
        }
        if (PlayerPrefs.GetInt("Play2") == 1)
        {
            ShootPoint(ShootPoints1);
        }
        if (PlayerPrefs.GetInt("Play3") == 1)
        {
            ShootPoint(ShootPoints2);
        }
        SoundManag.PlaySound(3);
    }
    void ShootPoint(Transform[] shootPoints)
    {
        //из каждой точки вылета пули, создаем пулю 
        foreach (Transform sp in shootPoints)
        {
            GameObject b = Instantiate(Bullet, sp.position, Quaternion.identity) as GameObject;
            Destroy(b, 6);
            if (sp == shootPoints[shootPoints.Length - 1])
            {
                StartCoroutine(ShootDelay()); //выстреливаем с задержкой
            }
        }
    }
    //создание ракеты
    public void RocketShoot()
    {
        if (RocketCount > 0)
        {
            GameObject r = Instantiate(Rocket, transform.position, Quaternion.identity) as GameObject;
            RocketCount--;
            SoundManag.PlaySound(2);
            Destroy(r, 7);
        }
    }
    //задержка стрельбы
    IEnumerator ShootDelay()
    {
        IsReadyToShot = false;
        yield return new WaitForSeconds(ShootDelayShip);
        IsReadyToShot = true;
    }
    public void Fire(bool fire)
    {
        IsFire = fire;
    }
    //получение урона по кораблю
    public void Damage(int dmg)
    {
        Life_points -= dmg;
        if (Life_points < 0)
        {
            Life_points = 0;
        }
        ChangeLife();
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("enemyB")) // при соприкосновении с пулей врага, отнимаетс 1хп
        {
            Damage(1);
            Destroy(coll.gameObject);
        }
        if (coll.gameObject.CompareTag("coin")) //при соприкосновении с монетой, добавлется монета
        {
            Destroy(coll.gameObject);
            CoinsCount++;
            SoundManag.PlaySound(4);
            Save();
        }
        if (coll.gameObject.CompareTag("enemy")) // при соприкосновении с врагов, уничтожается враг и нам наносится 2хпы
        {
            Damage(2);
            SoundManag.PlaySound(0);
            Destroy(coll.gameObject);
        }
    }
}

