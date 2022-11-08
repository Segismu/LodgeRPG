using System.Collections;
using RPG.Controls;
using UnityEngine;

namespace RPG.Arts.Targeting
{
    [CreateAssetMenu(fileName = "Demo Targeting", menuName = "Arts/Targeting/Delay", order = 0)]
    public class DelayedClicTargeting : TargetingStrategy
    {
        [SerializeField] Texture2D cursorTexture;
        [SerializeField] Vector2 cursorHotspot;

        public override void StartTargeting(GameObject user)
        {
            PlayerController playerController = user.GetComponent<PlayerController>();
            playerController.StartCoroutine(Targeting(user, playerController));
        }

        private IEnumerator Targeting(GameObject user, PlayerController playerController)
        {
            playerController.enabled = false;

            while (true)
            {
                Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);

                if (Input.GetMouseButtonDown(0))
                {

                    while (Input.GetMouseButton(0))
                    {
                        yield return null;
                    }

                    playerController.enabled = true;
                    yield break;
                }
                yield return null;
            }
        }
    }
}

