using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal"); //가로, 오른쪽 버튼 : 0에서 점점 1로 증가, 왼쪽 버튼 : 0에서 -1로 점점 감소, 다 떼면 점점 0으로 감
        float v = Input.GetAxis("Vertical"); //세로
        Vector3 dir = new Vector3(h,v,0);
        //transform.position += dir; //값을 강제로 주입 => 벽도 뚫음
        GetComponent<Rigidbody2D>().velocity = dir*3;
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.collider.CompareTag("WALL")){
            GetComponent<SpriteRenderer>().color = collision.collider.GetComponent<SpriteRenderer>().color;
        }   
    }

}
