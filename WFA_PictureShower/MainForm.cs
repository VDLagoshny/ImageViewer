using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using WFA_PictureShower.Properties;
using System.Threading;

namespace WFA_PictureShower
{
	/// <summary>
	/// The MainForm class
	/// </summary>
	public partial class MainForm : Form
    {
        #region The parameters
        /// <summary>
        /// The initialition of class CSafety's instance 
        /// </summary>
        private CSafety _cS;

        /// <summary>
        /// The initialition of class CSyncContext's instance 
        /// </summary>
        private CSyncContext _cSC;

        /// <summary>
        /// The initialition of class CUserSettings's instance 
        /// </summary>
        private CUserSettings _cUS;

        /// <summary>
        /// The initialition of class ConfigForm's instance 
        /// </summary>
        private ConfigForm _configForm;

		/// <summary>
		/// The initialition of class ConfigForm's instance 
		/// </summary>
		private InformationForm _informationForm;

		/// <summary>
		/// The initialition of class SynchronizationContext's instance
		/// </summary>
		public SynchronizationContext _context;
        #endregion 

        /// <summary>
        /// Class Form1's constructor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            _cS = new CSafety();
            _cSC = new CSyncContext();
            _cUS = new CUserSettings();
            _context = SynchronizationContext.Current;
        }

        /// <summary>
        /// The method is launched when event Form1 was launched
        /// </summary>
        /// <param name="sender">Standard input parameters, type object</param>
        /// <param name="e">Standard input parameters, type EventArgs</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                if (!StartVerificationCheckings())
                {
					button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    MessageBox.Show(String.Format("Specified links are not correct. Please, check them and save right ones!{0}{1}{2}", Environment.NewLine, Environment.NewLine, _cS._error)
                                                            , "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
				_context = SynchronizationContext.Current;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }

        #region The buttons' managment methods
        /// <summary>
        /// The method is launched when event click for button1 "Start" was happened
        /// </summary>
        /// <param name="sender">Standard input parameters, type object</param>
        /// <param name="e">Standard input parameters, type EventArgs</param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (_cS == null)
                    button1.Enabled = false;

                pictureBox1.BackgroundImage = null;
                foreach (var item in _cS.ExeList)
                {
                    System.Diagnostics.Process.Start(item);
                }
                button1.Enabled = false;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }

        /// <summary>
        /// The method is launched when event click for button2 "Left pointer" was happened
        /// </summary>
        /// <param name="sender">Standard input parameters, type object</param>
        /// <param name="e">Standard input parameters, type EventArgs</param>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (_cS == null)
                    button2.Enabled = false;

                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
				int _position = 0;

				if (_cS.JpgList.IndexOf(pictureBox1.ImageLocation) == -1)
					_position = 0;
				else if(_cS.JpgList.IndexOf(pictureBox1.ImageLocation) == 0)
					_position = (_cS.JpgList.Count - 1);
				else _position = _cS.JpgList.IndexOf(pictureBox1.ImageLocation) - 1;

				pictureBox1.Load(_cS.JpgList.ElementAt(_position));
			}
			catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }

        /// <summary>
        /// The method is launched when event click for button3 "Right pointer" was happened
        /// </summary>
        /// <param name="sender">Standard input parameters, type object</param>
        /// <param name="e">Standard input parameters, type EventArgs</param>
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (_cS == null)
                    button3.Enabled = false;

                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
				int _position = 0;

				if (_cS.JpgList.IndexOf(pictureBox1.ImageLocation) == -1)
					_position = 0;
				else if (_cS.JpgList.IndexOf(pictureBox1.ImageLocation) == (_cS.JpgList.Count - 1))
					_position = 0;
				else _position = _cS.JpgList.IndexOf(pictureBox1.ImageLocation) + 1;

				pictureBox1.Load(_cS.JpgList.ElementAt(_position));
			}
			catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }
        #endregion The buttons' managment methods

        #region The menu's buttons methods
        /// <summary>
        /// The method is launched by event click of menu point "File - Quit"
        /// and closes the application's form
        /// </summary>
        /// <param name="sender">Standard input parameters, type object</param>
        /// <param name="e">Standard input parameters, type EventArgs</param>
        private void FileQuit_Click(object sender, EventArgs e)
        {
            this.Close();
            Dispose();
        }

        /// <summary>
        /// The method is launched by event click of menu point "Help - About program"
        /// </summary>
        /// <param name="sender">Standard input parameters, type object</param>
        /// <param name="e">Standard input parameters, type EventArgs</param>
        private void AboutProgramInfo_Click(object sender, EventArgs e)
        {
			_informationForm = new InformationForm();
			_informationForm.Show();
		}

        /// <summary>
        /// The method is launched for initializing class ConfigForm's instance
        /// and passed class SynchronizationContext's instance to ConfigForm
        /// </summary>
        /// <param name="sender">Standard input parameters, type object</param>
        /// <param name="e">Standard input parameters, type EventArgs</param>
        private void SettingConfig_Click(object sender, EventArgs e)
        {
            _configForm = new ConfigForm();
            _cSC.SyncContextPassing(_configForm.PassingSyncContext, this);
            _configForm.Show();
        }
        #endregion The menu's buttons methods

        /// <summary>
        /// The method is launched for checking user's settings 
        /// and verification saved folders' paths
        /// </summary>
        /// <returns>
        /// The result, type bool:
        /// true - checkings passed
        /// false - checkings didn't pass
        /// </returns>
        private bool StartVerificationCheckings()
        {
            if (!_cUS.SetSettings(1))
                return false;

            foreach (var path in _cUS.SettingList)
            {
                if (!_cS.CheckingFoldersVerification(path))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// The method of passing SynchronizationContext to ConfigForm
        /// </summary>
        /// <param name="sender">The instance of object ConfigForm</param>
        public void ReturningSyncContext(object sender)
        {
            _configForm = sender as ConfigForm;
            if (_configForm._cS != null)
			{
				_cS = _configForm._cS;
			}
        }
    }
}