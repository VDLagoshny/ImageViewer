using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System;

namespace WFA_PictureShower
{
	/// <summary>
	/// The ConfigForm class
	/// </summary>
	public partial class ConfigForm : Form
    {
        #region Parameters
        /// <summary>
        /// The initialition of class CAddition's instance 
        /// </summary>
        private CAddition _cA;

        /// <summary>
        /// The initialition of class CFolderDialog's instance 
        /// </summary>
        private CFolderDialog _cFD;

        /// <summary>
        /// The initialition of class CSafety's instance  
        /// </summary>
        public CSafety _cS;

        /// <summary>
        /// The initialition of class CSyncContext's instance 
        /// </summary>
        private CSyncContext _cSC;

        /// <summary>
        /// The initialition of class CUserSettings's instance 
        /// </summary>
        private CUserSettings _cUS;

        /// <summary>
        /// The initialition of class SynchronizationContext's instance 
        /// </summary>
        private SynchronizationContext _context;

        /// <summary>
        /// The initialition of class MainForm's instance 
        /// </summary>
        private MainForm _mF;

		/// <summary>
		/// The parameter showed that pathes is selected
		/// </summary>
		private bool _selectedPathFlag;

		/// <summary>
		/// The property of _selectedPathFlag parameter
		/// </summary>
		public bool SelectedPathFlag
		{
			get { return _selectedPathFlag; }
		}

		#endregion

		/// <summary>
		/// The class's construction
		/// </summary>
		public ConfigForm()
        {
            InitializeComponent();

            _cA = new CAddition();
            _cFD = new CFolderDialog();
            _cS = new CSafety();
            _cSC = new CSyncContext();
            _cUS = new CUserSettings();
			_selectedPathFlag = false;
		}

        /// <summary>
        /// The method which is launched by ConfigForm loading event
        /// </summary>
        /// <param name="sender">Standard input parameters, type object</param>
        /// <param name="e">Standard input parameters, type EventArgs</param>
        public void ConfigForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (!_cUS.SetSettings(1))
                {
                    MessageBox.Show("System couldn't define clarified links at Settings.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                foreach (var item in _cUS.SettingList)
                {
                    switch (item.Substring(0, 4).ToLower())
                    {
                        case @"jpg|":
                            textBox1.Text = item.Substring(4);
                            break;
                        case @"exe|":
                            textBox2.Text = item.Substring(4);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// The method is launched when event click for button1 "Choosing of the path to picture's folder" was happened
        /// </summary>
        /// <param name="sender">Standard input parameters, type object</param>
        /// <param name="e">Standard input parameters, type EventArgs</param>
        private void button1JPG_Click(object sender, EventArgs e)
        {
            try
            {
                string _path = _cFD.ChoosingFolder();
                if (_path.Length > 0)
                    textBox1.Text = _path;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }

        /// <summary>
        /// The method is launched when event click for button2 "Choosing of the path to exe's folder" was happened
        /// </summary>
        /// <param name="sender">Standard input parameters, type object</param>
        /// <param name="e">Standard input parameters, type EventArgs</param>
        private void button2Exe_click(object sender, EventArgs e)
        {
            try
            {
                string _path = _cFD.ChoosingFolder();
                if (_path.Length > 0)
                    textBox2.Text = _path;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }

        /// <summary>
        /// The method is launched when event click for button3 "Accept Settings" was happened
        /// </summary>
        /// <param name="sender">Standard input parameters, type object</param>
        /// <param name="e">Standard input parameters, type EventArgs</param>
        private void button3Accept_Click(object sender, EventArgs e)
        {
            try
            {
                if ((textBox1.Text.Length < 3) | (textBox2.Text.Length < 3)) 
                {
                    MessageBox.Show("There are no any links for saving. Please, specify them and save right ones of press Cancel.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                List<string> _pathsList = new List<string>();
                _pathsList.Add(String.Format(@"jpg|{0}", textBox1.Text));
                _pathsList.Add(String.Format(@"exe|{0}", textBox2.Text));

                foreach (var path in _pathsList)
                {
                    if (!_cS.CheckingFoldersVerification(path))
                    {
                        MessageBox.Show(String.Format("Specified links are not correct. Please, check them and save right ones!{0}{1}{2}", Environment.NewLine, Environment.NewLine, _cS._error)
                                        , "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                if (_cUS.SettingList != null)
                    _cUS.SettingList.Clear();
                foreach (var path in _pathsList)
                {
                    _cUS.SettingList.Add(path);
                }

                /// 3. Сохранение путей в пользовательских настройках
                if (!_cUS.SetSettings(2))
                    MessageBox.Show("System could not save specified links. Please, check them and save right ones.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

				this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }

        /// <summary>
        /// The method is launched when event click for button4 "Cancel Settings" was happened
        /// </summary>
        /// <param name="sender">Standard input parameters, type object</param>
        /// <param name="e">Standard input parameters, type EventArgs</param>
        private void button4Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// The method for returning SynchronizationContext to MainForm
        /// </summary>
        /// <param name="sender">Standard input parameters, type object</param>
        /// <param name="e">Standard input parameters, type EventArgs</param>
        private void ConfigForm_Closing(object sender, FormClosingEventArgs e)
        {
			if ((textBox1.Text.Length < 3) | (textBox2.Text.Length < 3))
			{
				MessageBox.Show("There are no any links for saving. Please, specify them and save right ones or press Cancel.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			List<string> _pathsList = new List<string>();
			_pathsList.Add(String.Format(@"jpg|{0}", textBox1.Text));
			_pathsList.Add(String.Format(@"exe|{0}", textBox2.Text));

			foreach (var path in _pathsList)
			{
				if (!_cS.CheckingFoldersVerification(path))
				{
					MessageBox.Show(String.Format("Specified links are not correct. Please, check them and save right ones!{0}{1}{2}", Environment.NewLine, Environment.NewLine, _cS._error)
									, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
			}

			if (_cUS.SettingList != null)
				_cUS.SettingList.Clear();
			foreach (var path in _pathsList)
			{
				_cUS.SettingList.Add(path);
			}

			_cSC.SyncContextReturning(_mF.ReturningSyncContext, this, _context);
        }

        /// <summary>
        /// The method of getting SynchronizationContext by ConfigForm
        /// </summary>
        /// <param name="sender">The instance of object MainForm</param>
        public void PassingSyncContext(object sender)
        {
            _mF = sender as MainForm;
            _context = _mF._context;
        }
    }
}