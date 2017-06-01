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
    Animator animator;
    public AudioClip dropBomb;
    public AudioClip bigBoom;
    public AudioClip death;
    AudioSource sound;

    public int MaxBomb { get { return maxBomb; } }
    public int Radius { get { return (int)radius; } }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentBomb < maxBomb)
            StartCoroutine(PlantBomb());     
    }
    private void Start()
    {
        sound = GetComponentInChildren<AudioSource>();
        scoreCounter = GetComponent<UIController>();
        animator = GetComponent<Animator>();
    }

    public void PlayPutBombSound() {
        sound.PlayOneShot(dropBomb);
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
        animator.SetTrigger("SetBomb");
        GameObject bomb = resourceLoader.LoadBomb();       
        bomb = Instantiate(bomb,
            new Vector3(transform.position.x, bomb.transform.localScale.y / 2,transform.position.z).RoundPosition(),
            new Quaternion(0, 0, 0, 0));
        AddBarrierForAllEnemyPro(bomb);
        currentBomb++;
        yield return new WaitForSeconds(lifeTime);
        sound.PlayOneShot(bigBoom);
        Effects.Explode(bomb, resourceLoader.LoadExplodeEffect(), radius);
        DeleteBarrierForAllEnemyPro(bomb);
        currentBomb--;
    }

    public void DestroyObject(GameObject gameObject)
    {
        ActionDeactivate(gameObject);
    }

    private void ActionDeactivate(GameObject gameObject)
    {
        switch (gameObject.tag)
        {
            case "BreakableWall":
                StartCoroutine(Effects.FadeDeactivate(gameObject));
                DeleteBarrierForAllEnemyPro(gameObject);
                PowerUpsLoader.GenerateRandomPowerUp(gameObject.transform.position);
                break;
            case "Enemy":
                gameObject.GetComponent<AutoMoveDynamicObject>().enabled = false;
                StartCoroutine(Effects.DelayDeactivate(gameObject));
                gameObject.GetComponent<Animator>().SetTrigger("Killing");
                scoreCounter.AddScoreForEnemy();
                break;
            case "EnemyPro":
                StartCoroutine(Effects.FadeDeactivate(gameObject));
                scoreCounter.AddScoreForProEnemy();
                break;
            case "Player":
                Kill();
                break;
        }
    }

    public void Kill()
    {
        GetComponent<PlayerMoving>().enabled = false;
        StartCoroutine(Effects.DelayDeactivate(gameObject));
        animator.SetTrigger("Killing");
    }

    public void PlayDeathSound() {
        sound.PlayOneShot(death);
    }

    private static void DeleteBarrierForAllEnemyPro(GameObject gameObject)
    {
        foreach (var enemy in GameObject.FindGameObjectsWithTag("EnemyPro"))
            enemy.GetComponent<IntelligentMove>().DeleteBarrier(gameObject.transform.position);
    }

    private static void AddBarrierForAllEnemyPro(GameObject gameObject)
    {
        foreach (var enemy in GameObject.FindGameObjectsWithTag("EnemyPro"))
            enemy.GetComponent<IntelligentMove>().AddBarrier(gameObject.transform.position);
    }
}
