using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("落ちたボールの名前 : " + other.gameObject.name);
        // 穴に落ちたボールを非アクティブにする。
        other.gameObject.SetActive(false);
    }
}
