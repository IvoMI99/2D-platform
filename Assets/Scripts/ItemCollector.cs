using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    private int BananasCollected = 0;
    [SerializeField] private AudioSource collectSoundEffect;
    [SerializeField] private TextMeshProUGUI bananasText;
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Banana"))
        {
            Destroy(collision.gameObject);
            BananasCollected++;
            bananasText.text = "Bananas: " + BananasCollected;
            collectSoundEffect.Play();

        }
    }
}
