
using Microsoft.Reporting.WinForms;
using Microsoft.Reporting.WinForms.Internal.Soap.ReportingServices2005.Execution;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Srednee
{
    public class ReportOt
    {
        public void LoadReport(DataTable dataTable, String nameReport, ReportViewer reportViewer)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"..\..\{nameReport}.rdlc");
            reportViewer.LocalReport.DataSources.Clear();
            ReportDataSource source = new ReportDataSource("DataSet1", dataTable);
            reportViewer.LocalReport.ReportPath = path;
            reportViewer.LocalReport.DataSources.Add(source);
            reportViewer.RefreshReport();

        }
      

        
    }
}
