using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WFA_PictureShower.Properties;

namespace WFA_PictureShower
{
	/// <summary>
	/// The class realises methods of work with user's settings
	/// </summary>
	public class CUserSettings
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
        private void OutputSettings()
        {
            try
            {
                if (SettingList.Count != 0)
                    SettingList.Clear();

                foreach (var item in Settings.Default.SettingList)
                    SettingList.Add(item);
            }
            catch (Exception ex)
            {
				MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void SaveSettings()
        {
            try
            {
                if (Settings.Default.SettingList.Count != 0)
                    Settings.Default.SettingList.Clear();

                foreach (var item in SettingList)
                    Settings.Default.SettingList.Add(item);
                Settings.Default.Save();
            }
            catch (Exception ex)
            {
				MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// The method launches:
        /// 1 - creation list of user's settings for using 
        /// 2 - saving paths in user's settings
        /// </summary>
        /// <param name="flag">Message about result</param>
        public void SetSettings(int flag)
        {
            switch (flag)
            {
                case 1:
                    OutputSettings();
                    break;
                case 2:
                    SaveSettings();
                    break;
            }
        }
    }
}