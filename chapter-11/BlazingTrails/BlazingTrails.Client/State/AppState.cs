using Blazored.LocalStorage;

namespace BlazingTrails.Client.State;

public class AppState
{
    private bool _isInitialized;

    public NewTrailState NewTrailState { get; }
    public FavoriteTrailsState FavoriteTrailsState { get; }

    public AppState(ILocalStorageService localStorageService)
    {
        NewTrailState = new NewTrailState();
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
