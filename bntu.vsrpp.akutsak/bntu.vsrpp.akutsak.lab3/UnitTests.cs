using System.Collections.Generic;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Linq;
using Bntu.Vsrpp.Akutsak.�ore;
using NUnit.Framework;

namespace bntu.vsrpp.akutsak.lab3
{
    /// <summary>
    /// ����� ��� ������� � Unit �������.
    /// </summary>
    public class UnitTests
    {
        /// <summary>
        /// ����� ��� ������������ ������ GetElementNames, ����� � ���� ���������� ���������� ������ �
        /// ��������� ������ ���������.
        /// </summary>
        [Test]
        public void GetElementNames_CorrectItems_ReturnTrue()
        {
            List<XElement> items = new List<XElement>();
            XElement xElementFirst = new XElement("FirstName");
            xElementFirst.Add(new XElement("firstElement"));
            xElementFirst.Add(new XElement("secondElement"));
            XElement xElementSecond = new XElement("SecondName");
            xElementSecond.Add(new XElement("firstElement"));
            xElementSecond.Add(new XElement("thirdElement"));
            items.AddRange(new XElement[] { xElementFirst, xElementSecond });

            XMLOperations xmlOperations = new XMLOperations();
            var expectedResult = new List<string>();
            expectedResult.AddRange(new string[] { "firstElement", "secondElement", "thirdElement" });
            Assert.AreEqual(expectedResult, xmlOperations.GetElementNames(items));
        }

        /// <summary>
        /// ����� ��� ������������ ������ GetElementNames, ����� � ���� ���������� ������������ ������ �
        /// ��������� �������� ���������.
        /// </summary>
        [Test]
        public void GetElementNames_IncorrectItems_ReturnFalse()
        {
            List<XElement> items = new List<XElement>();
            XElement xElementFirst = new XElement("FirstName");
            xElementFirst.Add(new XElement("firstElement"));
            xElementFirst.Add(new XElement("secondElement"));
            XElement xElementSecond = new XElement("SecondName");
            xElementSecond.Add(new XElement("firstElement"));
            xElementSecond.Add(new XElement("thirdElement"));
            xElementSecond.Add(new XElement("fourthElement"));
            items.AddRange(new XElement[] { xElementFirst, xElementSecond });

            XMLOperations xmlOperations = new XMLOperations();
            var expectedResult = new List<string>();
            expectedResult.AddRange(new string[] { "firstElement", "secondElement", "thirdElement" });
            Assert.AreNotEqual(expectedResult, xmlOperations.GetElementNames(items));
        }

        /// <summary>
        /// ����� ��� ������������ ������ GetMaxValue, ����� � ���� ���������� ���������� ������ �
        /// ��������� ������ ���������.
        /// </summary>
        [Test]
        public void GetMaxValue_CorrectItems_ReturnTrue()
        {
            List<XElement> items = new List<XElement>();
            XElement xElementFirst = new XElement("FirstName");
            xElementFirst.Add(new XElement("firstElement", 1));
            XElement xElementSecond = new XElement("SecondName");
            xElementSecond.Add(new XElement("firstElement", 3));
            items.AddRange(new XElement[] { xElementFirst, xElementSecond });

            XMLOperations xmlOperations = new XMLOperations();
            Assert.AreEqual(3, xmlOperations.GetMaxValue("firstElement", items));
        }

        /// <summary>
        /// ����� ��� ������������ ������ GetMaxValue, ����� � ���� ���������� ������������ ������ �
        /// ��������� �������� ���������.
        /// </summary>
        [Test]
        public void GetMaxValue_IncorrectItems_ReturnFalse()
        {
            List<XElement> items = new List<XElement>();
            XElement xElementFirst = new XElement("FirstName");
            xElementFirst.Add(new XElement("firstElement", 1));
            XElement xElementSecond = new XElement("SecondName");
            xElementSecond.Add(new XElement("firstElement", 3));
            items.AddRange(new XElement[] { xElementFirst, xElementSecond });

            XMLOperations xmlOperations = new XMLOperations();
            Assert.AreNotEqual(1, xmlOperations.GetMaxValue("firstElement", items));
        }

        /// <summary>
        /// ����� ��� ������������ ������ GetMinValue, ����� � ���� ���������� ���������� ������ �
        /// ��������� ������ ���������.
        /// </summary>
        [Test]
        public void GetMinValue_CorrectItems_ReturnTrue()
        {
            List<XElement> items = new List<XElement>();
            XElement xElementFirst = new XElement("FirstName");
            xElementFirst.Add(new XElement("firstElement", 1));
            XElement xElementSecond = new XElement("SecondName");
            xElementSecond.Add(new XElement("firstElement", 3));
            items.AddRange(new XElement[] { xElementFirst, xElementSecond });

            XMLOperations xmlOperations = new XMLOperations();
            Assert.AreEqual(1, xmlOperations.GetMinValue("firstElement", items));
        }

        /// <summary>
        /// ����� ��� ������������ ������ GetMinValue, ����� � ���� ���������� ������������ ������ �
        /// ��������� �������� ���������.
        /// </summary>
        [Test]
        public void GetMinValue_IncorrectItems_ReturnFalse()
        {
            List<XElement> items = new List<XElement>();
            XElement xElementFirst = new XElement("FirstName");
            xElementFirst.Add(new XElement("firstElement", 1));
            XElement xElementSecond = new XElement("SecondName");
            xElementSecond.Add(new XElement("firstElement", 3));
            items.AddRange(new XElement[] { xElementFirst, xElementSecond });

            XMLOperations xmlOperations = new XMLOperations();
            Assert.AreNotEqual(3, xmlOperations.GetMinValue("firstElement", items));
        }

        /// <summary>
        /// ����� ��� ������������ ������ GetAvgValue, ����� � ���� ���������� ���������� ������ �
        /// ��������� ������ ���������.
        /// </summary>
        [Test]
        public void GetAvgValue_CorrectItems_ReturnTrue()
        {
            List<XElement> items = new List<XElement>();
            XElement xElementFirst = new XElement("FirstName");
            xElementFirst.Add(new XElement("firstElement", 1));
            XElement xElementSecond = new XElement("SecondName");
            xElementSecond.Add(new XElement("firstElement", 3));
            items.AddRange(new XElement[] { xElementFirst, xElementSecond });

            XMLOperations xmlOperations = new XMLOperations();
            Assert.AreEqual(2, xmlOperations.GetAvgValue("firstElement", items));
        }

        /// <summary>
        /// ����� ��� ������������ ������ GetAvgValue, ����� � ���� ���������� ������������ ������ �
        /// ��������� �������� ���������.
        /// </summary>
        [Test]
        public void GetAvgValue_IncorrectItems_ReturnFalse()
        {
            List<XElement> items = new List<XElement>();
            XElement xElementFirst = new XElement("FirstName");
            xElementFirst.Add(new XElement("firstElement", 1));
            XElement xElementSecond = new XElement("SecondName");
            xElementSecond.Add(new XElement("firstElement", 3));
            items.AddRange(new XElement[] { xElementFirst, xElementSecond });

            XMLOperations xmlOperations = new XMLOperations();
            Assert.AreNotEqual(3, xmlOperations.GetAvgValue("firstElement", items));
        }

        /// <summary>
        /// ����� ��� ������������ ������ GetMaxStringLength, ����� � ���� ���������� ���������� ������ �
        /// ��������� ������ ���������.
        /// </summary>
        [Test]
        public void GetMaxStringLength_CorrectItems_ReturnTrue()
        {
            List<XElement> items = new List<XElement>();
            XElement xElementFirst = new XElement("FirstName");
            xElementFirst.Add(new XElement("firstElement", "first string"));
            XElement xElementSecond = new XElement("SecondName");
            xElementSecond.Add(new XElement("firstElement", "second string"));
            items.AddRange(new XElement[] { xElementFirst, xElementSecond });

            XMLOperations xmlOperations = new XMLOperations();
            Assert.AreEqual(13, xmlOperations.GetMaxStringLength("firstElement", items));
        }

        /// <summary>
        /// ����� ��� ������������ ������ GetMaxStringLength, ����� � ���� ���������� ������������ ������ �
        /// ��������� �������� ���������.
        /// </summary>
        [Test]
        public void GetMaxStringLength_IncorrectItems_ReturnFalse()
        {
            List<XElement> items = new List<XElement>();
            XElement xElementFirst = new XElement("FirstName");
            xElementFirst.Add(new XElement("firstElement", "first string"));
            XElement xElementSecond = new XElement("SecondName");
            xElementSecond.Add(new XElement("firstElement", "second string"));
            items.AddRange(new XElement[] { xElementFirst, xElementSecond });

            XMLOperations xmlOperations = new XMLOperations();
            Assert.AreNotEqual(12, xmlOperations.GetMaxStringLength("firstElement", items));
        }

        /// <summary>
        /// ����� ��� ������������ ������ GetMinStringLength, ����� � ���� ���������� ���������� ������ �
        /// ��������� ������ ���������.
        /// </summary>
        [Test]
        public void GetMinStringLength_CorrectItems_ReturnTrue()
        {
            List<XElement> items = new List<XElement>();
            XElement xElementFirst = new XElement("FirstName");
            xElementFirst.Add(new XElement("firstElement", "first string"));
            XElement xElementSecond = new XElement("SecondName");
            xElementSecond.Add(new XElement("firstElement", "second string"));
            items.AddRange(new XElement[] { xElementFirst, xElementSecond });

            XMLOperations xmlOperations = new XMLOperations();
            Assert.AreEqual(12, xmlOperations.GetMinStringLength("firstElement", items));
        }

        /// <summary>
        /// ����� ��� ������������ ������ GetMinStringLength, ����� � ���� ���������� ������������ ������ �
        /// ��������� �������� ���������.
        /// </summary>
        [Test]
        public void GetMinStringLength_IncorrectItems_ReturnFalse()
        {
            List<XElement> items = new List<XElement>();
            XElement xElementFirst = new XElement("FirstName");
            xElementFirst.Add(new XElement("firstElement", "first string"));
            XElement xElementSecond = new XElement("SecondName");
            xElementSecond.Add(new XElement("firstElement", "second string"));
            items.AddRange(new XElement[] { xElementFirst, xElementSecond });

            XMLOperations xmlOperations = new XMLOperations();
            Assert.AreNotEqual(13, xmlOperations.GetMinStringLength("firstElement", items));
        }

        /// <summary>
        /// ����� ��� ������������ ������ GetAvgStringLength, ����� � ���� ���������� ���������� ������ �
        /// ��������� ������ ���������.
        /// </summary>
        [Test]
        public void GetAvgStringLength_CorrectItems_ReturnTrue()
        {
            List<XElement> items = new List<XElement>();
            XElement xElementFirst = new XElement("FirstName");
            xElementFirst.Add(new XElement("firstElement", "first string"));
            XElement xElementSecond = new XElement("SecondName");
            xElementSecond.Add(new XElement("firstElement", "second string"));
            items.AddRange(new XElement[] { xElementFirst, xElementSecond });

            XMLOperations xmlOperations = new XMLOperations();
            Assert.AreEqual(12.5, xmlOperations.GetAvgStringLength("firstElement", items));
        }

        /// <summary>
        /// ����� ��� ������������ ������ GetAvgStringLength, ����� � ���� ���������� ������������ ������ �
        /// ��������� �������� ���������.
        /// </summary>
        [Test]
        public void GetAvgStringLength_IncorrectItems_ReturnFalse()
        {
            List<XElement> items = new List<XElement>();
            XElement xElementFirst = new XElement("FirstName");
            xElementFirst.Add(new XElement("firstElement", "first string"));
            XElement xElementSecond = new XElement("SecondName");
            xElementSecond.Add(new XElement("firstElement", "second string"));
            items.AddRange(new XElement[] { xElementFirst, xElementSecond });

            XMLOperations xmlOperations = new XMLOperations();
            Assert.AreNotEqual(12, xmlOperations.GetAvgStringLength("firstElement", items));
        }

        /// <summary>
        /// ����� ��� ������������ ������ GetPossibleParameters, ����� � ���� ���������� ���������� ��������
        /// ��� ���� Score � ��������� ������ ���������.
        /// </summary>
        [Test]
        public void GetPossibleParameters_CorrectOpeartionForScore_ReturnTrue()
        {
            List<XElement> items = new List<XElement>();
            XElement xElementFirst = new XElement("FirstName");
            xElementFirst.Add(new XElement("Email", "first string"));
            xElementFirst.Add(new XElement("Score", 11));
            XElement xElementSecond = new XElement("SecondName");
            xElementSecond.Add(new XElement("Email", "second string"));
            xElementSecond.Add(new XElement("Score", 12));
            items.AddRange(new XElement[] { xElementFirst, xElementSecond });

            XMLOperations xmlOperations = new XMLOperations();
            var expectedResult = new List<string>();
            expectedResult.Add("Score");
            Assert.AreEqual(expectedResult, xmlOperations.GetPossibleParameters("Avg value", items));
        }

        /// <summary>
        /// ����� ��� ������������ ������ GetPossibleParameters, ����� � ���� ���������� ������������ ��������
        /// ��� ���� Email � ��������� �������� ���������.
        /// </summary>
        [Test]
        public void GetPossibleParameters_IncorrectOperationForEmail_ReturnFalse()
        {
            List<XElement> items = new List<XElement>();
            XElement xElementFirst = new XElement("FirstName");
            xElementFirst.Add(new XElement("Email", "first string"));
            xElementFirst.Add(new XElement("Score", 11));
            XElement xElementSecond = new XElement("SecondName");
            xElementSecond.Add(new XElement("Email", "second string"));
            xElementSecond.Add(new XElement("Score", 12));
            items.AddRange(new XElement[] { xElementFirst, xElementSecond });

            XMLOperations xmlOperations = new XMLOperations();
            var expectedResult = new List<string>();
            expectedResult.Add("Email");
            Assert.AreNotEqual(expectedResult, xmlOperations.GetPossibleParameters("Avg value", items));
        }

        /// <summary>
        /// ����� ��� ������������ ������ GetPossibleParameters, ����� � ���� ���������� ���������� ��������
        /// ��� ���� Email � ��������� ������ ���������.
        /// </summary>
        [Test]
        public void GetPossibleParameters_CorrectOpeartionForEmail_ReturnTrue()
        {
            List<XElement> items = new List<XElement>();
            XElement xElementFirst = new XElement("FirstName");
            xElementFirst.Add(new XElement("Email", "first string"));
            xElementFirst.Add(new XElement("Score", 11));
            XElement xElementSecond = new XElement("SecondName");
            xElementSecond.Add(new XElement("Email", "second string"));
            xElementSecond.Add(new XElement("Score", 12));
            items.AddRange(new XElement[] { xElementFirst, xElementSecond });

            var xmlOperations = new XMLOperations();
            var expectedResult = new List<string>();
            expectedResult.Add("Email");
            Assert.AreEqual(expectedResult, xmlOperations.GetPossibleParameters("Max string length", items));
        }

        /// <summary>
        /// ����� ��� ������������ ������ GetPossibleParameters, ����� � ���� ���������� ������������ ��������
        /// ��� ���� Score � ��������� �������� ���������.
        /// </summary>
        [Test]
        public void GetPossibleParameters_IncorrectOperationForScore_ReturnFalse()
        {
            List<XElement> items = new List<XElement>();
            XElement xElementFirst = new XElement("FirstName");
            xElementFirst.Add(new XElement("Email", "first string"));
            xElementFirst.Add(new XElement("Score", 11));
            XElement xElementSecond = new XElement("SecondName");
            xElementSecond.Add(new XElement("Email", "second string"));
            xElementSecond.Add(new XElement("Score", 12));
            items.AddRange(new XElement[] { xElementFirst, xElementSecond });

            var xmlOperations = new XMLOperations();
            var expectedResult = new List<string>();
            expectedResult.Add("Score");
            Assert.AreNotEqual(expectedResult, xmlOperations.GetPossibleParameters("Max string length", items));
        }

        /// <summary>
        /// ����� ��� ������������ ������ GetCurrentRate, ����� � ���� ���������� ���������� ������
        /// � ��������� ������ ���������.
        /// </summary>
        [Test]
        public void GetValueResult_CorrectItems_ReturnTrue()
        {
            var rates = new List<Rate>();
            var currencies = new List<Currency>();
            var currencyEuro = new Currency(); var currencyUSDollar = new Currency();
            currencyEuro.Cur_Name = "����"; currencyUSDollar.Cur_Name = "������ ���";
            currencyEuro.Cur_ID = 451; currencyUSDollar.Cur_ID = 431;
            currencyEuro.Cur_DateEnd = new System.DateTime(2050, 1, 1);
            currencyUSDollar.Cur_DateEnd = new System.DateTime(2050, 1, 1);
            currencies.Add(currencyEuro); currencies.Add(currencyUSDollar);
            var rateEuro = new Rate(); var rateUSDollar = new Rate();
            rateEuro.Cur_Name = "����"; rateUSDollar.Cur_Name = "������ ���";
            rateEuro.Cur_ID = 451; rateUSDollar.Cur_ID = 431;
            rateEuro.Cur_OfficialRate = (decimal?)2.8; rateUSDollar.Cur_OfficialRate = (decimal?)2.5;
            rateEuro.Cur_Scale = 1; rateUSDollar.Cur_Scale = 1;
            rates.Add(rateEuro); rates.Add(rateUSDollar);
            Assert.AreEqual(
                (15 * 2.8 / 2.5).ToString(),
                CurrencyOperations.GetValueResult("15", currencies, rates, "������ ���", "����"));
        }

        /// <summary>
        /// ����� ��� ������������ ������ GetCurrentRate, ����� � ���� ���������� ������������ ������
        /// � ��������� �������� ���������.
        /// </summary>
        [Test]
        public void GetValueResult_IncorrectItems_ReturnFalse()
        {
            var rates = new List<Rate>();
            var currencies = new List<Currency>();
            var currencyEuro = new Currency(); var currencyUSDollar = new Currency();
            currencyEuro.Cur_Name = "����"; currencyUSDollar.Cur_Name = "������ ���";
            currencyEuro.Cur_ID = 451; currencyUSDollar.Cur_ID = 431;
            currencyEuro.Cur_DateEnd = new System.DateTime(2050, 1, 1);
            currencyUSDollar.Cur_DateEnd = new System.DateTime(2050, 1, 1);
            currencies.Add(currencyEuro); currencies.Add(currencyUSDollar);
            var rateEuro = new Rate(); var rateUSDollar = new Rate();
            rateEuro.Cur_Name = "����"; rateUSDollar.Cur_Name = "������ ���";
            rateEuro.Cur_ID = 451; rateUSDollar.Cur_ID = 431;
            rateEuro.Cur_OfficialRate = (decimal?)2.8; rateUSDollar.Cur_OfficialRate = (decimal?)2.5;
            rateEuro.Cur_Scale = 1; rateUSDollar.Cur_Scale = 1;
            rates.Add(rateEuro); rates.Add(rateUSDollar);
            Assert.AreNotEqual(
                (15 * 2.8 / 2.5).ToString(),
                CurrencyOperations.GetValueResult("15", currencies, rates, "����", "������ ���"));
        }

        /// <summary>
        /// ����� ��� ������������ ������ GetNecessaryCurrencies, ����� � ���� ���������� ������������ ������
        /// � ��������� �������� ���������.
        /// </summary>
        [Test]
        public void GetNecessaryCurrencies_CorrectItems_ReturnTrue()
        {
            var currencies = new List<Currency>();
            var euro292 = new Currency(); var euro451 = new Currency();
            euro292.Cur_ID = 292; euro451.Cur_ID = 451;
            euro292.Cur_QuotName = "1 ����"; euro451.Cur_QuotName = "1 ����";
            euro292.Cur_DateEnd = new System.DateTime(2021, 7, 8);
            euro451.Cur_DateEnd = new System.DateTime(2050, 1, 1);
            currencies.AddRange(new Currency[] { euro292, euro451 });
            var expectedResult = new List<Currency>();
            expectedResult.Add(euro451);
            Assert.AreEqual(expectedResult, CurrencyOperations.GetNecessaryCurrencies(
                currencies,
                "1 ����",
                new System.DateTime(2021, 9, 9)));
        }

        /// <summary>
        /// ����� ��� ������������ ������ GetNecessaryCurrencies, ����� � ���� ���������� ������������ ������
        /// � ��������� �������� ���������.
        /// </summary>
        [Test]
        public void GetNecessaryCurrencies_IncorrectItems_ReturnFalse()
        {
            var currencies = new List<Currency>();
            var euro292 = new Currency(); var euro451 = new Currency();
            euro292.Cur_ID = 292; euro451.Cur_ID = 451;
            euro292.Cur_QuotName = "1 ����"; euro451.Cur_QuotName = "10 ����";
            euro292.Cur_DateEnd = new System.DateTime(2021, 7, 8);
            euro451.Cur_DateEnd = new System.DateTime(2050, 1, 1);
            currencies.AddRange(new Currency[] { euro292, euro451 });
            var expectedResult = new List<Currency>();
            expectedResult.Add(euro451);
            Assert.AreNotEqual(expectedResult, CurrencyOperations.GetNecessaryCurrencies(
                currencies,
                "1 ����",
                new System.DateTime(2021, 9, 9)));
        }

        /// <summary>
        /// ����� ��� ������������ ������ GetNecessaryDateTime, ����� � ���� ���������� ���������� ������
        /// ��� ������� �� ������������ �� ������� 365 ���� � ��������� ������ ���������.
        /// </summary>
        [Test]
        public void GetNextDateTime_ForLessThanYearCorrectItems_ReturnTrue()
        {
            var currencies = new List<Currency>();
            var euro451 = new Currency();
            euro451.Cur_ID = 451;
            euro451.Cur_DateEnd = new System.DateTime(2050, 1, 1);
            currencies.AddRange(new Currency[] { euro451 });
            Assert.AreEqual(new System.DateTime(2021, 10, 10), CurrencyOperations.GetNextDateTime(
                currencies,
                new System.DateTime(2021, 8, 10),
                new System.DateTime(2021, 10, 10),
                451));
        }

        /// <summary>
        /// ����� ��� ������������ ������ GetNecessaryDateTime, ����� � ���� ���������� ���������� ������
        /// ��� ������� ������������ �� ������� 365 ���� � ��������� ������ ���������.
        /// </summary>
        [Test]
        public void GetNextDateTime_ForMoreThanYearCorrectItems_ReturnTrue()
        {
            var currencies = new List<Currency>();
            var euro451 = new Currency();
            euro451.Cur_ID = 451;
            euro451.Cur_DateEnd = new System.DateTime(2050, 1, 1);
            currencies.AddRange(new Currency[] { euro451 });
            Assert.AreEqual(new System.DateTime(2022, 8, 10), CurrencyOperations.GetNextDateTime(
                currencies,
                new System.DateTime(2021, 8, 10),
                new System.DateTime(2022, 10, 10),
                451));
        }

        /// <summary>
        /// ����� ��� ������������ ������ GetNecessaryDateTime, ����� � ���� ���������� ���������� ������
        /// ��� ������� �� ������������ �� ������� 365 ���� � ��������� �������� ���������.
        /// </summary>
        [Test]
        public void GetNextDateTime_ForLessThanYearIncorrectItems_ReturnFalse()
        {
            var currencies = new List<Currency>();
            var euro451 = new Currency();
            euro451.Cur_ID = 451;
            euro451.Cur_DateEnd = new System.DateTime(2050, 1, 1);
            currencies.AddRange(new Currency[] { euro451 });
            Assert.AreNotEqual(new System.DateTime(2021, 10, 10), CurrencyOperations.GetNextDateTime(
                currencies,
                new System.DateTime(2021, 8, 10),
                new System.DateTime(2022, 10, 10),
                451));
        }

        /// <summary>
        /// ����� ��� ������������ ������ GetNecessaryDateTime, ����� � ���� ���������� ���������� ������
        /// ��� ������� ������������ �� ������� 365 ���� � ��������� �������� ���������.
        /// </summary>
        [Test]
        public void GetNextDateTime_ForMoreThanYearIncorrectItems_ReturnFalse()
        {
            var currencies = new List<Currency>();
            var euro451 = new Currency();
            euro451.Cur_ID = 451;
            euro451.Cur_DateEnd = new System.DateTime(2050, 1, 1);
            currencies.AddRange(new Currency[] { euro451 });
            Assert.AreNotEqual(new System.DateTime(2022, 8, 10), CurrencyOperations.GetNextDateTime(
                currencies,
                new System.DateTime(2021, 9, 10),
                new System.DateTime(2022, 10, 10),
                451));
        }
    }
}