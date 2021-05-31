using TMPro;
using UnityEngine;

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
            if (collider.gameObject.CompareTag("Ball"))
            {
                score++;
                scoreFX.Play();
            }
        }

        public void UnlockRainbow()
        {
            if (score >= 5)
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