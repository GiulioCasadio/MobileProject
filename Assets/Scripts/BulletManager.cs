using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public GameObject explosion;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(startPos, this.transform.position)>=30)
        {
            GameObject e;
            e = Instantiate(explosion, this.transform.position, Quaternion.identity);
            e.GetComponent<ParticleSystem>().Play();
            Destroy(this.gameObject);
        }
    }
}
