using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Office.Interop.Word;
using Microsoft.VisualStudio.Web.CodeGeneration;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization.Formatters;
using System.Web;
using WebApplicationMedical.Models;
using Word = Microsoft.Office.Interop.Word;

namespace WebApplicationMedical.Pages.Patient
{
    public class SearchModel : PageModel
    {
        [BindProperty, DisplayName("СНИЛС")]
        public string MedcardId { get; set; }

        public DPatient Patient { get; set; }

        public bool IsChecked { get; set; }

        public string QrCode { get; set; } = null!;

        private readonly MedicalDbContext _context;

        public SearchModel(MedicalDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (HttpContext.Request.Form.Files.Count > 0)
                {
                    string stringUrl = new QrCode().ReadQrCode(Request.Form.Files[0]);
                    if (stringUrl == null)
                    {
                        return Page();
                    }
                    Uri uri = new Uri(stringUrl);
                    int patientid = int.Parse(HttpUtility.ParseQueryString(uri.Query)["Id"]);
                    Patient = await _context.DPatients.FirstOrDefaultAsync(x => x.Id == patientid);
                    QrCode = new QrCode().CrateQrCode(Url.PageLink("Details", "Id", new { id = Patient.Id }));
                }
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                Patient = await _context.DPatients.FirstOrDefaultAsync(x => x.MedcardId == MedcardId);

                if (Patient != null)
                {
                    QrCode = new QrCode().CrateQrCode(Url.PageLink("Details", "Id", new { id = Patient.Id }));
                }
                return Page();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IActionResult OnPostDocument(bool pdfType)
        {
            Patient = _context.DPatients.First(x => x.MedcardId == MedcardId);
            //Словарь ключ значения для замены
            var replaceParams = new Dictionary<string, string>
            {
                {"<DATE>", DateTime.UtcNow.ToShortDateString() },
                {"<ORG>", "ООО МЕД ЛАБ" },
                {"<CUSTOMER_FIO>", Patient.FIO },
                {"<LICENSE_ORG>", "Министерство здравоохранения Пермского края" },
            };

            Word.Application app = null;
            try
            {
                //Создаём Word приложение
                app = new Word.Application();
                app.Visible = false;
                //Указывае путь к файлу шаблона
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files/договор.docx");
                //Тип файла
                string fileType = "text/plain";
                //Файл
                string fileName = "договор.docx";

                //Получаем путь к файлу
                var file = PhysicalFile(filePath, fileType, fileName);
                app.Documents.Open(file.FileName);

                //Поиск и замена ключей в шаблоне
                foreach (var param in replaceParams)
                {
                    Word.Find find = app.Selection.Find;
                    //Заменяемый текст
                    find.Text = param.Key;
                    //Новый текст
                    find.Replacement.Text = param.Value;

                    //Выполняем замену
                    find.Execute(
                        Replace: WdReplace.wdReplaceAll);
                }

                //Имя
                string newFile = file.FileName;
                //Сохранить Word документ
                    app.ActiveDocument.SaveAs2(fileName, WdExportFormat.wdExportFormatPDF);
   
                //Закрыть Word документ
                app.ActiveDocument.Close();
                //Возвращаем клиенту новый файл
                return PhysicalFile(newFile, fileType, $"{Patient.FIO} {DateTime.UtcNow.ToShortDateString()} {fileName}");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (app != null)
                {
                    //Закрыть Word приложение
                    app.Quit();
                }
            }
        }


        public IActionResult OnPostDocumentPD()
        {
            Patient = _context.DPatients.First(x => x.MedcardId == MedcardId);

            Word.Application app = null;
            var replaceParams = new Dictionary<string, string>()
            {
                {"<CUSTOMER_FIO>", Patient.FIO },
                {"<CUSTOMER_PASSPORT>", Patient.Passport },
                {"<CUSTOMER_ADDRESS>", "Г. Самара Ул. Пущкина, Д. 11 К. 9" },
                {"<ORG>", "ООО МЕД ЛАБ" },
                {"<DATE>", DateTime.UtcNow.ToShortDateString() },
                {"<CUSTOMER_PASSPORT_ISSUED>", "Отделом УФМС" }
            };

            try
            {
                //Создание приложения
                app = new Application();
                app.Visible = false;
                //Путь к документу
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files/согласие.docx");
                //Задаём тип файла
                string fileType = "text/plain";
                string fileName = "согласие.docx";

                //Получаем путь к файлу
                var file = PhysicalFile(filePath, fileType, fileName);
                //Передаём файл в приложение
                app.Documents.Open(file.FileName);

                //Перебераем ключи и заменяем найденные значения на новые
                foreach (var param in replaceParams)
                {
                    Word.Find find = app.Selection.Find;
                    find.Text = param.Key;
                    find.Replacement.Text = param.Value;

                    find.Execute(FindText: Type.Missing,
                        Wrap: WdFindWrap.wdFindContinue,
                        Replace: WdReplace.wdReplaceAll);
                }
                //Создаём новый файл
                string newFile = Path.Combine(file.FileName);
                //Сохраняем документ в файл
                app.ActiveDocument.SaveAs2(newFile);
                //Закрываем документ
                app.ActiveDocument.Close();
                return PhysicalFile(newFile, fileType, $"{Patient.FIO} {DateTime.UtcNow.ToShortDateString()} {fileName}");


            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (app != null)
                {
                    app.Quit();
                }
            }
        }
    }
}
