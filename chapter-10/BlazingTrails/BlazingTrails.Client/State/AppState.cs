﻿using BlazingTrails.Client.Features.Shared;
using BlazingTrails.Shared.Features.ManageTrails.Shared;
using Blazored.LocalStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazingTrails.Client.State
{
    public class AppState
    {
        private const string FavouriteTrailsKey = "favoriteTrails";

        private TrailDto _unsavedNewTrail = new();
        private bool _isInitialized;
        private List<Trail> _favoriteTrails = new();
        private readonly ILocalStorageService _localStorageService;

        public IReadOnlyList<Trail> FavoriteTrails => _favoriteTrails.AsReadOnly();

        public event Action OnChange;

        public AppState(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task InitializeAsync()
        {
            if (!_isInitialized)
            {
                await LoadFavoriteTrails();
                _isInitialized = true;
                NotifyStateHasChanged();
            }
        }

        public TrailDto GetTrail()
            => _unsavedNewTrail;

        public void SaveTrail(TrailDto trail)
            => _unsavedNewTrail = trail;

        public void ClearTrail()
            => _unsavedNewTrail = new();

        public async Task AddFavorite(Trail trail)
        {
            if (_favoriteTrails.Any(_ => _.Id == trail.Id)) return;

            _favoriteTrails.Add(trail);

            await _localStorageService.SetItemAsync(FavouriteTrailsKey, _favoriteTrails);

            NotifyStateHasChanged();
        }

        public async Task RemoveFavorite(Trail trail)
        {
            var existingTrail = _favoriteTrails.SingleOrDefault(_ => _.Id == trail.Id);

            if (existingTrail is null) return;

            _favoriteTrails.Remove(existingTrail);

            await _localStorageService.SetItemAsync(FavouriteTrailsKey, _favoriteTrails);

            NotifyStateHasChanged();
        }

        public bool IsFavorite(Trail trail)
        {
            return _favoriteTrails.Any(_ => _.Id == trail.Id);
        }

        private async Task LoadFavoriteTrails()
            => _favoriteTrails = await _localStorageService.GetItemAsync<List<Trail>>(FavouriteTrailsKey) ?? new List<Trail>();

        private void NotifyStateHasChanged() => OnChange?.Invoke();
    }
}
