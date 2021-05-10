using UnityEngine;

public class APIStatic : MonoBehaviour
{
    public Vector3 a = new Vector3(1, 1, 1);
    public Vector3 b = new Vector3(22, 22, 22);

    private void Start()
    {
        //屬性-得到
        print("Random=" + Random.value);

        print("所有攝影機的數量"+Camera.allCamerasCount);
        print("2D重力大小"+Physics2D.gravity);


        //屬性-設定
        Cursor.visible = false;
        Physics2D.gravity=new Vector2(0,20) ;

        //方法
        int r = Random.Range(50, 100);
        print("隨機攻擊" + r);
        Application.OpenURL("http://unity.com/");
        print("9.999去小數點"+Mathf.Floor(9.999f));

        print(Vector3.Distance(a,b));

        Input.GetKeyDown(KeyCode.Space);
        print(Input.GetKeyDown(KeyCode.Space));
    }

    private void Update()
    {
        print(Input.anyKeyDown);
        //print(Time.timeSinceLevelLoad);
    }
}
