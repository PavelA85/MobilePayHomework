﻿+-----------------------------------------------------------------------------------------------------------
|                                                Background                                                 
+-----------------------------------------------------------------------------------------------------------
|                                                                                                           
| MobilePay is a mobile payments solution which can be used to send money to your friends or buy goods from 
| merchants. Each merchant needs to pay various fees to payment service providers in order to make          
| customers lives easier by paying with card or phone. Even tough MobilePay is one of the top choices as    
| payment service providers for merchants, it doesn't come for free. MobilePay has various rules to         
| calculate the fee that we should charge each merchant for using MobilePay. So far we are doing this       
| manually by using data from databases and lot's of excel spreadsheets and macros, then we use calculated  
| fees to make an invoice which we send to a merchant.                                                      
|                                                                                                           
+-----------------------------------------------------------------------------------------------------------



+-----------------------------------------------------------------------------------------------------------
|                                                  Task                                                     
+-----------------------------------------------------------------------------------------------------------
|                                                                                                           
| You have been assigned to help our collegues and remove all those manual error prone calculations from    
| them by introducing a tool which will do that automatically. Our Product Owner and Tech Lead has created  
| an epic. It was broke down into user stories which needs to be delivered by you. You will be assesed by   
| all business/technical requirements listed in the epic, quality over delivery time is expected.           
|                                                                                                           
+-----------------------------------------------------------------------------------------------------------



+-----------------------------------------------------------------------------------------------------------
|                                             Epic MOBILEPAY-1                                              
+-----------------------------------------------------------------------------------------------------------
|                                                                                                           
| As a MobilePay accountant I want to have an app which will calculate merchant fees so that we would avoid 
| manual calculation                                                                                        
|                                                                                                           
+-----------------------------------------------------------------------------------------------------------
|                                                                                                           
| Business requirements:                                                                                    
| - App should calculate fees per transaction                                                               
| - App should take transactions from the file named transactions.txt with the following format:            
|   Date merchantName amount                                                                                
| - transactions.txt will always contain transactions ordered by date ASC                                   
| - App should output result to console with the following format:                                          
|   Date merchantName fee     
| - Fee amount should be formatted like this 25.00                                                          
| - Date should be formatted like this YYYY-MM-DD                                                           
|                                                                                                           
+-----------------------------------------------------------------------------------------------------------
|                                                                                                           
| Technical requirements:                                                                                   
| - App should be written as Console application in C# (.NET Framework or .NET Core)                        
| - Usage of external libraries are prohibited (except tests)                                               
| - Output should be written to console                                                                     
| - Ubiqutous language should be used in the solution -                                                     
|   https://www.agilealliance.org/glossary/ubiquitous-language                                              
| - Documentation is optional, if there is a need - put in README.md file                                   
| - Source code should be pushed to a publicly available git repository (GitHub, GitLab, Bitbucket, ...)    
| - Code should be flexible enough to change, add or remove rules                                           
| - Code should be covered with tests
| - Clean code principles should be applied
| - App should output transaction fee as soon as it processes transaction (app should not load all
|   transactions to memory)                                                                       
|                                                                                                           
+-----------------------------------------------------------------------------------------------------------
|                                                                                                           
| Stories in Epic:                                                                                          
| MOBILEPAY-2                                                                                               
| MOBILEPAY-3                                                                                               
| MOBILEPAY-4                                                                                               
| MOBILEPAY-5                                                                                               
|                                                                                                           
+-----------------------------------------------------------------------------------------------------------



+-----------------------------------------------------------------------------------------------------------
|                                           User Story MOBILEPAY-2                                          
+-----------------------------------------------------------------------------------------------------------
|                                                                                                           
| As a MobilePay accountant I want merchants to be charged Transaction Percentage Fee (1% of transaction    
|  amount), so that MobilePay would still be cheapest solution in the market and we could earn enough money 
|  to cover our expenses                                                                                    
|                                                                                                           
+-----------------------------------------------------------------------------------------------------------
|                                                                                                           
| Acceptance criteria:                                                                                      
|                                                                                                           
| Given that 120 DKK transaction is made to CIRCLE_K     on 2018-09-02                                      
| And        200 DKK transaction is made to TELIA        on 2018-09-04                                      
| And        300 DKK transaction is made to CIRCLE_K     on 2018-10-22                                      
| And        150 DKK transaction is made to CIRCLE_K     on 2018-10-29                                      
| When fees calculation app is executed                                                                     
| Then the output is:                                                                                       
|     2018-09-02 CIRCLE_K 1.20                                                                              
|     2018-09-04 TELIA    2.00                                                                              
|     2018-10-22 CIRCLE_K 3.00                                                                              
|     2018-10-29 CIRCLE_K 1.50                                                                              
|                                                                                                           
+-----------------------------------------------------------------------------------------------------------

    

+-----------------------------------------------------------------------------------------------------------
|                                           User Story MOBILEPAY-3                                          
+-----------------------------------------------------------------------------------------------------------
|                                                                                                           
| As a MobilePay accountant I want TELIA merchant to get Transaction Fee Percentage Discount (10% discount  
| for transaction), so that MobilePay would be more attractive to big merchants                             
|                                                                                                           
+-----------------------------------------------------------------------------------------------------------
|                                                                                                           
| Acceptance criteria:                                                                                      
| Given that 120 DKK transaction is made to TELIA on 2018-09-02                                             
| And        200 DKK transaction is made to TELIA on 2018-09-04                                             
| And        300 DKK transaction is made to TELIA on 2018-10-22                                             
| And        150 DKK transaction is made to TELIA on 2018-10-29                                             
| When fees calculation app is executed                                                                     
| Then the output is:                                                                                       
|     2018-09-02 TELIA 1.08                                                                                 
|     2018-09-04 TELIA 1.80                                                                                 
|     2018-10-22 TELIA 2.70                                                                                 
|     2018-10-29 TELIA 1.35                                                                                 
|                                                                                                           
+-----------------------------------------------------------------------------------------------------------
    
    

+-----------------------------------------------------------------------------------------------------------
|                                           User Story MOBILEPAY-4                                          
+-----------------------------------------------------------------------------------------------------------
|                                                                                                           
| As a MobilePay accountant I want CIRCLE_K merchant to get Transaction Fee Percentage Discount (20%        
| discount for transaction), so that MobilePay would be more attractive to big merchants                    
|                                                                                                           
+-----------------------------------------------------------------------------------------------------------
|                                                                                                           
| Acceptance criteria:                                                                                      
| Given that 120 DKK transaction is made to CIRCLE_K on 2018-09-02                                          
| And        200 DKK transaction is made to CIRCLE_K on 2018-09-04                                          
| And        300 DKK transaction is made to CIRCLE_K on 2018-10-22                                          
| And        150 DKK transaction is made to CIRCLE_K on 2018-10-29                                          
| When fees calculation app is executed                                                                     
| Then the output is:                                                                                       
|     2018-09-02 CIRCLE_K 0.96                                                                              
|     2018-09-04 CIRCLE_K 1.60                                                                              
|     2018-10-22 CIRCLE_K 2.40                                                                              
|     2018-10-29 CIRCLE_K 1.20                                                                              
|                                                                                                           
+-----------------------------------------------------------------------------------------------------------

    

+-----------------------------------------------------------------------------------------------------------
|                                           User Story MOBILEPAY-5                                          
+-----------------------------------------------------------------------------------------------------------
|                                                                                                           
|As a MobilePay accountant I want to charge merchants Invoice Fixed Fee (29 DKK) every month, so that we    
|could cover our expenses for sending physical invoices to merchants                                        
|                                                                                                           
+-----------------------------------------------------------------------------------------------------------
|                                                                                                           
|Requirements:                                                                                              
|- Invoice Fee should be included in the fee for first transaction of the month
|- If there aren't any transactions that month, Merchant should not be charged Invoice Fee
|- If transaction fee is 0 after applying discounts, InvoiceFee should not be added                         
|                                                                                                           
+-----------------------------------------------------------------------------------------------------------
|                                                                                                           
|Acceptance criteria:                                                                                       
|Given that 120 DKK transaction is made to 7-ELEVEN     on 2018-09-02                                       
|And        200 DKK transaction is made to NETTO        on 2018-09-04                                       
|And        300 DKK transaction is made to 7-ELEVEN     on 2018-10-22                                       
|And        150 DKK transaction is made to 7-ELEVEN     on 2018-10-29                                       
|When fees calculation app is executed                                                                      
|Then the output is:                                                                                        
|     2018-09-02 7-ELEVEN 30.20                                                                             
|     2018-09-04 NETTO    31.00                                                                             
|     2018-10-22 7-ELEVEN 32.00                                                                             
|     2018-10-29 7-ELEVEN 1.50                                                                              
|                                                                                                           
+-----------------------------------------------------------------------------------------------------------



+-----------------------------------------------------------------------------------------------------------
|                                           Sample test data                                                
+-----------------------------------------------------------------------------------------------------------
|                                                                                                           
|Input:                                                                                                     
|                                                                                                           
|2018-09-01 7-ELEVEN 100                                                                                    
|2018-09-04 CIRCLE_K 100                                                                                    
|2018-09-07 TELIA    100                                                                                    
|2018-09-09 NETTO    100                                                                                    
|2018-09-13 CIRCLE_K 100                                                                                    
|2018-09-16 TELIA    100                                                                                    
|2018-09-19 7-ELEVEN 100                                                                                    
|2018-09-22 CIRCLE_K 100                                                                                    
|2018-09-25 TELIA    100                                                                                    
|2018-09-28 7-ELEVEN 100                                                                                    
|2018-09-30 CIRCLE_K 100                                                                                    
|                                                                                                           
|2018-10-01 7-ELEVEN 100                                                                                    
|2018-10-04 CIRCLE_K 100                                                                                    
|2018-10-07 TELIA    100                                                                                    
|2018-10-10 NETTO    100                                                                                    
|2018-10-13 CIRCLE_K 100                                                                                    
|2018-10-16 TELIA    100                                                                                    
|2018-10-19 7-ELEVEN 100                                                                                    
|2018-10-22 CIRCLE_K 100                                                                                    
|2018-10-25 TELIA    100                                                                                    
|2018-10-28 7-ELEVEN 100                                                                                    
|2018-10-30 CIRCLE_K 100                                                                                    
|                                                                                                           
|                                                                                                           
|Output:                                                                                                    
|                                                                                                           
|2018-09-01 7-ELEVEN 30.00                                                                                  
|2018-09-04 CIRCLE_K 29.80                                                                                  
|2018-09-07 TELIA    29.90                                                                                  
|2018-09-09 NETTO    30.00                                                                                  
|2018-09-13 CIRCLE_K 0.80                                                                                   
|2018-09-16 TELIA    0.90                                                                                   
|2018-09-19 7-ELEVEN 1.00                                                                                   
|2018-09-22 CIRCLE_K 0.80                                                                                   
|2018-09-25 TELIA    0.90                                                                                   
|2018-09-28 7-ELEVEN 1.00                                                                                   
|2018-09-30 CIRCLE_K 0.80                                                                                   
|                                                                                                           
|2018-10-01 7-ELEVEN 30.00                                                                                  
|2018-10-04 CIRCLE_K 29.80                                                                                  
|2018-10-07 TELIA    29.90                                                                                  
|2018-10-10 NETTO    30.00                                                                                  
|2018-10-13 CIRCLE_K 0.80                                                                                   
|2018-10-16 TELIA    0.90                                                                                   
|2018-10-19 7-ELEVEN 1.00                                                                                   
|2018-10-22 CIRCLE_K 0.80                                                                                   
|2018-10-25 TELIA    0.90                                                                                   
|2018-10-28 7-ELEVEN 1.00                                                                                   
|2018-10-30 CIRCLE_K 0.80                                                                                   
|                                                                                                           
+-----------------------------------------------------------------------------------------------------------
