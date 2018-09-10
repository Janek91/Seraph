using System;
using System.Windows;

namespace Seraph.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //m_tbName.Text = ""; // Trigger Text Changed
            // Fill the last column to match the resize of the form
            //m_dgvHandler.Columns[m_dgvHandler.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            // The grid is readonly except for the 1st column
            //foreach (DataGridViewColumn col in m_dgvHandler.Columns)
            //{
            //    col.ReadOnly = true;
            //}

            //DataGridViewColumn selectedCol = m_dgvHandler.Columns[0];
            //selectedCol.ReadOnly = false;
            //selectedCol.Width = 50;
            //// Add an event on the header cell to sort the grid
            //m_dgvHandler.ColumnHeaderMouseClick += m_dgvHandler_ColumnHeaderMouseClick;
            //// CheckBox event handler walkaround https://stackoverflow.com/questions/11843488/datagridview-checkbox-event
            //m_dgvHandler.CellContentClick += m_dgvHandler_CellContentClick;
            //m_dgvHandler.CellValueChanged += m_dgvHandler_CellValueChanged;
        }

        public MainViewModel ViewModel { get; private set; }

        private void m_bHandle_Click(object sender, EventArgs e)
        {
            m_bHandle.IsEnabled = false;
            ViewModel.Handle(m_tbName.Text.Trim());
            m_bHandle.IsEnabled = true;
        }

        private void m_tbName_TextChanged(object sender, EventArgs e)
        {
            // Disable handle button is there is no name
            m_bHandle.IsEnabled = !string.IsNullOrEmpty(m_tbName.Text.Trim());
        }

        // Called whenever the DataSource is changed
        //private void m_dgvHandler_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        //{
        //    // Remove the text header of the first column and replace it by a "(Un)Select All" checkbox
        //    m_cbHeader = new DatagridViewCheckBoxHeaderCell();
        //    m_cbHeader.Value = "";
        //    m_cbHeader.OnCheckBoxClicked += m_cbHeader_CheckBoxClicked;
        //    m_dgvHandler.Columns[0].HeaderCell = m_cbHeader;
        //    m_dgvHandler.ClearSelection();
        //}

        //public void m_cbHeader_CheckBoxClicked(bool bState)
        //{
        //    // Propagate the state of the checbox header to all checkbox in the column
        //    m_dgvHandler.ClearSelection();
        //    foreach (Handler handler in m_handlers)
        //    {
        //        handler.Selected = bState;
        //    }

        //    m_dgvHandler.Update();
        //    m_dgvHandler.Refresh();
        //    m_bClose.Select(); // Set the focus on Close button
        //}

        //private void m_dgvHandler_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.ColumnIndex == 0) // Only for selected column
        //    {
        //        m_dgvHandler.CommitEdit(DataGridViewDataErrorContexts.Commit);
        //    }
        //}


        //private void m_dgvHandler_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //{
        //    m_cbHeader.IsDirty = true; // Mix between selected and unselected
        //    m_dgvHandler.Invalidate(); // Trigger the Paint() event
        //}

        //private void m_dgvHandler_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    // Sort by column, depending which column was clicked
        //    if (e.ColumnIndex == 1)
        //    {
        //        if (m_direction == ListSortDirection.Ascending)
        //        {
        //            m_handlers = m_handlers.OrderBy(h => h.Process).ToList();
        //        }
        //        else
        //        {
        //            m_handlers = m_handlers.OrderByDescending(h => h.Process).ToList();
        //        }
        //    }
        //    else if (e.ColumnIndex == 2)
        //    {
        //        if (m_direction == ListSortDirection.Ascending)
        //        {
        //            m_handlers = m_handlers.OrderBy(h => h.Pid).ToList();
        //        }
        //        else
        //        {
        //            m_handlers = m_handlers.OrderByDescending(h => h.Pid).ToList();
        //        }
        //    }
        //    else if (e.ColumnIndex == 3)
        //    {
        //        if (m_direction == ListSortDirection.Ascending)
        //        {
        //            m_handlers = m_handlers.OrderBy(h => h.Type).ToList();
        //        }
        //        else
        //        {
        //            m_handlers = m_handlers.OrderByDescending(h => h.Pid).ToList();
        //        }
        //    }
        //    else if (e.ColumnIndex == 4)
        //    {
        //        if (m_direction == ListSortDirection.Ascending)
        //        {
        //            m_handlers = m_handlers.OrderBy(h => h.Handle).ToList();
        //        }
        //        else
        //        {
        //            m_handlers = m_handlers.OrderByDescending(h => h.Handle).ToList();
        //        }
        //    }
        //    else if (e.ColumnIndex == 5)
        //    {
        //        if (m_direction == ListSortDirection.Ascending)
        //        {
        //            m_handlers = m_handlers.OrderBy(h => h.Path).ToList();
        //        }
        //        else
        //        {
        //            m_handlers = m_handlers.OrderByDescending(h => h.Path).ToList();
        //        }
        //    }
        //    else
        //    {
        //        return;
        //    }

        //    m_direction = m_direction == ListSortDirection.Ascending
        //        ? ListSortDirection.Descending : ListSortDirection.Ascending;

        //    m_dgvHandler.DataSource = m_handlers;
        //}

        private void ButtonClose_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.Close();
            m_bHandle_Click(sender, e); // Callback Handle command to see if there is still handle remaining
        }

        private void MainWindow_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ViewModel = DataContext as MainViewModel;
        }
    }
}
