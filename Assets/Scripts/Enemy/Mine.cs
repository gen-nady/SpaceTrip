using UnityEngine;

public class Mine : MonoBehaviour
{
    static readonly float DistToActive = 5f;
    static ShipControl Ship;
    static readonly float Speed = 2.5f;
    static SoundManager SoundManag;
    void Start()
    {
        SoundManag = GameObject.Find("PlayZone").GetComponent<SoundManager>();
        Ship = GameObject.Find("shipPlayer").GetComponent<ShipControl>();
    }
    void Update()
    {
        //при приближении к кораблю на определеную дистанцию, начать слежение за кораблем
        if (Vector2.Distance(transform.position, Ship.transform.position) <= DistToActive && !Ship.IsOver)
        {
            transform.position = Vector2.MoveTowards(transform.position, Ship.transform.position, Time.deltaTime * Speed);
        }
        //при приближении к кораблю,  наносим ему урон 
        if (Vector2.Distance(transform.position, Ship.transform.position) <= 0.75f && !Ship.IsOver)
        {
            Ship.Damage(3);
            SoundManag.PlaySound(0);
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("shipB")) //при уничтожении мини, дается 100 очков
        {
            Destroy(gameObject);
            Destroy(coll.gameObject);
            SoundManag.PlaySound(0);
            Ship.AddScore(100);
        }
        if (coll.gameObject.CompareTag("enemyB"))  //при уничтожении противником, просто уничтожается
        {
            Destroy(gameObject);
            Destroy(coll.gameObject);
            SoundManag.PlaySound(0);
        }
    }
}
