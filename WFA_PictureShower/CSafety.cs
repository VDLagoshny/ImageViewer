using System;
using System.Collections.Generic;
using System.IO;

namespace WFA_PictureShower
{
    /// <summary>
    /// Class realises methods for correct work with folders and files
    /// </summary>
    public class CSafety
    {
        #region The parameters
        /// <summary>
        /// The initialition of class CAddition's instance
        /// </summary>
        private CAddition cA;

        /// <summary>
        /// The initialition of class FileInfo's instance
        /// </summary>
        private FileInfo _fileInfo;

        /// <summary>
        /// !!! List of all folder's files (use local parameter)
        /// </summary>
        private string[] _fullFilesPathList;

		/// <summary>
		/// The parameter used for outputing mistakes index
		/// </summary>
		public string _errorIndex;

		/// <summary>
		/// The property of _error list
		/// </summary>
		private List<string> _errorsList;

        /// <summary>
        /// The parameter used for storage of folders' path
        /// </summary>
        public List<string> _folderList;

        /// <summary>
        /// The parameter for storage list of correct extension (exe-files )
        /// </summary>
        private List<string> _exeList;

        /// <summary>
        /// The property of _exeList list
        /// </summary>
        public List<string> ExeList
        {
            private set { _exeList = value; }
            get { return _exeList; }
        }

        /// <summary>
        /// List for collecting files of correct extension
        /// </summary>
        private List<string> _jpgList;
        
        /// <summary>
        /// Property of the parameter for collecting files of correct extension (?)
        /// </summary>
        public List<string> JpgList
        {
            private set { _jpgList = value; }
            get { return _jpgList; }
        }
        #endregion 

        /// <summary>
        /// Class CSafety's constructor
        /// </summary>
        public CSafety()
        {
			_errorsList = new List<string>()
			{
				String.Format("Указанная папка не существует. Пожалуйста, проверьте путь к папке"),
				String.Format("В папке не найдены файлы с некорректным расширением"),
				String.Format("Найдены файлы с некорректным расширением"),
				String.Format("Расширение не определено"),
				String.Format("Указанный файл не существует. Пожалуйста, проверьте путь к файлу")
			};

            cA = new CAddition();
        }

        /// <summary>                   
        /// The method checks existing of the path to folder
        /// </summary>
        /// <param name="path">The path to folder, string type</param>
        /// <returns>
        /// The result of checking, bool type:
        /// true - the folder exists
        /// false - the folder doesn't exist
        /// </returns>
        private bool CheckingFoldersPath(string folderPath, out string _errorIndex)
        {
            try
            {
				_errorIndex = "";
                if (Directory.Exists(folderPath))
                    return true;
				_errorIndex = String.Format("{0}: {1}", _errorsList[0], folderPath);
                return false;
            }
            catch(Exception ex)
            {
				_errorIndex = String.Format("{0}", ex.Message);
                return false;
            }
        }

        /// <summary>                      
        /// The method checks existing of the path to file
        /// </summary>
        /// <param name="path">The path to file, string type</param>
        /// <returns>
        /// The result of checking, bool type:
        /// true - the file exists
        /// false - the file doesn't exist
        /// </returns>
        private bool CheckingFilesPath(string filePath, out string _errorIndex)
        {
            try
            {
				_errorIndex = "";
                if (File.Exists(filePath))
                    return true;
				_errorIndex = String.Format("{0}: {1}", _errorsList[0], filePath);
                return false;
            }
            catch(Exception ex)
            {
				_errorIndex = String.Format("{0}", ex.Message);
                return false;
            }
        }

        /// <summary>                      
        /// The method checks quantity of files in the folder
        /// </summary>
        /// <param name="path">The path to folder, string type</param>
        /// <returns>
        /// The result of checking, bool type
        /// true - the quantity of files is bigger than 0
        /// false - the quantity of files is equal 0
        /// </returns>
        private bool CheckingFilesQuantity(string folderPath, out string _errorIndex)
        {
            try
			{ 
				_errorIndex = "";
                _fullFilesPathList = Directory.GetFiles(folderPath);
                if (_fullFilesPathList.Length != 0)
                    return true;
				_errorIndex = String.Format("{0}: {1}", _errorsList[1], folderPath);
                return false;
            }
            catch (Exception ex)
			{
                _errorIndex = String.Format("{0}", ex.Message);
                return false;
            }
        }

        /// <summary>                   
        /// The method defines the file's extension
        /// </summary>
        /// <param name="path">The path to file</param>
        /// <returns>
        /// The result is file's extension, string type
        /// </returns>
        private string ExtensionDefinition(string filePath, out string _errorIndex)
        {
            try
			{ 
				_errorIndex = "";
				 _fileInfo = new FileInfo(filePath);
                return _fileInfo.Extension;
            }
            catch (Exception ex)
			{
                _errorIndex = String.Format("{0}", ex.Message);
                return "";
            }
        }

        /// <summary>
        /// The method of checking folder:
        /// - path to it
        /// - files in it
        /// </summary>
        /// <param name="folderPath">The path to the folder</param>
        /// <returns>
        /// The result of method, bool type
        /// true - all checkings are passed
        /// false - any checkings aren't passed 
        /// </returns>
        private bool CheckingFolder(string folderPath, out string _errorIndex)
        {
            try
            {
                if (CheckingFoldersPath(folderPath, out _errorIndex))
                    if (CheckingFilesQuantity(folderPath, out _errorIndex)) return true;
                return false;
            }
            catch (Exception ex)
			{
                _errorIndex = String.Format("{0}", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// The method of checking of file's existing 
        /// and defining file's extension
        /// </summary>
        /// <param name="filePath">The path to the file</param>
        /// <returns>
        /// The result of method is parameter, string type
        /// "extension" - file's extension 
        /// "" - checking isn't passed
        /// </returns>
        private string CheckingFile(string filePath, out string _errorIndex)
        {
            try
            {
                if (CheckingFilesPath(filePath, out _errorIndex))
                    return ExtensionDefinition(filePath, out _errorIndex);

                return "";
            }
            catch (Exception ex)
			{
                _errorIndex = String.Format("{0}", ex.Message);
                return "";
            }
        }

        /// <summary>
        /// The method of checking folders'/files' verification
        /// </summary>
        /// <param name="folderPath">The folder's path</param>
        /// <returns>
        /// The result of method is parameter, bool type
        /// true - if all checks were completed successful
        /// false - otherwise 
        /// The method creates list of files of corresponding types
        /// </returns>
        public bool CheckingFoldersVerification(string folderPath, out string _errorIndex)
        {
            try
            {
                /// Definition of files' types which existing we 
                /// need to check in the folder
                List<string> _correctFilesPathList = new List<string>();
                List<string> _typeList = new List<string>();

                switch (folderPath.Substring(0, 4).ToLower())
                {
                    case @"exe|":
                        _typeList.Add(".exe");
                        break;
                    case @"jpg|":
                        _typeList.Add(".jpg");
                        _typeList.Add(".jpeg");
                        _typeList.Add(".png");
                        _typeList.Add(".bmp");
                        break;
                }

                /// Checking folder and files in it
                if (!CheckingFolder(folderPath.Substring(4), out _errorIndex))
                    return false;
                foreach (var filePath in Directory.GetFiles(folderPath.Substring(4)))
                {
                    if (_typeList.IndexOf(CheckingFile(filePath, out _errorIndex).ToLower()) != -1)
                        _correctFilesPathList.Add(filePath);
                }

				// Checking
				if (_typeList != null)
					_typeList.Clear();

				/// Checking of existing of corresponding files' types
				if (_correctFilesPathList.Count == 0)
                {
					/// Output message about reason
					_errorIndex = String.Format("{0}: {1}", _errorsList[1], folderPath.Substring(4));
                    return false;
                }

                /// Creation lists for corresponding files' types
                switch (folderPath.Substring(0, 4).ToLower())
                {
                    case @"exe|":
                        ExeList = new List<string>(_correctFilesPathList.Count);

                        foreach (var path in _correctFilesPathList)
                        {
                            _exeList.Add(path);
                        }
                        ExeList = _exeList;
                        if (_correctFilesPathList != null)
                            _correctFilesPathList.Clear();
                        break;
                    case @"jpg|":
                        JpgList = new List<string>(_correctFilesPathList.Count);
                        foreach (var path in _correctFilesPathList)
                        {
                            _jpgList.Add(path);
                        }
                        JpgList = _jpgList;

                        if (_correctFilesPathList != null)
                            _correctFilesPathList.Clear();
                        break;
                }
                return true;
            }
            catch (Exception ex)
			{
                _errorIndex = String.Format("{0}", ex.Message);
                return false;
            }
        }
    }
}