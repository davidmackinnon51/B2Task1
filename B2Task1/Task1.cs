using System.Data;
using System.Windows.Forms;

namespace B2Task1
{
    public partial class Task1 : Form
    {

        DataSet dataSet = new DataSet();

        public Task1()
        {
            InitializeComponent();            
        }

        private void SaveFile_Click(object sender, EventArgs e)
        {
            try
            {
                Stream myStream;
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "xml Files (*.xml)|*.*";
                saveFileDialog.Title = "Save XML File";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialog.OpenFile()) != null)
                    {
                        System.Xml.XmlTextWriter xmlWriter = new System.Xml.XmlTextWriter(myStream, System.Text.Encoding.Unicode);
                        dataSet.WriteXml(xmlWriter);
                        myStream.Close();


                        //if you don't include the extension its saved without one
                        //may be a better way to do this, but this works
                        if (!saveFileDialog.FileName.EndsWith(".xml"))
                        {
                            File.Move(saveFileDialog.FileName, saveFileDialog.FileName + ".xml");
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                ShowError("Error saving File, please retry", ex);
            }            
        }

        private void LoadNewFile_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();
                dataSet.Clear();
                OpenFileDialog loadXmlFile = new OpenFileDialog();
                loadXmlFile.Filter = "xml Files (*.xml)|*.*";

                if (loadXmlFile.ShowDialog() == DialogResult.OK)
                {
                    dataSet.ReadXml(loadXmlFile.FileName);
                    dataGridView1.DataSource = dataSet.Tables[0];
                }
            }
            catch(Exception ex)
            {
                ShowError("Error while loading File, please retry or check file to ensure it is valid",ex);
            } 
        }

        private void ShowError(string message,Exception ex)
        {
            MessageBox.Show(message + "\n\n" + ex.ToString(), "Error Loading File",  MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}