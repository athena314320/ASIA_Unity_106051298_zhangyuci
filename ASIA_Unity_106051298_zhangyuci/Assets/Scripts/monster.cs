using UnityEngine;

public class monster : MonoBehaviour
{
    #region 欄位區域
    [Header("移動速度")]
    [Range(1, 5000)]
    public int speed = 2000;         
    [Header("旋轉速度"), Tooltip("怪物的旋轉速度"), Range(1.5f, 500f)]
    public float turn = 20.5f;            
    [Header("玩家名稱")]
    public string name = "怪物";

    #endregion


    public Transform tran;
    public Rigidbody rig;
    public Animator ani;
    public AudioSource aud;

    public AudioClip soundBark;

    [Header("檢物品位置")]
    public Rigidbody rigCatch;

    private void Update()
    {
        Turn();
        Run();
        Attack();
    }

    private void OnTriggerStay(Collider other)
    {
        print(other.name);

        if (other.name == "手榴彈"&& ani.GetCurrentAnimatorStateInfo(0).IsName("攻擊"))
        {
            Physics.IgnoreCollision(other,GetComponent<Collider>());
            other.GetComponent<HingeJoint>().connectedBody = rigCatch;
        }

        if (other.name == "偵測" && ani.GetCurrentAnimatorStateInfo(0).IsName("攻擊"))
        {
            GameObject.Find("手榴彈").GetComponent<HingeJoint>().connectedBody = null;
        }
    }
    #region 方法區域
    private void Run()
    {
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("攻擊")) return;
        float v = Input.GetAxis("Vertical");
        rig.AddForce(tran.forward * speed * v * Time.deltaTime);
        ani.SetBool("跑步開關", v != 0);
    }

    private void Turn()
    {
        float h = Input.GetAxis("Horizontal");     
        tran.Rotate(0, turn * h * Time.deltaTime, 0);
    }


    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ani.SetTrigger("攻擊觸發器");

            aud.PlayOneShot(soundBark, 10f);
        }

    }
    #endregion
}
