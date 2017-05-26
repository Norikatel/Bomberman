using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planter:MonoBehaviour
{
    private float radius = 1;
    float lifeTime = 2f;
    private int maxBomb=1;
    private int currentBomb=0;
    private UIController scoreCounter;
    ResourceLoader resourceLoader = new ResourceLoader();

    public int MaxBomb { get { return maxBomb; } }
    public int Radius { get { return (int)radius; } }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentBomb < maxBomb)
            StartCoroutine(PlantBomb());     
    }
    private void Start()
    {
        scoreCounter = GetComponent<UIController>();
    }

    public void GiveOneMoreBomb() {
        maxBomb++;
    }

    public void SetMoreRadius()
    {
        radius++;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bomb"))
            other.isTrigger = false;
    }

    private IEnumerator PlantBomb()
    {
        GameObject bomb = resourceLoader.LoadBomb();       
        bomb = Instantiate(bomb,
            new Vector3(transform.position.x, bomb.transform.localScale.y / 2,transform.position.z).RoundPosition(),
            new Quaternion(0, 0, 0, 0));
        AddLetForAllEnemyPro(bomb);
        currentBomb++;
        yield return new WaitForSeconds(lifeTime);
        Effects.Explode(bomb, resourceLoader.LoadExplodeEffect(), radius);
        DeleteLetForAllEnemyPro(bomb);
        currentBomb--;
    }

    public void DestroyObject(GameObject gameObject)
    {
        StartCoroutine(Effects.FadeDeactivate(gameObject));
        CheckAnotherAction(gameObject);
    }

    private void CheckAnotherAction(GameObject gameObject)
    {
        switch (gameObject.tag)
        {
            case "BreakableWall":
                DeleteLetForAllEnemyPro(gameObject);
                PowerUpsLoader.GenerateRandomPowerUp(gameObject.transform.position);
                break;
            case "Enemy":
                scoreCounter.AddScoreForEnemy();
                break;
            case "EnemyPro":
                scoreCounter.AddScoreForProEnemy();
                break;
        }
    }

    private static void DeleteLetForAllEnemyPro(GameObject gameObject)
    {
        foreach (var enemy in GameObject.FindGameObjectsWithTag("EnemyPro"))
        {
            enemy.GetComponent<IntelligentMove>().DeleteLet(gameObject.transform.position);
        }
    }

    private static void AddLetForAllEnemyPro(GameObject gameObject)
    {
        foreach (var enemy in GameObject.FindGameObjectsWithTag("EnemyPro"))
        {
            enemy.GetComponent<IntelligentMove>().AddLet(gameObject.transform.position);
        }
    }
}
