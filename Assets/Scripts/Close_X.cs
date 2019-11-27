using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Close_X : MonoBehaviour
{

    public void Close()
    {
        GameObject.Find("GridManager").GetComponent<TileMouseOver>().menuOpen = false;
        transform.parent.gameObject.SetActive(false);
    }
}
