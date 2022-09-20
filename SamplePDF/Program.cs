using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using SamplePDF;
using System.Globalization;

Console.WriteLine("Hello, World!");

var helloDocument = Document.Create(container =>
{
    container.Page(page =>
    {
        page.Size(PageSizes.A4);
        page.Margin(2, Unit.Centimetre);
        page.PageColor(Colors.White);
        page.DefaultTextStyle(text => text.FontSize(20));

        page.Header()
            .Text("Hello PDF!")
            .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);

        page.Content()
            .PaddingVertical(1, Unit.Centimetre)
            .Column(column =>
            {
                column.Spacing(20);

                column.Item().Text(Placeholders.LoremIpsum());
                column.Item().Image(Placeholders.Image(200, 100));
            });

        page.Footer()
            .AlignCenter()
            .Text(text =>
            {
                text.Span("Page ");
                text.CurrentPageNumber();
            });
    });
});

helloDocument.GeneratePdf("hello.pdf");

#if DEBUG
try
{
    helloDocument.ShowInPreviewer();
}
catch (Exception)
{
}
#endif

Console.ReadLine();

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("es-ES");

var model = InvoiceDocumentDataSource.GetInvoiceDetails();
var invoiceDocument = new InvoiceDocument(model);

invoiceDocument.GeneratePdf("invoice.pdf");

#if DEBUG
try
{
    invoiceDocument.ShowInPreviewer();
}
catch (Exception)
{
}
#endif

Console.ReadLine();