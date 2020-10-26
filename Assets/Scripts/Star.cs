using UnityEngine;

public class Star : MonoBehaviour
{
    public GameObject Playzone;
    void Update()
    {
        //уничтожение звезд после преодоления по позициии х=-10
        if (transform.position.x <= -10)
        {
            Destroy(gameObject);
        }
    }
}
