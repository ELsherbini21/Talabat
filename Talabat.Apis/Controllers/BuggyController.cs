using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.Apis.Errors;
using Talabat.Repository.Data;

namespace Talabat.Apis.Controllers
{
    // i will make message default based on status code .
    public class BuggyController : BaseApiController
    {

        private readonly StoreContext _storeContext;

        public BuggyController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        

        [HttpGet("ServerError")] // Get : api  / buggy / notfound 
        public ActionResult GetServerError()
        {
            var product = _storeContext.Products.Find(333);

            var productToReturn = product.ToString();

            return Ok(productToReturn);
        }

       







    }
}
