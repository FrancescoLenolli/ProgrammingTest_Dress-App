using UnityEngine;

namespace UIFramework.StateMachine
{
    /// <summary>
    /// Holds logic for the UIView visual elements.
    /// </summary>
    public class UIView : MonoBehaviour
    {
        /// <summary>
        /// Set the UIView to be visible on screen.
        /// </summary>
        public virtual void ShowView()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Hide the UIView from the screen.
        /// </summary>
        public virtual void HideView()
        {
            gameObject.SetActive(false);
        }
    }
}
