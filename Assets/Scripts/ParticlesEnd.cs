using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesEnd : MonoBehaviour
{
    private ParticleSystem ps;
    private bool isStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ps)
        {
            if (ps.IsAlive())
            {
                isStarted = true;
            }
            else if (isStarted && !ps.IsAlive())
            {
                Destroy(this.gameObject);
            }
        }
    }
}
