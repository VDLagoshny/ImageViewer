using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WFA_PictureShower.Properties;

namespace WFA_PictureShower
{
    /// <summary>
    /// The class realises methods of work with user's settings
    /// </summary>
    class CUserSettings
    {
        #region Parameters
        /// <summary>
        /// The parameter for storage user's settings
        /// </summary>
        private List<string> _settingList;

        /// <summary>
        /// The property of _settingList list
        /// </summary>
        public List<string> SettingList
        {
            set { _settingList = value; }
            get { return _settingList; }
        }
        #endregion 

        /// <summary>
        /// The constructor CUserSetting's class
        /// </summary>
        public CUserSettings()
        {
            SettingList = new List<string>();
        }

        /// <summary>
        /// The method of getting user's settings
        /// </summary>
        /// <returns>
        /// The result, bool type:
        /// true - output list contains data
        /// false - output list is empty
        /// </returns>
        private bool OutputSettings()
        {
            try
            {
                if (Settings.Default.SettingList.Count == 0)
                    return false;
                if (SettingList.Count != 0)
                    SettingList.Clear();

                foreach (var item in Settings.Default.SettingList)
                    SettingList.Add(item);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
            }
        }

        /// <summary>
        /// The method of saving user's settings
        /// </summary>
        /// <returns>
        /// The result, bool type:
        /// true - saving data 
        /// false - there are no data for saving
        /// </returns>
        private bool SaveSettings()
        {
            try
            {
                if (SettingList.Count == 0)
                    return false;
                if (Settings.Default.SettingList.Count != 0)
                    Settings.Default.SettingList.Clear();

                foreach (var item in SettingList)
                    Settings.Default.SettingList.Add(item);
                Settings.Default.Save();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
            }
        }

        /// <summary>
        /// The method launches:
        /// 1 - creation list of user's settings for using 
        /// 2 - saving paths in user's settings
        /// </summary>
        /// <param name="flag">Message about result</param>
        /// <returns>
        /// true - settings were setted
        /// false - settings were not setted
        /// </returns>
        public bool SetSettings(int flag)
        {
            bool _f = false;
            switch (flag)
            {
                case 1:
                    _f = OutputSettings();
                    break;
                case 2:
                    _f = SaveSettings();
                    break;
            }
            return _f;
        }
    }
}