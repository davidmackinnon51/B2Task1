using System.Data;

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

        private void LoadNewFile_Click(object sender, EventArgs e)
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
    }
}