using System.Collections;
using System.Collections.Generic;
using AlleyOop.PC;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

namespace AlleyOop.VR
{
    public class ScoreManager : MonoBehaviour
    {
        public int score = 0;
        public TMP_Text scoreText;
        public AudioSource scoreFX;
        public int unlockScore;
        
        // Start is called before the first frame update
        void Start()
        {
            score = 0;
            unlockScore = 5;
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

        public void UnlockRainbow()
        {
            if (score <= unlockScore)
            {
                GameObject rainbow = Pool.instance.GetPooledBasketball("Rainbow");

                if (rainbow != null)
                {
                    rainbow.transform.position = Pool.instance.spawnPos.transform.position;
                    rainbow.transform.rotation = Pool.instance.spawnPos.transform.rotation;
                    rainbow.SetActive(true);
                }
            }
        }

    }
}