using Microsoft.AspNetCore.Components.Forms;
using System.Linq;

namespace BlazingTrails.Web.Validation
{
    public class BootstrapCssClassProvider : FieldCssClassProvider
    {
        public override string GetFieldCssClass(EditContext editContext, in FieldIdentifier fieldIdentifier)
        {
            var isValid = !editContext.GetValidationMessages(fieldIdentifier).Any();

            if (editContext.IsModified(fieldIdentifier))
            {
                return isValid ? "is-valid" : "is-invalid";
            }

            return isValid ? "" : "is-invalid";
        }
    }
}
