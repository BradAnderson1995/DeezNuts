using UnityEngine;

namespace Assets.Scripts
{
    public class TitleController : MonoBehaviour {
        [SerializeField] private string firstLevel;

        private void Update()
        {
            if (Input.GetButtonDown("Submit"))
            {
                Application.LoadLevel(firstLevel);
            }
        }
    }
}
