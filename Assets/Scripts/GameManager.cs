using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gM;

    private void Awake()
    {
        if (gM == null) //No existe
        {
            gM = this; //Por lo tanto, gM es este objeto
        }
        else if (gM != this) // Existe, pero, no es este, es un false gM
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
