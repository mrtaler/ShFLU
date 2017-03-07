using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Text.RegularExpressions;
using ShFLU.DataBase.Table;
using ShFLU.DataBase;

namespace ShFLU.SMGS
{
    class SmgsSeriallXML
    {
        public SmgsSeriallXML(string filePatch)
        {
            ShFluContext context = new ShFluContext();

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
            SmgsNakl smgsNak = new SmgsNakl();
            foreach (XElement smgsXElement in xdoc.Root.Elements("table"))
            {
                if (smgsXElement.FirstAttribute.Value == "nakl")
                {
                    //smgsNak.Smgs = Convert.ToInt32(smgsXElement.Element("data").Element("smgs").Value);
                    var smgs = Convert.ToInt32(smgsXElement.Element("data").Element("smgs").Value);
                    
                    var insertSmgs = context.SmgsNaklDbSet.FirstOrDefault(p => p.Smgs == smgs);
                    if (insertSmgs!=null)
                    {
                        smgsNak = context.SmgsNaklDbSet.FirstOrDefault(p => p.Smgs == smgs);
                    }
                    else
                    {
                        smgsNak.Smgs = Convert.ToInt32(smgsXElement.Element("data").Element("smgs").Value);
                        smgsNak.Smgsdat = Convert.ToDateTime(smgsXElement.Element("data").Element("smgsdat").Value);
                    }
                  
                }
                if (smgsXElement.FirstAttribute.Value == "nakl_gruz")
                {
                    smgsNak.Etsngn = smgsXElement.Element("data").Element("etsngn").Value;
                }
                if (smgsXElement.FirstAttribute.Value == "nakl_vag")
                {
                    foreach (var vagonItem in smgsXElement.Elements("data"))
                    {
                        int vagnum = Convert.ToInt32(vagonItem.Element("nwag").Value);
                        var insertWag = context.WagonDbSet.FirstOrDefault(p => p.Nwag == vagnum);
                        
                        if (insertWag == null)
                        {
                            Wagon wag = new Wagon
                            {
                                Nwag = Convert.ToInt32(vagonItem.Element("nwag").Value),
                                Ownerc = vagonItem.Element("ownerc").Value,
                                Gp = vagonItem.Element("gp").Value,
                                Tara = vagonItem.Element("tara").Value
                            };
                            context.WagonDbSet.Add(wag);
                        }
                        else
                        {
                            insertWag.Nwag = Convert.ToInt32(vagonItem.Element("nwag").Value);
                            insertWag.Ownerc = vagonItem.Element("ownerc").Value;
                            insertWag.Gp =vagonItem.Element("gp").Value;
                            insertWag.Tara = vagonItem.Element("tara").Value;
                        }

                        WagInSmgs wgs = new WagInSmgs();
                        wgs.WagonSmgs = context.WagonDbSet.Local.FirstOrDefault(p => p.Nwag == vagnum);
                        wgs.Tarapr = vagonItem.Element("tarapr").Value;
                        wgs.Weightb =vagonItem.Element("weightb").Value;
                        smgsNak.WagInSmgses.Add(wgs);
                    }
                }
            }
            context.SmgsNaklDbSet.Add(smgsNak);
            context.SaveChanges();
        }
    }
}
