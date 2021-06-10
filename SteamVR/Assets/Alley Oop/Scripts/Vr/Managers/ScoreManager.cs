using TMPro;
using UnityEngine;

namespace AlleyOop.VR
{
    public class ScoreManager : MonoBehaviour
    {
        #region Variables
        public int score = 0;
        public TMP_Text scoreText;
        public AudioSource scoreFX;
        #endregion
        #region Start
        void Start()
        {
            score = 0;
        }
        #endregion
        #region Update
        private void Update()
        {
            // Update Score
            scoreText.text = score.ToString();
        }
        #endregion
        #region On Trigger Enter
        public void OnTriggerEnter(Collider collider)
        {
            // Add score when ball enters hoop
            if (collider.gameObject.CompareTag("Ball"))
            {
                score++;
                scoreFX.Play();
            }
        }
        #endregion
        #region Unlock Rainbow
        //Rainbow ball will spawn when the player gets 5+ score
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
        #endregion
    }
}