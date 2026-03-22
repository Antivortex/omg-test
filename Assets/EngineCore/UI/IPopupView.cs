namespace EngineCore.UI
{
    public interface IPopupView
    {
        void Show();
        void Hide();
        bool IsVisible { get; }
    }
}
