using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SuperNiconicoCommentViewer.src.component.logic
{
    public class CommandCheck
    {
        /// <summary>
        /// コテハン用の正規表現
        /// </summary>
        private static readonly Regex _handleRegex = new Regex(@"(?<=(@|＠))[^ |　|@＠]+($|(?=([ |　|@＠])))");

        private Match _match;


        public bool HasHandle(string comment)
        {
            return (_match = _handleRegex.Match(comment)).Success;
        }

        public string GetMatchValue()
        {
            return _match.ToString();
        }
    }
}
