using Authorization.Data_Domain.Models;
using Authorization.Data_Domain.Models.Entity;
using Documents.API.Client.Abstraction;
using Documents.API.Client.GeneratedClient;
using Microsoft.AspNetCore.Identity;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Threading;
using Authorization.Data.Repository.Abstraction;
using Document = QuestPDF.Fluent.Document;
using Response = Appointment.API.Application.Contracts.Outgoing.Response;
using static SkiaSharp.HarfBuzz.SKShaper;
using Result = Authorization.Data_Domain.Models.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace Appointment.API.Application.Service
{
    public interface IPdfService
    {
        public Task<Response> GeneratePDF(CreateAppointmentResult createResult, CancellationToken cancellationToken = default);
    }
    public class PdfService:IPdfService
    {
        private readonly IDocumentApiProxy _documentApiProxy;
        private readonly IRepositoryBase<Result> _resuRepositoryBase;
        public PdfService(IDocumentApiProxy documentApiProxy,IRepositoryBase<Result> repository)
        {
            _documentApiProxy = documentApiProxy;
            _resuRepositoryBase = repository;
            
        }

        public async Task<Response> GeneratePDF(CreateAppointmentResult createResult,CancellationToken cancellationToken = default)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {




                Document.Create(container =>
                    {
                        container.Page(page =>
                        {
                            page.Size(PageSizes.A4);
                            page.Margin(1, Unit.Centimetre);
                            page.PageColor(Colors.White);
                            page.DefaultTextStyle(x => x.FontSize(14));

                            page.Header().AlignCenter()
                                .Text(
                                    $"Doctor's appointment {DateTime.Today.Day}.{DateTime.Today.Month}.{DateTime.Today.Year} ")
                                .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium);


                            page.Content().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();

                                });

                                // by using custom 'Element' method, we can reuse visual configuration
                                table.Cell().Row(1).Column(1).PaddingLeft(10).PaddingTop(30)
                                    .Text($"Patient: {createResult.PatientFullName} ");
                                table.Cell().Row(1).Column(2).PaddingLeft(20).PaddingTop(30)
                                    .Text($"Docotr: {createResult.DoctorFullName}");



                                table.Cell().Row(2).Column(2).PaddingLeft(20)
                                    .Text($"Specialisation: {createResult.SpecializationTitle}");
                                table.Cell().Row(3).Column(2).PaddingLeft(20)
                                    .Text($"Service: {createResult.ServiceTitle}");



                                table.Cell().ColumnSpan(2).PaddingTop(30).AlignCenter().Text("Complaints").SemiBold()
                                    .FontSize(17).FontColor(Colors.Blue.Medium);
                                table.Cell().ColumnSpan(2).PaddingTop(10).Text(createResult.Complaints);

                                table.Cell().ColumnSpan(2).PaddingTop(20).AlignCenter().Text("Conclusion").SemiBold()
                                    .FontSize(17).FontColor(Colors.Blue.Medium);
                                table.Cell().ColumnSpan(2).PaddingTop(10).Text(createResult.Conclusion);

                                table.Cell().ColumnSpan(2).PaddingTop(20).AlignCenter().Text("Recomendation").SemiBold()
                                    .FontSize(17).FontColor(Colors.Blue.Medium);
                                table.Cell().ColumnSpan(2).PaddingTop(10).Text(createResult.Recomendation);



                            });


                            page.Footer()
                                .AlignCenter()
                                .Text(x =>
                                {
                                    x.Span("Page ");
                                    x.CurrentPageNumber();
                                });
                        });
                    })
                    .GeneratePdf(memoryStream);

                var result = await _resuRepositoryBase.InsertAsync(new Result()
                {
                    AppointmentId = createResult.AppointmentId,
                    Complaints = createResult.Complaints,
                    Conclusion = createResult.Conclusion,
                    Recomendations = createResult.Recomendation

                });

                string fileName = $"{DateTime.Now.ToString("dd.MM.yyyy")}.pdf";
                byte[] pdfData = memoryStream.ToArray();
                try
                {
                    //memoryStream.Position = 0; // Reset the stream position to the beginning

                    //IFormFile file = new FormFile(memoryStream, 0, memoryStream.Length, "file", fileName);
                    memoryStream.Position = 0;
                    var id = await _documentApiProxy.UploadBlobAsync(
                        new FileParameter(memoryStream, fileName, "application/pdf"), createResult.PatientId, result.Id,
                        (SubjectUpdate)SubjectUpdate._4,cancellationToken);

                }
                catch (ApiException ex)
                {
                    Console.WriteLine(ex.Message);
                    throw new Exception(ex.Message);

                }

                return Response.Success;
            }
        }

        //public async Task<Response> Save(PdfResult? pdf, long entityId, long resultId, CancellationToken cancellationToken = default)
        //{
            
        //}
    }
}
