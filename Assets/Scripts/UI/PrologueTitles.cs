using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PrologueTitles : MonoBehaviour
    {
        public Text roleText;
        public Text nameText;
        public List<TitlesText> titlesTexts;

        private void Start()
        {
            roleText.text = "";
            nameText.text = "";

            StartCoroutine(TitlesCoroutine());
        }

        private IEnumerator TitlesCoroutine()
        {
            var colorStep = 0.1f;
            var colorWait = 0.1f;
            
            foreach (var titlesText in titlesTexts)
            {
                roleText.text = titlesText.role;
                nameText.text = titlesText.titleName;
                
                while (roleText.color.a <= 0.95f)
                {
                    var color = roleText.color;
                    
                    roleText.color = new Color(color.r, color.g, color.b, color.a += colorStep);
                    nameText.color = new Color(color.r, color.g, color.b, color.a += colorStep);
                    yield return new WaitForSeconds(colorWait);
                }
                
                roleText.color = Color.white;
                nameText.color = Color.white;

                yield return new WaitForSeconds(2f);

                while (roleText.color.a >= 0.01f)
                {
                    var color = roleText.color;
                    
                    roleText.color = new Color(color.r, color.g, color.b, color.a -= colorStep);
                    nameText.color = new Color(color.r, color.g, color.b, color.a -= colorStep);
                    yield return new WaitForSeconds(colorWait);
                }
            }
            Destroy(gameObject);
        }
    }
}
