using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using AlleyOop.PC.Powerup;

namespace AlleyOop.VR
{
    public class ScoreManager : MonoBehaviour
    {
        public int score = 0;
        public TMP_Text scoreText;
        public AudioSource scoreFX;

        // Start is called before the first frame update
        void Start()
        {
            score = 0;
        }

        private void Update()
        {
            scoreText.text = score.ToString();
        }


        public void OnTriggerEnter(Collider collider)
        {

            //collider = hoop.GetComponent<Collider>();
            if (collider.gameObject.CompareTag("Ball"))
            {
                score++;
                scoreFX.Play();
            }
            if (collider.gameObject.CompareTag("Fireball"))
            {
                score += 3;
                scoreFX.Play();
            }
        }

    }
}