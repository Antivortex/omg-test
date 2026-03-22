using Core.DI;
using Game.Popups.PuzzleStartPopup;

namespace Game.Popups
{
	public class PopupsFactory : IContextInitializable
	{
		private IContext _context;
		public void InitializeByContext(IContext context)
		{
			_context = context;
		}

		public PuzzleStartPopupPresenter CreateStartPopup()
		{
			var model = new PuzzleStartPopupModel();
			var presenter = new PuzzleStartPopupPresenter(model, _context);
			return presenter;
		}
	}
}