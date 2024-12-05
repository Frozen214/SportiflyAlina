using Microsoft.Office.Interop.Word;
using Sportifly.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Application = Microsoft.Office.Interop.Word.Application;
using Document = Microsoft.Office.Interop.Word.Document;

namespace Sportifly.Common
{
    public class ReceiptGenerator
    {
        public bool GenerateReceipt(PaymentDataModel paymentData, string inputFilePath, string outputFilePath)
        {
            var replacements = new Dictionary<string, string>
            {
                { "<Номер>", paymentData.Number },
                { "<Услуга>", paymentData.ServiceName },
                { "<Сумма>", $"{paymentData.Price:F2} руб." },
                { "<СтатусПлатежа>", paymentData.PaymentStatus },
                { "<СпособОплаты>", paymentData.PaymentMethod },
                { "<Дата>", paymentData.PaymentDate.ToString("dd.MM.yyyy HH:mm:ss") }
            };

            return ReplaceTags(inputFilePath, outputFilePath, replacements);
        }

        public static bool ReplaceTags(string inputFilePath, string outputFilePath, Dictionary<string, string> replacements)
        {
            Application wordApp = new Application();
            try
            {
                Document doc = wordApp.Documents.Open(inputFilePath, ReadOnly: true);
                Document newDoc = wordApp.Documents.Add();
                doc.Content.Copy();
                newDoc.Content.Paste();

                foreach (var replacement in replacements)
                {
                    newDoc.Content.Find.Execute(FindText: replacement.Key, ReplaceWith: replacement.Value, Replace: WdReplace.wdReplaceAll);
                }

                newDoc.SaveAs2(outputFilePath, WdSaveFormat.wdFormatPDF);
                newDoc.Close(SaveChanges: false);
                doc.Close(SaveChanges: false);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при формировании чека: " + ex.Message, "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                wordApp.Quit();
            }
        }
    }
}
