# ParkWayProjectsTask2
Transaction Surcharge

This application was built with ASP.NET CORE MVC

Kindly use the menu to navigate and test the application

The program was tested with the input amounts specified below,

5000, 45000, 50030

In the case of the third amount(50030), the debit amount corresponds to the expected amount because the configuration file already has it defined that amount above N50000 should be charged N50, 
and when N50 is deducted from the amount(50030) it equals to N49980 which is the transfer amount and after a charge of N50 is added, 
the debit amount (Transfer Amount + Charge) then equals to the expected amount.
