using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystemLogic.Logic
{
    public class BillImage
    {
        public byte[] GenerateInvoice23(string TransactionID, DataTable table)
        {
            Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10);
            MemoryStream PDFData = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, PDFData);

            var titleFont = FontFactory.GetFont("Arial", 8, Font.BOLD);
            var titleFontBlue = FontFactory.GetFont("Arial", 14, Font.NORMAL, BaseColor.BLUE);
            var boldTableFont = FontFactory.GetFont("Arial", 8, Font.BOLD);
            var bodyFont = FontFactory.GetFont("Arial", 8, Font.NORMAL);
            var EmailFont = FontFactory.GetFont("Arial", 8, Font.NORMAL, BaseColor.BLUE);
            BaseColor TabelHeaderBackGroundColor = WebColors.GetRGBColor("#EEEEEE");

            Rectangle pageSize = writer.PageSize;
            // Open the Document for writing
            pdfDoc.Open();
            //Add elements to the document here

            #region Top table
            // Create the header table 
            PdfPTable headertable = new PdfPTable(3);
            headertable.HorizontalAlignment = 0;
            headertable.WidthPercentage = 100;
            headertable.SetWidths(new float[] { 100f, 320f, 100f });
            //headertable.SetWidths(new float[] { 100f, 320f });  // then set the column's __relative__ widths
            headertable.DefaultCell.Border = Rectangle.NO_BORDER;
            //headertable.DefaultCell.Border = Rectangle.BOX; //for testing           
            string imagepath = @"D:\bornoaddress\bornoaddress.PNG";
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(imagepath);
            logo.ScaleToFit(200, 100);

            {
                PdfPCell pdfCelllogo = new PdfPCell(logo);
                pdfCelllogo.Border = Rectangle.NO_BORDER;
                pdfCelllogo.BorderColorBottom = BaseColor.BLACK;//new BaseColor(System.Drawing.Color.Black);
                pdfCelllogo.BorderWidthBottom = 1f;
                headertable.AddCell(pdfCelllogo);
            }

            {
                PdfPCell middlecell = new PdfPCell();
                middlecell.Border = Rectangle.NO_BORDER;
                middlecell.BorderColorBottom = BaseColor.BLACK;//new BaseColor(System.Drawing.Color.Black);
                middlecell.BorderWidthBottom = 1f;
                headertable.AddCell(middlecell);
            }

            {
                PdfPTable nested = new PdfPTable(1);
                nested.DefaultCell.Border = Rectangle.NO_BORDER;
                PdfPCell nextPostCell1 = new PdfPCell(new Phrase("" + table.Rows[0]["CustManager"].ToString(), titleFont));
                nextPostCell1.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell1);
                PdfPCell nextPostCell2 = new PdfPCell(new Phrase("Area Office : " + table.Rows[0]["CustLocation"].ToString(), bodyFont));
                nextPostCell2.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell2);
                PdfPCell nextPostCell3 = new PdfPCell(new Phrase("Title : Area Commercial Manager", bodyFont));
                nextPostCell3.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell3);
                PdfPCell nextPostCell4 = new PdfPCell(new Phrase("Contact : " + table.Rows[0]["ManagerContact"].ToString(), EmailFont));
                nextPostCell4.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell4);
                nested.AddCell("");
                PdfPCell nesthousing = new PdfPCell(nested);
                nesthousing.Border = Rectangle.NO_BORDER;
                nesthousing.BorderColorBottom = BaseColor.BLACK;//new BaseColor(System.Drawing.Color.Black);
                nesthousing.BorderWidthBottom = 1f;
                nesthousing.Rowspan = 5;
                nesthousing.PaddingBottom = 10f;
                headertable.AddCell(nesthousing);
            }

            PdfPTable Invoicetable = new PdfPTable(3);
            Invoicetable.HorizontalAlignment = 0;
            Invoicetable.WidthPercentage = 100;
            Invoicetable.SetWidths(new float[] { 100f, 320f, 100f });  // then set the column's __relative__ widths
            Invoicetable.DefaultCell.Border = Rectangle.NO_BORDER;

            {
                PdfPTable nested = new PdfPTable(1);
                nested.DefaultCell.Border = Rectangle.NO_BORDER;
                PdfPCell nextPostCell1 = new PdfPCell(new Phrase("INVOICE TO : ", bodyFont));
                nextPostCell1.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell1);
                PdfPCell nextPostCell2 = new PdfPCell(new Phrase(table.Rows[0]["CustomerName"].ToString(), titleFont));
                nextPostCell2.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell2);
                PdfPCell nextPostCell3 = new PdfPCell(new Phrase(table.Rows[0]["CustLocation"].ToString(), bodyFont));
                nextPostCell3.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell3);
                PdfPCell nextPostCell4 = new PdfPCell(new Phrase(table.Rows[0]["CustAccount"].ToString(), EmailFont));
                nextPostCell4.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell4);
                nested.AddCell("");
                PdfPCell nesthousing = new PdfPCell(nested);
                nesthousing.Border = Rectangle.NO_BORDER;
                //nesthousing.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                //nesthousing.BorderWidthBottom = 1f;
                nesthousing.Rowspan = 5;
                nesthousing.PaddingBottom = 10f;
                Invoicetable.AddCell(nesthousing);
            }

            {
                PdfPCell middlecell = new PdfPCell();
                middlecell.Border = Rectangle.NO_BORDER;
                //middlecell.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                //middlecell.BorderWidthBottom = 1f;
                Invoicetable.AddCell(middlecell);
            }


            {
                string billnumber = DateTime.Now.ToString(table.Rows[0]["CustAccount"].ToString() + "-" + "yyyy-MM-dd");
                PdfPTable nested = new PdfPTable(1);
                nested.DefaultCell.Border = Rectangle.NO_BORDER;
                PdfPCell nextPostCell1 = new PdfPCell(new Phrase("BILL NO. "+ billnumber, titleFontBlue));
                nextPostCell1.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell1);
                PdfPCell nextPostCell2 = new PdfPCell(new Phrase("Date of Bill: " + DateTime.Now.ToShortDateString(), bodyFont));
                nextPostCell2.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell2);
                PdfPCell nextPostCell3 = new PdfPCell(new Phrase("Due Date: " + DateTime.Now.AddDays(30).ToShortDateString(), bodyFont));
                nextPostCell3.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell3);
                nested.AddCell("");
                PdfPCell nesthousing = new PdfPCell(nested);
                nesthousing.Border = Rectangle.NO_BORDER;
                //nesthousing.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                //nesthousing.BorderWidthBottom = 1f;
                nesthousing.Rowspan = 5;
                nesthousing.PaddingBottom = 10f;
                Invoicetable.AddCell(nesthousing);
            }


            pdfDoc.Add(headertable);
            Invoicetable.PaddingTop = 10f;

            pdfDoc.Add(Invoicetable);
            #endregion

            //#region Items Table
            //Create body table
            PdfPTable itemTable = new PdfPTable(5);

            itemTable.HorizontalAlignment = 0;
            itemTable.WidthPercentage = 100;
            itemTable.SetWidths(new float[] { 5, 40, 10, 20, 25 });  // then set the column's __relative__ widths
            itemTable.SpacingAfter = 40;
            itemTable.DefaultCell.Border = Rectangle.BOX;
            PdfPCell cell1 = new PdfPCell(new Phrase("NO", boldTableFont));
            cell1.BackgroundColor = TabelHeaderBackGroundColor;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;
            itemTable.AddCell(cell1);
            PdfPCell cell2 = new PdfPCell(new Phrase("DESCRIPTION", boldTableFont));
            cell2.BackgroundColor = TabelHeaderBackGroundColor;
            cell2.HorizontalAlignment = 1;
            itemTable.AddCell(cell2);
            PdfPCell cell3 = new PdfPCell(new Phrase("QUANTITY", boldTableFont));
            cell3.BackgroundColor = TabelHeaderBackGroundColor;
            cell3.HorizontalAlignment = Element.ALIGN_CENTER;
            itemTable.AddCell(cell3);
            PdfPCell cell4 = new PdfPCell(new Phrase("UNIT AMOUNT", boldTableFont));
            cell4.BackgroundColor = TabelHeaderBackGroundColor;
            cell4.HorizontalAlignment = Element.ALIGN_CENTER;
            itemTable.AddCell(cell4);
            PdfPCell cell5 = new PdfPCell(new Phrase("TOTAL", boldTableFont));
            cell5.BackgroundColor = TabelHeaderBackGroundColor;
            cell5.HorizontalAlignment = Element.ALIGN_CENTER;
            itemTable.AddCell(cell5);
            //foreach (DataRow row in dt.Rows)
            {
                //balance b/f
                PdfPCell numberCell = new PdfPCell(new Phrase("1", bodyFont));
                numberCell.HorizontalAlignment = 1;
                numberCell.PaddingLeft = 10f;
                numberCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(numberCell);

                var _phrase = new Phrase();
                _phrase.Add(new Chunk("Balance B/f\n", EmailFont));
                //_phrase.Add(new Chunk("Subscription Plan description will add here.", bodyFont));
                PdfPCell descCell = new PdfPCell(_phrase);
                descCell.HorizontalAlignment = 0;
                descCell.PaddingLeft = 10f;
                descCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(descCell);

                PdfPCell qtyCell = new PdfPCell(new Phrase("1", bodyFont));
                qtyCell.HorizontalAlignment = 1;
                qtyCell.PaddingLeft = 10f;
                qtyCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(qtyCell);

                PdfPCell amountCell = new PdfPCell(new Phrase(table.Rows[0]["OpeningBal"].ToString(), bodyFont));
                amountCell.HorizontalAlignment = 1;
                amountCell.PaddingLeft = 10f;
                amountCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(amountCell);

                PdfPCell totalamtCell = new PdfPCell(new Phrase(table.Rows[0]["OpeningBal"].ToString(), bodyFont));
                totalamtCell.HorizontalAlignment = 1;
                totalamtCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(totalamtCell);


                //Total transactions
                PdfPCell numberCell2 = new PdfPCell(new Phrase("2", bodyFont));
                numberCell2.HorizontalAlignment = 1;
                numberCell2.PaddingLeft = 10f;
                numberCell2.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(numberCell2);

                var _phrase2 = new Phrase();
                _phrase2.Add(new Chunk("Payments\n", EmailFont));
                //_phrase.Add(new Chunk("Subscription Plan description will add here.", bodyFont));
                PdfPCell descCell2 = new PdfPCell(_phrase2);
                descCell2.HorizontalAlignment = 0;
                descCell2.PaddingLeft = 10f;
                descCell2.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(descCell2);

                PdfPCell qtyCell2 = new PdfPCell(new Phrase(table.Rows[0]["TotalPayments"].ToString(), bodyFont));
                qtyCell2.HorizontalAlignment = 1;
                qtyCell2.PaddingLeft = 10f;
                qtyCell2.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(qtyCell2);

                PdfPCell amountCell2 = new PdfPCell(new Phrase(table.Rows[0]["TotalPaymentValue"].ToString(), bodyFont));
                amountCell2.HorizontalAlignment = 1;
                amountCell2.PaddingLeft = 10f;
                amountCell2.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(amountCell2);

                PdfPCell totalamtCell2 = new PdfPCell(new Phrase(table.Rows[0]["TotalPaymentValue"].ToString(), bodyFont));
                totalamtCell2.HorizontalAlignment = 1;
                totalamtCell2.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(totalamtCell2);


                //Closing Balance
                PdfPCell numberCell3 = new PdfPCell(new Phrase("3", bodyFont));
                numberCell3.HorizontalAlignment = 1;
                numberCell3.PaddingLeft = 10f;
                numberCell3.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(numberCell3);

                var _phrase3 = new Phrase();
                _phrase3.Add(new Chunk("Current Bill\n", EmailFont));
                //_phrase.Add(new Chunk("Subscription Plan description will add here.", bodyFont));
                PdfPCell descCell3 = new PdfPCell(_phrase3);
                descCell3.HorizontalAlignment = 0;
                descCell3.PaddingLeft = 10f;
                descCell3.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(descCell3);

                PdfPCell qtyCell3 = new PdfPCell(new Phrase("1", bodyFont));
                qtyCell3.HorizontalAlignment = 1;
                qtyCell3.PaddingLeft = 10f;
                qtyCell3.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(qtyCell3);

                PdfPCell amountCell3 = new PdfPCell(new Phrase(table.Rows[0]["ClosingBal"].ToString(), bodyFont));
                amountCell3.HorizontalAlignment = 1;
                amountCell3.PaddingLeft = 10f;
                amountCell3.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(amountCell3);

                PdfPCell totalamtCell3 = new PdfPCell(new Phrase(table.Rows[0]["ClosingBal"].ToString(), bodyFont));
                totalamtCell3.HorizontalAlignment = 1;
                totalamtCell3.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(totalamtCell3);
            }
            // Table footer
            PdfPCell totalAmtCell1 = new PdfPCell(new Phrase(""));
            totalAmtCell1.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER;
            itemTable.AddCell(totalAmtCell1);
            PdfPCell totalAmtCell2 = new PdfPCell(new Phrase(""));
            totalAmtCell2.Border = Rectangle.TOP_BORDER; //Rectangle.NO_BORDER; //Rectangle.TOP_BORDER;
            itemTable.AddCell(totalAmtCell2);
            PdfPCell totalAmtCell3 = new PdfPCell(new Phrase(""));
            totalAmtCell3.Border = Rectangle.TOP_BORDER; //Rectangle.NO_BORDER; //Rectangle.TOP_BORDER;
            itemTable.AddCell(totalAmtCell3);
            PdfPCell totalAmtStrCell = new PdfPCell(new Phrase("Total Amount Due", boldTableFont));
            totalAmtStrCell.Border = Rectangle.TOP_BORDER;   //Rectangle.NO_BORDER; //Rectangle.TOP_BORDER;
            totalAmtStrCell.HorizontalAlignment = 1;
            itemTable.AddCell(totalAmtStrCell);
            PdfPCell totalAmtCell = new PdfPCell(new Phrase(table.Rows[0]["ClosingBal"].ToString(), boldTableFont));
            totalAmtCell.HorizontalAlignment = 1;
            itemTable.AddCell(totalAmtCell);

            PdfPCell cell = new PdfPCell(new Phrase("***NOTICE: Pay your arrears immediately,  for in failing to do so, the water service is to be disconnected. ***"+ "\n*** The current bill is payable within 14 days, after bill delivery.***", bodyFont));
            cell.Colspan = 5;
            cell.HorizontalAlignment = 1;
            itemTable.AddCell(cell);
            pdfDoc.Add(itemTable);

            //PdfContentByte cb = new PdfContentByte(writer);
            //BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, true);
            //cb = new PdfContentByte(writer);
            //cb = writer.DirectContent;
            //cb.BeginText();
            //cb.SetFontAndSize(bf, 8);
            //cb.SetTextMatrix(pageSize.GetLeft(120), 20);
            //cb.ShowText("Bill was created on a computer and is valid without the signature and seal. ");
            //cb.EndText();

            ////Move the pointer and draw line to separate footer section from rest of page
            //cb.MoveTo(40, pdfDoc.PageSize.GetBottom(50));
            //cb.LineTo(pdfDoc.PageSize.Width - 40, pdfDoc.PageSize.GetBottom(50));
            //cb.Stroke();

            pdfDoc.Close();
            return PDFData.ToArray();
        }

        public byte[] GenerateInvoice2(string TransactionID, DataTable table)
        {
            Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10);
            MemoryStream PDFData = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, PDFData);

            var titleFont = FontFactory.GetFont("Arial", 8, Font.BOLD);
            var titleFontBlue = FontFactory.GetFont("Arial", 14, Font.NORMAL, BaseColor.BLUE);
            var boldTableFont = FontFactory.GetFont("Arial", 8, Font.BOLD);
            var bodyFont = FontFactory.GetFont("Arial", 8, Font.NORMAL);
            var EmailFont = FontFactory.GetFont("Arial", 8, Font.NORMAL, BaseColor.BLUE);
            BaseColor TabelHeaderBackGroundColor = WebColors.GetRGBColor("#EEEEEE");

            Rectangle pageSize = writer.PageSize;
            // Open the Document for writing
            pdfDoc.Open();
            //Add elements to the document here

            #region Top table
            // Create the header table 
            PdfPTable headertable = new PdfPTable(3);
            headertable.HorizontalAlignment = 0;
            headertable.WidthPercentage = 100;
            headertable.SetWidths(new float[] { 100f, 320f, 100f });
            //headertable.SetWidths(new float[] { 100f, 320f });  // then set the column's __relative__ widths
            headertable.DefaultCell.Border = Rectangle.NO_BORDER;
            //headertable.DefaultCell.Border = Rectangle.BOX; //for testing           
            string imagepath = @"D:\LTA\emoji.PNG";
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(imagepath);
            logo.ScaleToFit(200, 100);

            {
                PdfPCell pdfCelllogo = new PdfPCell(logo);
                pdfCelllogo.Border = Rectangle.NO_BORDER;
                pdfCelllogo.BorderColorBottom = BaseColor.BLACK;//new BaseColor(System.Drawing.Color.Black);
                pdfCelllogo.BorderWidthBottom = 1f;
                headertable.AddCell(pdfCelllogo);
            }

            {
                PdfPCell middlecell = new PdfPCell();
                middlecell.Border = Rectangle.NO_BORDER;
                middlecell.BorderColorBottom = BaseColor.BLACK;//new BaseColor(System.Drawing.Color.Black);
                middlecell.BorderWidthBottom = 1f;
                headertable.AddCell(middlecell);
            }

            {
                PdfPTable nested = new PdfPTable(1);
                nested.DefaultCell.Border = Rectangle.NO_BORDER;
                PdfPCell nextPostCell1 = new PdfPCell(new Phrase("" + table.Rows[0]["CustManager"].ToString(), titleFont));
                nextPostCell1.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell1);
                PdfPCell nextPostCell2 = new PdfPCell(new Phrase("Location : " + table.Rows[0]["CustLocation"].ToString(), bodyFont));
                nextPostCell2.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell2);
                PdfPCell nextPostCell3 = new PdfPCell(new Phrase("Title : Area Commercial Manager", bodyFont));
                nextPostCell3.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell3);
                PdfPCell nextPostCell4 = new PdfPCell(new Phrase("Contact : " + table.Rows[0]["ManagerContact"].ToString(), EmailFont));
                nextPostCell4.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell4);
                nested.AddCell("");
                PdfPCell nesthousing = new PdfPCell(nested);
                nesthousing.Border = Rectangle.NO_BORDER;
                nesthousing.BorderColorBottom = BaseColor.BLACK;//new BaseColor(System.Drawing.Color.Black);
                nesthousing.BorderWidthBottom = 1f;
                nesthousing.Rowspan = 5;
                nesthousing.PaddingBottom = 10f;
                headertable.AddCell(nesthousing);
            }

            PdfPTable Invoicetable = new PdfPTable(3);
            Invoicetable.HorizontalAlignment = 0;
            Invoicetable.WidthPercentage = 100;
            Invoicetable.SetWidths(new float[] { 100f, 320f, 100f });  // then set the column's __relative__ widths
            Invoicetable.DefaultCell.Border = Rectangle.NO_BORDER;

            {
                PdfPTable nested = new PdfPTable(1);
                nested.DefaultCell.Border = Rectangle.NO_BORDER;
                PdfPCell nextPostCell1 = new PdfPCell(new Phrase("INVOICE TO : ", bodyFont));
                nextPostCell1.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell1);
                PdfPCell nextPostCell2 = new PdfPCell(new Phrase(table.Rows[0]["CustomerName"].ToString(), titleFont));
                nextPostCell2.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell2);
                PdfPCell nextPostCell3 = new PdfPCell(new Phrase(table.Rows[0]["CustLocation"].ToString(), bodyFont));
                nextPostCell3.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell3);
                PdfPCell nextPostCell4 = new PdfPCell(new Phrase(table.Rows[0]["CustAccount"].ToString(), EmailFont));
                nextPostCell4.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell4);
                nested.AddCell("");
                PdfPCell nesthousing = new PdfPCell(nested);
                nesthousing.Border = Rectangle.NO_BORDER;
                //nesthousing.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                //nesthousing.BorderWidthBottom = 1f;
                nesthousing.Rowspan = 5;
                nesthousing.PaddingBottom = 10f;
                Invoicetable.AddCell(nesthousing);
            }

            {
                PdfPCell middlecell = new PdfPCell();
                middlecell.Border = Rectangle.NO_BORDER;
                //middlecell.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                //middlecell.BorderWidthBottom = 1f;
                Invoicetable.AddCell(middlecell);
            }


            {
                string billnumber = DateTime.Now.ToString(table.Rows[0]["CustAccount"].ToString() + "-" + "yyyy-MM-dd");
                PdfPTable nested = new PdfPTable(1);
                nested.DefaultCell.Border = Rectangle.NO_BORDER;
                PdfPCell nextPostCell1 = new PdfPCell(new Phrase("BILL NO. " + billnumber, titleFontBlue));
                nextPostCell1.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell1);
                PdfPCell nextPostCell2 = new PdfPCell(new Phrase("Date of Bill: " + DateTime.Now.ToShortDateString(), bodyFont));
                nextPostCell2.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell2);
                PdfPCell nextPostCell3 = new PdfPCell(new Phrase("Due Date: " + DateTime.Now.AddDays(30).ToShortDateString(), bodyFont));
                nextPostCell3.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell3);
                nested.AddCell("");
                PdfPCell nesthousing = new PdfPCell(nested);
                nesthousing.Border = Rectangle.NO_BORDER;
                //nesthousing.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                //nesthousing.BorderWidthBottom = 1f;
                nesthousing.Rowspan = 5;
                nesthousing.PaddingBottom = 10f;
                Invoicetable.AddCell(nesthousing);
            }


            pdfDoc.Add(headertable);
            Invoicetable.PaddingTop = 10f;

            pdfDoc.Add(Invoicetable);
            #endregion

            //#region Items Table
            //Create body table
            PdfPTable itemTable = new PdfPTable(5);

            itemTable.HorizontalAlignment = 0;
            itemTable.WidthPercentage = 100;
            itemTable.SetWidths(new float[] { 5, 40, 10, 20, 25 });  // then set the column's __relative__ widths
            itemTable.SpacingAfter = 40;
            itemTable.DefaultCell.Border = Rectangle.BOX;
            PdfPCell cell11 = new PdfPCell(new Phrase("METER NO", boldTableFont));
            cell11.BackgroundColor = TabelHeaderBackGroundColor;
            cell11.HorizontalAlignment = Element.ALIGN_CENTER;
            itemTable.AddCell(cell11);
            PdfPCell cell12 = new PdfPCell(new Phrase("Reading Date", boldTableFont));
            cell12.BackgroundColor = TabelHeaderBackGroundColor;
            cell12.HorizontalAlignment = 1;
            itemTable.AddCell(cell12);
            PdfPCell cell13 = new PdfPCell(new Phrase("LastReading", boldTableFont));
            cell13.BackgroundColor = TabelHeaderBackGroundColor;
            cell13.HorizontalAlignment = Element.ALIGN_CENTER;
            itemTable.AddCell(cell13);
            PdfPCell cell14 = new PdfPCell(new Phrase("Previous Reading", boldTableFont));
            cell14.BackgroundColor = TabelHeaderBackGroundColor;
            cell14.HorizontalAlignment = Element.ALIGN_CENTER;
            itemTable.AddCell(cell14);
            PdfPCell cell15 = new PdfPCell(new Phrase("Water Consumed", boldTableFont));
            cell15.BackgroundColor = TabelHeaderBackGroundColor;
            cell15.HorizontalAlignment = Element.ALIGN_CENTER;
            itemTable.AddCell(cell15);
            //foreach (DataRow row in dt.Rows)
            {
                //balance b/f
                PdfPCell numberCell = new PdfPCell(new Phrase("1", bodyFont));
                numberCell.HorizontalAlignment = 1;
                numberCell.PaddingLeft = 10f;
                numberCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(numberCell);

                var _phrase = new Phrase();
                _phrase.Add(new Chunk("Balance B/f\n", EmailFont));
                //_phrase.Add(new Chunk("Subscription Plan description will add here.", bodyFont));
                PdfPCell descCell = new PdfPCell(_phrase);
                descCell.HorizontalAlignment = 0;
                descCell.PaddingLeft = 10f;
                descCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(descCell);

                PdfPCell qtyCell = new PdfPCell(new Phrase("1", bodyFont));
                qtyCell.HorizontalAlignment = 1;
                qtyCell.PaddingLeft = 10f;
                qtyCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(qtyCell);

                PdfPCell amountCell = new PdfPCell(new Phrase(table.Rows[0]["OpeningBal"].ToString(), bodyFont));
                amountCell.HorizontalAlignment = 1;
                amountCell.PaddingLeft = 10f;
                amountCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(amountCell);

                PdfPCell totalamtCell = new PdfPCell(new Phrase(table.Rows[0]["OpeningBal"].ToString(), bodyFont));
                totalamtCell.HorizontalAlignment = 1;
                totalamtCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(totalamtCell);
            }

            itemTable.HorizontalAlignment = 0;
            itemTable.WidthPercentage = 100;
            itemTable.SetWidths(new float[] { 5, 40, 10, 20, 25 });  // then set the column's __relative__ widths
            itemTable.SpacingAfter = 40;
            itemTable.DefaultCell.Border = Rectangle.BOX;
            PdfPCell cell1 = new PdfPCell(new Phrase("NO", boldTableFont));
            cell1.BackgroundColor = TabelHeaderBackGroundColor;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;
            itemTable.AddCell(cell1);
            PdfPCell cell2 = new PdfPCell(new Phrase("DESCRIPTION", boldTableFont));
            cell2.BackgroundColor = TabelHeaderBackGroundColor;
            cell2.HorizontalAlignment = 1;
            itemTable.AddCell(cell2);
            PdfPCell cell3 = new PdfPCell(new Phrase("QUANTITY", boldTableFont));
            cell3.BackgroundColor = TabelHeaderBackGroundColor;
            cell3.HorizontalAlignment = Element.ALIGN_CENTER;
            itemTable.AddCell(cell3);
            PdfPCell cell4 = new PdfPCell(new Phrase("UNIT AMOUNT", boldTableFont));
            cell4.BackgroundColor = TabelHeaderBackGroundColor;
            cell4.HorizontalAlignment = Element.ALIGN_CENTER;
            itemTable.AddCell(cell4);
            PdfPCell cell5 = new PdfPCell(new Phrase("TOTAL", boldTableFont));
            cell5.BackgroundColor = TabelHeaderBackGroundColor;
            cell5.HorizontalAlignment = Element.ALIGN_CENTER;
            itemTable.AddCell(cell5);
            //foreach (DataRow row in dt.Rows)
            {
                //balance b/f
                PdfPCell numberCell = new PdfPCell(new Phrase("1", bodyFont));
                numberCell.HorizontalAlignment = 1;
                numberCell.PaddingLeft = 10f;
                numberCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(numberCell);

                var _phrase = new Phrase();
                _phrase.Add(new Chunk("Balance B/f\n", EmailFont));
                //_phrase.Add(new Chunk("Subscription Plan description will add here.", bodyFont));
                PdfPCell descCell = new PdfPCell(_phrase);
                descCell.HorizontalAlignment = 0;
                descCell.PaddingLeft = 10f;
                descCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(descCell);

                PdfPCell qtyCell = new PdfPCell(new Phrase("1", bodyFont));
                qtyCell.HorizontalAlignment = 1;
                qtyCell.PaddingLeft = 10f;
                qtyCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(qtyCell);

                PdfPCell amountCell = new PdfPCell(new Phrase(table.Rows[0]["OpeningBal"].ToString(), bodyFont));
                amountCell.HorizontalAlignment = 1;
                amountCell.PaddingLeft = 10f;
                amountCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(amountCell);

                PdfPCell totalamtCell = new PdfPCell(new Phrase(table.Rows[0]["OpeningBal"].ToString(), bodyFont));
                totalamtCell.HorizontalAlignment = 1;
                totalamtCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(totalamtCell);


                //Total transactions
                PdfPCell numberCell2 = new PdfPCell(new Phrase("2", bodyFont));
                numberCell2.HorizontalAlignment = 1;
                numberCell2.PaddingLeft = 10f;
                numberCell2.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(numberCell2);

                var _phrase2 = new Phrase();
                _phrase2.Add(new Chunk("Payments\n", EmailFont));
                //_phrase.Add(new Chunk("Subscription Plan description will add here.", bodyFont));
                PdfPCell descCell2 = new PdfPCell(_phrase2);
                descCell2.HorizontalAlignment = 0;
                descCell2.PaddingLeft = 10f;
                descCell2.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(descCell2);

                PdfPCell qtyCell2 = new PdfPCell(new Phrase(table.Rows[0]["TotalPayments"].ToString(), bodyFont));
                qtyCell2.HorizontalAlignment = 1;
                qtyCell2.PaddingLeft = 10f;
                qtyCell2.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(qtyCell2);

                PdfPCell amountCell2 = new PdfPCell(new Phrase(table.Rows[0]["TotalPaymentValue"].ToString(), bodyFont));
                amountCell2.HorizontalAlignment = 1;
                amountCell2.PaddingLeft = 10f;
                amountCell2.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(amountCell2);

                PdfPCell totalamtCell2 = new PdfPCell(new Phrase(table.Rows[0]["TotalPaymentValue"].ToString(), bodyFont));
                totalamtCell2.HorizontalAlignment = 1;
                totalamtCell2.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(totalamtCell2);


                //Closing Balance
                PdfPCell numberCell3 = new PdfPCell(new Phrase("3", bodyFont));
                numberCell3.HorizontalAlignment = 1;
                numberCell3.PaddingLeft = 10f;
                numberCell3.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(numberCell3);

                var _phrase3 = new Phrase();
                _phrase3.Add(new Chunk("Current Balance\n", EmailFont));
                //_phrase.Add(new Chunk("Subscription Plan description will add here.", bodyFont));
                PdfPCell descCell3 = new PdfPCell(_phrase3);
                descCell3.HorizontalAlignment = 0;
                descCell3.PaddingLeft = 10f;
                descCell3.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(descCell3);

                PdfPCell qtyCell3 = new PdfPCell(new Phrase("1", bodyFont));
                qtyCell3.HorizontalAlignment = 1;
                qtyCell3.PaddingLeft = 10f;
                qtyCell3.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(qtyCell3);

                PdfPCell amountCell3 = new PdfPCell(new Phrase(table.Rows[0]["ClosingBal"].ToString(), bodyFont));
                amountCell3.HorizontalAlignment = 1;
                amountCell3.PaddingLeft = 10f;
                amountCell3.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(amountCell3);

                PdfPCell totalamtCell3 = new PdfPCell(new Phrase(table.Rows[0]["ClosingBal"].ToString(), bodyFont));
                totalamtCell3.HorizontalAlignment = 1;
                totalamtCell3.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(totalamtCell3);
            }
            // Table footer
            PdfPCell totalAmtCell1 = new PdfPCell(new Phrase(""));
            totalAmtCell1.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER;
            itemTable.AddCell(totalAmtCell1);
            PdfPCell totalAmtCell2 = new PdfPCell(new Phrase(""));
            totalAmtCell2.Border = Rectangle.TOP_BORDER; //Rectangle.NO_BORDER; //Rectangle.TOP_BORDER;
            itemTable.AddCell(totalAmtCell2);
            PdfPCell totalAmtCell3 = new PdfPCell(new Phrase(""));
            totalAmtCell3.Border = Rectangle.TOP_BORDER; //Rectangle.NO_BORDER; //Rectangle.TOP_BORDER;
            itemTable.AddCell(totalAmtCell3);
            PdfPCell totalAmtStrCell = new PdfPCell(new Phrase("Total Amount Due", boldTableFont));
            totalAmtStrCell.Border = Rectangle.TOP_BORDER;   //Rectangle.NO_BORDER; //Rectangle.TOP_BORDER;
            totalAmtStrCell.HorizontalAlignment = 1;
            itemTable.AddCell(totalAmtStrCell);
            PdfPCell totalAmtCell = new PdfPCell(new Phrase(table.Rows[0]["ClosingBal"].ToString(), boldTableFont));
            totalAmtCell.HorizontalAlignment = 1;
            itemTable.AddCell(totalAmtCell);

            PdfPCell cell = new PdfPCell(new Phrase("***NOTICE: Pay your arrears immediately,  for in failing to do so, the water service is to be disconnected. ***" + "\n*** The current bill is payable within 14 days, after bill delivery.***", bodyFont));
            cell.Colspan = 5;
            cell.HorizontalAlignment = 1;
            itemTable.AddCell(cell);
            pdfDoc.Add(itemTable);

            //PdfContentByte cb = new PdfContentByte(writer);
            //BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, true);
            //cb = new PdfContentByte(writer);
            //cb = writer.DirectContent;
            //cb.BeginText();
            //cb.SetFontAndSize(bf, 8);
            //cb.SetTextMatrix(pageSize.GetLeft(120), 20);
            //cb.ShowText("Bill was created on a computer and is valid without the signature and seal. ");
            //cb.EndText();

            ////Move the pointer and draw line to separate footer section from rest of page
            //cb.MoveTo(40, pdfDoc.PageSize.GetBottom(50));
            //cb.LineTo(pdfDoc.PageSize.Width - 40, pdfDoc.PageSize.GetBottom(50));
            //cb.Stroke();

            pdfDoc.Close();
            return PDFData.ToArray();
        }

        public byte[] GenerateInvoiceLTA(string TransactionID, DataTable table)
        {
            Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10);
            MemoryStream PDFData = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, PDFData);

            var titleFont = FontFactory.GetFont("Arial", 8, Font.BOLD);
            var titleFontBlue = FontFactory.GetFont("Arial", 14, Font.NORMAL, BaseColor.BLUE);
            var boldTableFont = FontFactory.GetFont("Arial", 8, Font.BOLD);
            var bodyFont = FontFactory.GetFont("Arial", 8, Font.NORMAL);
            var EmailFont = FontFactory.GetFont("Arial", 8, Font.NORMAL, BaseColor.BLUE);
            BaseColor TabelHeaderBackGroundColor = WebColors.GetRGBColor("#EEEEEE");

            Rectangle pageSize = writer.PageSize;
            // Open the Document for writing
            pdfDoc.Open();
            //Add elements to the document here

            #region Top table
            // Create the header table 
            PdfPTable headertable = new PdfPTable(3);
            headertable.HorizontalAlignment = 0;
            headertable.WidthPercentage = 100;
            headertable.SetWidths(new float[] { 100f, 320f, 100f });
            //headertable.SetWidths(new float[] { 100f, 320f });  // then set the column's __relative__ widths
            headertable.DefaultCell.Border = Rectangle.NO_BORDER;
            //headertable.DefaultCell.Border = Rectangle.BOX; //for testing           
            string imagepath = @"D:\LTA\emoji.PNG";
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(imagepath);
            logo.ScaleToFit(200, 100);

            {
                PdfPCell pdfCelllogo = new PdfPCell(logo);
                pdfCelllogo.Border = Rectangle.NO_BORDER;
                pdfCelllogo.BorderColorBottom = BaseColor.BLACK;//new BaseColor(System.Drawing.Color.Black);
                pdfCelllogo.BorderWidthBottom = 1f;
                headertable.AddCell(pdfCelllogo);
            }

            {
                PdfPCell middlecell = new PdfPCell();
                middlecell.Border = Rectangle.NO_BORDER;
                middlecell.BorderColorBottom = BaseColor.BLACK;//new BaseColor(System.Drawing.Color.Black);
                middlecell.BorderWidthBottom = 1f;
                headertable.AddCell(middlecell);
            }

            {
                PdfPTable nested = new PdfPTable(1);
                nested.DefaultCell.Border = Rectangle.NO_BORDER;
                PdfPCell nextPostCell1 = new PdfPCell(new Phrase("WATER SUPPLY & SEWEARAGE AUTHORITY", titleFont));//new PdfPCell(new Phrase("" + table.Rows[0]["CustManager"].ToString(), titleFont));
                nextPostCell1.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell1);
                PdfPCell nextPostCell2 = new PdfPCell(new Phrase("Location : FILTU TOWN WATER SUPPLY SERVICE", bodyFont));
                nextPostCell2.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell2);
                PdfPCell nextPostCell3 = new PdfPCell(new Phrase("P.O BOX", bodyFont));
                nextPostCell3.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell3);
                PdfPCell nextPostCell4 = new PdfPCell(new Phrase("_________________" , EmailFont));
                nextPostCell4.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell4);
                nested.AddCell("");
                PdfPCell nesthousing = new PdfPCell(nested);
                nesthousing.Border = Rectangle.NO_BORDER;
                nesthousing.BorderColorBottom = BaseColor.BLACK;//new BaseColor(System.Drawing.Color.Black);
                nesthousing.BorderWidthBottom = 1f;
                nesthousing.Rowspan = 5;
                nesthousing.PaddingBottom = 10f;
                headertable.AddCell(nesthousing);
            }

            PdfPTable Invoicetable = new PdfPTable(3);
            Invoicetable.HorizontalAlignment = 0;
            Invoicetable.WidthPercentage = 100;
            Invoicetable.SetWidths(new float[] { 100f, 320f, 100f });  // then set the column's __relative__ widths
            Invoicetable.DefaultCell.Border = Rectangle.NO_BORDER;

            {
                PdfPTable nested = new PdfPTable(1);
                nested.DefaultCell.Border = Rectangle.NO_BORDER;
                PdfPCell nextPostCell1 = new PdfPCell(new Phrase("INVOICE TO : ", bodyFont));
                nextPostCell1.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell1);
                PdfPCell nextPostCell2 = new PdfPCell(new Phrase(table.Rows[0]["FullName"].ToString(), titleFont));
                nextPostCell2.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell2);
                PdfPCell nextPostCell3 = new PdfPCell(new Phrase(table.Rows[0]["CustLocation"].ToString(), bodyFont));
                nextPostCell3.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell3);
                PdfPCell nextPostCell4 = new PdfPCell(new Phrase(table.Rows[0]["CustomerId"].ToString(), EmailFont));
                nextPostCell4.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell4);
                nested.AddCell("");
                PdfPCell nesthousing = new PdfPCell(nested);
                nesthousing.Border = Rectangle.NO_BORDER;
                //nesthousing.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                //nesthousing.BorderWidthBottom = 1f;
                nesthousing.Rowspan = 5;
                nesthousing.PaddingBottom = 10f;
                Invoicetable.AddCell(nesthousing);
            }

            {
                PdfPCell middlecell = new PdfPCell();
                middlecell.Border = Rectangle.NO_BORDER;
                //middlecell.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                //middlecell.BorderWidthBottom = 1f;
                Invoicetable.AddCell(middlecell);
            }


            {
                string billnumber = DateTime.Now.ToString(table.Rows[0]["RecordId"].ToString() + "-" + "yyyy-MM-dd");
                PdfPTable nested = new PdfPTable(1);
                nested.DefaultCell.Border = Rectangle.NO_BORDER;
                PdfPCell nextPostCell1 = new PdfPCell(new Phrase("BILL NO. " + billnumber, titleFontBlue));
                nextPostCell1.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell1);
                PdfPCell nextPostCell2 = new PdfPCell(new Phrase("Date of Bill: " + DateTime.Now.ToShortDateString(), bodyFont));
                nextPostCell2.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell2);
                PdfPCell nextPostCell3 = new PdfPCell(new Phrase("Due Date: " + DateTime.Now.AddDays(30).ToShortDateString(), bodyFont));
                nextPostCell3.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell3);
                nested.AddCell("");
                PdfPCell nesthousing = new PdfPCell(nested);
                nesthousing.Border = Rectangle.NO_BORDER;
                //nesthousing.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                //nesthousing.BorderWidthBottom = 1f;
                nesthousing.Rowspan = 5;
                nesthousing.PaddingBottom = 10f;
                Invoicetable.AddCell(nesthousing);
            }


            pdfDoc.Add(headertable);
            Invoicetable.PaddingTop = 10f;

            pdfDoc.Add(Invoicetable);
            #endregion

            //#region Items Table
            //Create body table
            PdfPTable itemTable = new PdfPTable(5);

            itemTable.HorizontalAlignment = 0;
            itemTable.WidthPercentage = 100;
            itemTable.SetWidths(new float[] { 5, 40, 10, 20, 25 });  // then set the column's __relative__ widths
            itemTable.SpacingAfter = 40;
            itemTable.DefaultCell.Border = Rectangle.BOX;
            PdfPCell cell11 = new PdfPCell(new Phrase("METER NO", boldTableFont));
            cell11.BackgroundColor = TabelHeaderBackGroundColor;
            cell11.HorizontalAlignment = Element.ALIGN_CENTER;
            itemTable.AddCell(cell11);
            PdfPCell cell12 = new PdfPCell(new Phrase("Reading Date", boldTableFont));
            cell12.BackgroundColor = TabelHeaderBackGroundColor;
            cell12.HorizontalAlignment = 1;
            itemTable.AddCell(cell12);
            PdfPCell cell13 = new PdfPCell(new Phrase("LastReading", boldTableFont));
            cell13.BackgroundColor = TabelHeaderBackGroundColor;
            cell13.HorizontalAlignment = Element.ALIGN_CENTER;
            itemTable.AddCell(cell13);
            PdfPCell cell14 = new PdfPCell(new Phrase("Previous Reading", boldTableFont));
            cell14.BackgroundColor = TabelHeaderBackGroundColor;
            cell14.HorizontalAlignment = Element.ALIGN_CENTER;
            itemTable.AddCell(cell14);
            PdfPCell cell15 = new PdfPCell(new Phrase("Water Consumed", boldTableFont));
            cell15.BackgroundColor = TabelHeaderBackGroundColor;
            cell15.HorizontalAlignment = Element.ALIGN_CENTER;
            itemTable.AddCell(cell15);
            //foreach (DataRow row in dt.Rows)
            {
                //balance b/f
                PdfPCell numberCell = new PdfPCell(new Phrase("1", bodyFont));
                numberCell.HorizontalAlignment = 1;
                numberCell.PaddingLeft = 10f;
                numberCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(numberCell);

                var _phrase = new Phrase();
                _phrase.Add(new Chunk(table.Rows[0]["CurrentReadingdate"].ToString(), bodyFont));
                //_phrase.Add(new Chunk("Balance B/f\n", EmailFont));
                //_phrase.Add(new Chunk("Subscription Plan description will add here.", bodyFont));
                PdfPCell descCell = new PdfPCell(_phrase);
                descCell.HorizontalAlignment = 0;
                descCell.PaddingLeft = 10f;
                descCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(descCell);

                PdfPCell qtyCell = new PdfPCell(new Phrase(table.Rows[0]["CurrentReading"].ToString(), bodyFont));
                qtyCell.HorizontalAlignment = 1;
                qtyCell.PaddingLeft = 10f;
                qtyCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(qtyCell);

                PdfPCell amountCell = new PdfPCell(new Phrase(table.Rows[0]["PrevReading"].ToString(), bodyFont));
                amountCell.HorizontalAlignment = 1;
                amountCell.PaddingLeft = 10f;
                amountCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(amountCell);

                PdfPCell totalamtCell = new PdfPCell(new Phrase(table.Rows[0]["Consumption"].ToString(), bodyFont));
                totalamtCell.HorizontalAlignment = 1;
                totalamtCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(totalamtCell);
            }

            itemTable.HorizontalAlignment = 0;
            itemTable.WidthPercentage = 100;
            itemTable.SetWidths(new float[] { 5, 40, 10, 20, 25 });  // then set the column's __relative__ widths
            itemTable.SpacingAfter = 40;
            itemTable.DefaultCell.Border = Rectangle.BOX;
            PdfPCell cell1 = new PdfPCell(new Phrase("NO", boldTableFont));
            cell1.BackgroundColor = TabelHeaderBackGroundColor;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;
            itemTable.AddCell(cell1);
            PdfPCell cell2 = new PdfPCell(new Phrase("DESCRIPTION", boldTableFont));
            cell2.BackgroundColor = TabelHeaderBackGroundColor;
            cell2.HorizontalAlignment = 1;
            itemTable.AddCell(cell2);
            PdfPCell cell3 = new PdfPCell(new Phrase("QUANTITY", boldTableFont));
            cell3.BackgroundColor = TabelHeaderBackGroundColor;
            cell3.HorizontalAlignment = Element.ALIGN_CENTER;
            itemTable.AddCell(cell3);
            PdfPCell cell4 = new PdfPCell(new Phrase("UNIT AMOUNT", boldTableFont));
            cell4.BackgroundColor = TabelHeaderBackGroundColor;
            cell4.HorizontalAlignment = Element.ALIGN_CENTER;
            itemTable.AddCell(cell4);
            PdfPCell cell5 = new PdfPCell(new Phrase("TOTAL", boldTableFont));
            cell5.BackgroundColor = TabelHeaderBackGroundColor;
            cell5.HorizontalAlignment = Element.ALIGN_CENTER;
            itemTable.AddCell(cell5);
            //foreach (DataRow row in dt.Rows)
            {
                //balance b/f
                PdfPCell numberCell = new PdfPCell(new Phrase("1", bodyFont));
                numberCell.HorizontalAlignment = 1;
                numberCell.PaddingLeft = 10f;
                numberCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(numberCell);

                var _phrase = new Phrase();
                _phrase.Add(new Chunk("Balance B/f\n", EmailFont));
                //_phrase.Add(new Chunk("Subscription Plan description will add here.", bodyFont));
                PdfPCell descCell = new PdfPCell(_phrase);
                descCell.HorizontalAlignment = 0;
                descCell.PaddingLeft = 10f;
                descCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(descCell);

                PdfPCell qtyCell = new PdfPCell(new Phrase("1", bodyFont));
                qtyCell.HorizontalAlignment = 1;
                qtyCell.PaddingLeft = 10f;
                qtyCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(qtyCell);

                PdfPCell amountCell = new PdfPCell(new Phrase(table.Rows[0]["OpeningBal"].ToString(), bodyFont));
                amountCell.HorizontalAlignment = 1;
                amountCell.PaddingLeft = 10f;
                amountCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(amountCell);

                PdfPCell totalamtCell = new PdfPCell(new Phrase(table.Rows[0]["OpeningBal"].ToString(), bodyFont));
                totalamtCell.HorizontalAlignment = 1;
                totalamtCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(totalamtCell);


                //Total transactions
                PdfPCell numberCell2 = new PdfPCell(new Phrase("2", bodyFont));
                numberCell2.HorizontalAlignment = 1;
                numberCell2.PaddingLeft = 10f;
                numberCell2.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(numberCell2);

                var _phrase2 = new Phrase();
                _phrase2.Add(new Chunk("Payments\n", EmailFont));
                //_phrase.Add(new Chunk("Subscription Plan description will add here.", bodyFont));
                PdfPCell descCell2 = new PdfPCell(_phrase2);
                descCell2.HorizontalAlignment = 0;
                descCell2.PaddingLeft = 10f;
                descCell2.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(descCell2);

                PdfPCell qtyCell2 = new PdfPCell(new Phrase(table.Rows[0]["TotalPayments"].ToString(), bodyFont));
                qtyCell2.HorizontalAlignment = 1;
                qtyCell2.PaddingLeft = 10f;
                qtyCell2.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(qtyCell2);

                PdfPCell amountCell2 = new PdfPCell(new Phrase(table.Rows[0]["TotalPaymentValue"].ToString(), bodyFont));
                amountCell2.HorizontalAlignment = 1;
                amountCell2.PaddingLeft = 10f;
                amountCell2.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(amountCell2);

                PdfPCell totalamtCell2 = new PdfPCell(new Phrase(table.Rows[0]["TotalPaymentValue"].ToString(), bodyFont));
                totalamtCell2.HorizontalAlignment = 1;
                totalamtCell2.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(totalamtCell2);


                //Closing Balance
                PdfPCell numberCell3 = new PdfPCell(new Phrase("3", bodyFont));
                numberCell3.HorizontalAlignment = 1;
                numberCell3.PaddingLeft = 10f;
                numberCell3.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(numberCell3);

                var _phrase3 = new Phrase();
                _phrase3.Add(new Chunk("Current Balance\n", EmailFont));
                //_phrase.Add(new Chunk("Subscription Plan description will add here.", bodyFont));
                PdfPCell descCell3 = new PdfPCell(_phrase3);
                descCell3.HorizontalAlignment = 0;
                descCell3.PaddingLeft = 10f;
                descCell3.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(descCell3);

                PdfPCell qtyCell3 = new PdfPCell(new Phrase("1", bodyFont));
                qtyCell3.HorizontalAlignment = 1;
                qtyCell3.PaddingLeft = 10f;
                qtyCell3.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(qtyCell3);

                PdfPCell amountCell3 = new PdfPCell(new Phrase(table.Rows[0]["ClosingBal"].ToString(), bodyFont));
                amountCell3.HorizontalAlignment = 1;
                amountCell3.PaddingLeft = 10f;
                amountCell3.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(amountCell3);

                PdfPCell totalamtCell3 = new PdfPCell(new Phrase(table.Rows[0]["ClosingBal"].ToString(), bodyFont));
                totalamtCell3.HorizontalAlignment = 1;
                totalamtCell3.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(totalamtCell3);
            }
            // Table footer
            PdfPCell totalAmtCell1 = new PdfPCell(new Phrase(""));
            totalAmtCell1.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER;
            itemTable.AddCell(totalAmtCell1);
            PdfPCell totalAmtCell2 = new PdfPCell(new Phrase(""));
            totalAmtCell2.Border = Rectangle.TOP_BORDER; //Rectangle.NO_BORDER; //Rectangle.TOP_BORDER;
            itemTable.AddCell(totalAmtCell2);
            PdfPCell totalAmtCell3 = new PdfPCell(new Phrase(""));
            totalAmtCell3.Border = Rectangle.TOP_BORDER; //Rectangle.NO_BORDER; //Rectangle.TOP_BORDER;
            itemTable.AddCell(totalAmtCell3);
            PdfPCell totalAmtStrCell = new PdfPCell(new Phrase("Total Amount Due", boldTableFont));
            totalAmtStrCell.Border = Rectangle.TOP_BORDER;   //Rectangle.NO_BORDER; //Rectangle.TOP_BORDER;
            totalAmtStrCell.HorizontalAlignment = 1;
            itemTable.AddCell(totalAmtStrCell);
            PdfPCell totalAmtCell = new PdfPCell(new Phrase(table.Rows[0]["ClosingBal"].ToString(), boldTableFont));
            totalAmtCell.HorizontalAlignment = 1;
            itemTable.AddCell(totalAmtCell);

            PdfPCell cell = new PdfPCell(new Phrase("***NOTICE: Pay your arrears immediately,  for in failing to do so, the water service is to be disconnected. ***" + "\n*** The current bill is payable within 14 days, after bill delivery.***", bodyFont));
            cell.Colspan = 5;
            cell.HorizontalAlignment = 1;
            itemTable.AddCell(cell);
            pdfDoc.Add(itemTable);

            //PdfContentByte cb = new PdfContentByte(writer);
            //BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, true);
            //cb = new PdfContentByte(writer);
            //cb = writer.DirectContent;
            //cb.BeginText();
            //cb.SetFontAndSize(bf, 8);
            //cb.SetTextMatrix(pageSize.GetLeft(120), 20);
            //cb.ShowText("Bill was created on a computer and is valid without the signature and seal. ");
            //cb.EndText();

            ////Move the pointer and draw line to separate footer section from rest of page
            //cb.MoveTo(40, pdfDoc.PageSize.GetBottom(50));
            //cb.LineTo(pdfDoc.PageSize.Width - 40, pdfDoc.PageSize.GetBottom(50));
            //cb.Stroke();

            pdfDoc.Close();
            return PDFData.ToArray();
        }

    }
}
