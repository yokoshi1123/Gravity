using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class GravityObserver : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    public GravityManager gravityManager;

    [SerializeField] private float OBJ_WEIGHT;

    void Awake()
    {
        rb.gravityScale = gravityManager.G_SCALE;
        rb.mass = OBJ_WEIGHT;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("GravityField")) // �d�͏ꒆ�ɂ���Ƃ��AgravityManager�ł̕ύX��ǂݍ���
        {
            rb.gravityScale = gravityManager.gravityScale * OBJ_WEIGHT;
            rb.mass = OBJ_WEIGHT * Mathf.Min(0.5f, Mathf.Abs(rb.gravityScale / gravityManager.G_SCALE));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("GravityField")) // �d�͏ꂩ��o���Ƃ��A�f�t�H���g�ɖ߂�
        {
            rb.gravityScale = gravityManager.G_SCALE;
            rb.mass = OBJ_WEIGHT;
        }

    }
}
