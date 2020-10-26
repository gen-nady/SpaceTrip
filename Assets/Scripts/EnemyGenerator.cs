using System.Collections;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [Header("Бонусы")]
    public GameObject Bonus;
    [Header("Враги")]
    public GameObject[] Enemys;
    static float MinDelay = 2f, MaxDelay = 6f;
    static readonly float MinYpos = -4.5f, MaxYpos = 4.5f, Delay = 45f;
    float LevelDifficult = 1;
    void Start()
    {
        StartCoroutine(Spawn());
    }
    void Repeat()
    {
        StartCoroutine(DoCheck()); 
        StartCoroutine(Spawn());   //зацикливаем
    }
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(Random.Range(MinDelay, MaxDelay));  //рандомим задержку
        Vector2 pos = new Vector2(transform.position.x, Random.Range(MinYpos, MaxYpos)); //задаем позиции врагов тоже рандомно
        GameObject enemyInstantiate = Instantiate(Enemys[Random.Range(0, Enemys.Length)], pos, Quaternion.identity) as GameObject; //генерируем врагов
        Destroy(enemyInstantiate, 11);
        Vector2 bonus_pos = new Vector2(transform.position.x, Random.Range(MinYpos, MaxYpos));  //задаем позиции метеорита
        int r = Random.Range(0, 100); //задаем рандомный шанс выпадения метеорита        
        if (r <= 10)
        {
            GameObject b = Instantiate(Bonus, bonus_pos, Quaternion.identity) as GameObject; //генерируем метеорит
            Destroy(b, 10);
        }
        Repeat();
    }
    IEnumerator DoCheck()
    {
        yield return new WaitForSeconds(Delay);  //усложнение игры. Чем дольше игра, тем меньше минимальна и максимальна задержка появления врагов
        LevelDifficult++;
        if (LevelDifficult < 7)
        {
            if (LevelDifficult % 2 == 0)
            {
                if (MaxDelay > 3)
                {
                    MaxDelay -= 1f;
                }
                else
                {
                    MaxDelay = 3f;
                }

            }
            if (LevelDifficult % 2 == 1)
            {

                if (MinDelay > 1f)
                {
                    MinDelay -= 1f;
                }
                else
                {
                    MinDelay = 1f;
                }
            }
        }
        else
        {
            MinDelay = 0.5f;
            MaxDelay = 2f;
        }

    }
}
