using Microsoft.AspNetCore.Mvc;

namespace WebAPI_Hello_World.Controllers
{
    //attribute to define this controller as an APIController, this helps by applying conventions in the background
    //TODO: figure out what happens in the background in a later part of the chapter
    [ApiController]
    public class FruitController : Controller
    {
        public List<Fruit> _Fruits = new List<Fruit> { new ("Apfel"), new("Birne"), new("Mandarine") };
        //attribute to define the endpoint
        [HttpGet("fruits")]
        public IEnumerable<Fruit> Index()
        {
            return _Fruits;
        }
        [HttpGet("fruit/{id}")]
        public ActionResult<Fruit> GetFruit(int id)
        {
            if (id >= 0 && id < _Fruits.Count)
            {
                return Ok(_Fruits[id]);
            }
            return NotFound();
        }
        public record Fruit(string Name);
    }
}
