using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace EthosLending_EmreTurul
{
    class Program
    {

        //amount: 100000
        //interest: 5.5%
        //downpayment: 20000
        //term: 30

        static void Main(string[] args)
        {

            double Amount = 0;
            double Interest = 0;
            double DownPayment = 0;
            double Term = 0;

            double MonthlyPayment = 0;
            double TotalInterest = 0;
            double TotalPayment = 0;

            string inputText = "";


            // reading input values

            inputText = Console.ReadLine();

            string AmountText = getDigitPart(InputType.Amount, inputText);
            Amount = convertToDouble(AmountText);


            inputText = Console.ReadLine();

            string InterestText = getDigitPart(InputType.Interest, inputText).Replace("%", "");
            Interest = convertToDouble(InterestText);


            inputText = Console.ReadLine();

            string DownPaymentText = getDigitPart(InputType.DownPayment, inputText);
            DownPayment = convertToDouble(DownPaymentText);


            inputText = Console.ReadLine();

            string TermText = getDigitPart(InputType.Term, inputText);
            Term = convertToDouble(TermText);


            Console.ReadLine();


            // Calculate the loan payment

            double MonthlyInterestRate = Interest / (100 * 12);
            double NumberOfPayments = Term * 12;


            // MonthlyPayment = Amount * (InterestRate / (1 - Math.Pow((1 + InterestRate), (-PaymentNumber))));

            // MonthlyPayment = (Amount * MonthlyInterestRate) / (1 - (Math.Pow(1 + MonthlyInterestRate, Term)));

            MonthlyPayment = (MonthlyInterestRate * (Amount-DownPayment)) / (1 - Math.Pow(1 + MonthlyInterestRate, NumberOfPayments * -1));


            TotalPayment = MonthlyPayment * NumberOfPayments;

            TotalInterest = TotalPayment - Amount- DownPayment;


            // Generate JSON

            LoanDTO loanDTO = new LoanDTO() { MonthlyPayment = Math.Round( MonthlyPayment ,2), TotalPayment = Math.Round(TotalPayment,2) , TotalInterest = Math.Round(TotalInterest,2) };

            var output = JsonConvert.SerializeObject(loanDTO);

            Console.WriteLine(output);
            Console.ReadKey();

        }



        static double convertToDouble(string text){
          
            double result;

            if(Double.TryParse(text, out result))
            {
                return result;
            }
            else
            {
                throw new Exception("Wrong input!");
            }

        }

        static string getDigitPart(InputType inputType,string text)
        {
            if(text.ToLower().IndexOf(inputType.ToString().ToLower(), StringComparison.Ordinal) == -1 )
            {
                throw new Exception($"{inputType.ToString()} is not valid!");
            }
            else if (text.IndexOf(Convert.ToChar(":")) == -1)
            {
                throw new Exception($"{inputType.ToString()} is not valid!");
            }


            return text.ToString().Substring(text.IndexOf(Convert.ToChar(":"))+1).Trim();
        }
       


    }

    enum InputType {
        Amount,
        Interest,
        DownPayment,
        Term
    }


    class LoanDTO {

        [JsonProperty(PropertyName = "monthly payment")]
        public double MonthlyPayment { get; set; }

        [JsonProperty(PropertyName = "total interest")]
        public double TotalInterest { get; set; }

        [JsonProperty(PropertyName = "total payment")]
        public double TotalPayment { get; set; }
    }


}


//double principal;     // total mortgage loan
//double interestPerc;  // percent annual interest
//double interestRate;  // monthly interest rate
//double years;         // years to pay
//double paymentNum;    // number of months to pay
//double paymentVal;    // value of monthly payment
//String fstr;
      //this.label4.Text = "";
      //principal = double.Parse(this.textBox1.Text);
      //interestPerc = double.Parse(this.textBox2.Text);
      //interestRate = interestPerc / (100 * 12);
      //years = double.Parse(this.textBox3.Text);
      //paymentNum = years * 12;


      //paymentVal = principal * (interestRate / (1 - Math.Pow((1 + interestRate), (-paymentNum))));
      //fstr = String.Format("Principal Loan: {0:C}\n", principal);
      //this.label4.Text += fstr;
      //this.label4.Text += "Interest (%)  : " + interestPerc + '\n';
      //this.label4.Text += "Years to pay  : " + years + '\n';
      //this.label4.Text += "Months to pay : " + paymentNum + '\n';
      //fstr = String.Format("Monthly pay   : {0:C}\n", paymentVal);
      //this.label4.Text += fstr;
      //fstr = String.Format("Total pay     : {0:C}\n", paymentVal * paymentNum);
      //this.label4.Text += fstr;