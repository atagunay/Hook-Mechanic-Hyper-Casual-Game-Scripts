using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] levelArr = new GameObject[9];

    // Start is called before the first frame update
    void Start()
    {
        int index = Random.Range(0, 8);
        Instantiate(levelArr[index], Vector3.zero, quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
