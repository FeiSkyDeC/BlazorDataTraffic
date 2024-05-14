//using Microsoft.AspNetCore.Mvc;
//using System.IO;

//namespace Server.Controllers
//{
//    [Route("api/[controller]/[action]")]
//    public class ReadFileController:ControllerBase
//    {
//        [HttpGet("{filePath")]
//        public IActionResult GetFileContent(string filePath)
//        {
//            try
//            {
//                string fullPath = Path.Combine(filePath);
//                if (System.IO.File.Exists(fullPath))
//                {
//                    return NotFound();
//                }

//                string content = System.IO.File.ReadAllText(fullPath);
//                return Ok(content);
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex);
//                throw;
//            }
//        }
//    }
//}
