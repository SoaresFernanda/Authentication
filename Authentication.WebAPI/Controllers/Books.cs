using Authentication.WebAPI.Infrastructure.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class Books : ControllerBase
    {
        
        [HttpGet, Authorize]
        public IActionResult GetBooks()
        {
            List<Book> book = new List<Book>();
            book.Add(new Book()
            {
                Id = 1,
                BookName = "The Lord of the Rings: The Fellowship of the Ring.",
                Date = System.DateTime.Now
            });
            book.Add(new Book()
            {
                Id = 2,
                BookName = "The Lord of the Rings: The Two Towers .",
                Date = System.DateTime.Now
            }); book.Add(new Book()
            {
                Id = 3,
                BookName = "The Lord of the Rings: The Return of the King.",
                Date = System.DateTime.Now
            });

            return new ObjectResult(book);
        }
    }
}
