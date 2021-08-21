using Blazored.LocalStorage;
using System.Threading.Tasks;

namespace BlazingTrails.Client.State
{
    public class AppState
    {
        private bool _isInitialized;

        public AddTrailState AddTrailState { get; }
        public FavoriteTrailsState FavoriteTrailsState { get; }

        public AppState(ILocalStorageService localStorageService)
        {
            AddTrailState = new AddTrailState();
            FavoriteTrailsState = new FavoriteTrailsState(localStorageService);
        }

        public async Task Initialize()
        {
            if (!_isInitialized)
            {
                await FavoriteTrailsState.Initialize();
                _isInitialized = true;
            }
        }
    }
}
