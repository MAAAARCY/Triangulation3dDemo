namespace Triangulation3d.Samples
{
    public class SelectableObjectModel
    {
        private readonly string objectName;
        
        public string ObjectName => objectName;
        
        public SelectableObjectModel(
            string objectName)
        {
            this.objectName = objectName;
        }
    }
}