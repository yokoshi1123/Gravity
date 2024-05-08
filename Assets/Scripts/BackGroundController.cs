using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    // �w�i�̖���
    int spriteCount = 2;
    // �w�i����荞��
    float rightOffset = 1.5f;
    float leftOffset = -0.5f;

    Transform bgTfm;
    SpriteRenderer mySpriteRndr;
    float width;

    void Start()
    {
        bgTfm = transform;
        mySpriteRndr = GetComponent<SpriteRenderer>();
        width = mySpriteRndr.bounds.size.x;
    }


    void Update()
    {
        // ���W�ϊ�
        Vector3 myViewport = Camera.main.WorldToViewportPoint(bgTfm.position);
        // Debug.Log(name + ":" + myViewport);

        // �w�i�̉�荞��(�J������X���v���X�����Ɉړ���)
        if (myViewport.x < leftOffset)
        {
            bgTfm.position += Vector3.right * (width * spriteCount);
        }
        // �w�i�̉�荞��(�J������X���}�C�i�X�����Ɉړ���)
        else if (myViewport.x > rightOffset)
        {
            bgTfm.position -= Vector3.right * (width * spriteCount);
        }
    }
}