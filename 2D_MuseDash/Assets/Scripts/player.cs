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

    private int score;
    private AudioClip ac1;
    private AudioClip ac2;
    private AudioSource aus;
    private Rigidbody2D rb2D;
    private Animator ani;
    #endregion

    #region 方式
    /// <summary>
    /// 跳躍
    /// </summary>
    private void Jump()
    {
         
    }

    /// <summary>
    /// 攻擊
    /// </summary>
    private void attack()
    {

    }

    /// <summary>
    /// 傷害
    /// </summary>
    /// <param name="hurt">受到的傷害</param>
    private void hit(int hurt)
    {

    }

    /// <summary>
    /// 死亡
    /// </summary>
    private bool dead()
    {
        return false;
    }

    /// <summary>
    /// 加分
    /// </summary>
    /// <param name="add">要加的分數</param>
    private void addscore(int add)
    {

    }
    #endregion
}
