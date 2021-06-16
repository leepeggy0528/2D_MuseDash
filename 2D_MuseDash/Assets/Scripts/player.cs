using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    #region 欄位
    [Header("跳躍高度"), Range(0, 3000)]
    public float jump=1000;
    [Range(0, 2000)]
    public int blood = 500;
    [Header("是否在地上"),Tooltip("可調整位置")]
    public bool isGround = false;
    [Header("查看地板半徑"), Range(0.1f, 1f)]
    public float groundRadius = 0.5f;
    [Header("檢查地板的位移")]
    public Vector3 grounOffset;
    [Header("音效")]
    public AudioClip soundjump;
    public AudioClip soundattack;

    private int score;
    private AudioSource aus;
    private Rigidbody2D rig;
    private Animator ani;



    #endregion

    #region 方式
    /// <summary>
    /// 跳躍
    /// </summary>
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.J) && isGround)
        {
            ani.SetTrigger("jump");
            rig.AddForce(new Vector2(0, jump));//剛體,添加推力(二維向量)
            aus.PlayOneShot(soundjump);
        }
        else if(Input.GetKeyDown(KeyCode.J) && !isGround)
        {
            ani.SetTrigger("jump");
            transform.position = new Vector3(-6.87f, 0.6f, 0.96f);
            rig.velocity = Vector2.zero;
            aus.PlayOneShot(soundjump);
        }
        //碰到物件=2D物理(覆蓋中心,半徑)
        //圖層 Laymask 寫法 1<< 8
        Collider2D hit = Physics2D.OverlapCircle(transform.position + grounOffset, groundRadius,1);

        //如果碰到東西存在且碰到的名稱是地板就代表在地板上
        if(hit && hit.name == "地板碰撞")
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }

    /// <summary>
    /// 攻擊
    /// </summary>
    private void attack()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ani.SetTrigger("run");
            transform.position = new Vector3(-6.87f, -3.296f, 0.96f);
            aus.PlayOneShot(soundattack);
        }
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
    public bool dead()
    {
        ani.SetBool("dead", true);
        return ani.GetBool("dead");
    }

    /// <summary>
    /// 加分
    /// </summary>
    /// <param name="add">要加的分數</param>
    private void addscore(int add)
    {

    }
    #endregion
    
    # region 事件
        private void Start()
        {
            //動畫元件=取得元件<泛形>()
            ani = GetComponent<Animator>();
            rig = GetComponent<Rigidbody2D>();
            aus = GetComponent<AudioSource>();
    }

        private void Update()
        {
        if (dead()) return;    
        Jump();
            attack();
        }

        //繪製圖示:輔助用
        private void OnDrawGizmos()
        {
            //決定顏色
            Gizmos.color = new Color(1, 0, 0, 0.35f);
            //繪製圖形
            Gizmos.DrawSphere(transform.position + grounOffset, groundRadius);
        }
    #endregion
}
