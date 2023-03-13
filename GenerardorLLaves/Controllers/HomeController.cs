using GenerardorLLaves.Models;
using GenerardorLLaves.VieModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;

namespace GenerardorLLaves.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GenerarLlaves(string data)
        {
            Llaves llaves = new Llaves();

            Random random = new Random();

            // Generar la primera llave
            string key1 = GenerateRandomKey(random);

            // Generar la segunda llave
            string key2 = GenerateRandomKey(random);

            llaves.numberOnehexadecimal = key1;
            llaves.numberTwohexadecimal = key2;

            // Se convierten ambias llaves a arraybytes

            byte[] key1Bytes = BitConverter.GetBytes(UInt64.Parse(key1.Substring(0, 16), NumberStyles.HexNumber));
            byte[] key2Bytes = BitConverter.GetBytes(UInt64.Parse(key2.Substring(0, 16), NumberStyles.HexNumber));

            // Se concatenan los resultados

            byte[] combinedBytes = key1Bytes.Concat(key2Bytes).ToArray();

            // Calcular la suma XOR de todos los bytes en el arreglo combinado de bytes utilizando un ciclo

            byte xorSum = 0;
            foreach (byte b in combinedBytes)
            {
                xorSum ^= b;
            }

            byte[] finalBytes = combinedBytes.Concat(new byte[] { xorSum }).ToArray();

            // Convertir el arreglo de bytes final en una cadena de caracteres en formato hexadecimal utilizando el método BitConverter.

            string finalKey = BitConverter.ToString(finalBytes).Replace("-", "");

            llaves.numberBinario = finalKey;

            return Json(llaves);
        }

        // Función para generar una llave aleatoria
        string GenerateRandomKey(Random random)
        {
            int keyLength = 31; // longitud de la llave
            string randomHex = "";
            for (int i = 0; i < keyLength; i++)
            {
                randomHex += random.Next(16).ToString("X");
            }
            int xorSum = 0;
            foreach (char c in randomHex)
            {
                xorSum ^= Convert.ToInt32(c.ToString(), 16);
            }
            return randomHex + xorSum.ToString("X");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}