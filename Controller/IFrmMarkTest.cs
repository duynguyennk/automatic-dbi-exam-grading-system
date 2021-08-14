using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;

namespace Controller
{
    public interface IFrmMarkTest
    {
        string DbName { get; set; }

        string getFolderPath();
        void addStudentToGrid(string studentName,bool isCorrect, int i);
        void clearRowGrid();
        void clearColumnGrid(int totalColumn);
        void clearDbName();
        void setDataGridValue(int i, string columnName, string value);
        void addQuestionToGrid(string questionName, int i);
        void showMessageBox(string message);
        void setBtnReset(bool isEnabled);
        void setBtnSave(bool isEnabled);
        void setBtnExport(bool isEnabled);
        void setBtnMark(bool isEnabled);
        void clearScoreColumn();
    }
}
