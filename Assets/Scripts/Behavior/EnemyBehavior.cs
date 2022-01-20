using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]private GameObject ragdoll;
    [SerializeField]private GameObject mainbody;

    private Animator anim;
    private CapsuleCollider cap;

    private void Awake()
    {
        cap = GetComponent<CapsuleCollider>();
    }

    public void KillEnemy()
    {
        mainbody.SetActive(false);
        ragdoll.SetActive(true);
        cap.enabled = false;
    }
}
