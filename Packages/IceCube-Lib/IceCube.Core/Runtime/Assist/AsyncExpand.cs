using System.Threading.Tasks;

namespace IceCube.Core
{
    public static class WaitAsync
    {
        public static async void WarpErrors(this Task rTask)
        {
            await rTask;
        }
    }
}
