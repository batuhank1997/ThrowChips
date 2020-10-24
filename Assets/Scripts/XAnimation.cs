using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XAnimation : MonoBehaviour
{
    public Animator anim;
    private void Update()
    {
        anim.Play("10xAnimation");
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
