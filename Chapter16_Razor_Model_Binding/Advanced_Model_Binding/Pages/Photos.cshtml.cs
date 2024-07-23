using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Advanced_Model_Binding.Pages
{
    [IgnoreAntiforgeryToken]
    public class PhotosModel : PageModel
    {
        //[FromBody]
        public List<Photo> ListPhotos { get; set; } = new List<Photo>();
        public void OnGet()
        {
        }
        /// <summary>
        /// The Postman request
            /// [
                //{
                //    "PhotoId": 1,
                //    "Photoname": "sunset"
                //},
                //{
                //    "PhotoId": 2,
                //    "Photoname": "mountain"
                //},
                //{
                //"PhotoId": 3,
                //    "Photoname": "ocean"
                //}
            //]
        /// </summary>
        /// <param name="photos">FromBody Annotation was necessary to bind JSON</param>
        public void OnPost([FromBody]List<Photo> photos)
        {
            ListPhotos = photos;
        }
        public class Photo
        {
            public int PhotoId { get; set; }
            public required string Photoname { get; set; }
            public Photo()
            {
                //Does not need to do anything
            }
        }
    }

}
