using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class GravityObserver : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    public GravityManager gravityManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("GravityField")) // �d�͏ꒆ�ɂ���Ƃ��AgravityManager�ł̕ύX��ǂݍ���
        {
            rb.gravityScale = gravityManager.gravityScale;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("GravityField")) // �d�͏ꂩ��o���Ƃ��A�f�t�H���g�ɖ߂�
        {
            rb.gravityScale = gravityManager.G_SCALE;
        }

    }
}
