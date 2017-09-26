using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShFLY.Base;
using ShFLY.DataBase.DAL.Interfaces;
using System.Collections.ObjectModel;
using ShFLY.DataBase.Models;
using ShFLY.DataBase.DAL.Implemtaations;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Windows.Media;

namespace ShFLY.SMGS
{
    public class allSmgsViewModel : ViewModelBase
    {
        private IUnitOfWork context;
        public ObservableCollection<SmgsNakl> AllSmgsNakl { get; set; }
        private SmgsNakl selectSmgs;
        public SmgsNakl SelectSmgs
        {
            get
            {
                return selectSmgs;
            }
            set
            {
                if (selectSmgs != value)
                {
                    selectSmgs = value;
                    NotifyPropertyChanged("AllWagInSmgs");
                    NotifyPropertyChanged("SelectSmgs");
                }
            }
        }
        public ObservableCollection<WagInSmgs> AllWagInSmgs
        {
            get
            {
                if (SelectSmgs != null)
                {
                    return new ObservableCollection<WagInSmgs>(SelectSmgs.WagInSmgses);
                }
                else
                {
                    return null;
                }

            }
        }

        public ViewModelCommand EditCommand { get; set; }
        public ViewModelCommand DeleteCommand { get; set; }
        public ViewModelCommand CreateXLSCommand { get; set; }

        public allSmgsViewModel()
        {
            context = new UnitOfWork();
            AllSmgsNakl = new ObservableCollection<SmgsNakl>(context.SmgsNaklRepository.GetAll());
            EditCommand = new ViewModelCommand(Edit, true);
            DeleteCommand = new ViewModelCommand(Delete, true);
            CreateXLSCommand = new ViewModelCommand(CreateXLS, true);

        }
        private void Edit(object param)
        {
            FindMatrixView win = new FindMatrixView((WagInSmgs)param, context);
            win.ShowDialog();
            //MessageBox.Show(((WagInSmgs)param).Wagon.Nwag.ToString());
        }

        private void Delete(object param)
        {
            AllSmgsNakl.Remove((SmgsNakl)param);
            context.SmgsNaklRepository.Delete((SmgsNakl)param);
            context.SaveChanges();
        }
         private void CreateXLS(object param)
        {
            var smgs=(SmgsNakl)param;
            FileInfo newFile = new FileInfo(@"D:\sample1.xlsx");
            if (newFile.Exists)
            {
                newFile.Delete();  // ensures we create a new workbook
                newFile = new FileInfo(@"D:\sample1.xlsx");
            }
            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Inventory");

                worksheet.Cells[1, 1].Value = "№ пп";
                worksheet.Cells[1, 2].Value = "№ накладной, по которой прибыла цистерна";
                worksheet.Cells[1, 3].Value = "Дата прихода на станцию";
                worksheet.Cells[1, 4].Value = "Грузоотправитель";
                worksheet.Cells[1, 5].Value = "Дата выхода со станции";
                worksheet.Cells[1, 6].Value = "№№ вагона";
                worksheet.Cells[1, 7].Value = "Вес сырья (кг) нетто";
                worksheet.Cells[1, 8].Value = "№ акта приема-сдачи";
                worksheet.Cells[1, 9].Value = "Дата слива";

                worksheet.Cells[1, 10].Value = "Тара с весов";
                worksheet.Cells[1, 11].Value = "Брутто с весов";
                worksheet.Cells[1, 12].Value = "Нетто с весов";
                
                worksheet.Cells[1, 13].Value = "Разность Нетто";
                worksheet.Cells[1, 14].Value = "%";


                //Add some items...
                worksheet.Cells["A2"].Value = "12001";
                worksheet.Cells["B2"].Value = "Nails";
                worksheet.Cells["C2"].Value = 37;
                worksheet.Cells["D2"].Value = 3.99;

                worksheet.Cells["A3"].Value = "12002";
                worksheet.Cells["B3"].Value = "Hammer";
                worksheet.Cells["C3"].Value = 5;
                worksheet.Cells["D3"].Value = 12.10;

                worksheet.Cells["A4"].Value = "12003";
                worksheet.Cells["B4"].Value = "Saw";
                worksheet.Cells["C4"].Value = 12;
                worksheet.Cells["D4"].Value = 15.37;

                //Add a formula for the value-column
               
                // worksheet.Cells["E2:E4"].Formula = "C2*D2";

                ////Ok now format the values;
                //using (var range = worksheet.Cells[1, 1, 1, 5])
                //{
                //    range.Style.Font.Bold = true;
                //    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                //    //range.Style.Fill.BackgroundColor.SetColor(System.Windows.Media.Color.DarkBlue);
                //   // range.Style.Font.Color.SetColor(Color.White);
                //}

                //worksheet.Cells["A5:E5"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                //worksheet.Cells["A5:E5"].Style.Font.Bold = true;

                //worksheet.Cells[5, 3, 5, 5].Formula = string.Format("SUBTOTAL(9,{0})", new ExcelAddress(2, 3, 4, 3).Address);
                //worksheet.Cells["C2:C5"].Style.Numberformat.Format = "#,##0";
                //worksheet.Cells["D2:E5"].Style.Numberformat.Format = "#,##0.00";

                ////Create an autofilter for the range
                //worksheet.Cells["A1:E4"].AutoFilter = true;

                //worksheet.Cells["A1:E5"].AutoFitColumns(0);

                // lets set the header text 
                worksheet.HeaderFooter.OddHeader.CenteredText = "&24&U&\"Arial,Regular Bold\" Inventory";
                // add the page number to the footer plus the total number of pages
                worksheet.HeaderFooter.OddFooter.RightAlignedText =
                string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);
                // add the sheet name to the footer
                worksheet.HeaderFooter.OddFooter.CenteredText = ExcelHeaderFooter.SheetName;
                // add the file path to the footer
                worksheet.HeaderFooter.OddFooter.LeftAlignedText = ExcelHeaderFooter.FilePath + ExcelHeaderFooter.FileName;

                worksheet.PrinterSettings.RepeatRows = worksheet.Cells["1:2"];
                worksheet.PrinterSettings.RepeatColumns = worksheet.Cells["A:G"];

                // Change the sheet view to show it in page layout mode
                worksheet.View.PageLayoutView = true;

                // set some document properties
                package.Workbook.Properties.Title = "Invertory";
                package.Workbook.Properties.Author = "Jan Källman";
                package.Workbook.Properties.Comments = "This sample demonstrates how to create an Excel 2007 workbook using EPPlus";

                // set some extended property values
                package.Workbook.Properties.Company = "AdventureWorks Inc.";

                // set some custom property values
                package.Workbook.Properties.SetCustomPropertyValue("Checked by", "Jan Källman");
                package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", "EPPlus");

                // save our new workbook and we are done!
                package.Save();


            }


        }

        
    }
}
