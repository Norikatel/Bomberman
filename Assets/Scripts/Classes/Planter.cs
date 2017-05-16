using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planter : Exploder
{
    float lifeTime = 2f;
    private int maxBomb=1;
    private int currentBomb=0;
    ResourceLoader resourceLoader = new ResourceLoader();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentBomb < maxBomb)
            StartCoroutine(PlantBomb());     
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
            new Vector3(transform.position.x, bomb.transform.localScale.y / 2,transform.position.z),
            new Quaternion(0, 0, 0, 0));
        currentBomb++;
        yield return new WaitForSeconds(lifeTime);
        Explode(bomb, resourceLoader.LoadExplodeEffect(),DestroyObjects);
        currentBomb--;
    }

    private void DestroyObjects(List<RaycastHit> raycastHitList) {
        foreach (var hit in raycastHitList) {
            if (hit.collider.CompareTag("BreakableWall")|| 
                hit.collider.CompareTag("Player") || hit.collider.CompareTag("Enemy"))
               hit.transform.gameObject.SetActive(false);
        }
    }
}
