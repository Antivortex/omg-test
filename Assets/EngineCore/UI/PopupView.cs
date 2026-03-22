using UnityEngine;

namespace EngineCore.UI
{
    public class PopupView : MonoBehaviour, IPopupView
    {
        public bool IsVisible => gameObject.activeSelf;

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
