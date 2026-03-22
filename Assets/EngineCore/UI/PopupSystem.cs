using System.Collections.Generic;
using Core.DI;

namespace EngineCore.UI
{
    public class PopupSystem
    {
        private readonly List<object> _activePresenters = new List<object>();

        public void ShowPopup<TPresenter, TView>(TPresenter presenter, TView view)
            where TPresenter : PopupPresenter<TView>
            where TView : IPopupView
        {
            presenter.Initialize(view);
            presenter.Show();

            if (!_activePresenters.Contains(presenter))
                _activePresenters.Add(presenter);
        }

        public void HidePopup<TPresenter, TView>(TPresenter presenter)
            where TPresenter : PopupPresenter<TView>
            where TView : IPopupView
        {
            presenter.Hide();
            _activePresenters.Remove(presenter);
        }

        public void HideAll()
        {
            for (int i = _activePresenters.Count - 1; i >= 0; i--)
            {
                var presenter = _activePresenters[i];
                var hideMethod = presenter.GetType().GetMethod("Hide");
                hideMethod?.Invoke(presenter, null);
            }

            _activePresenters.Clear();
        }
    }
}
