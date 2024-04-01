using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FONDOS : MonoBehaviour
{
    [SerializeField] Image imagen;
    [SerializeField] Sprite[] fondos;

    // pone un fondo random
    void Start()
    {
        int random = Random.Range(0, fondos.Length);
        imagen.sprite = fondos[random];
    }    
}
