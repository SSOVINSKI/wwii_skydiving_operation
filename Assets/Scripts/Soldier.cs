using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    public static Soldier soldier;
    public Rigidbody2D rg;
    public float skalaAnimacjiGoraDol = 1f;
    public float predkoscAnimacjiGoraDol = 1f;
    public float silaFlapu = 5f;
    private bool zyje = true;
    public Animator animator;
    public AudioClip flapAC;
    public AudioClip dieAC;
    public AudioClip failAC;
    public AudioClip punktAC;

    public bool Zyje
    {
        get
        {
            return zyje;
        }

        set
        {
            zyje = value;
            animator.SetBool("Zyje", value);
        }
    }



    private void Awake()
    {
        soldier = this;
        animator = GetComponent<Animator>();
    }
   
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        rg.gravityScale = 1.5f;
        Zyje = true;
    }

    
    void FixedUpdate()
    {
        if (Menu.gameOver)
        {
            transform.Translate(new Vector3(0, Mathf.Sin(Time.time * predkoscAnimacjiGoraDol) * Time.deltaTime * skalaAnimacjiGoraDol), Space.World);
        }
        else
        {
            
        }
    }

    private void Update()
    {
        if (!Menu.gameOver)
        {
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    Flap();
                }
            }
            if (Input.GetButtonDown("Fire1"))
            {
                Flap();
            }
        }
    }

    void Flap()
    {
        rg.velocity = Vector2.zero;
        rg.AddForce(new Vector2(0, silaFlapu), ForceMode2D.Impulse);
        animator.SetTrigger("Flap");
        Menu.instance.dzwiekiAS.PlayOneShot(flapAC);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Zyje = false;
        Menu.gameOver = true;
        GetComponent<PolygonCollider2D>().isTrigger = true;
        rg.AddForce(new Vector2(1f, 1f) * 5f , ForceMode2D.Impulse);
        Menu.Highscore = Menu.Punkty;
        PlayerPrefs.SetInt("Highscore", Menu.Highscore);
        StartCoroutine(NaSmierci());
        Menu.instance.dzwiekiAS.PlayOneShot(failAC);
    }
    IEnumerator NaSmierci()
    {
        yield return new WaitForSeconds(0.5f);
        Menu.instance.dzwiekiAS.PlayOneShot(dieAC);
        yield return new WaitForSeconds(0.5f);
        Menu.instance.respawnMenuTr.gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Zyje == false)
            return;
        collision.gameObject.SetActive(false);
        Menu.Punkty++;
        Menu.instance.dzwiekiAS.PlayOneShot(punktAC);
    }
}
