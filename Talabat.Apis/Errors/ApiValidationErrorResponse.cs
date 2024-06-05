namespace Talabat.Apis.Errors
{
    // validation Error is Bard Request => StatusCode = 400
    public class ApiValidationErrorResponse : ApiResponse
    {

        // that represent state for EndPoint (int id , string name) .
        // id && name => are called Model_State .

        public IEnumerable<string> Errors { get; set; }
        public ApiValidationErrorResponse() : base(400)
        {
            Errors = new List<string>();
        }
    }

}
