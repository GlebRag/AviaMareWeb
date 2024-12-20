using AviaMare.Data.Models;
using AviaMare.Data;
using Enums.Users;
using Microsoft.EntityFrameworkCore;
using AviaMare.Data.Interface.Repositories;
using System.ComponentModel.DataAnnotations;
using System;
using System.Linq;
using AviaMare.Data.Interface.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Data.SqlClient;
using System.Text;
using System;
using System.IO;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace AviaMare.Data.Repositories
{
    public interface ITicketRepositoryReal : ITicketRepository<TicketData>
    {
        void BuyTicket(int ticketId, int userId);
        void Create(TicketData dataTicket);
        IEnumerable<TicketData> GetTicket(int userId);
        bool IsThisUserBoughtThisTicket(int ticketId, int userId);
        void SaveTicket(TicketData dataTicket, string? userName);
        IEnumerable<TicketShortInfo> SearchTicket(string departure, string destination, DateTime? takeOffTime, decimal? cost, string sortOrder);
    }

    public class TicketRepository : BaseRepository<TicketData>, ITicketRepositoryReal
    {
        public TicketRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public void BuyTicket(int ticketId, int userId)
        {
            var user = _webDbContext.Users.First(x => x.Id == userId);
            var ticket = _dbSet.First(x => x.Id == ticketId);
            ticket.Buyers.Add(user);
            ticket.Count--;
            _webDbContext.SaveChanges();
        }

        public void Create(TicketData dataTicket)
        {
            Add(dataTicket);
        }

        public IEnumerable<TicketData> GetTicket(int userId)
        {
            var result = _webDbContext.Users
            .Where(u => u.Id == userId)
            .Select(u => u.Ticket);

            
            return result.ToList();

        }

        public bool IsThisUserBoughtThisTicket(int ticketId, int userId)
        {
            var user = _webDbContext.Users.First(x => x.Id == userId);
            var ticket = _dbSet.First(x => x.Id == ticketId);
            if(user.Ticket == ticket)
            {
                return true;
            }
            return false;

        }

        public void SaveTicket(TicketData dataTicket, string? userName)
        {
            string passengerName = userName ?? string.Empty;
            string flightNumber = dataTicket.IdPlane.ToString();
            string departure = dataTicket.Departure;
            string destination = dataTicket.Destination;
            string departureDate = dataTicket.TakeOffTime.ToString();
            string transactionId = "TXN123456789";
            string outputFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "TicketReceipt.pdf");
            try
            {
                // Создание нового документа
                PdfDocument document = new PdfDocument();
                document.Info.Title = "Чек оплаты авиабилета";

                // Добавление страницы
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Шрифты
                XFont headerFont = new XFont("Arial", 18, XFontStyleEx.Bold); // Заголовок
                XFont boldFont = new XFont("Arial", 12, XFontStyleEx.Regular); // Жирный текст
                XFont regularFont = new XFont("Arial", 12, XFontStyleEx.Regular); // Обычный текст

                // Заголовок
                gfx.DrawString("Чек оплаты авиабилета", headerFont, XBrushes.Black,
                    new XRect(0, 20, page.Width, 40), XStringFormats.Center);

                // Основная информация
                double y = 80; // начальная позиция по вертикали
                double lineHeight = 20;

                gfx.DrawString($"Имя пассажира: {passengerName}", boldFont, XBrushes.Black, new XRect(40, y, page.Width - 80, lineHeight), XStringFormats.TopLeft);
                y += lineHeight;
                gfx.DrawString($"Номер рейса: {flightNumber}", regularFont, XBrushes.Black, new XRect(40, y, page.Width - 80, lineHeight), XStringFormats.TopLeft);
                y += lineHeight;
                gfx.DrawString($"Место отправления: {departure}", regularFont, XBrushes.Black, new XRect(40, y, page.Width - 80, lineHeight), XStringFormats.TopLeft);
                y += lineHeight;
                gfx.DrawString($"Место назначения: {destination}", regularFont, XBrushes.Black, new XRect(40, y, page.Width - 80, lineHeight), XStringFormats.TopLeft);
                y += lineHeight;
                gfx.DrawString($"Дата вылета: {departureDate}", regularFont, XBrushes.Black, new XRect(40, y, page.Width - 80, lineHeight), XStringFormats.TopLeft);
                y += lineHeight;

                gfx.DrawLine(XPens.Black, 40, y, page.Width - 40, y); // горизонтальная линия
                y += lineHeight;

                gfx.DrawString($"ID транзакции: {transactionId}", regularFont, XBrushes.Black, new XRect(40, y, page.Width - 80, lineHeight), XStringFormats.TopLeft);
                y += lineHeight;

                gfx.DrawLine(XPens.Black, 40, y, page.Width - 40, y); // горизонтальная линия
                y += lineHeight;

                // Благодарность с правильным выравниванием по базовой линии
                gfx.DrawString("Спасибо за покупку и приятного полета!", regularFont, XBrushes.Black,
                    new XRect(40, y, page.Width - 80, 0), XStringFormats.TopLeft);  // Высота прямоугольника = 0

                // Сохранение PDF
                document.Save(outputFile);
                Console.WriteLine($"Чек успешно создан: {outputFile}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }

        public IEnumerable<TicketShortInfo> SearchTicket(string departure, string destination, DateTime? takeOffTime, decimal? cost, string sortOrder)
        {
            var parameters = new List<SqlParameter>();
            var sql = new StringBuilder("SELECT * FROM dbo.Tickets WHERE 1=1"); //Это условие всгеда истинно

            if (departure != "null")
            {
                sql.Append(" AND Departure = @Departure");
                parameters.Add(new SqlParameter("@Departure", departure));
            }

            if (destination != "null")
            {
                sql.Append(" AND Destination = @Destination");
                parameters.Add(new SqlParameter("@Destination", destination));
            }

            // Условия для TakeOffTime
            if (takeOffTime.HasValue)
            {
                if (takeOffTime.Value == DateTime.MinValue) // Если дата равна 01.01.0001
                {
                    // От сегодняшней даты и после (использовать потом)
                    //sql.Append(" AND TakeOffTime >= @CurrentDate");
                    //parameters.Add(new SqlParameter("@CurrentDate", DateTime.Now.Date));

                    //От сегодняшней даты и до(временно)
                    sql.Append(" AND TakeOffTime <= @CurrentDate");
                    parameters.Add(new SqlParameter("@CurrentDate", DateTime.Now.Date));
                }
                else
                {
                    sql.Append(" AND CAST(TakeOffTime AS DATE) = @TakeOffTime");
                    parameters.Add(new SqlParameter("@TakeOffTime", takeOffTime.Value.Date));
                }
            }

            if (cost.HasValue)
            {
                sql.Append(" AND Cost <= @Cost");
                parameters.Add(new SqlParameter("@Cost", cost.Value));
                if (sortOrder == "asc")
                {
                    sql.Append(" ORDER BY Cost ASC");
                }
                if(sortOrder == "desc")
                {
                    sql.Append(" ORDER BY Cost DESC");
                }
            }

            var result = _webDbContext
                .Database
                .SqlQueryRaw<TicketShortInfo>(sql.ToString(), parameters.ToArray())
                .ToList();

            return result;
        }

        //TODO: FindByDestination, FindByDeparture, FindByDate, FindByMaxCost, FindByMinCost, FindByTime
    }
}
