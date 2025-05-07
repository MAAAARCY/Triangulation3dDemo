using UnityEngine;

namespace Triangulation3d.Samples
{
    /// <summary>
    /// オブジェクトの色を変更するUIのView
    /// </summary>
    public class AppearanceView : BaseMenuContentView
    {
        [SerializeField] private ColorParameterView colorParameterView;
        
        public ColorParameterView ColorParameterView => colorParameterView;
    }
}