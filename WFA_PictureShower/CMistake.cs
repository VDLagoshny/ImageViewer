using System;
using System.Collections.Generic;
using System.Linq;

namespace WFA_PictureShower
{
	/// <summary>
	/// Class for work with mistake
	/// </summary>
	public class CMistake
	{
		#region Parameters
		public int _mistake;

		public List<string> _errorsList;
		#endregion Parameters

		/// <summary>
		/// Class CMistake's constructor
		/// </summary>
		public CMistake()
		{
			_errorsList = new List<string>()
			{
				String.Format("Указанная папка не существует, пожалуйста, проверьте путь к папке"),
				String.Format("В папке не найдены файлы с некорректным расширением"),
				String.Format("Найдены файлы с некорректным расширением"),
				String.Format("Расширение не определено"),
				String.Format("Указанная файл не существует, пожалуйста, проверьте путь к файлу")
			};
		}

		/// <summary>
		/// The method used for output exact mistake
		/// </summary>
		/// <returns></returns>
		public string MistakesMessage(int index)
		{
			string str = "";

			if (_errorsList.Count() > index)
				str = _errorsList.ElementAt(index);
			else
				str = "";

			return str;
		}
	}
}