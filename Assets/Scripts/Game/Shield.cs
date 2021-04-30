using UnityEngine;

namespace Game
{
    public class Shield : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        
        public float blockMultiplier = 1.0f / 2.0f; // Blocks (n - m)/n damages 

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        public void Show()
        {
            spriteRenderer.enabled = true;
        }

        public void Hide()
        {
            spriteRenderer.enabled = false;
        }
    }
}