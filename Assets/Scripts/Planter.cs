using Assets.Scripts;
using System.Collections;
using UnityEngine;

public class Planter : MonoBehaviour
{
    float bombEdge = 0.6f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(PlantBomb());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bomb"))
        {
            other.isTrigger = false;
        }
    }

    private IEnumerator PlantBomb()
    {
        GameObject bomb = new ResourceLoader().LoadBomb();
        bomb.transform.localScale = new Vector3(bombEdge, bombEdge, bombEdge);
        bomb = Instantiate(bomb, transform.position, new Quaternion(0, 0, 0, 0));
        yield return new WaitForSeconds(2f);
        bomb.SetActive(false);
    }

}
