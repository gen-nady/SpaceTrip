using UnityEngine;

public class RocketBonus : MonoBehaviour
{
    static ShipControl Ship;
    static SoundManager SoundManag;
    void Start()
    {
        SoundManag = GameObject.Find("PlayZone").GetComponent<SoundManager>();
        Ship = GameObject.Find("shipPlayer").GetComponent<ShipControl>();
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ship")) //добавление 3х ракет
        {
            Destroy(gameObject);
            Ship.RocketCount += 3;
            SoundManag.PlaySound(4);
        }
    }
}
