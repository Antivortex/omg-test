namespace Game.Popups.PuzzleStartPopup
{
    public class PuzzleStartPopupModel
    {
        public string[] ImageNames { get; }
        public int SelectedIndex { get; set; }
        public string SelectedImageName => ImageNames[SelectedIndex];

        public PuzzleStartPopupModel()
        {
            ImageNames = new string[]
            {
                "puzzle_01", "puzzle_02", "puzzle_03", "puzzle_04",
                "puzzle_05", "puzzle_06", "puzzle_07", "puzzle_08",
                "puzzle_09", "puzzle_10", "puzzle_11", "puzzle_12"
            };

            SelectedIndex = 0;
        }
    }
}
