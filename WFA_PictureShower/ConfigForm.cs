using System.Collections.Generic;
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
		/// The initialition of class CSyncContext's instance 
		/// </summary>
		private CSyncContext _cSC;

		/// <summary>
		/// The initialition of class SynchronizationContext's instance 
		/// </summary>
		private SynchronizationContext _context;

		/// <summary>
		/// The initialition of class MainForm's instance 
		/// </summary>
		private MainForm _mF;

		/// <summary>
		/// 
		/// </summary>
		public string _errorIndex;

		/// <summary>
		/// 
		/// </summary>
		private bool _jpgFlag;

		/// <summary>
		/// 
		/// </summary>
		public bool JpgFlag
		{
			get { return _jpgFlag; }
		}

		/// <summary>
		/// 
		/// </summary>
		private bool _exeFlag;

		/// <summary>
		/// 
		/// </summary>
		public bool ExeFlag
		{
			get { return _exeFlag; }
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
			_cSC = new CSyncContext();
			_mF = new MainForm();
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
				//Filling text field by data from user settings
				foreach (var item in _mF._cUS.SettingList)
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
				MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
				MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
				MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
				//Only saving filled data from text fields to user settings list
				//Adding links to user settings list
				List<string> _pathsList = new List<string>();
				_pathsList.Add(String.Format(@"jpg|{0}", textBox1.Text));
				_pathsList.Add(String.Format(@"exe|{0}", textBox2.Text));

				// Adding data to user settings list
				if (_mF._cUS.SettingList != null)
					_mF._cUS.SettingList.Clear();
				foreach (var path in _pathsList)
					_mF._cUS.SettingList.Add(path);

				// 3. Saving data
				_mF._cUS.SetSettings(2);

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
			// Don't save anything which were filled in text fields
			this.Close();
		}

		/// <summary>
		/// The method for returning SynchronizationContext to MainForm
		/// </summary>
		/// <param name="sender">Standard input parameters, type object</param>
		/// <param name="e">Standard input parameters, type EventArgs</param>
		private void ConfigForm_Closing(object sender, FormClosingEventArgs e)
		{
			// Checking the links which was saving 
			foreach (var path in _mF._cUS.SettingList)
			{
				// definition subject of the link
				switch (path.Substring(0, 4).ToLower())
				{
					case @"exe|":
						if (ExeFlag)
						{
							// Link's checking
							_mF._cS.CheckingFoldersVerification(path, out _errorIndex);

							ErrorListFilling(_mF._errorList, _errorIndex, @"exe|");
						}
						break;
					case @"jpg|":
						if (JpgFlag)
						{
							// Link's checking
							_mF._cS.CheckingFoldersVerification(path, out _errorIndex);

							ErrorListFilling(_mF._errorList, _errorIndex, @"jpg|");
						}
						break;
				}
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

		/// <summary>
		/// The method which fix flag about changing of textBox1
		/// </summary>
		/// <param name="sender">Standard input parameters, type object</param>
		/// <param name="e">Standard input parameters, type EventArgs</param>
		private void textBox1_ModifiedChanged(object sender, EventArgs e)
		{
			// Using flag
			_jpgFlag = true;
		}

		/// <summary>
		/// The method which fixes flag about changing of textBox1
		/// </summary>
		/// <param name="sender">Standard input parameters, type object</param>
		/// <param name="e">Standard input parameters, type EventArgs</param>
		private void textBox2_ModifiedChanged(object sender, EventArgs e)
		{
			// Using flag
			_exeFlag = true;
		}

		/// <summary>
		/// The method which fills error's list
		/// </summary>
		/// <param name="list">Error list</param>
		/// <param name="error">Error which was passed from checking by CSafety's methods class</param>
		/// <param name="kind">Link's type</param>
		private void ErrorListFilling(List<string> list, string error, string kind)
		{
			// was there a mistake at exe-link?
			var num = list.FindIndex(i => i.Substring(0, 4) == kind);
			if (num >= 0)
			{
				if (error.Length > 0)
				{
					// Replace previous mistake
					list.RemoveAt(num);
					list.Insert(num, String.Format($"{kind}{error}"));
				}
				else
					// Delete previous mistake
					list.RemoveAt(num);
			}
			else
			{
				if (error.Length > 0)
					// Add current mistake
					list.Add(String.Format($"{kind}{error}"));
			}
		}
	}
}