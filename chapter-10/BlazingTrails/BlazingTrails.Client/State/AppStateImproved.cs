using Blazored.LocalStorage;
using System.Threading.Tasks;

namespace BlazingTrails.Client.State
{
    public class AppStateImproved
    {
        private bool _isInitializing;
        private bool _isInitialized;

        public FavoriteTrailsState FavoriteTrailsState { get; }
        public AddTrailState AddTrailState { get; }

        public AppStateImproved(ILocalStorageService localStorageService)
        {
            FavoriteTrailsState = new FavoriteTrailsState(localStorageService);
            AddTrailState = new AddTrailState();
        }

        public async Task InitializeAsync()
        {
            if (!_isInitialized || !_isInitializing)
            {
                _isInitializing = true;

                await FavoriteTrailsState.InitializeAsync();
                _isInitialized = true;
                
                _isInitializing = false;
            }
        }
    }
}
