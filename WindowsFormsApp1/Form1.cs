using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace WindowsFormsApp1
{
    public partial class XMLProj : Form
    {
        private XmlNode testt;
        private XmlNode selectedNode;

        public XMLProj()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e) // Insert and creating file
        {
            if (!File.Exists("XMLProject.xml"))
            {
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                xmlWriterSettings.Indent = true;
                xmlWriterSettings.NewLineOnAttributes = true;
                using (XmlWriter xmlWriter = XmlWriter.Create("XMLProject.xml", xmlWriterSettings))
                {
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("Employees");

                    xmlWriter.WriteStartElement("Employee");
                    xmlWriter.WriteElementString("Name", textBox1.Text);
                    xmlWriter.WriteElementString("Address", textBox2.Text);
                    xmlWriter.WriteElementString("Telephone", textBox3.Text);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndDocument();
                    xmlWriter.Flush();
                    xmlWriter.Close();
                }
            }
            else
            {
                XDocument xDocument = XDocument.Load("XMLProject.xml");
                XElement root = xDocument.Element("Employees");
                IEnumerable<XElement> rows = root.Descendants("Employee");
                XElement firstRow = rows.First();
                firstRow.AddBeforeSelf(
                   new XElement("Employee",
                   new XElement("Name", textBox1.Text),
                   new XElement("Address", textBox2.Text),
                   new XElement("Telephone", textBox3.Text)
                   ));
                xDocument.Save("XMLProject.xml");
            }



        }

        private void button2_Click(object sender, EventArgs e) //Delete
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("XMLProject.xml");
            XmlNodeList nodes = doc.GetElementsByTagName("Employee");
            for(int i=0; i< nodes.Count; i++) 
            {
                XmlNode selectedName = nodes[i].SelectSingleNode("Name");
                if (selectedName.InnerText == textBox1.Text)
                {
                    nodes[i].ParentNode.RemoveChild(nodes[i]);
                }
                
            }
            doc.Save("XMLProject.xml");
        }

        private void button5_Click(object sender, EventArgs e) // Next
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("XMLProject.xml");
            XmlElement root = doc.DocumentElement;
            XmlNodeList nodes = root.SelectNodes("Employee");
            for (int i = 0; i < nodes.Count; i++)
            {
                XmlNode selectedName = nodes[i].SelectSingleNode("Name");
                if (selectedName.InnerText == textBox1.Text)
                {
                    selectedNode = selectedName.ParentNode.NextSibling;
                    textBox1.Text = selectedNode.ChildNodes[0].InnerText;
                    textBox2.Text = selectedNode.ChildNodes[1].InnerText;
                    textBox3.Text = selectedNode.ChildNodes[2].InnerText;
                    break;
                }
                
            }


        }

        private void button4_Click(object sender, EventArgs e) //Prev
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("XMLProject.xml");
            XmlElement root = doc.DocumentElement;
            XmlNodeList nodes = root.SelectNodes("Employee");
            for (int i = 0; i < nodes.Count; i++)
            {
                XmlNode selectedName = nodes[i].SelectSingleNode("Name");
                if (selectedName.InnerText == textBox1.Text)
                {
                    selectedNode = selectedName.ParentNode.PreviousSibling;
                    textBox1.Text = selectedNode.ChildNodes[0].InnerText;
                    textBox2.Text = selectedNode.ChildNodes[1].InnerText;
                    textBox3.Text = selectedNode.ChildNodes[2].InnerText;
                    break;
                }

            }
        }

        private void button3_Click(object sender, EventArgs e) //Search
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("XMLProject.xml");
            XmlElement root = doc.DocumentElement;
            XmlNodeList nodes = root.SelectNodes("Employee");
            for (int i = 0; i < nodes.Count; i++)
            {
                XmlNode selectedName = nodes[i].SelectSingleNode("Name");
                if (selectedName.InnerText == textBox1.Text)
                {
                    selectedNode = selectedName.ParentNode;
                    textBox1.Text = selectedNode.ChildNodes[0].InnerText;
                    textBox2.Text = selectedNode.ChildNodes[1].InnerText;
                    textBox3.Text = selectedNode.ChildNodes[2].InnerText;
                    break;
                }

            }
        }

        private void button6_Click(object sender, EventArgs e) // Update
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("XMLProject.xml");
            XmlElement root = doc.DocumentElement;
            XmlNodeList nodes = root.SelectNodes("Employee");
            for (int i = 0; i < nodes.Count; i++)
            {
                XmlNode selectedName = nodes[i].SelectSingleNode("Name");
                if (selectedName.InnerText == textBox1.Text)
                {
                    selectedNode = selectedName.ParentNode;
                    selectedNode.ChildNodes[0].InnerText = textBox1.Text;
                    selectedNode.ChildNodes[1].InnerText = textBox2.Text;
                    selectedNode.ChildNodes[2].InnerText = textBox3.Text;
                    break;
                }

            }
            doc.Save("XMLProject.xml");
        }
    }
}
