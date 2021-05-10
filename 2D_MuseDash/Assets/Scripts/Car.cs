using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    #region 欄位
    //基礎

    //欄位屬性:放在欄位的前面或上面
    //語法:[屬性名稱(值)]
    //標題Header(字串)/提示Tooltip(字串)/範圍Range(最小值，最大值)
    [Header("這是汽車的cc數")]
    [Tooltip("可以調整汽車cc數的多寡")]
    [Range(1000,5000)]
    public int cc = 2000;
    [Header("汽車重量"),Range(1f,5f)]
    public float weight = 3.5f;
    [Range(1000, 5000)]
    public string brand = "賓士";
    [Range(1000, 5000)]
    public bool openCold = false;

    //其他
    public Color color;
    public Color red = Color.red;
    public Color yellow = Color.yellow;
    //color(red,green,blue)-0~1
    public Color Color1 = new Color(0.6f, 0.1f, 0.4f);
    //color(red,green,blue,aphle)-0~1
    public Color Color2 = new Color(0.7f, 0.3f, 0.2f,0.8f);

    //座標 vector2 - 4
    public Vector2 v2;
    public Vector2 v2One= Vector2.one;
    public Vector2 v2myl = new Vector2(3.5f, 9);

    //輸入按鍵keycode
    public KeyCode key;
    public KeyCode a = KeyCode.A;
    public KeyCode mouse = KeyCode.Mouse0;
    public KeyCode joy = KeyCode.Joystick2Button15;

    //遊戲物件與元件
    public GameObject obj;

    public Transform tra;
    public Camera cam;
    public AudioListener aud;
    #endregion

    #region 方法
    //方法 method - 儲存程式區域或演算法
    //語法:
    //修飾詞 回傳類型 名稱() {程式區塊或演算法}
    //void 無回傳 - 沒有回傳資料
    //方法必須被呼叫才會執行
    ///<summary>
    ///leepeggy 05.05
    ///這是一個會輸出訊息的測試方法
    /// </summary>

    private void test()
    {
        print("test");
    }

    private void Drive50()
    {
        print("speed:" + 50);
        print("music");
    }
    private void Drive100()
    {
        print("speed:" + 100);
        print("music");
    }
    private void Drive150()
    {
        print("speed:"+150);
        print("music");
    }

    //參數 paramater 語法: 類型 名稱 指定 預設值
    //有預設值的參數必須寫在最右邊(選填式參數)
    //呼叫的時候可以決定參數的值
    //類型 名稱 指定 預設值
    /// <summary>
    /// 開車方式
    /// </summary>
    /// <param name="speed">速度</param>
    /// <param name="sound">音效</param>
    /// <param name="effect">特效</param>
    private void drive(int speed, string sound="ㄎㄎㄎ" ,string effect="雜質")
    {
        print("speed:" + speed);
        print("sound:" + sound);
        print("effect:" + effect);
    }


    //有傳回方法必須有關鍵字 return 在大括號內
    //return 後必須要放傳回類型資料
    /// <summary>
    /// 平方
    /// </summary>
    /// <param name="number">要平方的數</param>
    /// <return>輸入數字的平方結果</return>
    private int square(int number) 
    {
        return number * number;
    }

    // BMI:體重/身高*身高
    /// <summary>
    /// BMI
    /// </summary>
    /// <param name="weight">體重(kg)</param>
    /// <param name="high">身高(m)</param>
    /// <returns>BMI 結果</returns>

    private float bmi(float weight,float high)
    {
        return weight / (high * high);
    }
    #endregion

    #region 事件
    //事件 Event - 在特定的時間點會依指定次數執行(程式的入口)
    //開始事件 : 播放遊戲會會執行一次
    //應用 : 初始值，初始金錢，生命值...等
    private void Start()
    {
        //呼叫方式
        //方法名稱();
        test();
        Drive50();
        drive(99,"咻咻咻","灰塵");
        drive(99, "轟轟轟", "爆炸");
        drive(10);
        drive(20, "嘿嘿嘿");
        int r = square(9);
        print("9的平方:" + r);

        float l = bmi(50.2f, 153);
        print("BMI=" + l);
    }
    #endregion



}
