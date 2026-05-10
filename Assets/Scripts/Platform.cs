using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float delayBeforeDisappear = 2f;

    private bool triggered = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!triggered && collision.gameObject.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(Disappear());
        }
    }

    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(delayBeforeDisappear);

        gameObject.SetActive(false);
    }
}