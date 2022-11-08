using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using bntu.vsrpp.akutsak.lab2.Extensions;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bntu.vsrpp.akutsak.lab2
{
    public partial class CurrenciesParser : Form
    {        
        List<Currency> currencies = new List<Currency>();

        List<Rate> rates = new List<Rate>();
        List<Rate> ratesDaily = new List<Rate>();

        HttpX httpX = new HttpX();

        DateTime date = new DateTime();

        Rate currentRate = new Rate();
        Rate targetRate = new Rate();

        List<string> rateNames = new List<string>();

        public CurrenciesParser()
        {
            InitializeComponent();
            date = DateTime.Today;
            tbValueInput.Text = "1";
            tbValueOutput.Text = "1";
            dateTimePicker.MaxDate = DateTime.Today;
            dateTimePicker.MinDate = new DateTime(2016, 7, 1);
        }

        private void ClearData()
        {
            if (currencies.Count > 0)
                currencies.Clear();

            if (rates.Count > 0)
                rates.Clear();

            if (rateNames.Count > 0)
                rateNames.Clear();_
        }

        private void AddDataToComboboxes()
        {
            foreach (Currency item in this.currencies)
            {
                if (rates.Any(a => a.Cur_ID.Equals(item.Cur_ID)))
                {
                    cbCurrencyInput.Items.Add(item.Cur_Name);
                    cbCurrencyOutput.Items.Add(item.Cur_Name);
                }
            }

            cbCurrencyInput.Items.Add("Белорусский рубль");
            cbCurrencyOutput.Items.Add("Белорусский рубль");
        }

        private void SortDataInComboboxes()
        {
            foreach (object item in cbCurrencyInput.Items)
                rateNames.Add((string)item);
            rateNames.Sort();
            cbCurrencyInput.Items.Clear();
            cbCurrencyInput.Items.AddRange(rateNames.ToArray());
            rateNames.Clear();

            foreach (object item in cbCurrencyOutput.Items)
                rateNames.Add((string)item);
            rateNames.Sort();
            cbCurrencyOutput.Items.Clear();
            cbCurrencyOutput.Items.AddRange(rateNames.ToArray());
        }

        private void DoRequests()
        {
            currencies.AddRange(HttpX.GetCurrencies());

            System.Threading.Thread.Sleep(300);

            rates.AddRange(HttpX.GetRates(
                $"https://www.nbrb.by/api/exrates/rates?ondate={date.ToString("s")}" +
                "&periodicity=0"));

            ratesDaily.AddRange(rates.ToArray());

            System.Threading.Thread.Sleep(300);

            rates.AddRange(HttpX.GetRates(
                $"https://www.nbrb.by/api/exrates/rates?ondate={date.ToString("s")}" +
                "&periodicity=1"));

            System.Threading.Thread.Sleep(300);
        }

        private void btnGetCurrencies_Click(object sender, EventArgs e)
        {
            btnGetCurrencies.Enabled = false;
            btnResult.Enabled = false;
            btnSwitch.Enabled = false;
            var currentCurrency = "";
            var targetCurrency = "";
            if (cbCurrencyInput.Items.Count != 0)
                currentCurrency = cbCurrencyInput.SelectedItem.ToString();
            if (cbCurrencyOutput.Items.Count != 0)
                targetCurrency = cbCurrencyOutput.SelectedItem.ToString();

            ClearData();

            DoRequests();

            AddDataToComboboxes();

            SortDataInComboboxes();

            cbCurrencyInput.SelectedItem = currentCurrency;
            cbCurrencyOutput.SelectedItem = targetCurrency;

            MessageBox.Show("Данные получены", "Данные получены", MessageBoxButtons.OK, 
                MessageBoxIcon.Information);

            btnGetCurrencies.Enabled = true;
            btnResult.Enabled = true;
            btnSwitch.Enabled = true;
            btnGraph.Enabled = true;
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker.Value < DateTime.Today)
                date = dateTimePicker.Value;
        }

        private void cbCurrencyInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            string rateName;
            var curId = 0;
            List<Currency> foundCurrencies;

            if(!cbCurrencyInput.SelectedItem.Equals("Белорусский рубль"))
            {
                rateName = cbCurrencyInput.SelectedItem.ToString();

                foundCurrencies = currencies.FindAll(a => a.Cur_Name.Equals(rateName));
                foreach (Currency item in foundCurrencies)
                {
                    if (rates.Any(a => a.Cur_ID.Equals(item.Cur_ID)))
                    {
                        curId = item.Cur_ID;
                        break;
                    }                        
                }

                currentRate = rates.Find(a => a.Cur_ID.Equals(curId));
            }
        }

        private void cbCurrencyOutput_SelectedIndexChanged(object sender, EventArgs e)
        {
            string rateName;
            var curId = 0;
            List<Currency> foundCurrencies;

            if (!cbCurrencyOutput.SelectedItem.Equals("Белорусский рубль"))
            {
                rateName = cbCurrencyOutput.SelectedItem.ToString();

                foundCurrencies = currencies.FindAll(a => a.Cur_Name.Equals(rateName));
                foreach (Currency item in foundCurrencies)
                {
                    if (rates.Any(a => a.Cur_ID.Equals(item.Cur_ID)))
                    {
                        curId = item.Cur_ID;
                        break;
                    }
                }

                targetRate = rates.Find(a => a.Cur_ID.Equals(curId));
                
            }
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
            decimal valueInput = 0;
            decimal curRateValue = 0;
            decimal targetRateValue = 0;
            decimal curRateScale = 0;
            decimal targetRateScale = 0;

            if (tbValueInput.Text.Length > 0)
                decimal.TryParse(tbValueInput.Text, out valueInput);

            if(!cbCurrencyInput.SelectedItem.Equals(null) && !cbCurrencyOutput.SelectedItem.Equals(null))
            {
                if (cbCurrencyInput.SelectedItem.Equals("Белорусский рубль")) 
                {
                    curRateValue = 1;
                    curRateScale = 1;
                }
                    
                else
                {
                    curRateValue = (decimal)currentRate.Cur_OfficialRate;
                    curRateScale = currentRate.Cur_Scale;
                }

                if (cbCurrencyOutput.SelectedItem.Equals("Белорусский рубль"))
                {
                    targetRateValue = 1;
                    targetRateScale = 1;
                }
                    
                else
                {
                    targetRateValue = (decimal)targetRate.Cur_OfficialRate;
                    targetRateScale = targetRate.Cur_Scale;
                } 

                tbValueOutput.Text = (valueInput * curRateValue * targetRateScale /
                    targetRateValue / curRateScale).ToString();
            }
        }

        private void btnSwitch_Click(object sender, EventArgs e)
        {
            string currency;
            if (!cbCurrencyInput.SelectedItem.Equals(null) && !cbCurrencyOutput.SelectedItem.Equals(null) &&
                cbCurrencyOutput.Items.Contains(cbCurrencyInput.SelectedItem) &&
                cbCurrencyInput.Items.Contains(cbCurrencyOutput.SelectedItem))
            {
                currency = cbCurrencyInput.Text;
                cbCurrencyInput.SelectedItem = cbCurrencyOutput.SelectedItem;
                cbCurrencyOutput.SelectedIndex = cbCurrencyOutput.Items.IndexOf(currency);                
            }
        }

        private void btnGraph_Click(object sender, EventArgs e)
        {
            rateNames.Clear();
            foreach (Rate rate in ratesDaily)
                rateNames.Add(currencies.Find(a => a.Cur_ID.Equals(rate.Cur_ID)).Cur_QuotName);
            rateNames.Remove("Белорусский рубль");
            ChartBuilder chartBuilder = new ChartBuilder(ratesDaily, currencies, rateNames);
            chartBuilder.Show();
        }
    }
}
