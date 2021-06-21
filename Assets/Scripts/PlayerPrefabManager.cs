using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefabManager : MonoBehaviour
{
    public GameObject[] cannons;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Player").GetComponent<PlayerMovement>().cannons=cannons;
    }
}
