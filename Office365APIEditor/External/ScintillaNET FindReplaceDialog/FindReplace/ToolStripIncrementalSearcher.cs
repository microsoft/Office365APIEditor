// Copyright(c) 2017 Steve Towner
// Licensed under the MIT license.
// https://github.com/Stumpii/ScintillaNET-FindReplaceDialog/blob/master/LICENSE

#region Using Directives

using ScintillaNET;
using ScintillaNET_FindReplaceDialog;
using System;
using System.Windows.Forms;

#endregion Using Directives


namespace ScintillaNET_FindReplaceDialog
{
    public class ToolStripIncrementalSearcher : ToolStripControlHost
    {
        #region Properties

        public Scintilla Scintilla
        {
            get { return Searcher.Scintilla; }
            set { Searcher.Scintilla = value; }
        }


        public IncrementalSearcher Searcher
        {
            get { return Control as IncrementalSearcher; }
        }

        #endregion Properties


        #region Constructors

        public ToolStripIncrementalSearcher() : base(new IncrementalSearcher(true))
        {
        }

        #endregion Constructors
    }
}
