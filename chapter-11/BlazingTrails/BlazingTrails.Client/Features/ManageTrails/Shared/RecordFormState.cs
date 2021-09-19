using BlazingTrails.Client.State;
using BlazingTrails.Shared.Features.ManageTrails.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazingTrails.Client.Features.ManageTrails.Shared;

public class RecordFormState : ComponentBase
{
    [Inject] public AppState AppState { get; set; } = null!;

    [CascadingParameter] private EditContext CascadedEditContext { get; set; } = null!;

    protected override void OnInitialized()
    {
        if (CascadedEditContext is null)
        {
            throw new InvalidOperationException($"{nameof(RecordFormState)} requires a cascading parameter of type {nameof(EditContext)}");
        }

        CascadedEditContext.OnFieldChanged += CascadedEditContext_OnFieldChanged;
    }

    private void CascadedEditContext_OnFieldChanged(object? sender, FieldChangedEventArgs e)
    {
        var trail = (TrailDto)e.FieldIdentifier.Model;

        if (trail.Id == 0)
        {
            AppState.AddTrailState.SaveTrail(trail);
        }
    }
}
