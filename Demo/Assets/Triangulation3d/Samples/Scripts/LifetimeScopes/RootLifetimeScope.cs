using VContainer;
using VContainer.Unity;

namespace Triangulation3d.Samples
{
    public class RootLifetimeScope : LifetimeScope
    {
        /// <summary>
        /// 設定
        /// </summary>
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            builder.Register<SceneModel>(Lifetime.Singleton);
        }
    }
}