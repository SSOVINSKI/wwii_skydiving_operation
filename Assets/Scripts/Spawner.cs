using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;
    public Vector2 spawnPointMin, spawnPointMax;
    public float przerwaPomiedzyWiezami;
    public float spawnTimer, spawnTimerDef ;

    public Wieza wiezaPrefab;
    public List<Wieza> zespanowaneWieze = new List<Wieza>();
 
    void Start()
    {
        instance = this;
    }

    
    void Update()
    {
        if (Menu.gameOver)
            return;
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnujWieze();
            spawnTimer = spawnTimerDef;
        }
    }
    void SpawnujWieze()
    {
        Wieza wieza1 = Instantiate(wiezaPrefab.gameObject, new Vector3(spawnPointMin.x, Random.Range(spawnPointMin.y, spawnPointMax.y)), Quaternion.identity).GetComponent<Wieza>();
        Wieza wieza2 = Instantiate(wiezaPrefab.gameObject, new Vector3(wieza1.transform.position.x,  wieza1.transform.position.y + przerwaPomiedzyWiezami), Quaternion.identity).GetComponent<Wieza>();
        wieza2.transform.eulerAngles += new Vector3(0, 0, 0);
        wieza2.transform.GetChild(0).gameObject.SetActive(false);
        zespanowaneWieze.Add(wieza1);
        zespanowaneWieze.Add(wieza2);
    }
}
