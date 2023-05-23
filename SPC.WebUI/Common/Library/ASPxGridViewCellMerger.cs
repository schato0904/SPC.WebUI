using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections.Generic;


namespace SPC.WebUI.Common
{
    public class ASPxGridViewCellMerger
    {
        string usedMergeCell;
        ASPxGridView grid;
        Dictionary<GridViewDataColumn, TableCell> mergedCells = new Dictionary<GridViewDataColumn, TableCell>();
        Dictionary<TableCell, int> cellRowSpans = new Dictionary<TableCell, int>();

        public ASPxGridViewCellMerger(ASPxGridView grid)
        {
            this.grid = grid;
            Grid.HtmlRowCreated += new ASPxGridViewTableRowEventHandler(grid_HtmlRowCreated);
            Grid.HtmlDataCellPrepared += new ASPxGridViewTableDataCellEventHandler(grid_HtmlDataCellPrepared);
        }

        public ASPxGridViewCellMerger(ASPxGridView grid, string usedMergeCell)
        {
            this.grid = grid;
            this.usedMergeCell = usedMergeCell;
            Grid.HtmlRowCreated += new ASPxGridViewTableRowEventHandler(grid_HtmlRowCreated);
            Grid.HtmlDataCellPrepared += new ASPxGridViewTableDataCellEventHandler(grid_HtmlDataCellPrepared);
        }

        public ASPxGridView Grid { get { return grid; } }
        void grid_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
			if (e.VisibleIndex < 0)
				return;
            //add the attribute that will be used to find which column the cell belongs to
            e.Cell.Attributes.Add("ci", e.DataColumn.VisibleIndex.ToString());

            if (cellRowSpans.ContainsKey(e.Cell))
            {
                e.Cell.RowSpan = cellRowSpans[e.Cell];
            }
        }
        void grid_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
			if (e.VisibleIndex < 0)
				return;

            if (Grid.GetRowLevel(e.VisibleIndex) != Grid.GroupCount) return;
            for (int i = e.Row.Cells.Count - 1; i >= 0; i--)
            {
                DevExpress.Web.Rendering.GridViewTableDataCell dataCell = e.Row.Cells[i] as DevExpress.Web.Rendering.GridViewTableDataCell;
                if (dataCell != null)
                {
                    if (!String.IsNullOrEmpty(this.usedMergeCell) && this.usedMergeCell.Contains(dataCell.DataColumn.FieldName))
                        MergeCells(dataCell.DataColumn, e.VisibleIndex, dataCell);
                }
            }
        }

        void MergeCells(GridViewDataColumn column, int visibleIndex, TableCell cell)
        {
            bool isNextTheSame = IsNextRowHasSameData(column, visibleIndex);
            if (isNextTheSame)
            {
                if (!mergedCells.ContainsKey(column))
                {
                    mergedCells[column] = cell;
                }
            }
            if (IsPrevRowHasSameData(column, visibleIndex))
            {
                if (mergedCells.ContainsKey(column))
                {
                    TableCell mergedCell = mergedCells[column];
                    if (!cellRowSpans.ContainsKey(mergedCell))
                    {
                        cellRowSpans[mergedCell] = 1;
                    }
                    cellRowSpans[mergedCell] = cellRowSpans[mergedCell] + 1;
                }
                ((TableRow)cell.Parent).Cells.Remove(cell);
            }
            if (!isNextTheSame)
            {
                mergedCells.Remove(column);
            }
        }

        bool IsNextRowHasSameData(GridViewDataColumn column, int visibleIndex)
        {
            //is it the last visible row
            if (visibleIndex >= Grid.VisibleRowCount - 1)
                return false;

            return IsSameData(column.FieldName, visibleIndex, visibleIndex + 1);
        }

        bool IsPrevRowHasSameData(GridViewDataColumn column, int visibleIndex)
        {
            ASPxGridView grid = column.Grid;
            //is it the first visible row
            if (visibleIndex <= Grid.VisibleStartIndex)
                return false;

            return IsSameData(column.FieldName, visibleIndex, visibleIndex - 1);
        }

        //bool IsSameData(string fieldName, int visibleIndex1, int visibleIndex2)
        //{
        //    // is it a group row?
        //    if (Grid.GetRowLevel(visibleIndex2) != Grid.GroupCount)
        //        return false;

        //    return object.Equals(Grid.GetRowValues(visibleIndex1, fieldName), Grid.GetRowValues(visibleIndex2, fieldName));
        //}

        bool IsSameData(string fieldName, int visibleIndex1, int visibleIndex2)
        {
            bool retunVal = false;
            // is it a group row?
            if (Grid.GetRowLevel(visibleIndex2) != Grid.GroupCount)
                return false;

            string[] arrusedMergeCell = usedMergeCell.Split('|');

            for (int i = 0; i < arrusedMergeCell.Length; i++)
            {
                try
                {
                    if (Equals(Grid.GetRowValues(visibleIndex1, arrusedMergeCell[i]), Grid.GetRowValues(visibleIndex2, arrusedMergeCell[i])))
                        retunVal = true;
                    else
                    {
                        retunVal = false;
                        break;
                    }
                }
                catch (Exception)
                {
                    return object.Equals(Grid.GetRowValues(visibleIndex1, fieldName), Grid.GetRowValues(visibleIndex2, fieldName));
                }
                
            }

            //return object.Equals(Grid.GetRowValues(visibleIndex1, fieldName), Grid.GetRowValues(visibleIndex2, fieldName));
            return retunVal;
        }
    }
}
                                                                                                                                                                                       