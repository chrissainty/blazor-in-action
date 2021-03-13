namespace BlazingTrails.Web.Features.ManageTrails.Shared
{
    public record SubmitResult(bool IsSuccess, string ErrorMessage)
    {
        public static SubmitResult Success() => new SubmitResult(true, "");
        public static SubmitResult Fail(string errorMessage) => new SubmitResult(false, errorMessage);
    }
}
