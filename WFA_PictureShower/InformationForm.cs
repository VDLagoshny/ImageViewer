using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFA_PictureShower
{
	/// <summary>
	/// The InformationForm class
	/// </summary>
	public partial class InformationForm : Form
	{
		/// <summary>
		/// The class's construction
		/// </summary>
		public InformationForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// The method is launched when event click for button1 "Ok" was happened
		/// </summary>
        /// <param name="sender">Standard input parameters, type object</param>
        /// <param name="e">Standard input parameters, type EventArgs</param>
		private void Button1_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}