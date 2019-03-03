using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Manager : MonoBehaviour
{
    [SerializeField] public AudioSource musique_clavecin;
    [SerializeField] public AudioSource musique_stressante;
    
    [SerializeField] public AudioSource boo;
    [SerializeField] public AudioSource success;
    [SerializeField] public AudioSource failure;
    [SerializeField] public AudioSource coupe;

    //gestion fondu clavecin
    float timer_fondu;
    float duree_fondu = 3.0f;//seconde


    // Start is called before the first frame update
    void Start()
    {
        timer_fondu = 0;

        musique_clavecin.Play();
    }

    // Update is called once per frame
    void Update()
    {
        gestionFonduClavecin();
        
    }

    void gestionFonduClavecin() {
        if (timer_fondu > 0) {
            timer_fondu -= Time.deltaTime;

            musique_clavecin.volume = timer_fondu / duree_fondu;

            if (timer_fondu <= 0) {
                //fin fondu
                timer_fondu = 0;
                musique_clavecin.Stop();
                musique_clavecin.volume = 1;
            }
        }
    }

    public void fondreClavecin() {
        timer_fondu = duree_fondu;
    }
}
