using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SimpleWordPad
{
    public partial class MainForm : Form
    {
        private string appName = " | WordPad 2022";
        private bool isFileExist;
        private bool isFileChanged;
        private string currentFileName;

        public MainForm()
        {
            InitializeComponent();
        }

        private void aboutTextEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("\tSimpleWordPad\nCreated by LongNguyen (Finnie)\n\tIn 2022", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isFileChanged)
            {
                DialogResult result = MessageBox.Show("Do you wont to save your changes?", "File Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                switch (result)
                {
                    case DialogResult.Yes:
                        SaveFile();
                        ClearEditor();
                        toolStripStatusLabel.Text = "New Document is Created!";
                        break;
                    case DialogResult.No:
                        ClearEditor();
                        toolStripStatusLabel.Text = "New Document is Created!";
                        break;
                }

            }
            else
            {
                ClearEditor();
                toolStripStatusLabel.Text = "New Document is Created!";
            }


        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf";

            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (Path.GetExtension(openFileDialog.FileName) == ".txt")
                    RichTextBox.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.PlainText);

                if (Path.GetExtension(openFileDialog.FileName) == ".rtf")
                    RichTextBox.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.RichText);
                this.Text = Path.GetFileName(openFileDialog.FileName) + appName;

                isFileExist = true;
                isFileChanged = false;
                currentFileName = openFileDialog.FileName;

                undoToolStripMenuItem.Enabled = false;
                redoToolStripMenuItem.Enabled = false;

                toolStripStatusLabel.Text = "File is Opened!";

            }
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void SaveFile()
        {
            if (isFileExist)
            {
                if (Path.GetExtension(currentFileName) == ".txt")
                    RichTextBox.SaveFile(currentFileName, RichTextBoxStreamType.PlainText);

                if (Path.GetExtension(currentFileName) == ".rtf")
                    RichTextBox.SaveFile(currentFileName, RichTextBoxStreamType.RichText);

                isFileChanged = false;
                toolStripStatusLabel.Text = "File saved!";
            }
            else
            {
                if (isFileChanged)
                {
                    SaveAsFile();
                }
                else
                {
                    ClearEditor();
                }
            }
        }

        private void ClearEditor()
        {
            RichTextBox.Clear();
            this.Text = "Untitled" + appName;
            isFileChanged = false;
            isFileExist = false;
            currentFileName = "";
            undoToolStripMenuItem.Enabled = false;
            redoToolStripMenuItem.Enabled = false;
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAsFile();
        }

        private void SaveAsFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf";

            DialogResult result = saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (Path.GetExtension(saveFileDialog.FileName) == ".txt")
                    RichTextBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);

                if (Path.GetExtension(saveFileDialog.FileName) == ".rtf")
                    RichTextBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.RichText);
                this.Text = Path.GetFileName(saveFileDialog.FileName) + appName;

                isFileExist = true;
                isFileChanged = false;
                currentFileName = saveFileDialog.FileName;
                toolStripStatusLabel.Text = "File saved!";
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            isFileExist = false;
            isFileChanged = false;
            currentFileName = "";

            if (Control.IsKeyLocked(Keys.CapsLock))
            {
                capsToolStripStatusLabel.Text = "CAPS LOCK ON";
            }
            else
            {
                capsToolStripStatusLabel.Text = "CAPS LOCK OFF";
            }
        }

        private void RichTextBox_TextChanged(object sender, EventArgs e)
        {
            isFileChanged = true;
            undoToolStripMenuItem.Enabled = true;
            toolStripButtonUndo.Enabled = true;
            undoToolStripMenuItem1.Enabled = true;
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox.Undo();
            redoToolStripMenuItem.Enabled = true;
            toolStripButtonRedo.Enabled = true;
            redoToolStripMenuItem1.Enabled = true;
            undoToolStripMenuItem.Enabled = false;
            undoToolStripMenuItem1.Enabled = false;
            toolStripButtonUndo.Enabled = false;
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox.Redo();
            redoToolStripMenuItem.Enabled = false;
            redoToolStripMenuItem1.Enabled = false;
            toolStripButtonRedo.Enabled = false;
            undoToolStripMenuItem.Enabled = true;
            toolStripButtonUndo.Enabled = true;
            underlineToolStripMenuItem1.Enabled = true;
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox.SelectAll();
        }

        private void boltToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox.SelectionFont = new Font(RichTextBox.Font, FontStyle.Bold);
        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox.SelectionFont = new Font(RichTextBox.Font, FontStyle.Regular);
        }

        private void italicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox.SelectionFont = new Font(RichTextBox.Font, FontStyle.Italic);
        }

        private void underlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox.SelectionFont = new Font(RichTextBox.Font, FontStyle.Underline);
        }

        private void strikeoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox.SelectionFont = new Font(RichTextBox.Font, FontStyle.Strikeout);
        }

        private void formatFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog dialog = new FontDialog();
            dialog.ShowColor = true;

            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {

                RichTextBox.SelectionFont = dialog.Font;
                RichTextBox.SelectionColor = dialog.Color;
            }

        }

        private void textColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();

            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                RichTextBox.SelectionColor = dialog.Color;
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void RichTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (Control.IsKeyLocked(Keys.CapsLock))
            {
                capsToolStripStatusLabel.Text = "CAPS LOCK ON";
            }
            else
            {
                capsToolStripStatusLabel.Text = "CAPS LOCK OFF";
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RichTextBox.SelectionLength > 0)
                RichTextBox.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (RichTextBox.SelectionLength > 0)
                RichTextBox.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox.Paste();
        }
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.ShowDialog();
         }

  
    }
}
