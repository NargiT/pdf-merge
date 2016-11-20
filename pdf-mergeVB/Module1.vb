Imports iText.Forms
Imports iText.Kernel.Pdf
Imports iText.Pdfa

Module Module1

    Sub Main()
        Dim pdf1 = New PdfDocument(New PdfReader("path"))
        Dim pdf2 = New PdfDocument(New PdfReader("path"))

        Dim pdfWriter = New PdfWriter("finale")
        pdfWriter.SetSmartMode(True)
        Dim finalPdf = New PdfDocument(pdfWriter)
        finalPdf.InitializeOutlines()

        pdf1.CopyPagesTo(1, pdf1.GetNumberOfPages(), finalPdf, New PdfPageFormCopier())
        pdf2.CopyPagesTo(1, pdf2.GetNumberOfPages(), finalPdf, New PdfPageFormCopier())
        finalPdf.Close()
    End Sub



End Module
