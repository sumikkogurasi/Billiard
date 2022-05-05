using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBall : MonoBehaviour
{
    // リセットのための初期位置
    Vector3 defaultPosition = new Vector3();
    // リジッドボディ
    Rigidbody rigid = null;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        defaultPosition = this.transform.localPosition;
    }

    /// <summary>
    /// リセット時の処理
    /// </summary>

    // Update is called once per frame
    public void Reset()
    {
        gameObject.SetActive(true);
        // リジッドボディの速度を強制的に0にする。
        rigid.velocity = Vector3.zero;
        // リジッドボディの回転速度を強制的に0にする。
        rigid.angularVelocity = Vector3.zero;
        // 初期位置に戻す
        this.transform.localPosition = defaultPosition;
    }
}
