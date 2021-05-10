using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    #region 欄位
    [Header("跳躍高度"), Range(0, 3000)]
    public float jump=100;
    [Range(0, 2000)]
    public int blood = 500;
    [Header("是否在地上"),Tooltip("可調整位置")]
    public bool flood = false;

    private int score = 0;
    private AudioClip ac1;
    private AudioClip ac2;
    private AudioSource aus;
    private Rigidbody2D rb2D;
    private Animator ani;
    #endregion

    #region 方式

    #endregion
}
