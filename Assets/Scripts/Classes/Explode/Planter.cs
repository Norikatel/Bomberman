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
    private ScoreCounter scoreCounter;
    ResourceLoader resourceLoader = new ResourceLoader();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentBomb < maxBomb)
            StartCoroutine(PlantBomb());     
    }
    private void Start()
    {
        scoreCounter = GetComponent<ScoreCounter>();
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
        currentBomb++;
        yield return new WaitForSeconds(lifeTime);
        Effects.Explode(bomb, resourceLoader.LoadExplodeEffect(), radius);
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
}
