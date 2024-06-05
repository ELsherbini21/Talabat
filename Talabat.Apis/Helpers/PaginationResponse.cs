namespace Talabat.Apis.Helpers
{
    // return Dto , not return model 
    // so that i don't make constraint that is baseEntity Sir .
    public class PaginationResponse<T>  // Standard Response . 
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int Count { get; set; }

        public IReadOnlyList<T> Data { get; set; }


        public PaginationResponse()
        {
            
        }

        public PaginationResponse(int pageIndex, int pageSize, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Data = data;
        }
    }
}
