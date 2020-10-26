using UnityEngine;

public class ShipBullet : MonoBehaviour
{
    static SoundManager SoundManag;
    void Start()
    {
        SoundManag = GameObject.Find("PlayZone").GetComponent<SoundManager>();
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("enemyB")) //при соприкосновении с пулей уничтожает обе пули
        {
            Destroy(coll.gameObject);
            Destroy(gameObject);
            SoundManag.PlaySound(1);
        }
    }
}
