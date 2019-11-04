using System.Linq;
using System.Text.RegularExpressions;

namespace WFA_PictureShower
{
    /// <summary>
    /// Additional class realises methods for work with strings
    /// </summary>
    class CAddition
    {
        /// <summary>
        /// The method for cutting part of the path
        /// </summary>
        /// <param name="path">The path to the folder</param>
        /// <returns>
        /// The result of the method, string type:
        /// edited path
        /// </returns>
        public string StringReverse(string path)
        {
            string reversePath = "";
            string normalPath = "";

            foreach (var c in path.Reverse())
            {
                reversePath += c;
            }

            Regex rG = new Regex(@"\\");

            if (rG.IsMatch(reversePath))
            {          
                foreach (var c in FoldersDefinition1(reversePath, (rG.Match(reversePath).Index)).Reverse())
                {
                    normalPath += c;
                }
            }
            return normalPath;
        }

        /// <summary>
        /// The method of work folder's definition 
        /// </summary
        /// <param name="path">The path to the folder</param>
        /// <param name="firstIndex">The start index for cutting part of the path</param>
        /// <returns>
        /// The result of the method, string type
        /// jpg - the name of the folder
        /// exe - the name of the folder
        /// </returns>
        public string FoldersDefinition1(string path, int firstIndex)
        {
            return path.Substring(firstIndex);
        }

        /// <summary>
        /// The method of work folder's definition 
        /// </summary>
        /// <param name="path">The path to the folder</param>
        /// <param name="firstIndex">The start index for cutting part of the path</param>
        /// <returns>
        /// The result of the method, string type
        /// jpg - the name of the folder
        /// exe - the name of the folder
        /// </returns>
        public string FoldersDefinition2(string path, int firstIndex, int endIndex)
        {
            return path.Substring(firstIndex, endIndex);
        }            
    }
}