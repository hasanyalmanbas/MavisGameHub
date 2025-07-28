using System.Threading.Tasks;

namespace _Project.Core.Loader
{
    public interface ISceneLoaderUI
    {
        Task Open();
        Task Close();
        void SetProgress(float value);
    }
}