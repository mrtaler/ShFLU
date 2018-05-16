using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

namespace ShFLY.SMGS
{
    using System.Collections.ObjectModel;
    using System.IO;

    using OfficeOpenXml;

    using ShFLY.Base;
    using ShFLY.DataBase.DAL.Implemtaations;
    using ShFLY.DataBase.DAL.Interfaces;
    using ShFLY.DataBase.Models;

    public class AllSmgsViewModel : ViewModelBase
    {
        private IUnitOfWork context;
        public ObservableCollection<SmgsNakl> AllSmgsNakl { get; set; }
        private SmgsNakl selectSmgs;
        public SmgsNakl SelectSmgs
        {
            get => this.selectSmgs;
            set
            {
                if (this.selectSmgs != value)
                {
                    this.selectSmgs = value;
                    this.NotifyPropertyChanged("AllWagInSmgs");
                    this.NotifyPropertyChanged("SelectSmgs");
                }
            }
        }
        public ObservableCollection<WagInSmgs> AllWagInSmgs
        {
            get
            {
                return this.SelectSmgs != null ?
                      new ObservableCollection<WagInSmgs>(this.SelectSmgs.WagInSmgses) : null;
            }
        }

        public ViewModelCommand EditCommand { get; set; }
        public ViewModelCommand CalcWeigthCommand { get; set; }
        public ViewModelCommand CreateXLSCommand { get; set; }
        public ViewModelCommand SaveBruttoMatrixCommand { get; set; }

        public AllSmgsViewModel()
        {
            this.context = new UnitOfWork();
            this.AllSmgsNakl = new ObservableCollection<SmgsNakl>
                (this.context.SmgsNaklRepository.GetAll()
                .OrderBy(p => p.Smgsdat));

            this.EditCommand =
                new ViewModelCommand(this.Edit, true);
            this.CalcWeigthCommand =
                new ViewModelCommand(this.CalcWeigth, true);
            this.CreateXLSCommand =
                new ViewModelCommand(this.CreateXLS, true);
            this.SaveBruttoMatrixCommand =
                new ViewModelCommand(this.SaveBruttoMatrix, true);
        }

        private void Edit(object param)
        {
            FindMatrixView win = new FindMatrixView(
                (WagInSmgs)param, this.context);
            win.ShowDialog();
        }
        private void SaveBruttoMatrix(object param)
        {

            if (param is SmgsNakl smgs)
            {
                string newFile = $"d:\\cd\\{smgs.Smgsdat.ToString("yyyy-MM-dd")}_{smgs.Smgs}.txt";

                using (StreamWriter output = new StreamWriter(
                    Path.GetFullPath(newFile)))
                {
                    foreach (var item in new MatrixForm(smgs, MatrixType.Brutto).SmgsToMatrix().Split('\n'))
                    {
                        output.WriteLine(item);
                    }
                   
                }
            }
        }
        private void Delete(object param)
        {
            this.AllSmgsNakl.Remove((SmgsNakl)param);
            this.context.SmgsNaklRepository.Delete((SmgsNakl)param);
            this.context.SaveChanges();
        }

        private void CalcWeigth(object param)
        {
            if (param is SmgsNakl smgs)
            {
                if (smgs.etsngc != "421034" && !smgs.IsWeigherCalc)
                {
                    List<Weigher> weiList = new List<Weigher>();

                    foreach (var wagInSmgses in smgs.WagInSmgses)
                    {
                        if (wagInSmgses.Weight == "0")
                        {
                            wagInSmgses.Weight = "1";
                        }
                        var wei = new Weigher(wagInSmgses);
                        wei.GetDiff();
                        wei.wagInSmgs.IsWeigherCalc = true;

                        context.WeigherRepository.Create(wei);
                    }
                    smgs.IsWeigherCalc = true;
                    context.SmgsNaklRepository.Update(smgs);
                    context.SaveChanges();
                }
            }
        }


        private void CreateXLS(object param)
        {
            foreach (var smgs in AllSmgsNakl)
            {
                if (smgs.etsngc != "421034")
                {
                    // var smgs = (SmgsNakl)param;

                    FileInfo newFile = new FileInfo($"d:\\cd\\{smgs.Smgsdat.ToString("yyyy-MM-dd")}_{smgs.Smgs}.xlsx");
                    if (newFile.Exists)
                    {
                        newFile.Delete(); // ensures we create a new workbook
                                          //  newFile = new FileInfo(@"D:\sample1.xlsx");
                    }



                    using (ExcelPackage package = new ExcelPackage(newFile))
                    {

                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add($"08");

                        worksheet.Cells[1, 1].Value = "№ пп";
                        worksheet.Cells[1, 2].Value = "№ накладной";
                        worksheet.Cells[1, 3].Value = "Дата прихода на станцию";

                        worksheet.Cells[1, 4].Value = "№№ вагона";

                        worksheet.Cells[1, 5].Value = "Вес (кг) Нетто";
                        worksheet.Cells[1, 6].Value = "Вес (кг) Тарa";
                        worksheet.Cells[1, 7].Value = "Вес (кг) Брутто";

                        worksheet.Cells[1, 8].Value = "Тара с весов";
                        worksheet.Cells[1, 9].Value = "Брутто с весов";
                        worksheet.Cells[1, 10].Value = "Нетто с весов";

                        worksheet.Cells[1, 11].Value = "Разность Нетто";

                        int i = 2;

                        // var name = smgs.Smgs.ToString();

                        // Add some items...
                        List<Weigher> weiList = new List<Weigher>();
                        foreach (var wagInSmgses in smgs.WagInSmgses)
                        {
                            if (wagInSmgses.Weight == "0")
                            {
                                wagInSmgses.Weight = "1";
                            }
                            var wei = new Weigher(wagInSmgses);
                            wei.GetDiff();

                            worksheet.Cells[i, 1].Value = i;
                            worksheet.Cells[i, 2].Value = smgs.Smgs;
                            worksheet.Cells[i, 3].Value = smgs.Smgsdat.ToString();

                            worksheet.Cells[i, 4].Value = wagInSmgses.Wagon.Nwag;

                            worksheet.Cells[i, 5].Value = wagInSmgses.Weight;
                            worksheet.Cells[i, 6].Value = wagInSmgses.Tarapr;
                            worksheet.Cells[i, 7].Value = wagInSmgses.Weightb;

                            worksheet.Cells[i, 8].Value = wei.WeigherTara;
                            worksheet.Cells[i, 9].Value = wei.WeigherBrutto;
                            worksheet.Cells[i, 10].Value = wei.WeigherDiff;

                            worksheet.Cells[i, 11].Value = wei.WeigherDiffNotPer;
                            worksheet.Cells[i, 12].Value = wei.WeigherDiffPer;
                            weiList.Add(wei);

                            i++;
                        }

                        worksheet.Cells[i, 1].Value = "ИТОГО";
                        worksheet.Cells[i, 11].Value = weiList.Sum(p => p.WeigherDiffNotPer);
                        worksheet.Cells[i, 12].Value =
                            100 - weiList.Sum(p => p.WeigherDiff) * 100 / weiList.Sum(p => p.neettoSmgs);
                        i++;

                        // Add a formula for the value-column

                        // worksheet.Cells["E2:E4"].Formula = "C2*D2";

                        ////Ok now format the values;
                        // using (var range = worksheet.Cells[1, 1, 1, 5])
                        // {
                        // range.Style.Font.Bold = true;
                        // range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        // //range.Style.Fill.BackgroundColor.SetColor(System.Windows.Media.Color.DarkBlue);
                        // // range.Style.Font.Color.SetColor(Color.White);
                        // }

                        // worksheet.Cells["A5:E5"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        // worksheet.Cells["A5:E5"].Style.Font.Bold = true;

                        // worksheet.Cells[5, 3, 5, 5].Formula = string.Format("SUBTOTAL(9,{0})", new ExcelAddress(2, 3, 4, 3).Address);
                        // worksheet.Cells["C2:C5"].Style.Numberformat.Format = "#,##0";
                        // worksheet.Cells["D2:E5"].Style.Numberformat.Format = "#,##0.00";

                        ////Create an autofilter for the range
                        // worksheet.Cells["A1:E4"].AutoFilter = true;

                        // worksheet.Cells["A1:E5"].AutoFitColumns(0);

                        // lets set the header text 
                        worksheet.HeaderFooter.OddHeader.CenteredText = "&24&U&\"Arial,Regular Bold\" Weigth";

                        // add the page number to the footer plus the total number of pages
                        worksheet.HeaderFooter.OddFooter.RightAlignedText = string.Format(
                            "Page {0} of {1}",
                            ExcelHeaderFooter.PageNumber,
                            ExcelHeaderFooter.NumberOfPages);

                        // add the sheet name to the footer
                        worksheet.HeaderFooter.OddFooter.CenteredText = ExcelHeaderFooter.SheetName;

                        // add the file path to the footer
                        worksheet.HeaderFooter.OddFooter.LeftAlignedText =
                            ExcelHeaderFooter.FilePath + ExcelHeaderFooter.FileName;

                        worksheet.PrinterSettings.RepeatRows = worksheet.Cells["1:2"];
                        worksheet.PrinterSettings.RepeatColumns = worksheet.Cells["A:G"];

                        // Change the sheet view to show it in page layout mode
                        worksheet.View.PageLayoutView = true;

                        // set some document properties
                        package.Workbook.Properties.Title = "XXX";
                        package.Workbook.Properties.Author = "Jan Källman";
                        package.Workbook.Properties.Comments =
                            "This sample demonstrates how to create an Excel 2007 workbook using EPPlus";

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
    }
}
