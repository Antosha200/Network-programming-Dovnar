using bntu.vsrpp.akutsak.lab2.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace bntu.vsrpp.akutsak.lab2
{
    public partial class ChartBuilder : Form
    {
        List<Rate> rates = new List<Rate>();
        List<Currency> currencies = new List<Currency>();
        List<string> rateNames = new List<string>();
        List<RateShort> rateDynamic;
        Chart chart = new Chart();
        Series series = new Series();
        List<DateTime> days = new List<DateTime>();
        List<decimal> rateDynamicValues = new List<decimal>();
        int currentRateId;
        string rateName;
        DateTime curDateTime;
        DateTime nextDateTime;
        List<Currency> foundCurrencies;
        List<Currency> foundCurrenciesChanged = new List<Currency>();

        public ChartBuilder(List<Rate>rates, List<Currency> currencies, List<string>rateNames)
        {
            InitializeComponent();

            dateTimePickerStart.MinDate = new DateTime(2016, 7, 1);
            dateTimePickerStart.MaxDate = DateTime.Today.Subtract(new TimeSpan(1, 0, 0, 0));
            dateTimePickerEnd.MinDate = new DateTime(2016, 7, 1);
            dateTimePickerEnd.MaxDate = DateTime.Today;

            this.rates = rates;
            this.currencies = currencies;
            this.rateNames = rateNames;

            rateDynamic = new List<RateShort>();

            cbCurrencies.Items.AddRange(rateNames.ToArray());

            cbCurrencies.SelectedIndex = 0;

            chart.Parent = this;
            chart.Location = new Point(30, 30);
            chart.Size = new Size(700, 350);
            chart.ChartAreas.Add(new ChartArea("Rates"));
            chart.ChartAreas[0].AxisY.Title = "Бел.руб";
            series.ChartType = SeriesChartType.Line;
        }

        private void DisplayResults()
        {
            chart.Series.Clear();
            series.Points.Clear();
            foreach (RateShort item in rateDynamic)
            {                
                days.Add(item.Date);
                rateDynamicValues.Add((decimal)item.Cur_OfficialRate);
                series.Points.AddXY(item.Date, item.Cur_OfficialRate);
            }
            chart.Series.Add(series);

            lblMin.Visible = true;
            lblMax.Visible = true;
            lblAvg.Visible = true;
            lblMin.Text = "Min = " + rateDynamic.Min(a => a.Cur_OfficialRate);
            lblMax.Text = "Max = " + rateDynamic.Max(a => a.Cur_OfficialRate);
            lblAvg.Text = "Avg = " + string.Format("{0:0.0000}", rateDynamic.Average(a => a.Cur_OfficialRate));
        }

        private void btnPlot_Click(object sender, EventArgs e)
        {
            btnPlot.Enabled = false;

            rateDynamic.Clear();

            rateName = cbCurrencies.SelectedItem.ToString();                        

            foundCurrencies = currencies.FindAll(a => a.Cur_QuotName.Equals(rateName));
            foundCurrenciesChanged = currencies.FindAll(a => a.Cur_QuotName.Equals(rateName));

            foreach (Currency item in foundCurrencies)
            {
                if (item.Cur_DateEnd < dateTimePickerStart.Value)
                    foundCurrenciesChanged.Remove(item);
            }
            
            if (rateDynamicValues.Count > 0)
                rateDynamicValues.Clear();

            curDateTime = dateTimePickerStart.Value;
            nextDateTime = dateTimePickerEnd.Value;

            currentRateId = foundCurrenciesChanged[0].Cur_ID;

            for(; ; )
            {                
                if (curDateTime.AddDays(365) < foundCurrenciesChanged.Find(a => a.
                        Cur_ID.Equals(currentRateId)).Cur_DateEnd)
                {

                    if (curDateTime.AddDays(365) < dateTimePickerEnd.Value)
                    {
                        nextDateTime = curDateTime.AddDays(365);
                    }

                    else
                    {
                        nextDateTime = dateTimePickerEnd.Value;
                    }
                }

                else
                    nextDateTime = foundCurrenciesChanged.Find(a => a.
                        Cur_ID.Equals(currentRateId)).Cur_DateEnd;

                rateDynamic.AddRange(HttpX.GetRateDynamic("https://www.nbrb.by/API/ExRates/Rates/Dynamics/" +
                    $"{currentRateId}?startDate={curDateTime.ToString("s")}" +
                    $"&endDate={nextDateTime.ToString("s")}").ToArray());

                System.Threading.Thread.Sleep(300);

                curDateTime = nextDateTime.AddDays(1);

                if (nextDateTime.Equals(dateTimePickerEnd.Value))
                    break;

                if (curDateTime > foundCurrenciesChanged.Find(a => a.
                    Cur_ID.Equals(currentRateId)).Cur_DateEnd)
                {
                    if (foundCurrenciesChanged.Any(a => a.Cur_DateStart.Equals(curDateTime)))
                        currentRateId = foundCurrenciesChanged.Find(a => a.Cur_DateStart.Equals(curDateTime)).
                            Cur_ID;
                    else
                        break;
                }
            }

            DisplayResults();

            btnPlot.Enabled = true;
        }
    }
}
