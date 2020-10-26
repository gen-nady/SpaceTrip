using UnityEngine;

public class Rocket : MonoBehaviour
{
    static SoundManager SoundManag;
    void Start()
    {
        SoundManag = GameObject.Find("PlayZone").GetComponent<SoundManager>();
    }
    void OnCollisionEnter2D(Collision2D coll)
    {

        if (coll.gameObject.CompareTag("enemy"))  // при соприкосновении с врагом
        {
            coll.gameObject.GetComponent<SimpleEnemy>().Damage(10);    
            Destroy(gameObject);
            SoundManag.PlaySound(0);
        }
        if (coll.gameObject.CompareTag("enemyB") || coll.gameObject.CompareTag("mine")) //при соприкосновении с пулей или миной
        {
            Destroy(coll.gameObject);
            Destroy(gameObject);
            SoundManag.PlaySound(0);
        }
    }
}
