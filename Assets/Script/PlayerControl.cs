using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerControl : MonoBehaviour
{
    public GameObject[] Soul;
    float moveSpeed;
    int score;
    public TextMeshProUGUI tmp;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 3f;
        score = 0;
        // StartCoroutine(ColorBack());
        StartCoroutine("SoulCreate");
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal"); //가로, 오른쪽 버튼 : 0에서 점점 1로 증가, 왼쪽 버튼 : 0에서 -1로 점점 감소, 다 떼면 점점 0으로 감
        float v = Input.GetAxis("Vertical"); //세로
        Vector3 dir = new Vector3(h,v,0);
        //transform.position += dir; //값을 강제로 주입 => 벽도 뚫음
        GetComponent<Rigidbody2D>().velocity = dir*moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.collider.CompareTag("WALL")){
            GetComponent<SpriteRenderer>().color = collision.collider.GetComponent<SpriteRenderer>().color;

            StopCoroutine("ColorBack");
            StartCoroutine("ColorBack");
        }   

    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("SOUL")){
            //나와 소울 색이 같다면 소울 사라짐
            if(GetComponent<SpriteRenderer>().color == collision.GetComponent<SpriteRenderer>().color){
                Destroy(collision.gameObject);
                moveSpeed += 0.5f;
                score += 1;
                GetComponent<AudioSource>().Play();
            }

            //나와 소울 색이 다르다면 소울 도망감
            if(GetComponent<SpriteRenderer>().color != collision.GetComponent<SpriteRenderer>().color){
                float x = Random.Range(-10f,10f);
                float y = Random.Range(-4f,5f);
                Vector3 pos = new Vector3(x,y,0);
                collision.transform.position = pos;
                moveSpeed -= 0.5f;
                score -= 1;
            }
            tmp.text = "score : " + score;
        }
    }

    IEnumerator ColorBack(){
        yield return new WaitForSeconds(3f);
        
        // Color color = new Color32(255,255,255,255); //rgb
        // GetComponent<SpriteRenderer>().color = color;
        GetComponent<SpriteRenderer>().color = Color.white;

    }

    IEnumerator SoulCreate(){
        yield return new WaitForSeconds(2f);
        //생성 코드
        //Instantiate(gameobject,위치,회전);
        //Destroy(gameobject);
        //GameObject.SetActive(true or false)

        float x = Random.Range(-10f,10f);
        float y = Random.Range(-4f,5f);
        Vector3 pos = new Vector3(x,y,0);

        int i = Random.Range(0,3+1);
        Instantiate(Soul[i],pos,Quaternion.identity);

        StartCoroutine("SoulCreate");

    }

    public void Left(){
        transform.position += new Vector3(-1,0,0)*moveSpeed*0.4f;
    }
    public void Right(){
        transform.position += new Vector3(1,0,0)*moveSpeed*0.4f;
    }
    public void Up(){
        transform.position += new Vector3(0,1,0)*moveSpeed*0.4f;
    }
    public void Down(){
        transform.position += new Vector3(0,-1,0)*moveSpeed*0.4f;
    }
}
