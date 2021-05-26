using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public static Menu instance;

    public RectTransform menuTr;
    public RectTransform respawnMenuTr;
    public AudioSource dzwiekiAS;
    public Text punktyTxt;
    public Text highscoreTxt;
    public Button playBtn;
    public static bool gameOver = true;
    public Vector2 soldierStartPos;

    public Transform sky, sky1;
    public Vector2 /*skyDefPos,*/ sky1DefPos;

    private static int punkty;

    public static int Punkty
    {
        get
        {
            return punkty;
        }
        set
        {
            punkty = value;
            instance.punktyTxt.text = value.ToString();
        }
    }

    public static int Highscore
    {
        get
        {
            return highscore;
        }
        set
        {
            if (value > highscore)
            {
                highscore = value;
                instance.highscoreTxt.text = value.ToString();
            }
        }
    }

    private static int highscore;

    void Start()
    {
        //skyDefPos = sky.Transform.position;
        sky1DefPos = sky1.transform.position;
        soldierStartPos = Soldier.soldier.transform.position;
        instance = this;
        Highscore = PlayerPrefs.GetInt("Highscore", 0);
    }

    void FixedUpdate()
    {
        if (Soldier.soldier.Zyje)
            PrzesuwajTlo();
    }
    public void PlayBtn()
    {
        menuTr.gameObject.SetActive(false);
        Soldier.soldier.rg.isKinematic = false;
        gameOver = false;
    }
    void PrzesuwajTlo ()
    {
        float predkosc = Wieza.predkosc / 10f;
        float cameraXLeft = Camera.main.transform.position.x - Camera.main.orthographicSize * Screen.width / Screen.height;
        sky.Translate(Vector2.left * Time.deltaTime * predkosc , Space.World);
        sky1.Translate(Vector2.left * Time.deltaTime * predkosc, Space.World);
        if (sky.position.x <= cameraXLeft)
            sky.position = sky1DefPos;
        if (sky1.position.x <= cameraXLeft)
            sky1.position = sky1DefPos;

    }
    public void RespBtn()
    {
        Soldier.soldier.Zyje = true;
        gameOver = false;
        Soldier.soldier.GetComponent<PolygonCollider2D>().isTrigger = false;
        for (int i = Spawner.instance.zespanowaneWieze.Count-1; i >= 0  ; i--)
        {
            GameObject.Destroy(Spawner.instance.zespanowaneWieze[i].gameObject);
        }
        Spawner.instance.zespanowaneWieze.Clear();
        Soldier.soldier.transform.position = soldierStartPos;
        Soldier.soldier.rg.velocity = Vector2.zero;
        respawnMenuTr.gameObject.SetActive(false);
        Punkty = 0;
    }
}
