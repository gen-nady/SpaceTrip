using UnityEngine;

public class BonusLife : MonoBehaviour
{
    static ShipControl Ship;
    static SoundManager SoundManag;
    void Start()
    {
        Ship = GameObject.Find("shipPlayer").GetComponent<ShipControl>();
        SoundManag = GameObject.Find("PlayZone").GetComponent<SoundManager>();
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ship"))  // при соприкосновении с кораблем дает +2 жизни
        {
            Destroy(gameObject);
            if (Ship.Life_points < 9)
            {
                Ship.Life_points += 2;
            }
            else
            {
                Ship.Life_points = 10;
            }
            SoundManag.PlaySound(4);
        }
    }
}
