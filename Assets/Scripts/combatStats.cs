using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combatStats : MonoBehaviour
{
    public float poder;

    public void combate(GameObject enemy){
        float poderEnemigo = enemy.GetComponent<combatStats>().poder;

        float poderTotal = poder + poderEnemigo;
        float porcentajeAtacante = poder / poderTotal;

        float numeroRandom = Random.Range(0f, 1f);

        if (numeroRandom <= porcentajeAtacante){
            enemy.GetComponent<SimplePropagator>().removePropagator();
            enemy.SetActive(false);
        } else {
            this.gameObject.GetComponent<SimplePropagator>().removePropagator();
            this.gameObject.SetActive(false);
        }
    }

    public void atacarCiudad(GameObject ciudad){
        ciudad.GetComponent<vidaCuidad>().vida -= poder;

        if (ciudad.GetComponent<vidaCuidad>().vida <= 0){
            ciudad.SetActive(false);
        }

        this.gameObject.SetActive(false);
    }
}
