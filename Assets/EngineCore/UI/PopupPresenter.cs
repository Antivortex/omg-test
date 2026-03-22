namespace EngineCore.UI
{
    public abstract class PopupPresenter<TView> where TView : IPopupView
    {
        protected TView View { get; private set; }

        public void Initialize(TView view)
        {
            View = view;
            OnInitialize();
        }

        public void Show()
        {
            OnBeforeShow();
            View.Show();
        }

        public void Hide()
        {
            View.Hide();
            OnAfterHide();
        }

        protected virtual void OnInitialize() { }
        protected virtual void OnBeforeShow() { }
        protected virtual void OnAfterHide() { }
    }
}
