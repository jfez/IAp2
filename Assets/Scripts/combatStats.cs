using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combatStats : MonoBehaviour
{
    public float poder;
    public int rango;
    public bool puedeMoverse = true;

    public void combate(GameObject enemy){
        float poderEnemigo = enemy.GetComponent<combatStats>().poder;

        float poderTotal = poder + poderEnemigo;
        float porcentajeAtacante = poder / poderTotal;

        float numeroRandom = Random.Range(0f, 1f);

        StartCoroutine(combatCheck(enemy, numeroRandom, porcentajeAtacante));  
    }

    IEnumerator combatCheck(GameObject enemy, float numeroRandom, float porcentajeAtacante){
        yield return StartCoroutine(checkDistance(enemy));

        if (numeroRandom <= porcentajeAtacante){
            enemy.GetComponent<SimplePropagator>().removePropagator();
            enemy.SetActive(false);
        } else {
            this.gameObject.GetComponent<SimplePropagator>().removePropagator();
            this.gameObject.SetActive(false);
        }        
    }

    IEnumerator checkDistance(GameObject enemy){
        while(Vector3.Distance(this.gameObject.transform.position, enemy.gameObject.transform.position) > 1){
            yield return null;
        }
    }

    IEnumerator cityCheck(GameObject ciudad){
        yield return StartCoroutine(checkDistance(ciudad));

        ciudad.GetComponent<vidaCuidad>().vida -= poder;

        if (ciudad.GetComponent<vidaCuidad>().vida <= 0){
            ciudad.SetActive(false);
        }

        this.gameObject.SetActive(false);
    }

    public void atacarCiudad(GameObject ciudad){
        StartCoroutine(cityCheck(ciudad));
    }
}
