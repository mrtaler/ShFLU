using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Linq;

namespace ShFLU.SMGS
{
    class SmgsSeriallXML
    {
        public SmgsSeriallXML(string filePatch)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(filePatch);
            XDocument xdoc = XDocument.Load(filePatch);

            foreach (XElement smgsXElement in xdoc.Root.Elements("table"))
            {
                if (smgsXElement.FirstAttribute.Value == "nakl")
                {
                    MessageBox.Show(smgsXElement.Element("data").Element("smgs").Value);
                }
            }
        }
    }
}
