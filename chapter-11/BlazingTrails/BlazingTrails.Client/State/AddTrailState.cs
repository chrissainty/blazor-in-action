using BlazingTrails.Shared.Features.ManageTrails.Shared;

namespace BlazingTrails.Client.State;

public class AddTrailState
{
    private TrailDto _unsavedNewTrail = new();

    public TrailDto GetTrail()
        => _unsavedNewTrail;

    public void SaveTrail(TrailDto trail)
        => _unsavedNewTrail = trail;

    public void ClearTrail()
        => _unsavedNewTrail = new();
}
