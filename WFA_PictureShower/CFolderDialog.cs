using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFA_PictureShower
{
	/// <summary>
	/// The class for working with folders
	/// </summary>
    class CFolderDialog
    {
        #region Parameters
        /// <summary>
        /// The initialition of class FolderBrowserDialog's instance
        /// </summary>
        private FolderBrowserDialog _fbd;
        //private OpenFileDialog _ofd;
        //private CSafety _cS;
        #endregion

        /// <summary>
        /// The class's constructor
        /// </summary>
        public CFolderDialog() { }

        /// <summary>
        /// The method for selecting folders
        /// </summary>
        /// <returns>
        /// The result, string type:
        /// The selected folder's path
        /// </returns>
        public string ChoosingFolder()
        {
            try
            {
                using (_fbd = new FolderBrowserDialog())
                {
                    _fbd.Description = "Please, choose folder:";
                    if (_fbd.ShowDialog() == DialogResult.OK)
                        return String.Format("{0}", _fbd.SelectedPath);
                    return "";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return "";
            }
        }
    }
}