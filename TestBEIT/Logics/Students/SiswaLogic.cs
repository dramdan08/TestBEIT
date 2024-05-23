using System.Numerics;
using System.Text.Json;
using TestBEIT.Models;

namespace TestBEIT.Logics.Students
{
    public class SiswaLogic
    {
        public async Task<KelasSiswaData> AmbilDataSiswa()
        {
            using var httpClient = new HttpClient();

            var response = await httpClient.GetAsync("http://ecocim-backend-switch.beit.co.id/api/ManualConfig/TestBEIT");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                var kelasSiswaData = JsonSerializer.Deserialize<KelasSiswaData>(jsonString);

                return kelasSiswaData;
            }
            else
            {
                throw new Exception($"{response.StatusCode}");
            }
        }

        public async Task<SiswaResponse> MenentukanKelas()
        {
            var result = new SiswaResponse();
            var listSiswa = new List<Siswa>();
            var dataAwal = await AmbilDataSiswa();
            var index = 0;
            foreach (var item in dataAwal.listNama) {
                var siswa = new Siswa()
                {
                    Name = item,
                    Point = dataAwal.listNilai[index]
                };
                index++;
                listSiswa.Add(siswa);
            }

            // kelas 6
            listSiswa
                .Where(x => x.Kelas == null && x.Name.Contains("C") && x.Name.Contains("O"))
                .ToList()
                .ForEach(x => x.Kelas = "6");

            listSiswa
                .Where(x => x.Kelas == null && x.Point >= 90)
                .ToList()
                .ForEach(x => x.Kelas = "1");

            listSiswa
                .Where(x => x.Kelas == null && x.Point >= 80 && x.Point < 90)
                .ToList()
                .ForEach(x => x.Kelas = "2");

            listSiswa
                .Where(x => x.Kelas == null && x.Point >= 70 && x.Point < 80)
                .ToList()
                .ForEach(x => x.Kelas = "3");

            listSiswa
                .Where(x => x.Kelas == null && x.Point >= 70 && x.Point < 80)
                .ToList()
                .ForEach(x => x.Kelas = "3");

            listSiswa
                .Where(x => x.Kelas == null && x.Point >= 60 && x.Point < 70)
                .ToList()
                .ForEach(x => x.Kelas = "4");

            listSiswa
                .Where(x => x.Kelas == null && x.Point >= 50 && x.Point < 60)
                .ToList()
                .ForEach(x => x.Kelas = "5");

            listSiswa
                .Where(x => x.Kelas == "6" && x.Point % 7 == 0)
                .ToList()
                .ForEach(x => x.MarriedYear = DateTime.Now.Year + 1);

            listSiswa
                .Where(x => x.Kelas != null && Helpers.isPrime(x.Point))
                .ToList()
                .ForEach(x => x.DeadDate = new DateOnly(
                    DateTime.Now.Month > x.Point % 10 ? DateTime.Now.Year + 1 : DateTime.Now.Year,
                    x.Point % 10,
                    1));

            result.ListSiswaKelas1 = listSiswa
                .Where(x => x.Kelas == "1")
                .ToList();
            result.ListSiswaKelas2 = listSiswa
                .Where(x => x.Kelas == "2")
                .ToList();
            result.ListSiswaKelas3 = listSiswa
                .Where(x => x.Kelas == "3")
                .ToList();
            result.ListSiswaKelas4 = listSiswa
                .Where(x => x.Kelas == "4")
                .ToList();
            result.ListSiswaKelas5 = listSiswa
                .Where(x => x.Kelas == "5")
                .ToList();
            result.ListSiswaKelas6 = listSiswa
                .Where(x => x.Kelas == "6")
                .ToList();
            result.ListSiswaMeninggal = listSiswa
                .Where(x => x.DeadDate != null)
                .ToList();
            result.ListSiswaMarried= listSiswa
                .Where(x => x.MarriedYear != null)
                .ToList();

            return result;
        }

    }
}
