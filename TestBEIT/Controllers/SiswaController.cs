using Microsoft.AspNetCore.Mvc;
using TestBEIT.Logics.Students;

namespace TestBEIT.Controllers
{
    public class SiswaController(SiswaLogic siswaLogic) : Controller
    {
        public async Task<IActionResult> IndexAsync()
        {
            var listSiswa = await siswaLogic.MenentukanKelas();
            return View(listSiswa);
        }
    }
}
