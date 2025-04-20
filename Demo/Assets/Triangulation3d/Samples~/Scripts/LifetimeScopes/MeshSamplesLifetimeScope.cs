using iShape.Triangulation.Runtime;
using Triangulation3d.Runtime;
using Triangulation3d.Samples.UI;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

namespace Triangulation3d.Samples
{
    public class MeshSamplesLifetimeScope : BaseLifetimeScope
    {
        [SerializeField] private MeshView meshViewTemplate;
        [SerializeField] private MeshSamplesView meshSamplesView;
        [SerializeField] private MenuView menuView;
        [SerializeField] private CameraView cameraView;
        
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            
            ConfigureAPI(builder);
            ConfigureRepository(builder);
            ConfigureMesh(builder);
            ConfigureCamera(builder);
            ConfigureMenu(builder);
        }

        private void ConfigureAPI(IContainerBuilder builder)
        {
            builder.Register<Surface>(Lifetime.Singleton);
            builder.Register<BlenderSurfaces>(Lifetime.Singleton);
            builder.Register<SurfaceApiModel>(Lifetime.Singleton);
        }

        private void ConfigureRepository(IContainerBuilder builder)
        {
            builder.Register<MeshRepository>(Lifetime.Singleton);
            builder.Register<SurfaceRepository>(Lifetime.Singleton);
        }

        private void ConfigureMesh(IContainerBuilder builder)
        {
            builder.Register<MeshSamplesModel>(Lifetime.Singleton);
            builder.RegisterInstance(meshSamplesView);
            builder.Register<MeshFactoryModel>(Lifetime.Singleton);
            builder.Register<MeshModel>(Lifetime.Singleton);
            builder.Register<ShapeMeshCreatorExt>(Lifetime.Singleton);
            
            builder.RegisterInstance(meshViewTemplate);
            builder.RegisterEntryPoint<MeshSamplesPresenter>();
        }

        private void ConfigureCamera(IContainerBuilder builder)
        {
            builder.Register<CameraPoseCalculatorModel>(Lifetime.Singleton);
            builder.Register<CameraPoseModel>(Lifetime.Singleton);
            builder.Register<CameraModel>(Lifetime.Singleton);

            builder.RegisterInstance(cameraView);
            builder.RegisterEntryPoint<CameraPresenter>();
        }

        private void ConfigureMenu(IContainerBuilder builder)
        {
            // builder.RegisterInstance(menuElementViewTemplate);
            // builder.RegisterInstance(cameraControlsViewTemplate);
            // builder.RegisterInstance(cameraSensitivityViewTemplate);
            // builder.RegisterInstance(appearanceViewTemplate);
            // builder.RegisterInstance(jsonFileUploadViewTemplate);
            // builder.RegisterInstance(selectObjectViewTemplate);
            builder.Register<CameraSensitivityModel>(Lifetime.Singleton);
            
            builder.Register<MenuModel>(Lifetime.Singleton);
            builder.RegisterInstance(menuView);
            
            builder.RegisterEntryPoint<MenuPresenter>();
        }
    }
   
}