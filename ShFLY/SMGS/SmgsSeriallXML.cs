using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

using ShFLY.DataBase.DAL.Interfaces;
using ShFLY.DataBase.Models;
using ShFLY.SMGS.Specifications;

using TicketSaleCore.Models;

namespace ShFLY.SMGS
{
    class SmgsSeriallXML
    {
        private const string V = "mm dd yyyy";
        private IRepository<SmgsNakl> SmgsNaklRepository;
        private IRepository<Wagon> WagonRepository;
        private IUnitOfWork unitOfWork;

        public SmgsSeriallXML(string filePatch, IUnitOfWork context)
        {
            this.unitOfWork = context;
            this.SmgsNaklRepository = this.unitOfWork.SmgsNaklRepository;
            this.WagonRepository = this.unitOfWork.WagonRepository;

            string[] stringFromFile = File.ReadAllLines(filePatch);
            StringBuilder strResult = new StringBuilder();
            foreach (var itemStringFromFile in stringFromFile)
            {
                strResult.Append(itemStringFromFile);
                strResult.Append('\n');
            }

            string commentPattern3 = @"\<\?xml(\w|\W)*</doc>";
            var reg = Regex.Match(strResult.ToString(), commentPattern3).ToString();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(reg);

            XDocument xdoc = XDocument.Parse(doc.OuterXml);
            var atr = xdoc.Root.Attribute("name");
            if (atr.Value.ToLower() == "nakl")
            {
                SmgsNakl smgsNak = null;

                foreach (XElement smgsXElement in xdoc.Root.Elements("table"))
                {
                    if (smgsXElement.FirstAttribute.Value == "nakl")
                    {
                        // smgsNak.Smgs = Convert.ToInt32(smgsXElement.Element("data").Element("smgs").Value);
                        var smgs = Convert.ToInt32(smgsXElement.Element("data").Element("smgs").Value);
                        var smgsNumSpec = new FindSmgsBySmgsNum(smgs);


                        smgsNak = this.SmgsNaklRepository.GetOne(smgsNumSpec);
                        if (smgsNak == null)
                        {
                            smgsNak = new SmgsNakl();
                            smgsNak.Smgs = Convert.ToInt32(smgsXElement.Element("data").Element("smgs").Value);
                            var cul = new CultureInfo("ru-ru");
                            var strin = smgsXElement.Element("data").Element("smgsdat").Value;

                            DateTime parsedDate = DateTime.Parse(strin, cul);

                            smgsNak.Smgsdat = parsedDate;
                            smgsNak.mnet = smgsXElement.Element("data").Element("mnet").Value;
                            smgsNak.mbrt = smgsXElement.Element("data").Element("mbrt").Value;
                        }

                    }

                    if (smgsXElement.FirstAttribute.Value == "nakl_gruz")
                    {
                        smgsNak.Etsngn = smgsXElement.Element("data").Element("etsngn").Value;
                        smgsNak.gngc = smgsXElement.Element("data").Element("gngc").Value;
                        smgsNak.gngn = smgsXElement.Element("data").Element("gngn").Value;
                        smgsNak.etsngc = smgsXElement.Element("data").Element("etsngc").Value;
                    }

                    if (smgsXElement.FirstAttribute.Value == "nakl_vag")
                    {
                        foreach (var vagonItem in smgsXElement.Elements("data"))
                        {
                            int vagnum = Convert.ToInt32(vagonItem.Element("nwag").Value);

                            var vagnumSpec = new FindWagonByWagonNum(vagnum);
                            var insertWag = this.WagonRepository.GetOne(vagnumSpec);

                            if (insertWag == null)
                            {
                                Wagon wag = new Wagon
                                {
                                    Nwag = Convert.ToInt32(vagonItem.Element("nwag").Value),
                                    Ownerc = vagonItem.Element("ownerc").Value,
                                    Gp = vagonItem.Element("gp").Value,
                                    Tara = vagonItem.Element("tara").Value
                                };

                                this.WagonRepository.Create(wag);

                            }
                            else
                            {
                                insertWag.Nwag = Convert.ToInt32(vagonItem.Element("nwag").Value);
                                insertWag.Ownerc = vagonItem.Element("ownerc").Value;
                                insertWag.Gp = vagonItem.Element("gp").Value;
                                insertWag.Tara = vagonItem.Element("tara").Value;
                            }

                            if (smgsNak.SmgsId == 0)
                            {
                                WagInSmgs wgs = new WagInSmgs();
                                wgs.Wagon = this.WagonRepository.GetLocal().FirstOrDefault(p => p.Nwag == vagnum);
                                wgs.Tarapr = vagonItem.Element("tarapr").Value;
                                wgs.Weightb = vagonItem.Element("weightb").Value;
                                wgs.Weight = vagonItem.Element("weight").Value;
                                smgsNak.WagInSmgses.Add(wgs);
                            }
                            else
                            {
                                WagInSmgs wgs = smgsNak.WagInSmgses.First(p => p.Wagon.Nwag == vagnum);
                                wgs.Wagon = this.WagonRepository.GetLocal().FirstOrDefault(p => p.Nwag == vagnum);
                                wgs.Tarapr = vagonItem.Element("tarapr").Value;
                                wgs.Weightb = vagonItem.Element("weightb").Value;
                                wgs.Weight = vagonItem.Element("weight").Value;
                            }
                        }
                    }
                }

                if (smgsNak.SmgsId == 0)
                {
                    this.SmgsNaklRepository.Create(smgsNak);

                }
                this.unitOfWork.SaveChanges();

            }
            else
            {
                var t = atr.Value;
            }
        }
    }
}
