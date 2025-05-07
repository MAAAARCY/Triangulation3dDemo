using R3;

namespace Triangulation3d.Samples
{
    public class MenuElementPresenter
    {
        private readonly MenuElementModel model;
        private readonly MenuElementView view;

        public MenuElementPresenter(
            MenuElementModel model,
            MenuElementView view)
        {
            this.model = model;
            this.view = view;
            
            OnSubscribe();
        }

        private void OnSubscribe()
        {
            
        }
    }
}